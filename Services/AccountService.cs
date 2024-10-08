﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly RestaurantDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        public AccountService(RestaurantDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
          

        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _context.Users
                .Include(u=>u.Role)
                .FirstOrDefault(u=>u.Email == dto.Email);
            if (user == null)
            {
                throw new BadHttpRequestException("invalid username or password");
            }

           var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if(result ==PasswordVerificationResult.Failed)
            {
                throw new BadHttpRequestException("invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,$"{user.Name}"),
                new Claim(ClaimTypes.Role,$"{user.Role.Name}"),
                new Claim("DateOfBirth",user.DateOfBirth.Value.ToString("yyyy-MM-dd"))
                
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials:cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);




        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId
             
            };
            var HashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = HashedPassword;
            _context.Users.Add(newUser);

            _context.SaveChanges();


        }


    }
}
