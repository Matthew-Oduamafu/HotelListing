using HotelListing.Data;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models
{
    public class HotelDto:CreateHotelDto
    {
        public int Id { get; set; }
        public CountryDto Country { get; set; }

    }
    public class CreateHotelDto
    {
        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Hotel name exceed 150 chars", MinimumLength=2, ErrorMessageResourceName ="Hotel name  can be less than 2 chars")]
        public string Name { get; set; }
        [Required]
        [StringLength(maximumLength: 150, ErrorMessage = "Address exceed 150 chars")]
        public string Address { get; set; }
        [Required]
        [Range(1.0, 5.0)]
        public double Rating { get; set; }
        [Required]
        public int CountryId { get; set; }
    }
}
