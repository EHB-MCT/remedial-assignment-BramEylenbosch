using UnityEngine;
using SQLite;
using System;
using System.IO;

public class EconomyService : MonoBehaviour
{
    private string _dbPath;

    [Header("Economy Settings")]
    public int resourceAmount = 100;
    public int resourcePerTick = 5;

    void Start()
    {
        _dbPath = Path.Combine(Application.persistentDataPath, "economy.db");

        // Zorg dat tabel bestaat
        using (var db = new SQLiteConnection(_dbPath))
        {
            db.CreateTable<ResourceData>();

            var existing = db.Table<ResourceData>().FirstOrDefault();
            if (existing == null)
            {
                db.Insert(new ResourceData
                {
                    Amount = resourceAmount,
                    UpdatedAt = DateTime.UtcNow
                });
                Debug.Log("[Economy] New resource entry created in DB.");
            }
            else
            {
                resourceAmount = existing.Amount;
                Debug.Log($"[Economy] Loaded existing amount: {resourceAmount}");
            }
        }

        SimulationManager.Instance.OnTick += HandleTick;
    }

    private void HandleTick()
    {
        resourceAmount += resourcePerTick;
        SaveToDatabase();
        Debug.Log($"[Economy] Tick → resources = {resourceAmount}");
    }

    private void SaveToDatabase()
    {
        using (var db = new SQLiteConnection(_dbPath))
        {
            var resource = db.Table<ResourceData>().FirstOrDefault();
            if (resource != null)
            {
                resource.Amount = resourceAmount;
                resource.UpdatedAt = DateTime.UtcNow;
                db.Update(resource);
            }
        }
    }
}

[Table("Resources")]
public class ResourceData
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int Amount { get; set; }
    public DateTime UpdatedAt { get; set; }
}
