using Core.IServices;
using Core.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAuthService _authService;
		public AccountController(IAuthService authService)
		{
			_authService = authService;
		}
		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto model)
		{
			var result = await _authService.LoginAsync(model);
			if (result == null)
			{
				return BadRequest(new { message = "Invalid login attempt" });
			}
			return Ok(result);

		}
		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto model)
		{
			var result = await _authService.Register(model);
			if (result == null)
			{
				return BadRequest(new { message = "User already exists" });
			}
			return Ok(result);

		}
		[HttpPost("logout")]
		public async Task<ActionResult<UserDto>> Logout()
		{
			var result = await _authService.Logout();
			if (result == null)
			{
				return BadRequest(new { message = "Invalid logout attempt" });
			}
			return Ok(result);

		}
		[HttpGet("me")]
		public async Task<ActionResult<UserDto>> Me()
		{
			var result = await _authService.Me();
			if (result == null)
			{
				return BadRequest(new { message = "Invalid me attempt" });
			}
			return Ok(result);

		}



	}
}
