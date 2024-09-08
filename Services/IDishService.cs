using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IDishService
    {
        int Create(int RestaurantId, CreateDishDto dto);

        int Delete(int RestaurantId);
        List<DishDto> GetAll(int restaurantId);
        DishDto GetById(int restaurantId, int DishId);
    }
}