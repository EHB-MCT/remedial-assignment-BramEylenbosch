using UnityEngine;
using SQLite;           // uit de package
using System.IO;       // voor Path.Combine
using System.Linq;
using System;

public class DatabaseTestPOC : MonoBehaviour
{
    private string _dbPath;

    void Start()
    {
        _dbPath = Path.Combine(Application.persistentDataPath, "economy.db");
        Debug.Log("[DB] Using: " + _dbPath);

        using (var db = new SQLiteConnection(_dbPath))
        {
            db.CreateTable<PlayerData>();

            if (!db.Table<PlayerData>().Any())
            {
                db.Insert(new PlayerData { Name = "Alice", Score = 100, CreatedAt = DateTime.UtcNow });
                db.Insert(new PlayerData { Name = "Bob",   Score = 150, CreatedAt = DateTime.UtcNow });
                Debug.Log("[DB] Seeded 2 test rows in PlayerData.");
            }

            var players = db.Table<PlayerData>().OrderByDescending(p => p.Score).ToList();
            foreach (var p in players)
                Debug.Log($"[DB] Player: {p.Id} {p.Name} score={p.Score} created={p.CreatedAt:O}");
        }

        Debug.Log("[DB] POC complete. Open via Tools > Open Persistent Data Path");
    }
}

public class PlayerData
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    [NotNull] public string Name { get; set; }
    public int Score { get; set; }
    public DateTime CreatedAt { get; set; }
}
