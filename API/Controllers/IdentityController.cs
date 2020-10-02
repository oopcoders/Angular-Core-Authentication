using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using API.ViewModels;
using Core.Services.Email;
using Core.Services.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
		private readonly IConfiguration _config;
		private readonly IEmailSender _emailSender;

		public IdentityController(
			UserManager<IdentityUser> userManager,
			 SignInManager<IdentityUser> signInManager,
			  IJWTTokenGenerator jwtToken,
			  RoleManager<IdentityRole> roleManager,
			  IConfiguration config,
			  IEmailSender emailSender)
		{
			_jwtToken = jwtToken;
			_roleManager = roleManager;
			_config = config;
			_emailSender = emailSender;
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

			IList<Claim> claims = await _userManager.GetClaimsAsync(userFromDb);
			return Ok(new
			{
				result = result,
				username = userFromDb.UserName,
				email = userFromDb.Email,
				token = _jwtToken.GenerateToken(userFromDb, roles, claims)
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

				var token = await _userManager.GenerateEmailConfirmationTokenAsync(userFromDb);

				var uriBuilder = new UriBuilder(_config["ReturnPaths:ConfirmEmail"]);
				var query = HttpUtility.ParseQueryString(uriBuilder.Query);
				query["token"] = token;
				query["userid"] = userFromDb.Id;
				uriBuilder.Query = query.ToString();
				var urlString = uriBuilder.ToString();

				var senderEmail = _config["ReturnPaths:SenderEmail"];

				await _emailSender.SendEmailAsync(senderEmail, userFromDb.Email, "Confirm your email address", urlString);

				//Add role to user
				await _userManager.AddToRoleAsync(userFromDb, model.Role);

				var claim = new Claim("JobTitle", model.JobTitle);

				await _userManager.AddClaimAsync(userFromDb, claim);

				return Ok(result);
			}

			return BadRequest(result);
		}

		[HttpPost("confirmemail")]
		public async Task<IActionResult> ConfirmEmail(ConfirmEmailViewModel model)
		{

			var user = await _userManager.FindByIdAsync(model.UserId);

			var result = await _userManager.ConfirmEmailAsync(user, model.Token);

			if (result.Succeeded)
			{
				return Ok();
			}
			return BadRequest();
		}
	}
}
