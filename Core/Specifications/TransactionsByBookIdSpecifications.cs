using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class TransactionsByBookIdSpecifications : BaseSpecifcation<BookTransaction>
    {
        public TransactionsByBookIdSpecifications(Guid? id) : base(x => x.BookId == id)
        {
        }

    }
}
