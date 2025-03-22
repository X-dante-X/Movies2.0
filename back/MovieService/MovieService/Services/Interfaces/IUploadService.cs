namespace MovieService.Services.Interfaces;

public interface IUploadService
{
    public Task<string> UploadMovieAsync(string fileName, IFile file);
    public Task<string> UploadPosterAsync(string fileName, IFile file);
    public Task<string> UploadBackdropAsync(string fileName, IFile file);
    public Task<string> UploadLogoAsync(string fileName, IFile file);
    public Task<string> UploadPersonPhotoAsync(string fileName, IFile file);
}
