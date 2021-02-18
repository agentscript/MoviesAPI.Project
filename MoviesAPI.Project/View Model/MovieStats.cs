namespace MoviesAPI.Project.View_Model
{
    public class MovieStats
    {
        public string title { get; set; }
        public int movieId { get; set; }
        public int  averageWatchDurationS { get; set; }
        public int  watches { get; set; }
        public int releaseYear { get; set; }
    }
}
