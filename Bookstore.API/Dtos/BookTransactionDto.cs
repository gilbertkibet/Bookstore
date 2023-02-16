using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Dtos
{
    public class BookTransactionDto
    {
        public Guid BookId { get; set; }
        public string DoneBy { get; set; }
        public DateTime DateDone { get; set; } = DateTime.Now;
        public string TransactionType { get; set; }
    }
}
