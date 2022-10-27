namespace AspNetCoreWebApi6.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string Day { get; set; } = string.Empty;
        public string Room { get; set; } = string.Empty;
        public List<Movie>? Movies { get; set; }
    }
}
