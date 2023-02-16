using AutoMapper;
using Bookstore.API.Dtos;
using Bookstore.API.Helpers;
using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BooksController : ControllerBase
    {

        private readonly IRepository<Book> _bookRepo;

        private readonly IMapper _mapper;
        private readonly IBookTransactionsService _service;

        public BooksController(IRepository<Book> bookRepo, IMapper mapper, IBookTransactionsService service)
        {
            _bookRepo = bookRepo;

            _mapper = mapper;

            _service = service;
        }

        // GET: api/<BooksController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetBooks([FromQuery] SpecsParams Params)
        {
            if (Params.PageSize < 1)
            {
                return BadRequest("Page size can't be less than 1");
            }

            var spec = new BooksWithYearOfPublicationAndAuthor(Params);

            var countSpec = new BooksFiltersForCountSpecification(Params);

            var totalItems = await _bookRepo.CountAsync(countSpec);

            var books = await _bookRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<BookDto>>(books);

            return Ok(new Pagination<BookDto>(Params.PageIndex, Params.PageSize, totalItems, data));
        }

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetBook(Guid id)
        {
            var book = await _bookRepo.GetByIdAsync(id);

            var bookDto = new BookDto();

            var returnValue = _mapper.Map(book, bookDto);

            if (returnValue == null)
            {
                return NotFound(returnValue);
            }

            return Ok(returnValue);
        }

        // POST api/<BooksController>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Post([FromBody] BookToSaveDto value)
        {
            if (User==null)
            {
                return Unauthorized();
            }
            var email = User.FindFirstValue(ClaimTypes.Email);

            var trans = new BookTransaction()
            {
                TransactionType = TransactionTypes.StockAddition,
                DoneBy = email,
            };


            var newBook = new Book();

            var book = _mapper.Map(value, newBook);

            book.CreatedBy = email;

            book.CreatedDate = DateTime.Now;

            if (book == null)
            {
                return BadRequest("Problem creating the book");
            }
            try
            {
                _bookRepo.Add(book);

                await _bookRepo.Complete();

                trans.BookId = book.Id;

                await _service.CreateTransactionAsync(trans);
            }
            catch (Exception)
            {
                return new BadRequestObjectResult("Problem creating the book");
            }

            return Ok(book);
        }

        // PUT api/<BooksController>/5
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Put([FromBody] BookToEditDto value)
        {

            var newBook = new Book();

            var email = User.FindFirstValue(ClaimTypes.Email);

            var book = _mapper.Map(value, newBook);

            if (book == null)
            {
                return BadRequest("Problem updating the book");
            }

            var trans = new BookTransaction()
            {
                BookId = value.Id,
                TransactionType = TransactionTypes.StockReduction,
                DoneBy = email,
            };

            try
            {
                _bookRepo.Update(book);

                await _bookRepo.Complete();

                await _service.CreateTransactionAsync(trans);

            }
            catch (Exception)
            {
                return BadRequest("Problem updating the book");
            }

            return Ok(book);
        }

        // POST api/<BooksController>/5
        // Pass Positive or Negative value depending on whether you want to add or reduce the stock
        [HttpPost("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<BookDto>> AddExistingBookStock(Guid id, int StockToAddOrRemove)
        {
            var book = await _bookRepo.GetByIdAsync(id);

            if (User==null)
            {
                return Unauthorized();
            }

            var email = User.FindFirstValue(ClaimTypes.Email);

            if (book == null)
            {
                return NotFound($"Book not found with id {id}");
            }

            var trans = new BookTransaction()
            {
                BookId = book.Id,
                TransactionType = StockToAddOrRemove > 0 ? TransactionTypes.StockAddition : TransactionTypes.StockReduction,
                DoneBy = email,
            };

            if ((book.Count + StockToAddOrRemove)<0)
            {
                return new BadRequestObjectResult("Stock can not go below 0");
            }

            book.Count += StockToAddOrRemove;

            book.ModifiedBy = email;

            book.ModifiedDate = DateTime.Now;

            try
            {
               
                _bookRepo.Update(book);

                await _bookRepo.Complete();

                await _service.CreateTransactionAsync(trans);

            }
            catch (Exception)
            {
                return new BadRequestObjectResult("Problem updating the book");

            }

            var returnValue = _mapper.Map(book, new BookDto());

            return Ok(returnValue);
        }

        // GET api/<BooksController>/transactions/5
        [HttpGet("Transactions/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetTransactionsPerBook(Guid id)
        {
            var bookTrans = await _service.GetTransactionsByBookAsync(id);

            if (bookTrans==null) return NotFound();
            
            var trans = _mapper.Map(bookTrans, new List<BookTransactionDto>());

            return Ok(trans);
        }
    }
}
