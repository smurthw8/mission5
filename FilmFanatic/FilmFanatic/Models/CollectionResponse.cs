using System;
using System.ComponentModel.DataAnnotations;
using FilmFanatic.Models;

namespace FilmFanatic.Models
{
    //model containing data stored on films in collection
    public class CollectionResponse
    {
        [Key]
        [Required]
        public int FilmId { get; set; }

        [Required(ErrorMessage = "How could you forget the title of the film?! Try again.")]
        public string Title{ get; set;}

        [Required]
        [Range(1900, 2022, ErrorMessage = "Please Enter a valid year")]
        public int Year { get; set; }

        [Required]
        public string Director { get; set; }
        [Required]
        public string Rating { get; set; }
        public bool Edited { get; set; }
        public string LentTo { get; set; }
        public string Notes { get; set; }

        //build FK relationship
        [Required(ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}

