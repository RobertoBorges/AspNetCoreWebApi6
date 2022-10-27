﻿namespace AspNetCoreWebApi6.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        
        public string Genre { get; set; } = string.Empty;

        public DateTime ReleaseDate { get; set; }

        public int SessionId { get; set; }
    }
}
