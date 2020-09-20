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
		private readonly RoleManager<IdentityRole> _roleManager;

		public IdentityController(
			UserManager<IdentityUser> userManager,
			 SignInManager<IdentityUser> signInManager,
			  IJWTTokenGenerator jwtToken,
			  RoleManager<IdentityRole> roleManager)
		{
			_jwtToken = jwtToken;
			_roleManager = roleManager;
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

			var roles = await _userManager.GetRolesAsync(userFromDb);
			return Ok(new
			{
				result = result,
				username = userFromDb.UserName,
				email = userFromDb.Email,
				token = _jwtToken.GenerateToken(userFromDb, roles)
			});
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterModel model)
		{

			if (!(await _roleManager.RoleExistsAsync(model.Role)))
			{
				await _roleManager.CreateAsync(new IdentityRole(model.Role));
			}

			var userToCreate = new IdentityUser
			{
				Email = model.Email,
				UserName = model.Username
			};

			//Create User
			var result = await _userManager.CreateAsync(userToCreate, model.Password);

			if (result.Succeeded)
			{

				var userFromDb = await _userManager.FindByNameAsync(userToCreate.UserName);

				//Add role to user
				await _userManager.AddToRoleAsync(userFromDb, model.Role);

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
