using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Core.Services.Token
{
	public class JWTTokenGenerator : IJWTTokenGenerator
	{
		public string GenerateToken(IdentityUser user, IList<string> roles, IList<Claim> claims)
		{
			throw new System.NotImplementedException();
		}
	}
}