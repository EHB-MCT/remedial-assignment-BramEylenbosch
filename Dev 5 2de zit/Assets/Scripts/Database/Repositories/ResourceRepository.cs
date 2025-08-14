using System.Collections.Generic;
using System.Linq;
using EconomySim.Database.Models;
using EconomySim.Database.Services;

namespace EconomySim.Database.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly IDatabaseService _db;

        public ResourceRepository(IDatabaseService db) => _db = db;

        public IEnumerable<Resource> GetAll() => _db.Connection.Table<Resource>().ToList();

        public Resource GetById(int id) => _db.Connection.Find<Resource>(id);

        public Resource GetByName(string name) =>
            _db.Connection.Table<Resource>().FirstOrDefault(r => r.Name == name);

        public int Insert(Resource r) => _db.Connection.Insert(r);

        public void Update(Resource r) => _db.Connection.Update(r);
    }
}
