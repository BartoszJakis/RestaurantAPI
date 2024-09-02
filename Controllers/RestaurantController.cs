using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{

    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase

    {

        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {

            _restaurantService = restaurantService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAll()
        {


            var restaurantDtos = _restaurantService.GetAll();
            return Ok(restaurantDtos);


        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> GetOne([FromRoute] int id)
        {

            var restaurant = _restaurantService.GetById(id);

            return Ok(restaurant);

        }



        [HttpDelete("{Id}")]

        public ActionResult Delete([FromRoute] int Id)
        {

            var restaurant = _restaurantService.Delete(Id);

            return NoContent();
        }


        [HttpPut("{Id}")]

        public ActionResult UpdateRestaurant([FromRoute] int Id, [FromBody] RestaurantDto restaurant)
        {

            var updateRestaurant = _restaurantService.Update(Id, restaurant);

            return Ok();

        }

        [HttpPost]

        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

           var Id= _restaurantService.Create(dto);
        

            return Created("/api/restaurant/{Id}", null);
        }

    }
}
