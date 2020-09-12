using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : ControllerBase
	{


		public UserController()
		{

		}

		//Only admins should be able to get all admins
		//For testing roles
		[HttpGet]
		public IActionResult GetAllAdmins()
		{
			return Ok();
		}
		//Only admins should be able to get all managers
		//For testing roles
		[HttpGet("managers")]
		public IActionResult GetAllManagers()
		{
			return Ok();
		}
		//Only admins and managers should get all non admin and users
		//For testing roles
		[HttpGet("users")]
		public IActionResult GetAllUsers()
		{
			return Ok();
		}
		//For testing claims and policies
		[HttpGet("managerdevelopers")]
		public IActionResult AdminDesigners()
		{
			return Ok(new
			{
				role = "This user ROLE is Manager",
				claim = "User using this Api claims to be DEVELOPER"
			});
		}

		//For testing claims and policies
		[HttpGet("admindevelopers")]
		public IActionResult AdminDevelopers()
		{
			return Ok(new
			{
				role = "This user ROLE is Admin",
				claim = "User using this Api claims to be DEVELOPER"
			});
		}

	}
}
