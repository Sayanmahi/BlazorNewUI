using BlazorFastFood.DTO;
using Food_DataAccess.Data;
using Food_DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorFastFood.Services
{
    public class LoginService : ILoginService
    {
        private readonly ApplicationDbContext db;
        private readonly IConfiguration _config;


        public LoginService(ApplicationDbContext db, IConfiguration config)
        {
            this.db = db;
            _config = config;
        }
        public async Task<string> LoginAdmin(LoginDTO l)
        {
            var d = await db.Users.FirstOrDefaultAsync(x => x.Email == l.Email);
            if (d != null && d.userType=="Admin")
            {
                var dd = await db.Users.FirstOrDefaultAsync(x => x.Email == l.Email && x.Password == l.Password);
                if (dd != null)
                {
                    //var token = JwtGenerate(l.Email, dd.userType, dd.Id);
                    return ("True");
                }
                return ("Not");
            }
            return ("Not");
        }

        public async Task<string> LoginUser(LoginDTO l)
        {
            var d= await db.Users.FirstOrDefaultAsync(x=>x.Email == l.Email);   
            if(d!=null && d.userType=="User")
            {
                var dd= await db.Users.FirstOrDefaultAsync(x=>x.Email == l.Email && x.Password==l.Password);
                if(dd!=null)
                {
                    //var token = JwtGenerate(l.Email, dd.userType, dd.Id);
                    var dd1= Convert.ToString(d.Id);
                    return (dd1);
                }
                return ("Not");
            }
            return ("Not");
        }

        public async Task<bool> Register(User user)
        {
            var d= await db.Users.FirstOrDefaultAsync(x=>x.Email==user.Email);
            if(d==null)
            {
                user.userType = "User";
                await db.AddAsync(user);
                await db.SaveChangesAsync();
                return (true);
            }
            return (false);
        }
        private string JwtGenerate(string email, string role, int userid)
        {
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256); //security key is public key so hashing security key
            var claims = new[]//dismantling the payload datas
            {
                 new Claim("Email",email),
                 new Claim("UserId",userid.ToString()),
                 new Claim(ClaimTypes.Role,role)
                };
            var token = new JwtSecurityToken(
                issuer: _config["JWT:Issuer"],
                audience: _config["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
