using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace MoviesAPI.Project.Models
{
    public class Stat
    {
        public int movieId { get; set; }
        public int watchDurationMs { get; set; }
    }
}
