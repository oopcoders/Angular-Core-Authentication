using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ViewModels;
using Core.Services.Token;
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
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly IJWTTokenGenerator _jwtToken;

		public IdentityController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IJWTTokenGenerator jwtToken)
		{
			_jwtToken = jwtToken;
			_signInManager = signInManager;
			_userManager = userManager;

		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginModel model)
		{

			var userFromDb = await _userManager.FindByNameAsync(model.Username);

			if (userFromDb == null)
			{
				return BadRequest();
			}

			var result = await _signInManager.CheckPasswordSignInAsync(userFromDb, model.Password, false);


			if (!result.Succeeded)
			{
				return BadRequest();
			}
			return Ok(new
			{
				result = result,
				username = userFromDb.UserName,
				email = userFromDb.Email,
				token = _jwtToken.GenerateToken(userFromDb)
			});
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
