

using Web1_Server.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace Web1_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {


        private readonly DB_Context _context;


        public LoginController(DB_Context context)
        {
            _context = context;
        }


        [HttpPost("Auth")]// GET: api/Login/Auth
        public async Task<ActionResult> PostLoginModel([FromBody] LoginModel data)
        {
            if (_context.Logins == null)
            {
                return NotFound();
            }

            try
            {
                var res = await _context.Logins.FirstOrDefaultAsync(x => x.Email == data.Email);
                if (res == null)
                    return NotFound();
                else if (res.Password != data.Password)
                    return NotFound();
                else
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Email, res.Email) };
                    // создаем JWT-токен
                    var jwt = new JwtSecurityToken(
                            issuer: Constants.JwtConstant.ISSUER,
                            audience: Constants.JwtConstant.AUDIENCE,
                    claims: claims,
                            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                            signingCredentials: new SigningCredentials(Constants.JwtConstant.GetSymmetricSecurityKey(), 
                                                                       SecurityAlgorithms.HmacSha256));

                    return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
                }
            }
            catch (Exception e) { Console.WriteLine("Error GetEmployees() " + e.Message); }

            return Problem();
        }
    }
}
