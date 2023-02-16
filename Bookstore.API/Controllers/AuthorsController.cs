using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IRepository<Author> _authorRepo;

        private readonly IMapper _mapper;

        public AuthorsController(IRepository<Author> authorRepo, IMapper mapper)
        {
            _authorRepo = authorRepo;

            _mapper = mapper;
        }

        // GET: api/<AuthorsController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAuthors()
        {
            var authors = await _authorRepo.ListAllAsync();

            return Ok(authors);
        }

        // GET api/<AuthorsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAuthor(Guid id)
        {
            var author = await _authorRepo.GetByIdAsync(id);

            if (author==null)
            {
                return NotFound("No author with that id was found");
            }

            return Ok(author);
        }

    }
}
