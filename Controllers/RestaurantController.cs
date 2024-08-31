﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{

    [Route("api/restaurant")]
    public class RestaurantController : ControllerBase

    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        public RestaurantController(RestaurantDbContext dbContext, IMapper mappingProfile)
        {
        _dbContext = dbContext;
            _mapper = mappingProfile;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>>GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(r=>r.Address)
                .Include(r => r.Dishes)
                .ToList();

            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
           
            return Ok(restaurantsDtos);
            

        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> GetOne([FromRoute]int id)
        {
            var restaurant= _dbContext
                .Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .FirstOrDefault(x => x.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            var restaurantDtos = _mapper.Map<RestaurantDto>(restaurant);


            return Ok(restaurantDtos);

        }

    }
}
