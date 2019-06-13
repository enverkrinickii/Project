using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Ingredient
    {
        [Key]
        public string IngredientId { get; set; }

        [Required]
        [Index("NameKeyIndex", IsUnique = true)]
        public string NameKey { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }

        public Ingredient()
        {
            Dishes = new List<Dish>();
        }
    }
}