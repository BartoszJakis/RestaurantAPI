namespace RestaurantAPI.Models
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public String Description { get; set; }

        public string Category { get; set; }

        public bool HasDelivery { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string PostalCode { get; set; }

        public ICollection<DishDto> Dishes { get; set; }


    }
}
