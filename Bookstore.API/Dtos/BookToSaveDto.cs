using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Dtos
{
    public class BookToSaveDto
    {
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime DateOfPublication { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        public int Count { get; set; }
    }
}
