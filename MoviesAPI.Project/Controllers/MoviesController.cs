using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Project.Data;
using MoviesAPI.Project.Models;

namespace MoviesAPI.Project.Controllers
{
    
    [Route("metadata")]
    [ApiController]
    public class MoviesController: Controller
    {
        private readonly IRepository _moviesRepository;
        private List<Movie> database;

        public MoviesController(IRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
            database = moviesRepository.GetAllMovies().ToList();
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<Movie>> GetAllMovies()
        {
            var movies = _moviesRepository.GetAllMovies();
            if ( movies.Any())
                return Ok(movies);
            return NotFound();
        }

       
        [HttpGet("{MovieId}")]
        public ActionResult<IEnumerable<Movie>> GetMoviesById(int movieId)
        {
            var moviesbyId = _moviesRepository.GetMoviesByID(movieId).ToList();

            var validMovies = ValidMovies(moviesbyId);


            if (validMovies.Count==1)
                return Ok((validMovies));
            else if (validMovies.Count > 1)
                return Ok(SortOrder(validMovies));
            
            return NotFound();
        }

        private List<Movie> ValidMovies(List<Movie> moviesbyId)
        {
            var validMovlist = moviesbyId.Where(t => !string.IsNullOrEmpty(t.Title))
                .Where(d => !string.IsNullOrEmpty(d.Duration))
                .Where(l => !string.IsNullOrEmpty(l.Language))
                .Where(r => r.ReleaseYear != 0);

            return validMovlist.ToList();
        }

        [HttpPost]
        public ActionResult CreateMovie(Movie newMovie)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // Normally dealt by DBContext auto increment 

            var latest_id = _moviesRepository.GetAllMovies().Max(c => c.Id);
            newMovie.Id = latest_id + 1;
            
            database.Add(newMovie);
            
            return Created("metadata",newMovie);
            
        }

        public IEnumerable<Movie> SortOrder(IEnumerable<Movie> unOrderedMovies)
        {
            var moviesGroupedByLanguage = unOrderedMovies.
                GroupBy(x => x.Language).
                SelectMany(a =>
                    a.Where(b => b.Id ==
                                 a.Max(c => c.Id))).
                OrderBy(l => l.Language);
            return moviesGroupedByLanguage;
        }

    }
}
