using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService) 
        {
            _dishService = dishService;
        
        }


        [HttpPost]
      public ActionResult Post([FromRoute] int restaurantId, [FromBody]CreateDishDto dto)
        {
            try
            {
               var newDishId = _dishService.Create(restaurantId, dto);

                return Created($"api/{restaurantId}/dish/{newDishId}", null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int restaurantId, [FromRoute] int id)
        {
            try
            {
                var deleteDish = _dishService.Delete(id);
                return NoContent(); // Usunięcie zakończone sukcesem, nie zwracamy treści
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // Jeśli danie nie zostało znalezione
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Inne błędy
            }
        }


    }
}
