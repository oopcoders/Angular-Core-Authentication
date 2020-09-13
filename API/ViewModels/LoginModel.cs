using System.ComponentModel.DataAnnotations;
namespace API.ViewModels
{
	public class LoginModel
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
	}
}