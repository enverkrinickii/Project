using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class Place
    {
        [Required]
        [Index("TitleKeyIndex", IsUnique = true)]
        public string TitleKey { get; set; }

        [Required]
        [Index("DescriptionKeyIndex", IsUnique = true)]
        public string DescriptionKey { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public int? AddressId { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}