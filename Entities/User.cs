﻿using Microsoft.AspNetCore.Identity;

namespace RestaurantAPI.Entities
{
    public class User
    {

        public int Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Nationality { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }


    }
}
