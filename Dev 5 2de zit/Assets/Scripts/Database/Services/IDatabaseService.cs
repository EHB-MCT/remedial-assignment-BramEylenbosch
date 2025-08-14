using SQLite;

namespace EconomySim.Database.Services
{
    public interface IDatabaseService
    {
        SQLiteConnection Connection { get; }
        void Initialize();
    }
}
