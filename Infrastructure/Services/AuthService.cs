using Core.Entities.identity;
using Core.IServices;
using Core.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class AuthService(
		IOptions<JwtSettings> options,
		UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthService
	{

		private readonly JwtSettings jwtSettings=	options.Value;

		public async Task<UserDto> LoginAsync(LoginDto login)
		{
			var user = await userManager.FindByEmailAsync(login.Email);
			if (user == null)
			{
				throw new Exception("Invalid login attempt");
			}
			var result = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);
			if (!result.Succeeded)
			{
				throw new Exception("Invalid login attempt");
			}
			return new UserDto
			{
				Email = user.Email,
				Token = "token",
				DisplayName = user.DisplayName,
				Id = user.Id
			};



		}

		public Task<UserDto> Logout()
		{
			throw new NotImplementedException();
		}

		public async Task<UserDto> Register(RegisterDto register)
		{
			var user = new ApplicationUser
			{
				Email = register.Email,
				UserName = register.UserName,
				DisplayName = register.UserName,
				PhoneNumber = register.PhoneNumber
			};
			var result = await userManager.CreateAsync(user, register.Password);

			if (!result.Succeeded)
			{
				throw new Exception("Failed to create user");
			}
			return new UserDto
			{
				Email = user.Email,
				Token = await GenerateTokenAsync(user),
				DisplayName = user.DisplayName,
				Id = user.Id,
			};

		}

		public Task<UserDto> Me()
		{
			var user = new UserDto
			{
				Email = "moamen@gmail.com",
				Token = "token",
				DisplayName = "moamen",
				Id = "1"
			};
			return Task.FromResult(user);


		}

		private async Task<string> GenerateTokenAsync(ApplicationUser applicationUser)
		{
			var claims = new List<Claim>
	{
		new Claim(ClaimTypes.NameIdentifier, applicationUser.Id),
		new Claim(ClaimTypes.Email, applicationUser.Email),
		new Claim(ClaimTypes.Name, applicationUser.DisplayName)
	};

			//var roles = await userManager.GetRolesAsync(applicationUser);
			//claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

			// Hardcoded values for key, issuer, audience, and expiration
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("k79X6OsAbsdjUyKxm6QqSbtutM3KnD3vB1aEc6G19iPch7bZEQZECyEj6M9h5ucF\r\nUSGY26RAM/tsE+2YD4ZwYKQ+1p/bchBwKiWIiYlzfCy2ocLQo4yhHFX+Y6/YkyNK\r\nbIQkE6s6xOlhLMwrQLHpmtYMuCIrYzZ6PKGwFO9K7wU="));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = creds
			};

			var tokenObj = new JwtSecurityToken(
				issuer: "Ecommerce",
				audience: "users",
				claims: claims,
				expires: DateTime.Now.AddDays(7),
				signingCredentials: creds
			);

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

	}
}
