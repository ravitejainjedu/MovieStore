using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models
{
    public class CreateMovieModel
    {
        [Required, StringLength(256)] public string Title { get; set; } = "";
        [Url] public string? PosterUrl { get; set; }
        public string? Overview { get; set; }
        public decimal? Price { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? RunTime { get; set; }
    }
}
