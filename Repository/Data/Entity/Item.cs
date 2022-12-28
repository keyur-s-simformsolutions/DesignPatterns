using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Data.Entity
{
    public class Item
    {
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Please, Enter a Category first")]
        public string Category { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public int StockItem { get; set; }
        public DateTime DateofAddingItem { get; set; }

    }
}
