using Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.IServices
{
	public interface IAuthService
	{
		Task<UserDto> LoginAsync(LoginDto login);
		Task<UserDto> Register(RegisterDto register);
		Task<UserDto> Logout();
		Task<UserDto> Me();

	}
}
