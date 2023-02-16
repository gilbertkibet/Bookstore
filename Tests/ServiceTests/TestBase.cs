using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Tests
{
    public abstract class TestBase
    {
        protected BookstoreDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<BookstoreDbContext>()
                                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                  .Options;
            return new BookstoreDbContext(options);
        }
    }
}
