using Minio.DataModel.Args;
using Minio;
using Grpc.Core;
using Movieupload;
using Posterupload;
using Backdropupload;

namespace ProcessFileService.Service;

public class MovieUploadService : MovieUpload.MovieUploadBase
{
    private readonly IMinioClient _minioClient;
    private const string MovieBucket = "uploads";

    public MovieUploadService(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public override async Task<MovieUploadResponse> UploadMovie(IAsyncStreamReader<MovieUploadRequest> requestStream, ServerCallContext context)
    {
        await foreach (var request in requestStream.ReadAllAsync())
        {
            if (string.IsNullOrEmpty(request.MovieFilePath) || request.MovieData.Length == 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid file data"));
            }

            using var stream = new MemoryStream(request.MovieData.ToByteArray());

            var args = new PutObjectArgs()
                .WithBucket(MovieBucket)
                .WithObject(request.MovieFilePath)
                .WithStreamData(stream)
                .WithObjectSize(request.MovieData.Length);

            await _minioClient.PutObjectAsync(args);
        }

        return new MovieUploadResponse { Message = "Movie uploaded successfully" };
    }
}

public class PosterUploadService : PosterUpload.PosterUploadBase
{
    private readonly IMinioClient _minioClient;
    private const string PosterBucket = "uploads";

    public PosterUploadService(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public override async Task<PosterUploadResponse> UploadPoster(IAsyncStreamReader<PosterUploadRequest> requestStream, ServerCallContext context)
    {
        await foreach (var request in requestStream.ReadAllAsync())
        {
            if (string.IsNullOrEmpty(request.PosterFilePath) || request.PosterData.Length == 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid file data"));
            }

            using var stream = new MemoryStream(request.PosterData.ToByteArray());

            var args = new PutObjectArgs()
                .WithBucket(PosterBucket)
                .WithObject(request.PosterFilePath)
                .WithStreamData(stream)
                .WithObjectSize(request.PosterData.Length);

            await _minioClient.PutObjectAsync(args);
        }

        return new PosterUploadResponse { Message = "Poster uploaded successfully" };
    }
}

public class BackdropUploadService : BackdropUpload.BackdropUploadBase
{
    private readonly IMinioClient _minioClient;
    private const string BackdropBucket = "uploads";

    public BackdropUploadService(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public override async Task<BackdropUploadResponse> UploadBackdrop(IAsyncStreamReader<BackdropUploadRequest> requestStream, ServerCallContext context)
    {
        await foreach (var request in requestStream.ReadAllAsync())
        {
            if (string.IsNullOrEmpty(request.BackdropFilePath) || request.BackdropData.Length == 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid file data"));
            }

            using var stream = new MemoryStream(request.BackdropData.ToByteArray());

            var args = new PutObjectArgs()
                .WithBucket(BackdropBucket)
                .WithObject(request.BackdropFilePath)
                .WithStreamData(stream)
                .WithObjectSize(request.BackdropData.Length);

            await _minioClient.PutObjectAsync(args);
        }

        return new BackdropUploadResponse { Message = "Backdrop uploaded successfully" };
    }
}
