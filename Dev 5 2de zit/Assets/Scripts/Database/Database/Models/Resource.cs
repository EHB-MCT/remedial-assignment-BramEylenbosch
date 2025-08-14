using SQLite;

namespace EconomySim.Database.Models
{
    [Table("Resources")]
    public class Resource
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        [Unique, NotNull] public string Name { get; set; }
        [NotNull] public int Quantity { get; set; }
        [NotNull] public float Price { get; set; }
    }
}
