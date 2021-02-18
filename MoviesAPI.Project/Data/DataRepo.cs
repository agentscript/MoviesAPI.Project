using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using MoviesAPI.Project.Models;

namespace MoviesAPI.Project.Data
{
    public class DataRepo:IRepository
    {
        private IEnumerable<Movie> _movies;
        private readonly IEnumerable<Stat> _stats;

        public DataRepo()
        {
            using (var reader = new StreamReader(@"C:\Users\chint\source\repos\MoviesAPI.Project\MoviesAPI.Project\Data\CSV\metadata.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                _movies = csv.GetRecords<Movie>().ToList();
            }

            using (var reader = new StreamReader(@"C:\Users\chint\source\repos\MoviesAPI.Project\MoviesAPI.Project\Data\CSV\stats.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                _stats = csv.GetRecords<Stat>().ToList();
            }

        }


        public IEnumerable<Movie> GetAllMovies()
        {
            return _movies;
        }

        public IEnumerable<Movie> GetMoviesByID(int movieId)
        {
            var movie = _movies
                .Where(x => x.MovieId == movieId).ToList();

            return movie;
        }

        public IEnumerable<Stat> GetAllMoviesStats()
        {
            return _stats;
        }
    }
}
