namespace AspNetCoreWebApi6.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string Day { get; set; }
        public string Room { get; set; }
        public List<Movie>? Movies { get; set; }
    }
}
