using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;


        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext
              .Restaurants
              .Include(r => r.Address)
              .Include(r => r.Dishes)
              .FirstOrDefault(x => x.Id == id);

            if (restaurant == null)
            {
                return null;
            }

            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;


        }


        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext
             .Restaurants
             .Include(r => r.Address)
             .Include(r => r.Dishes)
             .ToList();



            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);

            return restaurantsDtos;


        }
        public bool Update(int id, RestaurantDto restaurantDto)
        {
            var existingRestaurant = _dbContext.Restaurants
                .FirstOrDefault(x => x.Id == id);

            if (existingRestaurant == null)
            {
                return false; 
            }

          
            existingRestaurant.Name = restaurantDto.Name;
            existingRestaurant.Description = restaurantDto.Description;
            existingRestaurant.HasDelivery = restaurantDto.HasDelivery;

            
            _dbContext.SaveChanges();

            return true;
        }
        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;

        }

        public bool Delete(int Id)
        {
            _logger.LogError($"Restaurant with id: {Id} Deleted");
            _logger.LogWarning($"Restaurant with id: {Id} Deleted");

            var restaurant = _dbContext
           .Restaurants
           .FirstOrDefault(x => x.Id == Id);

            if (restaurant == null)
            {
                return false;
            }
           

           _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
            return true;

        }



    }
}
