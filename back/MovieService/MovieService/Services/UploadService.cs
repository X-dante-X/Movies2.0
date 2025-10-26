using Fileupload;
using Google.Protobuf;
using MovieService.Services.Interfaces;

namespace MovieService.Services;

/// <summary>
/// Service responsible for uploading videos and images to the FileUpload microservice via gRPC.
/// Handles streaming large files in chunks to avoid memory overuse.
/// </summary>
public class UploadService : IUploadService
{
    private readonly FileUpload.FileUploadClient _uploadClient;

    /// <summary>
    /// Initializes a new instance of <see cref="UploadService"/>.
    /// </summary>
    /// <param name="uploadClient">gRPC client for the FileUpload service.</param>
    public UploadService(FileUpload.FileUploadClient uploadClient)
    {
        _uploadClient = uploadClient;
    }

    /// <summary>
    /// Uploads a video file using gRPC streaming.
    /// The file is read in chunks (1 KB by default) and sent to the FileUpload service.
    /// </summary>
    /// <param name="fileName">Normalized file name for the video.</param>
    /// <param name="file">File abstraction implementing <see cref="IFile"/> (e.g., from GraphQL or REST upload).</param>
    /// <returns>Absolute file path returned by the FileUpload service.</returns>
    public async Task<string> UploadVideoAsync(string fileName, IFile file)
    {
        using var call = _uploadClient.UploadVideo();
        using var fileStream = file.OpenReadStream();

        byte[] buffer = new byte[1024]; // Chunk size: 1 KB
        int bytesRead;

        // Stream the file in chunks
        while ((bytesRead = await fileStream.ReadAsync(buffer)) > 0)
        {
            var request = new VideoUploadRequest
            {
                FileName = fileName,
                Chunk = ByteString.CopyFrom(buffer, 0, bytesRead),
            };

            await call.RequestStream.WriteAsync(request);
        }

        await call.RequestStream.CompleteAsync();

        var response = await call.ResponseAsync;
        return response.AbsoluteFilePath;
    }

    /// <summary>
    /// Uploads an image file (poster, backdrop, or person photo) using gRPC streaming.
    /// The file is read in chunks (1 KB by default) and sent to the FileUpload service.
    /// </summary>
    /// <param name="fileName">Normalized base file name for the image.</param>
    /// <param name="type">Image type, e.g., "poster", "backdrop", "personPhoto".</param>
    /// <param name="file">File abstraction implementing <see cref="IFile"/> (e.g., from GraphQL or REST upload).</param>
    /// <returns>Absolute file path returned by the FileUpload service.</returns>
    public async Task<string> UploadImageAsync(string fileName, string type, IFile file)
    {
        using var call = _uploadClient.UploadImage();
        using var fileStream = file.OpenReadStream();

        byte[] buffer = new byte[1024]; // Chunk size: 1 KB
        int bytesRead;

        // Stream the file in chunks
        while ((bytesRead = await fileStream.ReadAsync(buffer)) > 0)
        {
            var request = new ImageUploadRequest
            {
                FileName = fileName,
                Type = type,
                Chunk = ByteString.CopyFrom(buffer, 0, bytesRead),
            };

            await call.RequestStream.WriteAsync(request);
        }

        await call.RequestStream.CompleteAsync();

        var response = await call.ResponseAsync;
        return response.AbsoluteFilePath;
    }
}
