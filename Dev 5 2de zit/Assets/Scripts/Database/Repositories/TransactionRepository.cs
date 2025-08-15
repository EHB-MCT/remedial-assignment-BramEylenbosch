using System;
using System.Collections.Generic;
using System.Linq;
using EconomySim.Database.Models;
using EconomySim.Database.Services;

namespace EconomySim.Database.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDatabaseService _db;

        public TransactionRepository(IDatabaseService db) => _db = db;

        public int Insert(Transaction t)
        {
            if (t.Timestamp == default) t.Timestamp = DateTime.UtcNow;
            return _db.Connection.Insert(t);
        }

        public IEnumerable<Transaction> GetLatest(int count = 20) =>
            _db.Connection.Table<Transaction>()
                .OrderByDescending(t => t.Timestamp)
                .Take(count)
                .ToList();

        public IEnumerable<Transaction> GetByResourceId(int resourceId, int count = 50) =>
            _db.Connection.Table<Transaction>()
                .Where(t => t.ResourceId == resourceId)
                .OrderByDescending(t => t.Timestamp)
                .Take(count)
                .ToList();
        public IEnumerable<Transaction> GetByResourceName(string resourceName)
        {
            return _db.Connection.Table<Transaction>()
                .Where(t => t.ResourceName == resourceName)
                .OrderByDescending(t => t.Timestamp)
                .ToList();
        }
    }
}
