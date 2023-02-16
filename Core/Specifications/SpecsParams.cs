using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class SpecsParams
    {
        private const int MaxPageSize = 20;
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 5;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public Guid? AuthorId { get; set; }
        public SortBy Sort { get; set; }
        private int _yearOfPublication;
        public int PublicationYear
        {
            get => _yearOfPublication;
            set => _yearOfPublication = value;
        }

        public enum SortBy
        {
            priceAsc = 1,
            priceDesc
        }

    }
}
