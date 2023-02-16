using AutoMapper;
using Bookstore.API.Controllers;
using Bookstore.API.Dtos;
using Bookstore.API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Services;
using Infrastructure.Services.FakeServices;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ControllerTests
{
    public class BooksControllerTests
    {
        private static IMapper _mapper;
        private static IRepository<Book> _repo;
        private static IBookTransactionsService _transService;
        private static BooksController _controller;

        [SetUp]
        public void Setup()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfiles());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;

            }

            _repo = new FakeBookRepository();
            _transService = new FakeBookTransactionService();
            _controller = new BooksController(_repo, _mapper, _transService);
        }


        [Test]
        public async Task Find_book_by_id_should_return_404_Not_Found_if_book_not_exist()
        {
            // Arrange
            var id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c209");
            // Act
            var notResult = await _controller.GetBook(id);

            var result = notResult as NotFoundObjectResult;

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public async Task Find_book_by_id_should_return_200_Ok_status_if__bookexists()
        {
            // Arrange
            var id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            // Act
            var okResult = await _controller.GetBook(id);
            var result = okResult as OkObjectResult;

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task Add_stock_updates_book_status_when_authenticated()
        {
            var id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

            var initialres = await _controller.GetBook(id);

            var initialBook = (OkObjectResult)initialres;

            var b = initialBook.Value as BookDto;

            Assert.IsNotNull(b);
            Assert.AreEqual("Critical", b.Status);

            _controller.WithIdentity("super admin", "superadmin@bookstore.com");

            var bookRes = await _controller.AddExistingBookStock(id, 20);

            var res = (OkObjectResult)bookRes.Result;

            var book = res.Value as BookDto;

            Assert.IsNotNull(book);
            Assert.AreEqual("Good", book.Status);

        }

        [Test]
        public async Task Reduce_stock_updates_book_status_when_authenticated()
        {
            var id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad");

            var initialres = await _controller.GetBook(id);

            var initialBook = (OkObjectResult)initialres;

            var b = initialBook.Value as BookDto;

            Assert.IsNotNull(b);
            Assert.AreEqual("Good", b.Status);

            _controller.WithIdentity("super admin", "superadmin@bookstore.com");

            var bookRes = await _controller.AddExistingBookStock(id, -20);

            var res = (OkObjectResult)bookRes.Result;

            var book = res.Value as BookDto;

            Assert.IsNotNull(book);
            Assert.AreEqual("Out of Stock".ToLower(), book.Status.ToLower());

        }

        [Test]
        public async Task Reduce_stock_returns_401_not_authorized_when_not_authenticated()
        {
            var id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad");

            var bookRes = await _controller.AddExistingBookStock(id, -20);

            var res = (UnauthorizedResult)bookRes.Result;

            Assert.IsNotNull(res);

            Assert.AreEqual(401, res.StatusCode);

        }

        [Test]
        public async Task Get_book_returns_the_books()
        {
            var spec = new SpecsParams();

            var bookRes = await _controller.GetBooks(spec);

            var initialBook = (OkObjectResult)bookRes;

            var bks = initialBook.Value as Pagination<BookDto>;

            Assert.IsNotNull(bks);

            Assert.AreEqual(3, bks.Count);

        }

        [Test]
        public async Task Get_transactions_per_book_returns_transanctions()
        {
            var id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad");

            _controller.WithIdentity("super admin", "superadmin@bookstore.com");

            var bookRes = await _controller.AddExistingBookStock(id, -20);

            var bookTrans = await _controller.GetTransactionsPerBook(id);

            var bkstra = (OkObjectResult)bookTrans;

            var bks = bkstra.Value as List<BookTransactionDto>;

            Assert.IsNotNull(bks);

            Assert.AreEqual(2, bks.Count);

        }

        [Test]
        public async Task Add_book_returns_200_Ok()
        {
            var id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18cb");

            _controller.WithIdentity("super admin", "superadmin@bookstore.com");

            var bk = new BookToSaveDto()
            {
                Count = 2,
                Title = "Lorem ipsum",
                Price = 2000
            };

            var svRes = await _controller.Post(bk);

            var bkOkRes = (OkObjectResult)svRes;

            Assert.IsNotNull(bkOkRes);

            Assert.AreEqual(200, bkOkRes.StatusCode);

            var bkFin = bkOkRes.Value as Book;

            Assert.IsNotNull(bkFin);

            Assert.AreEqual(bk.Price, bkFin.Price);

        }

        [Test]
        public async Task Add_book_returns_created_book()
        {
            var id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18cb");

            _controller.WithIdentity("super admin", "superadmin@bookstore.com");

            var bk = new BookToSaveDto()
            {
                Count = 2,
                Title = "Lorem ipsum",
                Price = 2000
            };

            var svRes = await _controller.Post(bk);

            var bkOkRes = (OkObjectResult)svRes;

            var bkFin = bkOkRes.Value as Book;

            Assert.IsNotNull(bkFin);

            Assert.AreEqual(bk.Price, bkFin.Price);

        }

        [Test]
        public async Task Add_book_returns_401_when_not_authorized()
        {
            var id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18cb");

            var bk = new BookToSaveDto()
            {
                Count = 2,
                Title = "Lorem ipsum",
                Price = 2000
            };

            var svRes = await _controller.Post(bk);

            var bkOkRes = (UnauthorizedResult)svRes;

            Assert.IsNotNull(bkOkRes);

            Assert.AreEqual(401, bkOkRes.StatusCode);

        }

        [Test]
        public async Task Update_book_returns_200_ok_when_authorized()
        {
            var title = "Changed title";

            var bk = new BookToEditDto()
            {
                Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                Title = title,
                Price = 2000
            };

            _controller.WithIdentity("super admin", "superadmin@bookstore.com");

            var svRes = await _controller.Put(bk);

            var bkOkRes = (OkObjectResult)svRes;

            Assert.IsNotNull(bkOkRes);

            Assert.AreEqual(200, bkOkRes.StatusCode);

        }

        [Test]
        public async Task Update_book_updates_the_ok_when_authorized()
        {
            var title = "Changed title";

            var bk = new BookToEditDto()
            {
                Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                Title = title,
                Price = 2000
            };

            _controller.WithIdentity("super admin", "superadmin@bookstore.com");

            var svRes = await _controller.Put(bk);

            var bkOkRes = (OkObjectResult)svRes;

            var bkfin = bkOkRes.Value as Book;

            Assert.IsNotNull(bkfin);

            Assert.AreEqual(title, bkfin.Title);

        }


        [Test]
        public async Task Update_book_returns_401_when_not_authorized()
        {
            var id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18cb");

            var bk = new BookToSaveDto()
            {
                Count = 2,
                Title = "Lorem ipsum",
                Price = 2000
            };

            var svRes = await _controller.Post(bk);

            var bkOkRes = (UnauthorizedResult)svRes;

            Assert.IsNotNull(bkOkRes);

            Assert.AreEqual(401, bkOkRes.StatusCode);

        }
    }
}
