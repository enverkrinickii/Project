using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Catering> Сaterings { get; set; }
        public virtual ICollection<Sight> Sights { get; set; }

        public Tag()
        {
            Сaterings = new List<Catering>();
            Sights = new List<Sight>();
        }
    }
}