#  Movies2.0 — Technical Documentation

## Overview
**Movies2.0** is a cross-platform application that allows users to browse, stream, and manage movies and TV shows. The app supports personalized watchlists, and includes adaptive streaming technology for seamless playback.

## Architecture

### System Diagram

![Architecture Diagram](https://raw.githubusercontent.com/X-dante-X/Movies2.0/main/architecture.png)


### Components
- **Frontend (Mobile/Web):**
  - Built with React Web.
  - Connects to backend via REST API.

- **Backend API:**
  - C# ASP.NET API.
  - Authentication, movie metadata, playback session tracking.

- **Database:**
  - PostgreSQL for structured data.

---

## Tech Stack

| Layer | Technology |
|-------|------------|
| Frontend | React, TypeScript |
| Backend | C# ASP.Net API |
| Database | PostgreSQL  |
| Auth | JWT, OAuth 2.0 |
| CI/CD | GitHub Actions, Docker |


## API Structure

### Base URL

http://localhost/

### Endpoints

#### **GET /movies**
Returns paginated list of movies.
**Response**
```json
{
   [
    {
      "id": 301,
      "title": "Inception",
      "poster": "/img/inception.jpg",
      "rating": 5.8
    },
    {
        "id": 302,
        "title": "Matrix",
        "poster": "/img/matrix.jpg",
        "rating": 7.8
    }
  ]
}
```
#### **POST /auth/login**
Authenticates user using email and password.
**BODY**
```json
{
    "email": "test@example.com",
    "password": "examplepassword"
}
```
**RESPONSE**
```json
{
{
  "accessToken": "jwt-token",
  "isAdmin": "true",
  "expiration": "date",
  "refreshToken": "refresh-token"
}
}
```
### DATA MODELS
# MovieDTO Model

## Namespace
`Models.DTO`

## Description
`MovieDTO` represents the data structure used for creating or updating movie records, including metadata, media file uploads, and relational references such as tags, genres, and cast information.

---

## Properties

| Name | Type | Is required | Description | 
|-------|------------|------------|------------|
| **Title** | `string` | No | The title of the movie. |
| **ReleaseDate** | `DateTime?` | Yes | The official release date of the movie. |
| **Budget** | `int?` | Yes | The movie's production budget in USD. |
| **Description** | `string` | No | A detailed summary of the movie. |
| **Popularity** | `decimal?` | Yes | Popularity score from external providers. |
| **Runtime** | `int?` | Yes | Duration of the movie (in minutes). |
| **MovieStatus** | `string` | No | Status such as *Released*, *In Production*, etc. |
| **VoteAverage** | `decimal?` | Yes | Average rating of the movie. |
| **VoteCount** | `int?` | Yes | Number of votes used to calculate `VoteAverage`. |
| **PEGI** | `string` | No | Age rating according to PEGI standards. |
| **Movie** | `IFile` | No | Video file for the movie. |
| **Poster** | `IFile` | No | Poster image file. |
| **Backdrop** | `IFile` | No | Backdrop image file. |
| **Tags** | `List<int>` | No | List of tag IDs associated with the movie. |
| **Genre** | `List<int>` | No | List of genre IDs. |
| **ProductionCompanyId** | `int?` | Yes | ID of the production company. |
| **LanguageId** | `int` | No | ID representing the movie's language. |
| **CountryId** | `int` | No | ID representing the country of origin. |
| **movieCasts** | `List<MovieCastDTO>?` | Yes | Collection of cast members appearing in the movie. |

---

## Model definition

```csharp
namespace Models.DTO;

public class MovieDTO
{
    public string Title { get; set; } = null!;
    public DateTime? ReleaseDate { get; set; }
    public int? Budget { get; set; }
    public string Description { get; set; } = null!;
    public decimal? Popularity { get; set; }
    public int? Runtime { get; set; }
    public string MovieStatus { get; set; } = null!;
    public decimal? VoteAverage { get; set; }
    public int? VoteCount { get; set; }
    public string PEGI { get; set; } = null!;
    public IFile Movie { get; set; } = null!;
    public IFile Poster { get; set; } = null!;
    public IFile Backdrop { get; set; } = null!;
    public List<int> Tags { get; set; } = [];
    public List<int> Genre { get; set; } = [];
    public int? ProductionCompanyId { get; set; }
    public int LanguageId { get; set; }
    public int CountryId { get; set; }
    public List<MovieCastDTO>? movieCasts { get; set; }
}
```

# User Model

## Namespace
`AuthService.Models`

## Description
The `User` model represents an application user with authentication details, profile information, OAuth identity, authorization status, and security tokens.

---

## Properties

| Name | Type | Is required | Description | 
|-------|------------|------------|------------|
| **Id** | `int` | No | Unique identifier for the user. |
| **Username** | `string` | No | Display or login username. |
| **Email** | `string` | No | User's email address. Must be unique. |
| **PasswordHash** | `byte[]?` | Yes | Hashed password for local authentication. |
| **PasswordSalt** | `byte[]?` | Yes | Salt used for password hashing. |
| **GoogleId** | `string?` | Yes | Google account ID for OAuth login. |
| **FirstName** | `string?` | Yes | User's first name. |
| **LastName** | `string?` | Yes | User's last name. |
| **IsGoogleUser** | `bool` | No | Whether the user registered via Google OAuth. |
| **CreatedAt** | `DateTime` | No | Date and time the user was created. Defaults to UTC now. |
| **LastLoginAt** | `DateTime?` | Yes | Timestamp of the user's last successful login. |
| **UserStatus** | `int` | No | Numeric status: `0 = User`, `1 = Admin`. |
| **IsAdmin** | `bool` (computed) | No | True if `UserStatus == 1`. |
| **RefreshToken** | `string` | No | Current refresh token for user session renewal. |
| **RefreshTokenExpiry** | `DateTime` | No | Expiration date of the refresh token. |
| **HasPassword** | `bool` (computed) | Yes | True if both `PasswordHash` and `PasswordSalt` are set. |
| **EmailToken** | `string?` | Yes | Token used for email verification. |
| **IsVerified** | `bool` | No | True if email is successfully verified. |

---

## Computed Properties

### **IsAdmin**
```csharp
namespace AuthService.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;

    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }

    public string? GoogleId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool IsGoogleUser { get; set; } = false;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }

    public int UserStatus { get; set; } = 0;
    public bool IsAdmin => UserStatus == 1;
    public string RefreshToken { get; set; } = null!;
    public DateTime RefreshTokenExpiry { get; set; } = DateTime.UtcNow;

    public bool HasPassword => PasswordHash != null && PasswordSalt != null;
    public string? EmailToken { get; set; } = null!;
    public bool IsVerified { get; set; } = false;
}
```
# UserFavoriteMovie Model

## Namespace
`UserService.Models`

## Description
`UserFavoriteMovie` represents a movie saved by a user in their personal list.  
It includes metadata such as the movie title, poster path, a description, whether it is marked as a favorite, and the user’s watch status.

---

## Properties

| Name | Type | Is required | Description | 
|-------|------------|------------|------------|
| **Id** | `int` | No | Unique identifier for the record. |
| **Description** | `string` | No | Summary or overview of the movie. |
| **PosterPath** | `string` | No | URL or file path to the movie poster. |
| **Title** | `string` | No | Title of the movie. |
| **IsFavorite** | `bool` | No | Indicates if the user has marked this movie as a favorite. |
| **Status** | `WatchStatus?` | Yes | Watch status enum (e.g., *Planned*, *Watching*, *Completed*). |

---

## Notes

### WatchStatus Enum
The `Status` property uses the `WatchStatus` enum located in:


- `Planned`
- `Watching`
- `Completed`
- `Dropped`
- `OnHold`


## C# Model Definition

```csharp
using UserService.Models.Enums;

namespace UserService.Models;

public class UserFavoriteMovie
{
    public int Id { get; set; }  
    public string Description { get; set; } = null!;
    public string PosterPath { get; set; } = null!;
    public string Title { get; set; } = null!;
    public bool IsFavorite { get; set; }
    public WatchStatus? Status { get; set; }
}
```
