using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBookTransactionsService
    {
        Task<bool> CreateTransactionAsync(BookTransaction transaction);
        Task<IReadOnlyList<BookTransaction>> GetTransactionsByBookAsync(Guid? bookId);
    }
}
