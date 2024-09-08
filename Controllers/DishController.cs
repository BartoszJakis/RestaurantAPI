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


        [HttpGet("{dishId}")]
        public ActionResult<DishDto> Get([FromRoute] int restaurantId, [FromRoute]int dishId)
        {
            try
            {
                var dish = _dishService.GetById(restaurantId, dishId);

                return Ok(dish);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public ActionResult<List<DishDto>> GetAll([FromRoute] int RestaurantId)
        {
            try
            {
                var dishes = _dishService.GetAll(RestaurantId);

                return Ok(dishes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

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

        [HttpDelete("{dishId}")]
        public ActionResult Delete([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            try
            {
                var deleteDish = _dishService.Delete(dishId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); 
            }
        }


    }
}
