using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Project.Data;
using MoviesAPI.Project.Models;
using MoviesAPI.Project.View_Model;

namespace MoviesAPI.Project.Controllers
{
    [Route("movies/stats")]
    [ApiController]
    public class StatsController:Controller
    {
        private readonly IRepository _statRepo;

        public StatsController(IRepository statsRepository)
        {
            _statRepo = statsRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieStats>> GetAllStats()
        {
            var stats = _statRepo.GetAllMoviesStats().ToList();

            var movieStats = SortOrder(stats);

            if(movieStats.Any())
                return Ok(movieStats);

            return NoContent();
        }

        public IEnumerable<MovieStats> SortOrder(IEnumerable<Stat> stats )
        {
            var movieStats = stats.GroupBy(a => a.movieId)
                .Select(b =>
                    new MovieStats()
                    {
                        title = _statRepo.GetMoviesByID(b.Key)
                            .Select(c => c.Title)
                            .DefaultIfEmpty("")
                            .First(),

                        releaseYear = (int) _statRepo.GetMoviesByID(b.Key)
                            .Select(c => c.ReleaseYear)
                            .DefaultIfEmpty(0)
                            .First(),
                        movieId = b.Key,
                        watches = b.Count(),
                        averageWatchDurationS = (int)b.Average(c => c.watchDurationMs)
                    }
                ).OrderByDescending(x => x.watches)
                .ThenByDescending(x => x.releaseYear);
            
            return movieStats;
        }

    }
}
