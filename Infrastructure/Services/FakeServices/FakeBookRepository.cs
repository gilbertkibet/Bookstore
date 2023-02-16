using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class FakeBookRepository : IRepository<Book>
    {
        private readonly List<Book> _book;
        public FakeBookRepository()
        {
            _book = new List<Book>()
            {
                new Book() { Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),Title = "Book one", Count=2, Price = 5.00M,Description="Decription one"},
                new Book() { Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),Title = "Book two", Count=4, Price = 15.00M,Description="Decription two" },
                new Book() { Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),Title = "Book three", Count=20, Price = 25.00M,Description="Decription three" }
            };
        }

        public void Add(Book entity)
        {
            _book.Add(entity);
        }

        public async Task<int> Complete()
        {
            return await Task.Run(() =>
            {
                return 1;
            });
        }

        public async Task<int> CountAsync(ISpecification<Book> spec)
        {
            return await Task.Run(() =>
            {
                return _book.Count;
            });
        }

        public async Task<Book> GetByIdAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                return _book.FirstOrDefault(b => b.Id == id);
            });
        }

        public Task<Book> GetEntityWithSpec(ISpecification<Book> spec)
        {
            throw new NotImplementedException();

        }

        public async Task<IReadOnlyList<Book>> ListAllAsync()
        {
            return await Task.Run(() =>
            {
                return _book;
            });
        }

        public async Task<IReadOnlyList<Book>> ListAsync(ISpecification<Book> spec)
        {
            return await Task.Run(() =>
            {
                return _book;
            });
        }

        public void Update(Book entity)
        {
            var book = _book.FirstOrDefault(b => b.Id == entity.Id);

            book = entity;
        }

    }
}
