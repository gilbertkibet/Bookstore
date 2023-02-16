using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class BooksFiltersForCountSpecification : BaseSpecifcation<Book>
    {
        public BooksFiltersForCountSpecification(SpecsParams Params) : base(x =>
           (Params.PublicationYear == 0 || x.DateOfPublication.Year == Params.PublicationYear) &&
           (!Params.AuthorId.HasValue || x.AuthorId == Params.AuthorId))
        {

        }

    }
}
