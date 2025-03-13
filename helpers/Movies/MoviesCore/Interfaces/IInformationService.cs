using MoviesCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesCore.Interfaces;

public interface IInformationService
{
    Task<IQueryable<Keyword?>> GetAllKeywords();
    Task<IQueryable<Genre?>> GetAllGenres();
    Task<IQueryable<Language?>> GetAllLanguages();
    Task<IQueryable<LanguageRole?>> GetAllLanguageRoles();
    Task<IQueryable<Country?>> GetAllCountries();
    Task<IQueryable<Gender?>> GetAllGenders();
}
