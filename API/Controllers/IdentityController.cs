using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class IdentityController : ControllerBase
	{


		public IdentityController()
		{

		}

		[HttpPost("login")]
		public IActionResult Login(LoginModel model)
		{
			return Ok();
		}

		[HttpPost("register")]
		public IActionResult Register(RegisterModel model)
		{
			return Ok();
		}

		[HttpPost("confirmemail")]
		public IActionResult ConfirmEmail(ConfirmEmailViewModel model)
		{
			return Ok();
		}
	}
}
