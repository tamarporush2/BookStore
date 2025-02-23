using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore
{
    public class Book
    {
        [Required]
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public List<string> Author { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }


    }
}
