using System.Collections.Generic;
using EconomySim.Database.Models;

namespace EconomySim.Database.Repositories
{
    public interface IResourceRepository
    {
        IEnumerable<Resource> GetAll();
        Resource GetById(int id);
        Resource GetByName(string name);
        int Insert(Resource r);
        void Update(Resource r);
    }
}
