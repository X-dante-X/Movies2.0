using Grpc.Net.Client;
using Google.Protobuf;
using Movieupload;
using Posterupload;
using Backdropupload;
using System.Threading.Channels;

class Program
{
    static async Task Main(string[] args)
    {
        var channel = GrpcChannel.ForAddress("https://localhost:7133");

        var movieClient = new MovieUpload.MovieUploadClient(channel);
        var posterClient = new PosterUpload.PosterUploadClient(channel);
        var backdropClient = new BackdropUpload.BackdropUploadClient(channel);

        var movieUploadTask = UploadMovieAsync(movieClient, "daredevil.mp4");
        var posterUploadTask = UploadPosterAsync(posterClient, "daredevil.jpg");
        var backdropUploadTask = UploadBackdropAsync(backdropClient, "backdropdaredevil.jpg");

        await Task.WhenAll(movieUploadTask, posterUploadTask, backdropUploadTask);

        Console.WriteLine("Все файлы загружены.");
    }

    static async Task UploadMovieAsync(MovieUpload.MovieUploadClient client, string filePath)
    {
        using var call = client.UploadMovie();
        using var fileStream = File.OpenRead(filePath);

        byte[] buffer = new byte[1024 * 1024 * 100];
        int bytesRead;

        while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            var request = new MovieUploadRequest
            {
                MovieFilePath = "movies/" + filePath,
                MovieData = ByteString.CopyFrom(buffer, 0, bytesRead),
            };

            await call.RequestStream.WriteAsync(request);
        }

        await call.RequestStream.CompleteAsync();
        var response = await call.ResponseAsync;
        Console.WriteLine($"{filePath} загружен: {response.Message}");
    }

    static async Task UploadPosterAsync(PosterUpload.PosterUploadClient client, string filePath)
    {
        using var call = client.UploadPoster();
        using var fileStream = File.OpenRead(filePath);

        byte[] buffer = new byte[1024 * 1024];
        int bytesRead;

        while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            var request = new PosterUploadRequest
            {
                PosterFilePath = "posters/" + filePath,
                PosterData = ByteString.CopyFrom(buffer, 0, bytesRead),
            };

            await call.RequestStream.WriteAsync(request);
        }

        await call.RequestStream.CompleteAsync();
        var response = await call.ResponseAsync;
        Console.WriteLine($"{filePath} загружен: {response.Message}");
    }

    static async Task UploadBackdropAsync(BackdropUpload.BackdropUploadClient client, string filePath)
    {
        using var call = client.UploadBackdrop();
        using var fileStream = File.OpenRead(filePath);

        byte[] buffer = new byte[1024*1024];
        int bytesRead;

        while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            var request = new BackdropUploadRequest
            {
                BackdropFilePath = "backdrops/" + filePath,
                BackdropData = ByteString.CopyFrom(buffer, 0, bytesRead),
            };

            await call.RequestStream.WriteAsync(request);
        }

        await call.RequestStream.CompleteAsync();
        var response = await call.ResponseAsync;
        Console.WriteLine($"{filePath} загружен: {response.Message}");
    }
}
