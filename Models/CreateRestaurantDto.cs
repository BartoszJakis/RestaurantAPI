namespace RestaurantAPI.Models
{
    public class CreateRestaurantDto
    {

        public String Name { get; set; }

        public String Description { get; set; }

        public string Category { get; set; }

        public bool HasDelivery { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }
    }
}
