using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class IdentityController : ControllerBase
	{

		private readonly UserManager<IdentityUser> _userManager;

		public IdentityController(UserManager<IdentityUser> userManager)
		{
			_userManager = userManager;

		}

		[HttpPost("login")]
		public IActionResult Login(LoginModel model)
		{
			return Ok();
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterModel model)
		{

			var userToCreate = new IdentityUser
			{
				Email = model.Email,
				UserName = model.Username
			};

			//Create User
			var result = await _userManager.CreateAsync(userToCreate, model.Password);

			if (result.Succeeded)
			{
				return Ok(result);
			}

			return BadRequest(result);
		}

		[HttpPost("confirmemail")]
		public IActionResult ConfirmEmail(ConfirmEmailViewModel model)
		{
			return Ok();
		}
	}
}
