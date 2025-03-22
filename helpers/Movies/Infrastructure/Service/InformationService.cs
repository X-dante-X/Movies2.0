using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using MoviesCore.Interfaces;
using MoviesCore.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service;

public class InformationService: IInformationService
{
    private readonly MoviesDbContext _context;
    private readonly IDistributedCache _cache;

    public InformationService(MoviesDbContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IQueryable<Keyword?>> GetAllKeywords()
    {
        return await GetOrSetCachedDataAsync("AllKeywords", async () => await _context.Keywords.ToListAsync());
    }

    public async Task<IQueryable<Genre?>> GetAllGenres()
    {
        return await GetOrSetCachedDataAsync("AllGenres", async () => await _context.Genres.ToListAsync());
    }

    public async Task<IQueryable<Language?>> GetAllLanguages()
    {
        return await GetOrSetCachedDataAsync("AllLanguages", async () => await _context.Languages.ToListAsync());
    }

    public async Task<IQueryable<LanguageRole?>> GetAllLanguageRoles()
    {
        return await GetOrSetCachedDataAsync("AllLanguageRoles", async () => await _context.LanguageRoles.ToListAsync());
    }

    public async Task<IQueryable<Country?>> GetAllCountries()
    {
        return await GetOrSetCachedDataAsync("AllCountries", async () => await _context.Countries.ToListAsync());
    }

    public async Task<IQueryable<Gender?>> GetAllGenders()
    {
        return await GetOrSetCachedDataAsync("AllGenders", async () => await _context.Genders.ToListAsync());
    }

    private async Task<IQueryable<T>> GetOrSetCachedDataAsync<T>(string key, Func<Task<List<T>>> getData)
    {
        var cachedData = await _cache.GetStringAsync(key);
        if (cachedData != null)
        {
            return JsonConvert.DeserializeObject<List<T>>(cachedData).AsQueryable();
        }
        else
        {
            var data = await getData();
            await _cache.SetStringAsync(key, JsonConvert.SerializeObject(data));
            return data.AsQueryable();
        }
    }
}
