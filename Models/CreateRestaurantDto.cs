using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class CreateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public bool HasDelivery { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required]
        [MaxLength(25)]
        public string City { get; set; }

        // Street: Allows letters, digits, spaces, hyphens, and dots.
        [RegularExpression(@"^[a-zA-Z0-9\s\.-]+$", ErrorMessage = "Street can only contain letters, digits, spaces, hyphens, and dots.")]
        public string Street { get; set; }

        // PostalCode: Specific format like "83-270"
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "PostalCode must be in the format 'XX-XXX'.")]
        public string PostalCode { get; set; }
    }
}
