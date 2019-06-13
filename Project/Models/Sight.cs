using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Sight : Place
    {

        [Key]
        public int SigthId { get; set; }

        public Sight()
        {
            Tags = new List<Tag>();
            Pictures = new List<Picture>();
            Users = new List<ApplicationUser>();
        }
    }
}

