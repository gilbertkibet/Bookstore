using Core.Entities;
using Infrastructure.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class AuthorTests:TestBase
    {
        [Test]
        public async Task List_should_return_list_of_authors()
        {
            var id1 = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var id2 = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb2");

            using var dbContext = GetDbContext();
            dbContext.Authors.Add(new Author { Id = id1, FirstName = "Erick" });
            dbContext.Authors.Add(new Author { Id = id2, FirstName = "Peter" });
            await dbContext.SaveChangesAsync();

            var service = new Repository<Author>(dbContext);
            var result = await service.ListAllAsync();

            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetAuthor_by_id_should_return_null_for_missing_author()
        {
            using var dbContext = GetDbContext();
            var id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb9"); //not exist

            var service = new Repository<Author>(dbContext);
            var result = await service.GetByIdAsync(id);

            Assert.Null(result);
        }

        [Test]
        public async Task GetAuthor_should_return_author_for_existing_book()
        {
            var id1 = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb7");
            var id2 = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afc4");
            using var dbContext = GetDbContext();
            dbContext.Authors.Add(new Author { Id = id1, FirstName = "James" });
            dbContext.Authors.Add(new Author { Id = id2, FirstName = "John" });
            await dbContext.SaveChangesAsync();

            var service = new Repository<Author>(dbContext);
            var result = await service.GetByIdAsync(id1);

            Assert.NotNull(result);
            Assert.AreEqual(id1, result.Id);
        }

        [Test]
        public async Task UpdateAuthor_should_update_values_for_existing_book()
        {
            var id1 = new Guid("4aa85f64-5717-4562-b3fc-2c963f66afb7");
            var id2 = new Guid("4ca85f64-5717-4562-b3fc-2c963f66afc4");
            using var dbContext = GetDbContext();
            dbContext.Authors.Add(new Author { Id = id1, FirstName = "Sarah" });
            dbContext.Authors.Add(new Author { Id = id2, FirstName = "Doe" });
            await dbContext.SaveChangesAsync();

            var service = new Repository<Author>(dbContext);
            var author = await service.GetByIdAsync(id1);
            author.Email = "sarah@test.com";
            service.Update(author);

            var updatedAuthor = await service.GetByIdAsync(id1);

            Assert.NotNull(updatedAuthor);
            Assert.AreEqual("sarah@test.com", updatedAuthor.Email);
        }
    }
}
