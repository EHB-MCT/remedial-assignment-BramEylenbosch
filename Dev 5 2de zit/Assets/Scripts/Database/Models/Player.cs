using SQLite;

namespace EconomySim.Database.Models
{
    [Table("Players")]
    public class Player
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        [Unique, NotNull] public string Name { get; set; }
        public float Balance { get; set; } 
    }
}
