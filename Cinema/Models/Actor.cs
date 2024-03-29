﻿using CinemaApp.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace CinemaApp.Models
{
    public class Actor : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Picture")]

        public string? ProfilePictureUrl { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage ="Full Name is required")]
        public string FullName { get; set; }
        [Display(Name = "Biography")]

        public string? Bio { get; set; }
        public int? Age { get; set; }
        public List<Movie>? Movies { get; set; }
    }
}
