using System;
using System.IO;
using System.Linq;
using SQLite;
using UnityEngine;
using EconomySim.Database.Models;

namespace EconomySim.Database.Services
{    public class SQLiteDatabaseService : IDatabaseService
    {
        public SQLiteConnection Connection { get; private set; }
        private readonly string _dbPath;

        public SQLiteDatabaseService(string fileName = "economy.db")
        {
            _dbPath = Path.Combine(Application.persistentDataPath, fileName);
            Connection = new SQLiteConnection(_dbPath);
            Debug.Log($"[DB] Using SQLite at: {_dbPath}");
        }

        public void Initialize()
        {
            Connection.CreateTable<Player>();
            Connection.CreateTable<Resource>();
            Connection.CreateTable<Transaction>();

            SeedIfEmpty();
        }

        private void SeedIfEmpty()
        {
            if (!Connection.Table<Resource>().Any())
            {
                Connection.InsertAll(new[]
                {
                    new Resource { Name = "Wood",  Quantity = 100, Price = 5.0f },
                    new Resource { Name = "Stone", Quantity = 100, Price = 10.0f },
                    new Resource { Name = "Gold",  Quantity = 50,  Price = 50.0f }
                });
                Debug.Log("[DB] Seeded default resources.");
            }

            if (!Connection.Table<Player>().Any())
            {
                Connection.Insert(new Player { Name = "Player1", Balance = 500f });
                Debug.Log("[DB] Seeded default player.");
            }
        }
    }
}
