using SQLite;
using System;

public class EconomyState
{
    [PrimaryKey] public int Id { get; set; } = 1; // Altijd 1 record
    public int Resources { get; set; }
    public DateTime LastUpdated { get; set; }
}
