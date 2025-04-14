using DBContext;
using Microsoft.EntityFrameworkCore;
using Models;
using Tag = Models.Tag;

namespace Movies;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new Context(
            serviceProvider.GetRequiredService<DbContextOptions<Context>>());

        if (context.Movies.Any())
        {
            return; // DB has been seeded
        }

        var countries = new List<Country>
        {
            new() { CountryIsoCode = "US", CountryName = "United States" },
            new() { CountryIsoCode = "CA", CountryName = "Canada" },
            new() { CountryIsoCode = "GB", CountryName = "United Kingdom" },
            new() { CountryIsoCode = "FR", CountryName = "France" },
            new() { CountryIsoCode = "JP", CountryName = "Japan" },
            new() { CountryIsoCode = "DE", CountryName = "Germany" },
            new() { CountryIsoCode = "IT", CountryName = "Italy" },
            new() { CountryIsoCode = "ES", CountryName = "Spain" },
            new() { CountryIsoCode = "NL", CountryName = "Netherlands" },
            new() { CountryIsoCode = "SE", CountryName = "Sweden" },
            new() { CountryIsoCode = "NO", CountryName = "Norway" },
            new() { CountryIsoCode = "FI", CountryName = "Finland" },
            new() { CountryIsoCode = "DK", CountryName = "Denmark" },
            new() { CountryIsoCode = "BE", CountryName = "Belgium" },
            new() { CountryIsoCode = "AT", CountryName = "Austria" },
            new() { CountryIsoCode = "CH", CountryName = "Switzerland" },
            new() { CountryIsoCode = "PT", CountryName = "Portugal" },
            new() { CountryIsoCode = "GR", CountryName = "Greece" },
            new() { CountryIsoCode = "IE", CountryName = "Ireland" },
            new() { CountryIsoCode = "PL", CountryName = "Poland" },
            new() { CountryIsoCode = "HU", CountryName = "Hungary" },
            new() { CountryIsoCode = "CZ", CountryName = "Czech Republic" },
            new() { CountryIsoCode = "SK", CountryName = "Slovakia" },
            new() { CountryIsoCode = "RO", CountryName = "Romania" },
            new() { CountryIsoCode = "BG", CountryName = "Bulgaria" },
            new() { CountryIsoCode = "HR", CountryName = "Croatia" },
            new() { CountryIsoCode = "SI", CountryName = "Slovenia" },
            new() { CountryIsoCode = "LT", CountryName = "Lithuania" },
            new() { CountryIsoCode = "LV", CountryName = "Latvia" },
            new() { CountryIsoCode = "EE", CountryName = "Estonia" },
            new() { CountryIsoCode = "RU", CountryName = "Russia" },
            new() { CountryIsoCode = "UA", CountryName = "Ukraine" },
            new() { CountryIsoCode = "BY", CountryName = "Belarus" },
            new() { CountryIsoCode = "KZ", CountryName = "Kazakhstan" },
            new() { CountryIsoCode = "AM", CountryName = "Armenia" },
            new() { CountryIsoCode = "GE", CountryName = "Georgia" },
            new() { CountryIsoCode = "AZ", CountryName = "Azerbaijan" },
            new() { CountryIsoCode = "MD", CountryName = "Moldova" },
            new() { CountryIsoCode = "UZ", CountryName = "Uzbekistan" },
            new() { CountryIsoCode = "KG", CountryName = "Kyrgyzstan" },
            new() { CountryIsoCode = "TM", CountryName = "Turkmenistan" },
            new() { CountryIsoCode = "TJ", CountryName = "Tajikistan" },
            new() { CountryIsoCode = "CN", CountryName = "China" },
            new() { CountryIsoCode = "IN", CountryName = "India" },
            new() { CountryIsoCode = "KR", CountryName = "South Korea" },
            new() { CountryIsoCode = "VN", CountryName = "Vietnam" },
            new() { CountryIsoCode = "TH", CountryName = "Thailand" },
            new() { CountryIsoCode = "PH", CountryName = "Philippines" },
            new() { CountryIsoCode = "MY", CountryName = "Malaysia" },
            new() { CountryIsoCode = "ID", CountryName = "Indonesia" },
            new() { CountryIsoCode = "SG", CountryName = "Singapore" },
            new() { CountryIsoCode = "HK", CountryName = "Hong Kong" },
            new() { CountryIsoCode = "TW", CountryName = "Taiwan" },
            new() { CountryIsoCode = "MN", CountryName = "Mongolia" },
            new() { CountryIsoCode = "PK", CountryName = "Pakistan" },
            new() { CountryIsoCode = "BD", CountryName = "Bangladesh" },
            new() { CountryIsoCode = "LK", CountryName = "Sri Lanka" },
            new() { CountryIsoCode = "NP", CountryName = "Nepal" },
            new() { CountryIsoCode = "MM", CountryName = "Myanmar" },
            new() { CountryIsoCode = "LA", CountryName = "Laos" },
            new() { CountryIsoCode = "KH", CountryName = "Cambodia" },
            new() { CountryIsoCode = "BT", CountryName = "Bhutan" },
            new() { CountryIsoCode = "MV", CountryName = "Maldives" },
            new() { CountryIsoCode = "AF", CountryName = "Afghanistan" }
        };

        context.Countries.AddRange(countries);
        context.SaveChanges();

        var languages = new List<Language>
        {
            new() { LanguageName = "English" },
            new() { LanguageName = "French" },
            new() { LanguageName = "Japanese" },
            new() { LanguageName = "German" },
            new() { LanguageName = "Spanish" },
            new() { LanguageName = "Italian" },
            new() { LanguageName = "Dutch" },
            new() { LanguageName = "Swedish" },
            new() { LanguageName = "Norwegian" },
            new() { LanguageName = "Danish" },
            new() { LanguageName = "Finnish" },
            new() { LanguageName = "Portuguese" },
            new() { LanguageName = "Greek" },
            new() { LanguageName = "Polish" },
            new() { LanguageName = "Hungarian" },
            new() { LanguageName = "Czech" },
            new() { LanguageName = "Slovak" },
            new() { LanguageName = "Romanian" },
            new() { LanguageName = "Bulgarian" },
            new() { LanguageName = "Croatian" },
            new() { LanguageName = "Slovenian" },
            new() { LanguageName = "Lithuanian" },
            new() { LanguageName = "Latvian" },
            new() { LanguageName = "Estonian" },
            new() { LanguageName = "Russian" },
            new() { LanguageName = "Ukrainian" },
            new() { LanguageName = "Belarusian" },
            new() { LanguageName = "Kazakh" },
            new() { LanguageName = "Armenian" },
            new() { LanguageName = "Georgian" },
            new() { LanguageName = "Azerbaijani" },
            new() { LanguageName = "Moldovan" },
            new() { LanguageName = "Uzbek" },
            new() { LanguageName = "Kyrgyz" },
            new() { LanguageName = "Turkmen" },
            new() { LanguageName = "Tajik" },
            new() { LanguageName = "Chinese" },
            new() { LanguageName = "Mandarin" },
            new() { LanguageName = "Cantonese" },
            new() { LanguageName = "Korean" },
            new() { LanguageName = "Vietnamese" },
            new() { LanguageName = "Thai" },
            new() { LanguageName = "Filipino" },
            new() { LanguageName = "Malay" },
            new() { LanguageName = "Indonesian" },
            new() { LanguageName = "Hindi" },
            new() { LanguageName = "Bengali" },
            new() { LanguageName = "Urdu" },
            new() { LanguageName = "Tamil" },
            new() { LanguageName = "Telugu" },
            new() { LanguageName = "Marathi" },
            new() { LanguageName = "Punjabi" },
            new() { LanguageName = "Gujarati" },
            new() { LanguageName = "Sinhala" },
            new() { LanguageName = "Nepali" },
            new() { LanguageName = "Burmese" },
            new() { LanguageName = "Lao" },
            new() { LanguageName = "Khmer" },
            new() { LanguageName = "Arabic" },
            new() { LanguageName = "Hebrew" },
            new() { LanguageName = "Turkish" },
            new() { LanguageName = "Persian" },
            new() { LanguageName = "Pashto" }
        };

        context.Languages.AddRange(languages);
        context.SaveChanges();

        var genres = new List<Genre>
        {
            new() { GenreName = "Action" },
            new() { GenreName = "Adventure" },
            new() { GenreName = "Comedy" },
            new() { GenreName = "Drama" },
            new() { GenreName = "Horror" },
            new() { GenreName = "Sci-Fi" },
            new() { GenreName = "Fantasy" },
            new() { GenreName = "Thriller" },
            new() { GenreName = "Mystery" },
            new() { GenreName = "Crime" },
            new() { GenreName = "Romance" },
            new() { GenreName = "Animation" },
            new() { GenreName = "Family" },
            new() { GenreName = "Musical" },
            new() { GenreName = "Documentary" },
            new() { GenreName = "Biography" },
            new() { GenreName = "History" },
            new() { GenreName = "War" },
            new() { GenreName = "Western" },
            new() { GenreName = "Sport" },
            new() { GenreName = "Psychological Thriller" },
            new() { GenreName = "Post-Apocalyptic" },
            new() { GenreName = "Dystopian" },
            new() { GenreName = "Superhero" },
            new() { GenreName = "Dark Fantasy" },
            new() { GenreName = "Space Opera" },
            new() { GenreName = "Slasher" },
            new() { GenreName = "Gore" },
            new() { GenreName = "Found Footage" },
            new() { GenreName = "Mockumentary" },
            new() { GenreName = "Satire" },
            new() { GenreName = "Parody" },
            new() { GenreName = "Heist" },
            new() { GenreName = "Espionage" },
            new() { GenreName = "Martial Arts" },
            new() { GenreName = "Noir" },
            new() { GenreName = "Neo-Noir" },
            new() { GenreName = "Kaiju" },
            new() { GenreName = "Disaster" },
            new() { GenreName = "Psychological Drama" },
            new() { GenreName = "Coming-of-Age" },
            new() { GenreName = "Survival" },
            new() { GenreName = "Time Travel" },
            new() { GenreName = "Legal Drama" },
            new() { GenreName = "Medical Drama" },
            new() { GenreName = "Political Drama" },
            new() { GenreName = "Silent Film" },
            new() { GenreName = "Experimental" },
            new() { GenreName = "Art House" },
            new() { GenreName = "Road Movie" }
        };
        context.Genres.AddRange(genres);
        context.SaveChanges();

        var productionCompanies = new List<ProductionCompany>
        {
            new() { CompanyName = "Warner Bros.", Country = countries.First(c => c.CountryIsoCode == "US"), LogoPath = "/companylogo/wb.jpg" },
            new() { CompanyName = "20th Century Studios", Country = countries.First(c => c.CountryIsoCode == "US"), LogoPath = "/companylogo/20thca.jpg" },
            new() { CompanyName = "Paramount Pictures", Country = countries.First(c => c.CountryIsoCode == "US") },
            new() { CompanyName = "Universal Pictures", Country = countries.First(c => c.CountryIsoCode == "US") },
            new() { CompanyName = "Columbia Pictures", Country = countries.First(c => c.CountryIsoCode == "US") },
            new() { CompanyName = "Walt Disney Pictures", Country = countries.First(c => c.CountryIsoCode == "US") },
            new() { CompanyName = "Marvel Studios", Country = countries.First(c => c.CountryIsoCode == "US") },
            new() { CompanyName = "DC Films", Country = countries.First(c => c.CountryIsoCode == "US") },
            new() { CompanyName = "Pixar", Country = countries.First(c => c.CountryIsoCode == "US") },
            new() { CompanyName = "Lionsgate Films", Country = countries.First(c => c.CountryIsoCode == "US") },
            new() { CompanyName = "New Line Cinema", Country = countries.First(c => c.CountryIsoCode == "US") },
            new() { CompanyName = "DreamWorks Pictures", Country = countries.First(c => c.CountryIsoCode == "US") },

            new() { CompanyName = "BBC Films", Country = countries.First(c => c.CountryIsoCode == "GB") },
            new() { CompanyName = "Working Title Films", Country = countries.First(c => c.CountryIsoCode == "GB") },
            new() { CompanyName = "Pinewood Studios", Country = countries.First(c => c.CountryIsoCode == "GB") },

            new() { CompanyName = "Studio Ghibli", Country = countries.First(c => c.CountryIsoCode == "JP"), LogoPath = "/companylogo/sg.jpg" },
            new() { CompanyName = "Toho", Country = countries.First(c => c.CountryIsoCode == "JP") },
            new() { CompanyName = "Sunrise", Country = countries.First(c => c.CountryIsoCode == "JP") },
            new() { CompanyName = "Kyoto Animation", Country = countries.First(c => c.CountryIsoCode == "JP") },

            new() { CompanyName = "Gaumont", Country = countries.First(c => c.CountryIsoCode == "FR") },
            new() { CompanyName = "Pathé", Country = countries.First(c => c.CountryIsoCode == "FR") },
            new() { CompanyName = "EuropaCorp", Country = countries.First(c => c.CountryIsoCode == "FR") },

            new() { CompanyName = "Bavaria Film", Country = countries.First(c => c.CountryIsoCode == "DE") },
            new() { CompanyName = "UFA", Country = countries.First(c => c.CountryIsoCode == "DE") },

            new() { CompanyName = "Cinecittà Studios", Country = countries.First(c => c.CountryIsoCode == "IT") },

            new() { CompanyName = "Mosfilm", Country = countries.First(c => c.CountryIsoCode == "RU") },
            new() { CompanyName = "Lenfilm", Country = countries.First(c => c.CountryIsoCode == "RU") },
            new() { CompanyName = "CTB Film Company", Country = countries.First(c => c.CountryIsoCode == "RU") },

            new() { CompanyName = "Yash Raj Films", Country = countries.First(c => c.CountryIsoCode == "IN") },
            new() { CompanyName = "Eros International", Country = countries.First(c => c.CountryIsoCode == "IN") },

            new() { CompanyName = "CJ Entertainment", Country = countries.First(c => c.CountryIsoCode == "KR") },
            new() { CompanyName = "Showbox", Country = countries.First(c => c.CountryIsoCode == "KR") },

            new() { CompanyName = "China Film Group", Country = countries.First(c => c.CountryIsoCode == "CN") },
            new() { CompanyName = "Wanda Media", Country = countries.First(c => c.CountryIsoCode == "CN") }
        };
        context.ProductionCompanies.AddRange(productionCompanies);
        context.SaveChanges();

        var tags = new List<Tag>
        {
            new() { TagName = "Thrilling" },
            new() { TagName = "Mind-Bending" },
            new() { TagName = "Emotional" },
            new() { TagName = "Epic" },
            new() { TagName = "Classic" },
            new() { TagName = "Heartwarming" },
            new() { TagName = "Inspiring" },
            new() { TagName = "Dark" },
            new() { TagName = "Chilling" },
            new() { TagName = "Suspenseful" },
            new() { TagName = "Tearjerker" },
            new() { TagName = "Wholesome" },
            new() { TagName = "Feel-Good" },
            new() { TagName = "Plot Twist" },
            new() { TagName = "Slow Burn" },
            new() { TagName = "Non-Linear Narrative" },
            new() { TagName = "Psychological" },
            new() { TagName = "Thought-Provoking" },
            new() { TagName = "Unpredictable" },
            new() { TagName = "Philosophical" },
            new() { TagName = "Satirical" },
            new() { TagName = "Social Commentary" },
            new() { TagName = "Visually Stunning" },
            new() { TagName = "Artistic" },
            new() { TagName = "Dreamlike" },
            new() { TagName = "Surreal" },
            new() { TagName = "Gritty" },
            new() { TagName = "Neo-Noir" },
            new() { TagName = "Colorful" },
            new() { TagName = "Monochrome" },
            new() { TagName = "Fast-Paced" },
            new() { TagName = "Action-Packed" },
            new() { TagName = "Slow-Paced" },
            new() { TagName = "Intense" },
            new() { TagName = "Adrenaline-Fueled" },
            new() { TagName = "Time Travel" },
            new() { TagName = "Dystopian" },
            new() { TagName = "Post-Apocalyptic" },
            new() { TagName = "Cyberpunk" },
            new() { TagName = "Space Opera" },
            new() { TagName = "Supernatural" },
            new() { TagName = "Superhero" },
            new() { TagName = "Martial Arts" },
            new() { TagName = "Heist" },
            new() { TagName = "Spy" },
            new() { TagName = "Samurai" },
            new() { TagName = "Zombie" },
            new() { TagName = "Vampire" },
            new() { TagName = "Mythological" },
            new() { TagName = "Folklore" },
            new() { TagName = "Alien Invasion" },
            new() { TagName = "Oscar-Winning" },
            new() { TagName = "Cult Classic" },
            new() { TagName = "Critically Acclaimed" },
            new() { TagName = "Underrated" },
            new() { TagName = "Indie" },
            new() { TagName = "Experimental" }
        };
        context.Tags.AddRange(tags);
        context.SaveChanges();

        var people = new List<Person>
        {
            new() { PersonName = "Leonardo DiCaprio", Gender = "Male", DateOfBirth = new DateTime(1974, 11, 11).ToUniversalTime(), Nationality = countries[0], Biography = "Famous actor known for Titanic and Inception",
                PhotoPath = "/personphoto/ldc.jpg" },
            new() { PersonName = "Emma Watson", Gender = "Female", DateOfBirth = new DateTime(1990, 4, 15).ToUniversalTime(), Nationality = countries[2], Biography = "Known for Harry Potter series" },
            new() { PersonName = "Robert Downey Jr.", Gender = "Male", DateOfBirth = new DateTime(1965, 4, 4).ToUniversalTime(), Nationality = countries[0], Biography = "Famous for playing Iron Man" },
            new() { PersonName = "Scarlett Johansson", Gender = "Female", DateOfBirth = new DateTime(1984, 11, 22).ToUniversalTime(), Nationality = countries[0], Biography = "Known for Black Widow in Marvel movies" },
            new() { PersonName = "Tom Hardy", Gender = "Male", DateOfBirth = new DateTime(1977, 9, 15).ToUniversalTime(), Nationality = countries[2], Biography = "Known for roles in Inception, Venom, and Mad Max" },
            new() { PersonName = "Morgan Freeman", Gender = "Male", DateOfBirth = new DateTime(1937, 6, 1).ToUniversalTime(), Nationality = countries[0], Biography = "Legendary actor known for Shawshank Redemption" },
            new() { PersonName = "Keanu Reeves", Gender = "Male", DateOfBirth = new DateTime(1964, 9, 2).ToUniversalTime(), Nationality = countries[0], Biography = "Known for The Matrix and John Wick" },
            new() { PersonName = "Christopher Nolan", Gender = "Male", DateOfBirth = new DateTime(1970, 7, 30).ToUniversalTime(), Nationality = countries[2], Biography = "Famous for Inception, Interstellar, and The Dark Knight trilogy" },
            new() { PersonName = "Quentin Tarantino", Gender = "Male", DateOfBirth = new DateTime(1963, 3, 27).ToUniversalTime(), Nationality = countries[0], Biography = "Known for Pulp Fiction, Kill Bill, and Django Unchained" },
            new() { PersonName = "Steven Spielberg", Gender = "Male", DateOfBirth = new DateTime(1946, 12, 18).ToUniversalTime(), Nationality = countries[0], Biography = "Legendary director of Jurassic Park, E.T., and Indiana Jones" },
            new() { PersonName = "Hayao Miyazaki", Gender = "Male", DateOfBirth = new DateTime(1941, 1, 5).ToUniversalTime(), Nationality = countries[4], Biography = "Japanese animation master, co-founder of Studio Ghibli",
            PhotoPath = "/personphoto/hm.jpg" },
            new() { PersonName = "James Cameron", Gender = "Male", DateOfBirth = new DateTime(1954, 8, 16).ToUniversalTime(), Nationality = countries[1], Biography = "Director of Titanic, Avatar, and Terminator",
            PhotoPath = "/personphoto/jc.jpg" }
        };
        context.People.AddRange(people);
        context.SaveChanges();

        var movies = new List<Movie>
        {
            new()
            {
                Title = "Inception",
                ReleaseDate = new DateTime(2010, 7, 16).ToUniversalTime(),
                Budget = 160000000,
                Description = "A thief who enters the dreams of others to steal secrets.",
                Popularity = 9.2m,
                Runtime = 148,
                MovieStatus = "Released",
                VoteAverage = 8.8m,
                VoteCount = 22000,
                PEGI = "13",
                Genre = [genres[4], genres[0]],
                Tags = [tags[1], tags[3]],
                ProductionCompany = productionCompanies[0],
                ProductionLanguage = languages[0],
                ProductionCountry = countries[0],
                MovieCasts =
                [
                    new() { Person = people.First(p => p.PersonName == "Leonardo DiCaprio"), CharacterName = "Dom Cobb", CharacterGender = "Male", Job = "Actor" },
                    new() { Person = people.First(p => p.PersonName == "Tom Hardy"), CharacterName = "Eames", CharacterGender = "Male", Job = "Actor" },
                    new() { Person = people.First(p => p.PersonName == "Christopher Nolan"), CharacterGender = "Male", Job = "Director" }
                ],
                MoviePath = "movie/movie/1/master.m3u8",
                PosterPath = "/movie/poster/1.jpg",
                BackdropPath ="/movie/backdrop/1.jpg",
            },
            new()
            {
                Title = "The Matrix",
                ReleaseDate = new DateTime(1999, 3, 31).ToUniversalTime(),
                Budget = 63000000,
                Description = "A hacker discovers the world is a simulated reality and joins the resistance.",
                Popularity = 9.0m,
                Runtime = 136,
                MovieStatus = "Released",
                VoteAverage = 8.7m,
                VoteCount = 20000,
                PEGI = "18",
                Genre = [genres[4], genres[0]],
                Tags = [tags[1], tags[3], tags[4]],
                ProductionCompany = productionCompanies.First(pc => pc.CompanyName == "Warner Bros."),
                ProductionLanguage = languages[0],
                ProductionCountry = countries[0],
                MovieCasts =
                [
                    new() { Person = people.First(p => p.PersonName == "Keanu Reeves"), CharacterName = "Neo", CharacterGender = "Male", Job = "Actor" },
                    new() { Person = people.First(p => p.PersonName == "Scarlett Johansson"), CharacterName = "Trinity", CharacterGender = "Female", Job = "Actor" }
                ],
                MoviePath = "movie/movie/1/master.m3u8",
                PosterPath = "/movie/poster/2.jpg",
                BackdropPath ="/movie/backdrop/2.jpg",
            },
            new()
            {
                Title = "Titanic",
                ReleaseDate = new DateTime(1997, 12, 19).ToUniversalTime(),
                Budget = 200000000,
                Description = "A romance blossoms aboard the ill-fated Titanic.",
                Popularity = 9.5m,
                Runtime = 195,
                MovieStatus = "Released",
                VoteAverage = 8.9m,
                VoteCount = 25000,
                PEGI = "13",
                Genre = [genres[2], genres[10]],
                Tags = [tags[2], tags[4]],
                ProductionCompany = productionCompanies.First(pc => pc.CompanyName == "20th Century Studios"),
                ProductionLanguage = languages[0],
                ProductionCountry = countries[0],
                MovieCasts =
                [
                    new() { Person = people.First(p => p.PersonName == "Leonardo DiCaprio"), CharacterName = "Jack Dawson", CharacterGender = "Male", Job = "Actor" },
                    new() { Person = people.First(p => p.PersonName == "James Cameron"), CharacterGender = "Male", Job = "Director" }
                ],
                MoviePath = "movie/movie/1/master.m3u8",
                PosterPath = "/movie/poster/3.jpg",
                BackdropPath ="/movie/backdrop/3.jpg",
            },
            new()
            {
                Title = "Spirited Away",
                ReleaseDate = new DateTime(2001, 7, 20).ToUniversalTime(),
                Budget = 19000000,
                Description = "A young girl wanders into a world of spirits and must find her way home.",
                Popularity = 9.8m,
                Runtime = 125,
                MovieStatus = "Released",
                VoteAverage = 9.3m,
                VoteCount = 18000,
                PEGI = "18",
                Genre = [genres[6], genres[11]],
                Tags = [tags[2], tags[5], tags[12]],
                ProductionCompany = productionCompanies.First(pc => pc.CompanyName == "Studio Ghibli"),
                ProductionLanguage = languages.First(l => l.LanguageName == "Japanese"),
                ProductionCountry = countries.First(c => c.CountryIsoCode == "JP"),
                MovieCasts =
                [
                    new() { Person = people.First(p => p.PersonName == "Hayao Miyazaki"), CharacterGender = "Male", Job = "Director" }
                ],
                MoviePath = "movie/movie/1/master.m3u8",
                PosterPath = "/movie/poster/4.jpg",
                BackdropPath ="/movie/backdrop/4.jpg",
            },
            new()
            {
                Title = "Spirited Away",
                ReleaseDate = new DateTime(2001, 7, 20).ToUniversalTime(),
                Budget = 19000000,
                Description = "A young girl wanders into a world of spirits and must find her way home.",
                Popularity = 9.8m,
                Runtime = 125,
                MovieStatus = "Released",
                VoteAverage = 9.3m,
                VoteCount = 18000,
                PEGI = "18",
                Genre = [genres[6], genres[11]],
                Tags = [tags[2], tags[5], tags[12]],
                ProductionCompany = productionCompanies.First(pc => pc.CompanyName == "Studio Ghibli"),
                ProductionLanguage = languages.First(l => l.LanguageName == "Japanese"),
                ProductionCountry = countries.First(c => c.CountryIsoCode == "JP"),
                MovieCasts =
                [
                    new() { Person = people.First(p => p.PersonName == "Hayao Miyazaki"), CharacterGender = "Male", Job = "Director" }
                ],
                MoviePath = "movie/movie/1/master.m3u8",
                PosterPath = "/movie/poster/5.jpg",
                BackdropPath ="/movie/backdrop/5.jpg",
            },
            new()
            {
                Title = "Spirited Away",
                ReleaseDate = new DateTime(2001, 7, 20).ToUniversalTime(),
                Budget = 19000000,
                Description = "A young girl wanders into a world of spirits and must find her way home.",
                Popularity = 9.8m,
                Runtime = 125,
                MovieStatus = "Released",
                VoteAverage = 9.3m,
                VoteCount = 18000,
                PEGI = "13",
                Genre = [genres[6], genres[11]],
                Tags = [tags[2], tags[5], tags[12]],
                ProductionCompany = productionCompanies.First(pc => pc.CompanyName == "Studio Ghibli"),
                ProductionLanguage = languages.First(l => l.LanguageName == "Japanese"),
                ProductionCountry = countries.First(c => c.CountryIsoCode == "JP"),
                MovieCasts =
                [
                    new() { Person = people.First(p => p.PersonName == "Hayao Miyazaki"), CharacterGender = "Male", Job = "Director" }
                ],
                MoviePath = "movie/movie/1/master.m3u8",
                PosterPath = "/movie/poster/6.jpg",
                BackdropPath ="/movie/backdrop/6.jpg",
            },
            new()
            {
                Title = "Spirited Away",
                ReleaseDate = new DateTime(2001, 7, 20).ToUniversalTime(),
                Budget = 19000000,
                Description = "A young girl wanders into a world of spirits and must find her way home.",
                Popularity = 9.8m,
                Runtime = 125,
                MovieStatus = "Released",
                VoteAverage = 9.3m,
                VoteCount = 18000,
                PEGI = "3",
                Genre = [genres[6], genres[11]],
                Tags = [tags[2], tags[5], tags[12]],
                ProductionCompany = productionCompanies.First(pc => pc.CompanyName == "Studio Ghibli"),
                ProductionLanguage = languages.First(l => l.LanguageName == "Japanese"),
                ProductionCountry = countries.First(c => c.CountryIsoCode == "JP"),
                MovieCasts =
                [
                    new() { Person = people.First(p => p.PersonName == "Hayao Miyazaki"), CharacterGender = "Male", Job = "Director" }
                ],
                MoviePath = "movie/movie/1/master.m3u8",
                PosterPath = "/movie/poster/7.jpg",
                BackdropPath ="/movie/backdrop/7.jpg",
            },
            new()
            {
                Title = "Spirited Away",
                ReleaseDate = new DateTime(2001, 7, 20).ToUniversalTime(),
                Budget = 19000000,
                Description = "A young girl wanders into a world of spirits and must find her way home.",
                Popularity = 9.8m,
                Runtime = 125,
                MovieStatus = "Released",
                VoteAverage = 9.3m,
                VoteCount = 18000,
                PEGI = "7",
                Genre = [genres[6], genres[11]],
                Tags = [tags[2], tags[5], tags[12]],
                ProductionCompany = productionCompanies.First(pc => pc.CompanyName == "Studio Ghibli"),
                ProductionLanguage = languages.First(l => l.LanguageName == "Japanese"),
                ProductionCountry = countries.First(c => c.CountryIsoCode == "JP"),
                MovieCasts =
                [
                    new() { Person = people.First(p => p.PersonName == "Hayao Miyazaki"), CharacterGender = "Male", Job = "Director" }
                ],
                MoviePath = "movie/movie/1/master.m3u8",
                PosterPath = "/movie/poster/8.jpg",
                BackdropPath ="/movie/backdrop/8.jpg",
            },
            new()
            {
                Title = "Spirited Away",
                ReleaseDate = new DateTime(2001, 7, 20).ToUniversalTime(),
                Budget = 19000000,
                Description = "A young girl wanders into a world of spirits and must find her way home.",
                Popularity = 9.8m,
                Runtime = 125,
                MovieStatus = "Released",
                VoteAverage = 9.3m,
                VoteCount = 18000,
                PEGI = "16",
                Genre = [genres[6], genres[11]],
                Tags = [tags[2], tags[5], tags[12]],
                ProductionCompany = productionCompanies.First(pc => pc.CompanyName == "Studio Ghibli"),
                ProductionLanguage = languages.First(l => l.LanguageName == "Japanese"),
                ProductionCountry = countries.First(c => c.CountryIsoCode == "JP"),
                MovieCasts =
                [
                    new() { Person = people.First(p => p.PersonName == "Hayao Miyazaki"), CharacterGender = "Male", Job = "Director" }
                ],
                MoviePath = "movie/movie/1/master.m3u8",
                PosterPath = "/movie/poster/9.jpg",
                BackdropPath ="/movie/backdrop/9.jpg",
            },
            new()
            {
                Title = "Spirited Away",
                ReleaseDate = new DateTime(2001, 7, 20).ToUniversalTime(),
                Budget = 19000000,
                Description = "A young girl wanders into a world of spirits and must find her way home.",
                Popularity = 9.8m,
                Runtime = 125,
                MovieStatus = "Released",
                VoteAverage = 9.3m,
                VoteCount = 18000,
                PEGI = "21",
                Genre = [genres[6], genres[11]],
                Tags = [tags[2], tags[5], tags[12]],
                ProductionCompany = productionCompanies.First(pc => pc.CompanyName == "Studio Ghibli"),
                ProductionLanguage = languages.First(l => l.LanguageName == "Japanese"),
                ProductionCountry = countries.First(c => c.CountryIsoCode == "JP"),
                MovieCasts =
                [
                    new() { Person = people.First(p => p.PersonName == "Hayao Miyazaki"), CharacterGender = "Male", Job = "Director" }
                ],
                MoviePath = "movie/movie/1/master.m3u8",
                PosterPath = "/movie/poster/10.jpg",
                BackdropPath ="/movie/backdrop/10.jpg",
            },
            new()
            {
                Title = "Spirited Away",
                ReleaseDate = new DateTime(2001, 7, 20).ToUniversalTime(),
                Budget = 19000000,
                Description = "A young girl wanders into a world of spirits and must find her way home.",
                Popularity = 9.8m,
                Runtime = 125,
                MovieStatus = "Released",
                VoteAverage = 9.3m,
                VoteCount = 18000,
                PEGI = "18",
                Genre = [genres[6], genres[11]],
                Tags = [tags[2], tags[5], tags[12]],
                ProductionCompany = productionCompanies.First(pc => pc.CompanyName == "Studio Ghibli"),
                ProductionLanguage = languages.First(l => l.LanguageName == "Japanese"),
                ProductionCountry = countries.First(c => c.CountryIsoCode == "JP"),
                MovieCasts =
                [
                    new() { Person = people.First(p => p.PersonName == "Hayao Miyazaki"), CharacterGender = "Male", Job = "Director" }
                ],
                MoviePath = "movie/movie/1/master.m3u8",
                PosterPath = "/movie/poster/11.jpg",
                BackdropPath ="/movie/backdrop/11.jpg",
            },
            new()
            {
                Title = "Spirited Away",
                ReleaseDate = new DateTime(2001, 7, 20).ToUniversalTime(),
                Budget = 19000000,
                Description = "A young girl wanders into a world of spirits and must find her way home.",
                Popularity = 9.8m,
                Runtime = 125,
                MovieStatus = "Released",
                VoteAverage = 9.3m,
                VoteCount = 18000,
                PEGI = "12",
                Genre = [genres[6], genres[11]],
                Tags = [tags[2], tags[5], tags[12]],
                ProductionCompany = productionCompanies.First(pc => pc.CompanyName == "Studio Ghibli"),
                ProductionLanguage = languages.First(l => l.LanguageName == "Japanese"),
                ProductionCountry = countries.First(c => c.CountryIsoCode == "JP"),
                MovieCasts =
                [
                    new() { Person = people.First(p => p.PersonName == "Hayao Miyazaki"), CharacterGender = "Male", Job = "Director" }
                ],
                MoviePath = "movie/movie/1/master.m3u8",
                PosterPath = "/movie/poster/12.jpg",
                BackdropPath ="/movie/backdrop/12.jpg",
            },
            new()
            {
                Title = "Spirited Away",
                ReleaseDate = new DateTime(2001, 7, 20).ToUniversalTime(),
                Budget = 19000000,
                Description = "A young girl wanders into a world of spirits and must find her way home.",
                Popularity = 9.8m,
                Runtime = 125,
                MovieStatus = "Released",
                VoteAverage = 9.3m,
                VoteCount = 18000,
                PEGI = "0",
                Genre = [genres[6], genres[11]],
                Tags = [tags[2], tags[5], tags[12]],
                ProductionCompany = productionCompanies.First(pc => pc.CompanyName == "Studio Ghibli"),
                ProductionLanguage = languages.First(l => l.LanguageName == "Japanese"),
                ProductionCountry = countries.First(c => c.CountryIsoCode == "JP"),
                MovieCasts =
                [
                    new() { Person = people.First(p => p.PersonName == "Hayao Miyazaki"), CharacterGender = "Male", Job = "Director" }
                ],
                MoviePath = "movie/movie/1/master.m3u8",
                PosterPath = "/movie/poster/11.jpg",
                BackdropPath ="/movie/backdrop/11.jpg",
            },
            new()
            {
                Title = "Spirited Away",
                ReleaseDate = new DateTime(2001, 7, 20).ToUniversalTime(),
                Budget = 19000000,
                Description = "A young girl wanders into a world of spirits and must find her way home.",
                Popularity = 9.8m,
                Runtime = 125,
                MovieStatus = "Released",
                VoteAverage = 9.3m,
                VoteCount = 18000,
                PEGI = "12",
                Genre = [genres[6], genres[11]],
                Tags = [tags[2], tags[5], tags[12]],
                ProductionCompany = productionCompanies.First(pc => pc.CompanyName == "Studio Ghibli"),
                ProductionLanguage = languages.First(l => l.LanguageName == "Japanese"),
                ProductionCountry = countries.First(c => c.CountryIsoCode == "JP"),
                MovieCasts =
                [
                    new() { Person = people.First(p => p.PersonName == "Hayao Miyazaki"), CharacterGender = "Male", Job = "Director" }
                ],
                MoviePath = "movie/movie/1/master.m3u8",
                PosterPath = "/movie/poster/7.jpg",
                BackdropPath ="/movie/backdrop/7.jpg",
            },
        };
        context.Movies.AddRange(movies);
        context.SaveChanges();
    }
}

