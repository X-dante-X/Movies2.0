using Fileupload;
using Google.Protobuf;
using MovieService.Services.Interfaces;

namespace MovieService.Services;

public class UploadService : IUploadService
{
    private readonly FileUpload.FileUploadClient _uploadClient;

    public UploadService(FileUpload.FileUploadClient uploadClient)
    {
        _uploadClient = uploadClient;
    }

    public async Task<string> UploadVideoAsync(string fileName, IFile file)
    {
        using var call = _uploadClient.UploadVideo();
        using var fileStream = file.OpenReadStream();

        byte[] buffer = new byte[1024];
        int bytesRead;

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

    public async Task<string> UploadImageAsync(string fileName, string type, IFile file)
    {
        using var call = _uploadClient.UploadImage();
        using var fileStream = file.OpenReadStream();

        byte[] buffer = new byte[1024];
        int bytesRead;

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
