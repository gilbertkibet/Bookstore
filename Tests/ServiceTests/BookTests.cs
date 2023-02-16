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
    public class BookTests:TestBase
    {
        [Test]
        public async Task List_should_return_list_of_books()
        {
            var id1 = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
            var id2 = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb2");
           
            using var dbContext = GetDbContext();
            dbContext.Books.Add(new Book { Id = id1,Description="Lorem Ipsum"});
            dbContext.Books.Add(new Book { Id = id2,Description="Lorem Ipsum"});
            await dbContext.SaveChangesAsync();

            var service = new Repository<Book>(dbContext);
            var result = await service.ListAllAsync();

            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetBook_by_id_should_return_null_for_missing_book()
        {
            using var dbContext = GetDbContext();
            var id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb9"); //not exist

            var service = new Repository<Book>(dbContext);
            var result = await service.GetByIdAsync(id);

            Assert.Null(result);
        }

        [Test]
        public async Task GetBook_should_return_book_for_existing_book()
        {
            var id1 = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afb7");
            var id2 = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afc4");
            using var dbContext = GetDbContext();
            dbContext.Books.Add(new Book { Id = id1, Description = "Lorem Ipsum" });
            dbContext.Books.Add(new Book { Id = id2, Description = "Lorem Ipsum" });
            await dbContext.SaveChangesAsync();

            var service = new Repository<Book>(dbContext);
            var result = await service.GetByIdAsync(id1);

            Assert.NotNull(result);
            Assert.AreEqual(id1, result.Id);
        }

        [Test]
        public async Task UpdateBook_should_update_values_for_existing_book()
        {
            var id1 = new Guid("4aa85f64-5717-4562-b3fc-2c963f66afb7");
            var id2 = new Guid("4ca85f64-5717-4562-b3fc-2c963f66afc4");
            using var dbContext = GetDbContext();
            dbContext.Books.Add(new Book { Id = id1, Description = "Lorem Ipsum" });
            dbContext.Books.Add(new Book { Id = id2, Description = "Lorem Ipsum" });
            await dbContext.SaveChangesAsync();

            var service = new Repository<Book>(dbContext);
            var book = await service.GetByIdAsync(id1);
            book.Count = 2;
            service.Update(book);

            var updatedBook = await service.GetByIdAsync(id1);

            Assert.NotNull(updatedBook);
            Assert.AreEqual(2, updatedBook.Count);
        }
    }
}
