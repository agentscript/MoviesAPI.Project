
using System.Collections.Generic;
using MoviesAPI.Project.Models;

namespace MoviesAPI.Project.Data
{
   public interface IRepository
    {
        IEnumerable<Movie> GetAllMovies();
        IEnumerable<Movie> GetMoviesByID(int movieId);
        IEnumerable<Stat> GetAllMoviesStats();
    }
}
