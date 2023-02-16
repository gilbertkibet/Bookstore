using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BookTransaction:BaseEntity
    {
        public Book Book { get; set; }
        public Guid BookId { get; set; }
        public string DoneBy { get; set; }
        public DateTime DateDone { get; set; } = DateTime.Now;
        public TransactionTypes TransactionType { get; set; }

        public string GetTransactionType()
        {
            return TransactionType.ToString();
        }
    }
}
