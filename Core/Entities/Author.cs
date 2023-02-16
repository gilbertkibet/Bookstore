using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Book> Books { get; set; }
    }
}
