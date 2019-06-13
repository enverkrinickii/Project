using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Catering : Place
    {

        [Key]
        public int CateringId { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }

        public Catering()
        {
            Tags = new List<Tag>();
            Pictures = new List<Picture>();
            Users = new List<ApplicationUser>();
            Dishes = new List<Dish>();
        }
    }
}