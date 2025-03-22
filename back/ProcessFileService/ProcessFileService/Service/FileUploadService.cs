using Minio.DataModel.Args;
using Minio;
using Grpc.Core;
using Fileupload;

namespace ProcessFileService.Service;

public class FileUploadService : FileUpload.FileUploadBase
{
    private readonly IMinioClient _minioClient;
    private const string Bucket = "uploads";

    public FileUploadService(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public override async Task<Response> UploadMovie(IAsyncStreamReader<MovieUploadRequest> requestStream, ServerCallContext context)
    {
        string absoluteFilePath = string.Empty;

        await foreach (var request in requestStream.ReadAllAsync())
        {
            if (string.IsNullOrEmpty(request.MovieFileName) || request.MovieData.Length == 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid file data"));
            }

            using var stream = new MemoryStream(request.MovieData.ToByteArray());

            absoluteFilePath = "movie/movie/" + request.MovieFileName + ".mp4";

            var args = new PutObjectArgs()
                .WithBucket(Bucket)
                .WithObject(absoluteFilePath)
                .WithStreamData(stream)
                .WithObjectSize(request.MovieData.Length);

            await _minioClient.PutObjectAsync(args);
        }

        return new Response { Message = "Movie uploaded successfully", AbsoluteFilePath = absoluteFilePath };
    }

    public override async Task<Response> UploadMoviePoster(IAsyncStreamReader<MoviePosterUploadRequest> requestStream, ServerCallContext context)
    {
        string absoluteFilePath = string.Empty;

        await foreach (var request in requestStream.ReadAllAsync())
        {
            if (string.IsNullOrEmpty(request.MoviePosterFileName) || request.MoviePosterData.Length == 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid file data"));
            }

            using var stream = new MemoryStream(request.MoviePosterData.ToByteArray());

            absoluteFilePath = "movie/poster/" + request.MoviePosterFileName + ".jpg";

            var args = new PutObjectArgs()
                .WithBucket(Bucket)
                .WithObject(absoluteFilePath)
                .WithStreamData(stream)
                .WithObjectSize(request.MoviePosterData.Length);

            await _minioClient.PutObjectAsync(args);
        }

        return new Response { Message = "Movie uploaded successfully", AbsoluteFilePath = absoluteFilePath };
    }

    public override async Task<Response> UploadMovieBackdrop(IAsyncStreamReader<MovieBackdropUploadRequest> requestStream, ServerCallContext context)
    {
        string absoluteFilePath = string.Empty;

        await foreach (var request in requestStream.ReadAllAsync())
        {
            if (string.IsNullOrEmpty(request.MovieBackdropFileName) || request.MovieBackdropData.Length == 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid file data"));
            }

            using var stream = new MemoryStream(request.MovieBackdropData.ToByteArray());

            absoluteFilePath = "movie/backdrop/" + request.MovieBackdropFileName + ".jpg";

            var args = new PutObjectArgs()
                .WithBucket(Bucket)
                .WithObject(absoluteFilePath)
                .WithStreamData(stream)
                .WithObjectSize(request.MovieBackdropData.Length);

            await _minioClient.PutObjectAsync(args);
        }

        return new Response { Message = "Movie uploaded successfully", AbsoluteFilePath = absoluteFilePath };
    }

    public override async Task<Response> UploadCompanyLogo(IAsyncStreamReader<CompanyLogoUploadRequest> requestStream, ServerCallContext context)
    {
        string absoluteFilePath = string.Empty;

        await foreach (var request in requestStream.ReadAllAsync())
        {
            if (string.IsNullOrEmpty(request.CompanyLogoFileName) || request.CompanyLogoData.Length == 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid file data"));
            }

            using var stream = new MemoryStream(request.CompanyLogoData.ToByteArray());

            absoluteFilePath = "companylogo/" + request.CompanyLogoFileName + ".jpg";

            var args = new PutObjectArgs()
                .WithBucket(Bucket)
                .WithObject(absoluteFilePath)
                .WithStreamData(stream)
                .WithObjectSize(request.CompanyLogoData.Length);

            await _minioClient.PutObjectAsync(args);
        }

        return new Response { Message = "Movie uploaded successfully", AbsoluteFilePath = absoluteFilePath };
    }

    public override async Task<Response> UoloadPersonPhoto(IAsyncStreamReader<PersonPhotoUploadRequest> requestStream, ServerCallContext context)
    {
        string absoluteFilePath = string.Empty;

        await foreach (var request in requestStream.ReadAllAsync())
        {
            if (string.IsNullOrEmpty(request.PersonPhotoFileName) || request.PersonPhotoData.Length == 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid file data"));
            }

            using var stream = new MemoryStream(request.PersonPhotoData.ToByteArray());

            absoluteFilePath = "personphoto/" + request.PersonPhotoFileName + ".jpg";

            var args = new PutObjectArgs()
                .WithBucket(Bucket)
                .WithObject(absoluteFilePath)
                .WithStreamData(stream)
                .WithObjectSize(request.PersonPhotoData.Length);

            await _minioClient.PutObjectAsync(args);
        }

        return new Response { Message = "Movie uploaded successfully", AbsoluteFilePath = absoluteFilePath };
    }
}