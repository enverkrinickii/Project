using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Sight
    {
        [Key]
        public int SightId { get; set; }


        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Rating { get; set; }

        [Required]
        public int? AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }

        public Sight()
        {
            Tags = new List<Tag>();
            Pictures = new List<Picture>();
        }
    }
}

