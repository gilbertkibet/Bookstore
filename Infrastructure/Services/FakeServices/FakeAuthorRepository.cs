using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.FakeServices
{
    
    public class FakeAuthorRepository : IRepository<Author>
    {
        private readonly List<Author> _authors;
        public FakeAuthorRepository()
        {
            _authors = new List<Author>()
            {
                new Author() { Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"), LastName = "Author one"},
                new Author() { Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),LastName = "Author two" },
                new Author() { Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),LastName = "Author three" }
            };
        }

        public void Add(Author entity)
        {
            _authors.Add(entity);
        }

        public async Task<int> Complete()
        {
            return await Task.Run(() =>
            {
                return 1;
            });
        }

        public async Task<int> CountAsync(ISpecification<Author> spec)
        {
            return await Task.Run(() =>
            {
                return _authors.Count;
            });
        }

        public async Task<Author> GetByIdAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                return _authors.FirstOrDefault(b => b.Id == id);
            });
        }

        public Task<Author> GetEntityWithSpec(ISpecification<Author> spec)
        {
            throw new NotImplementedException();

        }

        public async Task<IReadOnlyList<Author>> ListAllAsync()
        {
            return await Task.Run(() =>
            {
                return _authors;
            });
        }

        public async Task<IReadOnlyList<Author>> ListAsync(ISpecification<Author> spec)
        {
            return await Task.Run(() =>
            {
                return _authors;
            });
        }

        public void Update(Author entity)
        {
            var book = _authors.FirstOrDefault(b => b.Id == entity.Id);

            book = entity;
        }

    }
}
