using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.FakeServices
{
    public class FakeBookTransactionService : IBookTransactionsService
    {
        private readonly List<BookTransaction> _transactions;
        public FakeBookTransactionService()
        {
            _transactions = new List<BookTransaction>()
            {
                new BookTransaction() { Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),BookId=new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"), DateDone=DateTime.Now},
                new BookTransaction() { Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),BookId=new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),DateDone=DateTime.Now },
                new BookTransaction() { Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"), BookId=new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),DateDone=DateTime.Now }
            };
        }

        public async Task<bool> CreateTransactionAsync(BookTransaction transaction)
        {
            return await Task.Run(() =>
            {
                try
                {
                    _transactions.Add(transaction);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        
        }

        public async Task<IReadOnlyList<BookTransaction>> GetTransactionsByBookAsync(Guid? bookId)
        {
            return await Task.Run(() =>
            {
                return _transactions.FindAll(b => b.BookId == bookId);
            });
        }
    }
}
