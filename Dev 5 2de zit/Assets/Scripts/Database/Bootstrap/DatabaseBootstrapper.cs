using UnityEngine;
using EconomySim.Database.Services;
using EconomySim.Database.Repositories;

namespace EconomySim.Database.Bootstrap
{
    public static class DatabaseBootstrapper
    {
        public static IDatabaseService DB { get; private set; }
        public static IResourceRepository Resources { get; private set; }
        public static IPlayerRepository Players { get; private set; }
        public static ITransactionRepository Transactions { get; private set; }

        private static bool _isInitialized = false;

        public static void Initialize()
        {
            if (_isInitialized) return;

            DB = new SQLiteDatabaseService("economy.db");
            DB.Initialize();

            Resources = new ResourceRepository(DB);
            Players = new PlayerRepository(DB);
            Transactions = new TransactionRepository(DB);

            Debug.Log("[DB] Bootstrap complete.");
            _isInitialized = true;
        }
    }
}
