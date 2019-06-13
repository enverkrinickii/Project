using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required]
        [Index("TitleKeyIndex", IsUnique = true)]
        public string TitleKey { get; set; }

        [Required]
        [Index("DescriptionKeyIndex", IsUnique = true)]
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