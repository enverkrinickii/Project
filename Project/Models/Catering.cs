using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Catering
    {
        [Key]
        public int CateringId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public int? AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public Catering()
        {
            Tags = new List<Tag>();
            Pictures = new List<Picture>();
            Users = new List<ApplicationUser>();
            Dishes = new List<Dish>();
        }
    }
}