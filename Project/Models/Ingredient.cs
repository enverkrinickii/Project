using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Ingredient
    {
        [Key]
        public string IngredientId { get; set; }

        [Required]
        public string NameKey { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }

        public Ingredient()
        {
            Dishes = new List<Dish>();
        }
    }
}