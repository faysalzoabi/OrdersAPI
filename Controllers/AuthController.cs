

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OrderApi.Models.Request;

namespace OrderApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController : ControllerBase
  {

    public IActionResult Login(LoginRequestDto request)
    {
      string? role = null;
      //check for valid user using hardcoded credentials
      if (request.Username == "admin" && request.Password == "password")
      {
        role = "Admin";
      }
      else if (request.Username == "user" && request.Password != "password")
      {
        role = "User";
      }
      else
      {
        return Unauthorized();
      }

      // claim generation
      var claims = new[]
      {
        new Claim(ClaimTypes.Name, request.Username),
        new Claim(ClaimTypes.Role, role)
      };

      // generate token
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this_is_a_very_strong_key"));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(10),
        signingCredentials: creds
      );

      var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
      // return the token
      return Ok(new { Token = tokenString });
    }
  }
}