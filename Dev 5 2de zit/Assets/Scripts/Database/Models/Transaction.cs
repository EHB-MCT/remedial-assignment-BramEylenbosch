using System;
using SQLite;

namespace EconomySim.Database.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        [Indexed] public int ResourceId { get; set; }
        [Indexed] public int? PlayerId { get; set; } 
        public int Amount { get; set; }             
        public float UnitPrice { get; set; }
        public string Type { get; set; }            
        public DateTime Timestamp { get; set; }
    }
}
