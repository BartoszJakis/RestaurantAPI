﻿using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    public class RestaurantSeeder

      
    {
        private  readonly RestaurantDbContext _context;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _context = dbContext;
        }
        public void Seed()
        {
            if(_context.Database.CanConnect())
            {

                if(!_context.Roles.Any())
                {
                    var roles = GetRoles();
                    _context.AddRange(roles);
                    _context.SaveChanges();
                }

                if(!_context.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _context.Restaurants.AddRange(restaurants);
                    _context.SaveChanges();
                }
            }
        }


        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name= "Admin"
                },
                new Role()
                {
                    Name = "User"
                }
            };
            return roles;
        }
        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description =
                        "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                    Email = "contact@kfc.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Nashville Hot Chicken",
                            Price = 10.30M,
                             Description = "A signature sandwich"
                        },

                        new Dish()
                        {
                            Name = "Chicken Nuggets",
                            Price = 5.30M,
                            Description = "Bite-sized pieces of fried chicken"
                        },
                    },
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Długa 5",
                        PostalCode = "30-001"
                    }
                },
                new Restaurant()
                {
                    Name = "McDonald Szewska",
                    Category = "Fast Food",
                    Description =
                        "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                    Email = "contact@mcdonald.com",
                    HasDelivery = true,
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Szewska 2",
                        PostalCode = "30-001"
                    }
                }
            };

            return restaurants;
        }
    }
}
