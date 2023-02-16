using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Dtos
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfPublication { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime AuthorDateOfBirth { get; set; }
        public string Status { get; set; }
    }
}

