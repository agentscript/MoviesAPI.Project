using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
