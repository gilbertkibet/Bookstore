using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BookStoreSeeder
    {
        public static async Task SeedAsync(BookstoreDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                if (!context.Authors.Any())
                {
                    var authorData = File.ReadAllText(path + @"/Data/SeedData/authors.json");

                    var authors = JsonSerializer.Deserialize<List<Author>>(authorData);

                    foreach (var author in authors)
                    {
                        context.Authors.Add(author);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Books.Any())
                {
                    var booksData = File.ReadAllText(path + @"/Data/SeedData/books.json");

                    var books = JsonSerializer.Deserialize<List<Book>>(booksData);

                    foreach (var book in books)
                    {
                        context.Books.Add(book);
                    }

                    await context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<BookstoreDbContext>();

                logger.LogError(ex.Message);
            }
        }
    }
}
