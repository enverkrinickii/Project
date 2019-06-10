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

        public virtual ICollection<Сatering> Сaterings { get; set; }

        public Tag()
        {
            Сaterings = new List<Сatering>();
        }
    }
}