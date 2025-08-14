using System.Collections.Generic;
using EconomySim.Database.Models;

namespace EconomySim.Database.Repositories
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetAll();
        Player GetById(int id);
        Player GetByName(string name);
        int Insert(Player p);
        void Update(Player p);
    }
}
