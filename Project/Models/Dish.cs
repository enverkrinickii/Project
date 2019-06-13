using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required]
        public string TitleKey { get; set; }

        public string DescriptionKey { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]
        public decimal Price { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public Dish()
        {
            Pictures = new List<Picture>();
            Ingredients = new List<Ingredient>();
        }
    }
}