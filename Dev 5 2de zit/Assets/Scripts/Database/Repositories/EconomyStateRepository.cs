using SQLite;
using System.IO;
using UnityEngine;
using System.Linq;

public class EconomyStateRepository
{
    private readonly string _dbPath;

    public EconomyStateRepository()
    {
        _dbPath = Path.Combine(Application.persistentDataPath, "economy.db");
        using (var db = new SQLiteConnection(_dbPath))
        {
            db.CreateTable<EconomyState>();
        }
    }

    public EconomyState GetState()
    {
        using (var db = new SQLiteConnection(_dbPath))
        {
            return db.Table<EconomyState>().FirstOrDefault();
        }
    }

    public void SaveState(EconomyState state)
    {
        using (var db = new SQLiteConnection(_dbPath))
        {
            db.InsertOrReplace(state);
        }
    }
}
