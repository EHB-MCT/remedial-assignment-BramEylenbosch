using System.Collections.Generic;
using System.Linq;
using EconomySim.Database.Models;
using EconomySim.Database.Services;

namespace EconomySim.Database.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IDatabaseService _db;

        public PlayerRepository(IDatabaseService db) => _db = db;

        public IEnumerable<Player> GetAll() => _db.Connection.Table<Player>().ToList();

        public Player GetById(int id) => _db.Connection.Find<Player>(id);

        public Player GetByName(string name) =>
            _db.Connection.Table<Player>().FirstOrDefault(p => p.Name == name);

        public int Insert(Player p) => _db.Connection.Insert(p);

        public void Update(Player p) => _db.Connection.Update(p);
    }
}
