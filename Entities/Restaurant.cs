namespace RestaurantAPI.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public string Category { get; set; }

        public bool HasDelivery {  get; set; }

        public string Email { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Dish> Dishes { get; set; }
    }
}
