using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BookTransactionsService : IBookTransactionsService
    {
        private readonly IRepository<BookTransaction> _repository;

        public BookTransactionsService(IRepository<BookTransaction> repository)
        {
            _repository = repository;
        }
        public async Task<bool> CreateTransactionAsync(BookTransaction transaction)
        {
            if (transaction == null)
            {
                throw new NullReferenceException("Transaction can not be null");
            }

            _repository.Add(transaction);

            return await _repository.Complete() == 0 ? false : true;
        }

        public async Task<IReadOnlyList<BookTransaction>> GetTransactionsByBookAsync(Guid? bookId)
        {
            if (bookId == null) return null;
            
            var spec = new TransactionsByBookIdSpecifications(bookId);

            return await _repository.ListAsync(spec);
        }
    }
}
