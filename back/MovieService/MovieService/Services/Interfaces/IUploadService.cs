namespace MovieService.Services.Interfaces;

public interface IUploadService
{
    public Task<string> UploadVideoAsync(string fileName, IFile file);
    public Task<string> UploadImageAsync(string fileName, string type, IFile file);
}
