using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string House { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string ContactEmail { get; set; }

        [Required]
        public string ContactPhone { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}