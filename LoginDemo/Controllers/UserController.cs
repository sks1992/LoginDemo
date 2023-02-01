using LoginDemo.Data;
using LoginDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoginDemo.Controllers
{
	[Route("api/demoData")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly AppDbContext _authContext;
		public UserController(AppDbContext authContext) 
		{
			_authContext= authContext;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Authenticate([FromBody]User user)
		{
			if(user == null)
			{
				return BadRequest();
			}

			var userObj =await  _authContext.Users.FirstOrDefaultAsync(u=> u.UserName == user.UserName &&
			u.Password == user.Password); 
			if(userObj == null)
			{
				return NotFound(new { Message ="User is Not Found!"});
			}
			return Ok(new {Message="Login Success"});
		}

		[HttpPost("register")]
		public async Task<IActionResult> RegisterUser([FromBody] User  user)
		{
			if(user==null)
			{
				return BadRequest();	
			}

			await _authContext.Users.AddAsync(user);
			await _authContext.SaveChangesAsync();
			return Ok(new { Message= "User Register Success" });
		}
	}
}
