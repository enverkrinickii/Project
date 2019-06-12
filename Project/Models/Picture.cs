using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Picture
    {
        [Key]
        public int PictureId { get; set; }

        public string ImageUrl { get; set; }

        public byte[] ImageBytes { get; set; }

        public int? ThumbnailId { get; set; }
        [ForeignKey("ThumbnailId")]
        public Picture ThumbnailPicture { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }

        public virtual ICollection<Sight> Sights { get; set; }

        public virtual ICollection<Catering> Сaterings { get; set; }

        public Picture()
        {
            Dishes = new List<Dish>();
            Sights = new List<Sight>();
            Сaterings = new List<Catering>();
        }
    }
}