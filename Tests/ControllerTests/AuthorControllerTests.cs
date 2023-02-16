using AutoMapper;
using Bookstore.API.Controllers;
using Bookstore.API.Helpers;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.FakeServices;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.ControllerTests
{
    public class AuthorControllerTests
    {
        private static IMapper _mapper;
        private static IRepository<Author> _repo;
        private static AuthorsController _controller;

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

            _repo = new FakeAuthorRepository();

            _controller = new AuthorsController(_repo, _mapper);
        }


        [Test]
        public async Task Find_Author_by_id_should_return_404_Not_Found_if_author_not_exist()
        {
            // Arrange
            var id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c209");
            // Act
            var notResult = await _controller.GetAuthor(id);

            var result = notResult as NotFoundObjectResult;

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(404, result.StatusCode);
        }

        [Test]
        public async Task Find_author_by_id_should_return_200_Ok_status_if__author_exists()
        {
            // Arrange
            var id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");
            // Act
            var okResult = await _controller.GetAuthor(id);

            var result = okResult as OkObjectResult;

            // assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }


        [Test]
        public async Task Get_authors_returns_the_authors()
        {
            
            var authorRes = await _controller.GetAuthors();

            var initialauthor = (OkObjectResult)authorRes;

            var ats = initialauthor.Value as List<Author>;

            Assert.IsNotNull(ats);

            Assert.AreEqual(3, ats.Count);

        }

        
    }
}
