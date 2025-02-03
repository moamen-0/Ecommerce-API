using System.ComponentModel.DataAnnotations;

namespace Core.Models.Auth
{
	public class RegisterDto
	{

		[Required]
		public string UserName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$",
			ErrorMessage = "Password must be between 8 and 15 characters and contain at least one uppercase letter, one lowercase letter, one digit and one special character.")]
		public string Password { get; set; }


		[Required]
		public string PhoneNumber { get; set; }

		
	}
}