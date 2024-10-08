﻿using FluentValidation;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(RestaurantDbContext dbContext) 
        {
            RuleFor(x=> x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x=> x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var emailIsInUse = dbContext.Users.Any(u => u.Email == value);
                    if (emailIsInUse)
                    {
                        context.AddFailure("Email", "That Email is taken");
                    }
                });
        
        
        }
    }
}
