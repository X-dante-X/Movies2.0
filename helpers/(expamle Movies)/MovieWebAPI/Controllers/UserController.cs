using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesCore.Interfaces;
using MoviesCore.Models;
using MovieWebAPI.DTO;

namespace MovieWebAPI.Controllers;

[Route("api/")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet("movies/{movieId}")]
    public async Task<ActionResult<MovieDTO>> GetMovieById(int movieId)
    {
        var movie = await _userService.GetMovieById(movieId);

        if (movie == null)
        {
            return NotFound();
        }

        var movieDTO = _mapper.Map<MovieDTO>(movie);
        return Ok(movieDTO);
    }

    [HttpGet("movies")]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies(
    [FromQuery] int page = 1,
    [FromQuery] int[]? genresId = null,
    [FromQuery] int[]? keywordsId = null)
    {
        var (movies, lastPage) = await _userService.GetMovies(genresId, keywordsId, page);
        var movieDTOs = _mapper.Map<IEnumerable<MovieDTO>>(movies);

        page++;
        var queryString = GenerateQueryString(genresId, keywordsId, page);
        var uri = new Uri($"{Request.Scheme}://{Request.Host}/api/movies{queryString}");


        return Ok(new { Movies = movieDTOs, NextPageUri = uri, MaxPage = lastPage});
    }

    [HttpGet("movies/byTitleFragment/{titleFragment}")]
    public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMoviesByTitleFragment(string titleFragment)
    {
        var movies = await _userService.GetMoviesByTitleFragment(titleFragment);

        if (movies == null)
        {
            return NotFound();
        }

        var movieDTOs = movies.Select(movie => _mapper.Map<MovieDTO>(movie)).ToList();

        return Ok(movieDTOs);
    }

    [HttpGet("movies/{movieId}/casts")]
    public async Task<ActionResult<IEnumerable<MovieCastDTO>>> GetMovieCasts(int movieId)
    {
        var casts = await _userService.GetMovieCastByMovieId(movieId);

        if (casts == null)
        {
            return NotFound();
        }

        var castDTOs = _mapper.Map<IEnumerable<MovieCastDTO>>(casts);
        return Ok(castDTOs);
    }

    [HttpGet("movies/{movieId}/crew")]
    public async Task<ActionResult<IEnumerable<MovieCrewDTO>>> GetMovieCrew(int movieId)
    {
        var crews = await _userService.GetMovieCrewByMovieId(movieId);

        if (crews == null)
        {
            return NotFound();
        }

        var crewDTOs = _mapper.Map<IEnumerable<MovieCrewDTO>>(crews);
        return Ok(crewDTOs);
    }

    [HttpGet("productionCompanies/{companyId}")]
    public async Task<ActionResult<ProductionCompanyDTO>> GetProductionCompanyById(int companyId)
    {
        var company = await _userService.GetProductionCompanyId(companyId);

        if (company == null)
        {
            return NotFound();
        }

        var companyDTO = _mapper.Map<ProductionCompanyDTO>(company);
        return Ok(companyDTO);
    }

    [HttpGet("actors/{actorId}")]
    public async Task<ActionResult<ActorDTO>> GetActorById(int actorId)
    {
        var actor = await _userService.GetMovieCastByActorId(actorId);

        if (actor == null)
        {
            return NotFound();
        }       
        var actorDTO = _mapper.Map<ActorDTO>(actor.ToList());
        return Ok(actorDTO);
    }

    private string GenerateQueryString(int[]? genresId, int[]? keywordsId, int page)
    {
        var queryString = $"?page={page}";

        if (genresId != null && genresId.Length > 0)
        {
            queryString += $"&genresId={string.Join("&genresId=", genresId)}";
        }

        if (keywordsId != null && keywordsId.Length > 0)
        {
            queryString += $"&keywordsId={string.Join("&keywordsId=", keywordsId)}";
        }

        return queryString;
    }
}
