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

    public async Task<string> UploadMovieAsync(string fileName, IFile file)
    {
        using var call = _uploadClient.UploadMovie();
        using var fileStream = file.OpenReadStream();

        byte[] buffer = new byte[1024 * 1024 * 100];
        int bytesRead;

        while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            var request = new MovieUploadRequest
            {
                MovieFileName = fileName,
                MovieData = ByteString.CopyFrom(buffer, 0, bytesRead),
            };

            await call.RequestStream.WriteAsync(request);
        }

        await call.RequestStream.CompleteAsync();
        var response = await call.ResponseAsync;
        return response.AbsoluteFilePath;
    }

    public async Task<string> UploadPosterAsync(string fileName, IFile file)
    {
        using var call = _uploadClient.UploadMoviePoster();
        using var fileStream = file.OpenReadStream();

        byte[] buffer = new byte[1024 * 1024];
        int bytesRead;

        while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            var request = new MoviePosterUploadRequest
            {
                MoviePosterFileName = fileName,
                MoviePosterData = ByteString.CopyFrom(buffer, 0, bytesRead),
            };

            await call.RequestStream.WriteAsync(request);
        }

        await call.RequestStream.CompleteAsync();
        var response = await call.ResponseAsync;
        return response.AbsoluteFilePath;
    }

    public async Task<string> UploadBackdropAsync(string fileName, IFile file)
    {
        using var call = _uploadClient.UploadMovieBackdrop();
        using var fileStream = file.OpenReadStream();

        byte[] buffer = new byte[1024 * 1024];
        int bytesRead;

        while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            var request = new MovieBackdropUploadRequest
            {
                MovieBackdropFileName = fileName,
                MovieBackdropData = ByteString.CopyFrom(buffer, 0, bytesRead),
            };

            await call.RequestStream.WriteAsync(request);
        }

        await call.RequestStream.CompleteAsync();
        var response = await call.ResponseAsync;
        return response.AbsoluteFilePath;
    }

    public async Task<string> UploadLogoAsync(string fileName, IFile file)
    {
        using var call = _uploadClient.UploadCompanyLogo();
        using var fileStream = file.OpenReadStream();

        byte[] buffer = new byte[1024 * 1024];
        int bytesRead;

        while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            var request = new CompanyLogoUploadRequest
            {
                CompanyLogoFileName = fileName,
                CompanyLogoData = ByteString.CopyFrom(buffer, 0, bytesRead),
            };

            await call.RequestStream.WriteAsync(request);
        }

        await call.RequestStream.CompleteAsync();
        var response = await call.ResponseAsync;
        return response.AbsoluteFilePath;
    }

    public async Task<string> UploadPersonPhotoAsync(string fileName, IFile file)
    {
        using var call = _uploadClient.UoloadPersonPhoto();
        using var fileStream = file.OpenReadStream();

        byte[] buffer = new byte[1024 * 1024];
        int bytesRead;

        while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            var request = new PersonPhotoUploadRequest
            {
                PersonPhotoFileName = fileName,
                PersonPhotoData = ByteString.CopyFrom(buffer, 0, bytesRead),
            };

            await call.RequestStream.WriteAsync(request);
        }

        await call.RequestStream.CompleteAsync();
        var response = await call.ResponseAsync;
        return response.AbsoluteFilePath;
    }
}
