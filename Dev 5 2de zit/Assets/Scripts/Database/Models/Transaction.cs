using System;
using SQLite;

namespace EconomySim.Database.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public int ResourceId { get; set; } 

        [NotNull]
        public string ResourceName { get; set; }  

        [NotNull]
        public int Quantity { get; set; }

        [NotNull]
        public float Price { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
