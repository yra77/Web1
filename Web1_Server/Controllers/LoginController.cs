

using Web1_Server.Models;
using Web1_Server.Services.JwtService;
using Web1_Server.Services.PrintService;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using System.IdentityModel.Tokens.Jwt;


namespace Web1_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {


        private readonly DB_Context _context;
        private readonly IPrint _print;
        private readonly IJwtToken _token;


        public LoginController(IPrint print,
                               IJwtToken token,
                               DB_Context context)
        {
            _print = print;
            _token = token;
            _context = context;
        }


        [HttpPost("Auth")]// GET: api/Login/Auth
        public async Task<ActionResult> PostLoginModel([FromBody] LoginModel data)
        {
            if (_context.Logins == null)
            {
                return Problem(statusCode: 404, title: "Not found data base");
            }

            try
            {
                if (data != null && data.Email != null && data.Password != null)
                {
                    var res = await _context.Logins.FirstOrDefaultAsync(x => x.Email == data.Email);
                    if (res == null)
                    {
                        return Problem(statusCode: 404, title: "Not found email");
                    }
                    else if (res.Password != data.Password)
                    {
                        return Problem(statusCode: 404, title: "Not found password");
                    }
                    else
                    {
                        var jwt = _token.CreateJwtToken(res.Email);
                        OkResponseModel responseModel = new()
                        {
                            Email = res.Email,
                            Token = new JwtSecurityTokenHandler().WriteToken(jwt)
                        };
                        _print.NotificationPrint("Found the contact");
                        return Ok(responseModel);
                    }
                }
            }
            catch (Exception e) { _print.ErrorPrint("Error GetEmployees() " + e.Message); }

            return Problem(statusCode: 400, title: "Request error");
        }


        [HttpPost]// POST: api/Login
        public async Task<ActionResult> Post_LoginModel([FromBody] LoginModel data)
        {
            if (_context.Logins == null)
            {
                return Problem(statusCode: 404, title: "Not found data base");
            }

            try
            {
                if (data != null && data.Email != null && data.Password != null)
                {
                    var res = await _context.Logins.FirstOrDefaultAsync(x => x.Email == data.Email);
                    if (res != null) return Problem(statusCode: 404, title: "There is already such an email");
                    else
                    {
                        var jwt = _token.CreateJwtToken(data.Email);
                        OkResponseModel responseModel = new()
                        {
                            Email = data.Email,
                            Token = new JwtSecurityTokenHandler().WriteToken(jwt)
                        };

                        _context.Logins?.Add(data);
                        await _context.SaveChangesAsync();
                        _print.NotificationPrint("Inserted new contact");

                        return Ok(responseModel);
                    }
                }
            }
            catch (Exception e) { _print.ErrorPrint("Error Insert Post_LoginModel() " + e.Message); }

            return Problem(statusCode: 400, title: "Request error");
        }


        [Authorize]
        [HttpDelete("{email}")]// DELETE: api/Login/email
        public async Task<IActionResult> DeleteLoginModel(string email)
        {
            // if(Microsoft.Extensions.Primitives.StringValues.IsNullOrEmpty(Request.Headers.Authorization))
            //             { System.Console.WriteLine("PPPPPPPPPPPPPPPPPPPPp"); }

            if (_context.Logins == null)
            {
                return NotFound();
            }

            try
            {
                await _context.SaveChangesAsync();
                _print.NotificationPrint("Delete contact");
            }
            catch (Exception e) { _print.ErrorPrint("Error Delete contact " + e.Message); }

            return Problem(statusCode: 400, title: "Request error");
        }
    }
}
