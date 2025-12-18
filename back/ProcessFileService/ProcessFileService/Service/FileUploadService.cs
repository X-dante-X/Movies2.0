using Minio.DataModel.Args;
using Minio;
using Grpc.Core;
using Fileupload;

namespace ProcessFileService.Service;

/// <summary>
/// gRPC service responsible for receiving large files (videos/images) as stream chunks,
/// temporarily storing them on disk, processing (for videos), and uploading to MinIO storage.
/// </summary>
public class FileUploadService : FileUpload.FileUploadBase
{
    private readonly IMinioClient _minioClient;
    private const string Bucket = "uploads";

    public FileUploadService(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    /// <summary>
    /// Streams a video upload from the client in chunks, buffers it to a temp file,
    /// converts it to multi-bitrate HLS using FFmpeg, and uploads all generated files to MinIO.
    /// </summary>
    /// <param name="requestStream">Incoming stream of <see cref="VideoUploadRequest"/> chunks.</param>
    /// <param name="context">gRPC server call context.</param>
    /// <returns>A success response containing the path to the uploaded HLS master playlist.</returns>
    /// <exception cref="RpcException">Thrown when invalid file data is received.</exception>
    public override async Task<Response> UploadVideo(IAsyncStreamReader<VideoUploadRequest> requestStream, ServerCallContext context)
    {
        var tempInputPath = Path.GetTempFileName();
        var hlsOutputDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        Directory.CreateDirectory(hlsOutputDir);

        var movieName = string.Empty;

        await using var outputFile = File.Create(tempInputPath);

        await foreach (var request in requestStream.ReadAllAsync())
        {
            if (string.IsNullOrEmpty(request.FileName) || request.Chunk.Length == 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid file data"));
            }

            movieName = Path.GetFileNameWithoutExtension(request.FileName);
            await outputFile.WriteAsync(request.Chunk.Memory);
        }

        await outputFile.FlushAsync();
        outputFile.Close();

        FfmpegService.GenerateMultiBitrateHls(tempInputPath, hlsOutputDir);

        var minioPrefix = $"movie/movie/{movieName}";
        await UploadDirectoryStreamedAsync(hlsOutputDir, minioPrefix);

        File.Delete(tempInputPath);

        return new Response
        {
            Message = "Movie uploaded successfully",
            AbsoluteFilePath = $"{minioPrefix}/master.m3u8"
        };
    }

    /// <summary>
    /// Streams an image upload from client in chunks, writes to temp file
    /// and uploads to MinIO path based on image type (poster/backdrop/logo/person).
    /// </summary>
    public override async Task<Response> UploadImage(IAsyncStreamReader<ImageUploadRequest> requestStream, ServerCallContext context)
    {
        string absoluteFilePath = string.Empty;

        string tempInputPath = Path.GetTempFileName();
        await using var outputFile = File.Create(tempInputPath);

        await foreach (var request in requestStream.ReadAllAsync())
        {
            if (string.IsNullOrEmpty(request.FileName) || request.Chunk.Length == 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid file data"));
            }

            await outputFile.WriteAsync(request.Chunk.Memory);
            // Construct full destination path inside MinIO
            absoluteFilePath = GetImagePath(request.Type) + request.FileName + ".jpg";
        }

        outputFile.Close();
        await UploadFileStreamedAsync(tempInputPath, absoluteFilePath);

        return new Response { Message = "Movie uploaded successfully", AbsoluteFilePath = absoluteFilePath };
    }

    /// <summary>
    /// Maps logical image type to a MinIO prefix path.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when unknown type is passed.</exception>
    private static string GetImagePath(string type)
    {
        return type.ToLowerInvariant() switch
        {
            "poster" => "movie/poster/",
            "backdrop" => "movie/backdrop/",
            "logo" => "companylogo/",
            "personphoto" => "personphoto/",
            _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unknown image type: {type}")
        };
    }

    /// <summary>
    /// Uploads a single file to MinIO from a temp file stream.
    /// Automatically deletes the temp file after upload.
    /// </summary>
    private async Task UploadFileStreamedAsync(string file, string filename)
    {
        await using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
        await _minioClient.PutObjectAsync(new PutObjectArgs()
            .WithBucket(Bucket)
            .WithObject(filename)
            .WithStreamData(stream)
            .WithObjectSize(stream.Length)
            .WithContentType("application/octet-stream"));

        File.Delete(file);
    }

    /// <summary>
    /// Recursively uploads all files under a directory to MinIO using a given prefix.
    /// Deletes the directory after successful upload.
    /// </summary>
    private async Task UploadDirectoryStreamedAsync(string directory, string prefix)
    {
        var files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);

        foreach (var file in files)
        {
            string objectName = Path.Combine(prefix, Path.GetRelativePath(directory, file)).Replace("\\", "/");

            await using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
            await _minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(Bucket)
                .WithObject(objectName)
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithContentType("application/octet-stream"));
        }

        Directory.Delete(directory, recursive: true);
    }
}