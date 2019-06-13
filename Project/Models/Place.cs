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

        [Key]
        public int CateringId { get; set; }

        [Required]
        public string TitleKey { get; set; }

        [Required]
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