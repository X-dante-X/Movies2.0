using GraphQL_DEMO.DBContext;
using HotChocolate;
using Models;
using System.Security.Claims;

namespace GraphQL_DEMO.GraphQL;

public class Query
{
    [UseProjection]
    [UseFiltering()]
    [UseSorting()]
    public IQueryable<Movie> Movies([Service] Context ctx)
    {
        return ctx.Movies;
    }
}
