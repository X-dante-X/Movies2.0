using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesCore.Interfaces;
using MoviesCore.Models;

namespace MovieWebAPI.Controllers;

[Route("api/")]
[ApiController]
public class InformationController : ControllerBase
{
    private readonly IInformationService _informationService;

    public InformationController(IInformationService informationService)
    {
        _informationService = informationService;
    }
    [HttpGet("keywords")]
    public async Task<ActionResult<IEnumerable<Keyword>>> GetAllKeywords()
    {
        var keywords = await _informationService.GetAllKeywords();
        return Ok(keywords);
    }

    [HttpGet("genres")]
    public async Task<ActionResult<IEnumerable<Genre>>> GetAllGenres()
    {
        var genres = await _informationService.GetAllGenres();
        return Ok(genres);
    }

    [HttpGet("languages")]
    public async Task<ActionResult<IEnumerable<Keyword>>> GetAllLanguages()
    {
        var languages = await _informationService.GetAllLanguages();
        return Ok(languages);
    }

    [HttpGet("languageroles")]
    public async Task<ActionResult<IEnumerable<Keyword>>> GetAllLanguageRoles()
    {
        var languageRoles = await _informationService.GetAllLanguageRoles();
        return Ok(languageRoles);
    }

    [HttpGet("countries")]
    public async Task<ActionResult<IEnumerable<Keyword>>> GetAllCountries()
    {
        var countryies = await _informationService.GetAllCountries();
        return Ok(countryies);
    }
}
