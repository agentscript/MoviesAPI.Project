

using System.Text.Json.Serialization;


namespace MoviesAPI.Project.Models
{
 
    public class Movie
    {
      
        [JsonIgnore]
        public int Id { get; set; }

        public int MovieId { get; set; }

        public string Title { get; set; }

        public string  Language { get; set; }

        public string Duration { get; set; }

        public int ReleaseYear { get; set; }

    }
}
