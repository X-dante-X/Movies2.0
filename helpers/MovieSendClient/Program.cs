using Grpc.Net.Client;
using Google.Protobuf;
using Fileupload;

class Program
{
    static async Task Main(string[] args)
    {

        var FileUploadClient = new FileUpload.FileUploadClient(GrpcChannel.ForAddress("http://localhost:5001"));

        var movieUploadTask = UploadMovieAsync(FileUploadClient, "daredevil.mp4");
        var posterUploadTask = UploadPosterAsync(FileUploadClient, "daredevil.jpg");
        var backdropUploadTask = UploadBackdropAsync(FileUploadClient, "backdropdaredevil.jpg");

        await Task.WhenAll(movieUploadTask, posterUploadTask, backdropUploadTask);

        Console.WriteLine("Все файлы загружены.");
    }

    static async Task UploadMovieAsync(FileUpload.FileUploadClient client, string filePath)
    {
        using var call = client.UploadVideo();
        using var fileStream = File.OpenRead(filePath);

        byte[] buffer = new byte[1024];
        int bytesRead;

        while ((bytesRead = await fileStream.ReadAsync(buffer)) > 0)
        {
            var request = new VideoUploadRequest
            {
                FileName = filePath,
                Chunk = ByteString.CopyFrom(buffer, 0, bytesRead),
            };

            await call.RequestStream.WriteAsync(request);
        }

        await call.RequestStream.CompleteAsync();
        var response = await call.ResponseAsync;
        Console.WriteLine($"{filePath} загружен: {response.Message}");
    }

    static async Task UploadPosterAsync(FileUpload.FileUploadClient client, string filePath)
    {
        using var call = client.UploadImage();
        using var fileStream = File.OpenRead(filePath);

        byte[] buffer = new byte[1024];
        int bytesRead;

        while ((bytesRead = await fileStream.ReadAsync(buffer)) > 0)
        {
            var request = new ImageUploadRequest
            {
                FileName = filePath,
                Type = "poster",
                Chunk = ByteString.CopyFrom(buffer, 0, bytesRead),
            };

            await call.RequestStream.WriteAsync(request);
        }

        await call.RequestStream.CompleteAsync();
        var response = await call.ResponseAsync;
        Console.WriteLine($"{filePath} загружен: {response.Message}");
    }

    static async Task UploadBackdropAsync(FileUpload.FileUploadClient client, string filePath)
    {
        using var call = client.UploadImage();
        using var fileStream = File.OpenRead(filePath);

        byte[] buffer = new byte[1024];
        int bytesRead;

        while ((bytesRead = await fileStream.ReadAsync(buffer)) > 0)
        {
            var request = new ImageUploadRequest
            {
                FileName = filePath,
                Type = "backdrop",
                Chunk = ByteString.CopyFrom(buffer, 0, bytesRead),
            };

            await call.RequestStream.WriteAsync(request);
        }

        await call.RequestStream.CompleteAsync();
        var response = await call.ResponseAsync;
        Console.WriteLine($"{filePath} загружен: {response.Message}");
    }
}
