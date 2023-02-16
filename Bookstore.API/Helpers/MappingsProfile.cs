using AutoMapper;
using Bookstore.API.Dtos;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Book, BookDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.GetBookStatus()));
            CreateMap<BookToSaveDto, Book>();
            CreateMap<BookToEditDto, Book>();
            CreateMap<BookTransaction, BookTransactionDto>().ForMember(d => d.TransactionType, o => o.MapFrom(s => s.GetTransactionType()));

        }
    }
}
