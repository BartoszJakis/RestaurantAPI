using AutoMapper;
using OpenQA.Selenium;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public int Create(int RestaurantId, CreateDishDto dto)
        {
            var restuarant = _context.Restaurants.FirstOrDefault(r => r.Id == RestaurantId);

            if (restuarant == null)
                throw new DirectoryNotFoundException("Restaurant not found");

            var NewDish = _mapper.Map<Dish>(dto);

            NewDish.RestaurantId = RestaurantId;
            _context.Dishes.Add(NewDish);
            _context.SaveChanges();
            return NewDish.Id;

        }

        public int Delete(int DishId)
        {
            var dish = _context.Dishes.FirstOrDefault(r => r.Id == DishId);

            if (dish == null)
                throw new NotFoundException("Restaurant not found");

            
        
            _context.Dishes.Remove(dish);
            _context.SaveChanges();
            return dish.Id;

        }
    }
}
