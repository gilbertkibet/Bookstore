using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class TransactionsWithBooksSpecifications : BaseSpecifcation<BookTransaction>
    {
        public TransactionsWithBooksSpecifications(Guid id) : base(x => x.BookId == id)
        {
            AddInclude(x => x.Book);
        }
    }
}
