using Core.Entities;
using System;

namespace Core.Specifications
{
    public class BooksWithYearOfPublicationAndAuthor : BaseSpecifcation<Book>
    {
        public BooksWithYearOfPublicationAndAuthor(SpecsParams Params) : base(x =>
            (Params.PublicationYear == 0 || x.DateOfPublication.Year == Params.PublicationYear) &&
            (!Params.AuthorId.HasValue || x.AuthorId == Params.AuthorId)
        )
        {
            AddInclude(x => x.Author);
            AddOrderBy(x => x.Title);
            ApplyPaging(Params.PageSize * (Params.PageIndex - 1), Params.PageSize);

            if (Params.Sort != 0)
            {
                switch (Params.Sort)
                {
                    case SpecsParams.SortBy.priceAsc:
                        AddOrderBy(p => p.Price);
                        break;
                    case SpecsParams.SortBy.priceDesc:
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Title);
                        break;
                }
            }

        }

        public BooksWithYearOfPublicationAndAuthor(Guid id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Author);
        }
    }
}
