using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateOfPublication { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Count { get; set; }
        public virtual Guid AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public string GetBookStatus()
        {
            if (Count >= 10)
            {
                return "Good";
            }
            else if (Count >= 5 && Count < 10)
            {
                return "Bad";
            }
            else if (Count >= 1 && Count < 5)
            {
                return "Critical";
            }
            else
            {
                return "Out of stock";
            }
        }
      
    }
}
