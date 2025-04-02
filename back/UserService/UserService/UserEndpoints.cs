using Microsoft.AspNetCore.Http.HttpResults;

namespace UserService
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            app.MapGet("/test", () => Results.Ok("Hello")).WithOpenApi();
        }
    }
}
