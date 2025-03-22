using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesCore.Interfaces;
using MoviesCore.Models;
using MovieWebAPI.DTO;

namespace MovieWebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;
    private readonly IMapper _mapper;

    public AdminController(IAdminService adminService, IMapper mapper)
    {
        _adminService = adminService;
        _mapper = mapper;
    }

    // Endpoints for movies
    [HttpPost("movies")]
    public async Task<IActionResult> AddMovie(RequestMovieDTO movieDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (movieDTO.MovieStatus != "Released" && movieDTO.MovieStatus != "InProduction")
        {
            ModelState.AddModelError("MovieStatus", "The MovieStatus field must be 'Released' or 'InProduction'.");
            return BadRequest(ModelState);
        }

        if (movieDTO.MovieStatus == "InProduction" && movieDTO.ReleaseDate.HasValue)
        {
            ModelState.AddModelError("ReleaseDate", "ReleaseDate should be null for movies in production.");
            return BadRequest(ModelState);
        }

        if (movieDTO.Popularity.HasValue && movieDTO.Popularity < 0)
        {
            ModelState.AddModelError("Popularity", "Popularity should be a positive number.");
            return BadRequest(ModelState);
        }

        if (movieDTO.Budget.HasValue && movieDTO.Budget < 0)
        {
            ModelState.AddModelError("Budget", "Budget should be a positive number.");
            return BadRequest(ModelState);
        }

        if (movieDTO.Revenue.HasValue && movieDTO.Revenue < 0)
        {
            ModelState.AddModelError("Revenue", "Revenue should be a positive number.");
            return BadRequest(ModelState);
        }

        if (movieDTO.Runtime.HasValue && movieDTO.Runtime <= 0)
        {
            ModelState.AddModelError("Runtime", "Runtime should be a positive number.");
            return BadRequest(ModelState);
        }

        if (movieDTO.VoteAverage.HasValue && (movieDTO.VoteAverage < 0 || movieDTO.VoteAverage > 10))
        {
            ModelState.AddModelError("VoteAverage", "VoteAverage should be a number between 0 and 10.");
            return BadRequest(ModelState);
        }

        if (movieDTO.VoteCount.HasValue && movieDTO.VoteCount < 0)
        {
            ModelState.AddModelError("VoteCount", "VoteCount should be a positive number.");
            return BadRequest(ModelState);
        }

        var movie = _mapper.Map<Movie>(movieDTO);
        var addedMovie = await _adminService.AddMovie(movie);
        return Ok(addedMovie);
    }

    [HttpPut("movies")]
    public async Task<IActionResult> UpdateMovie(Movie movie)
    {
        var updatedMovie = await _adminService.UpdateMovie(movie);
        return Ok(updatedMovie);
    }

    [HttpDelete("movies/{movieId}")]
    public async Task<IActionResult> DeleteMovie(int movieId)
    {
        await _adminService.DeleteMovie(movieId);
        return Ok();
    }

    [HttpPost("movies/{movieId}/setkeywords")]
    public async Task<IActionResult> SetKeywordsToMovie(int movieId, int[] keywordsId)
    {
        await _adminService.SetKeywordsToMovie(movieId, keywordsId);
        return Ok();
    }

    [HttpPost("movies/{movieId}/setgenres")]
    public async Task<IActionResult> SetGenresToMovie(int movieId,  int[] genresId)
    {
        await _adminService.SetGenresToMovie(movieId, genresId);
        return Ok();
    }

    [HttpPost("movies/{movieId}/setproductionCountries")]
    public async Task<IActionResult> SetProductionCountriesToMovie(int movieId, int[] productionCountriesId)
    {
        await _adminService.SetProductionCountriesToMovie(movieId, productionCountriesId);
        return Ok();
    }


    // Endpoints for persons
    [HttpPost("persons")]
    public async Task<IActionResult> AddPerson(Person person)
    {
        var addedPerson = await _adminService.AddPerson(person);
        return Ok(addedPerson);
    }

    [HttpPut("persons")]
    public async Task<IActionResult> UpdatePerson(Person person)
    {
        var updatedPerson = await _adminService.UpdatePerson(person);
        return Ok(updatedPerson);
    }

    [HttpDelete("persons/{personId}")]
    public async Task<IActionResult> DeletePerson(int personId)
    {
        await _adminService.DeletePerson(personId);
        return Ok();
    }

    // Endpoints for production companies
    [HttpPost("productionCompanies")]
    public async Task<IActionResult> AddProductionCompany(ProductionCompany productionCompany)
    {
        var addedProductionCompany = await _adminService.AddProductionCompany(productionCompany);
        return Ok(addedProductionCompany);
    }

    [HttpPut("productionCompanies")]
    public async Task<IActionResult> UpdateProductionCompany(ProductionCompany productionCompany)
    {
        var updatedProductionCompany = await _adminService.UpdateProductionCompany(productionCompany);
        return Ok(updatedProductionCompany);
    }

    [HttpDelete("productionCompanies/{productionCompanyId}")]
    public async Task<IActionResult> DeleteProductionCompany(int productionCompanyId)
    {
        await _adminService.DeleteProductionCompany(productionCompanyId);
        return Ok();
    }
    [HttpPost("productionCompanies/{productionCompanyId}/setmovies")]
    public async Task<IActionResult> SetMoviesToProductionCompany(int productionCompanyId, int[] moviesId)
    {
        await _adminService.SetMoviesToProdactionCompany(productionCompanyId, moviesId);
        return Ok();
    }
}
