using System.Collections.Generic;
using EconomySim.Database.Models;

namespace EconomySim.Database.Repositories
{
    public interface ITransactionRepository
    {
        int Insert(Transaction t);
        IEnumerable<Transaction> GetLatest(int count = 20);
        IEnumerable<Transaction> GetByResourceId(int resourceId, int count = 50);
    }
}
