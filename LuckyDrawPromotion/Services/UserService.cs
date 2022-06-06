using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Data;
using Microsoft.Extensions.Options;
using LuckyDrawPromotion.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace LuckyDrawPromotion.Services
{
    public interface IUserService
    {
        UserDTO_AuthenticateResponse Authenticate(UserDTO_AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Save(User user);
        string generateJwtTokenforPassword(User user);
        
    }

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private readonly LuckyDrawPromotionContext _context;
        private readonly AppSettings _appSettings;
        public UserService(IOptions<AppSettings> appSettings, LuckyDrawPromotionContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public UserDTO_AuthenticateResponse Authenticate(UserDTO_AuthenticateRequest model)
        {
            var user = _context.Users.ToList().SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);

            // return null if user not found
            if (user == null) return null!;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new UserDTO_AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.ToList().FirstOrDefault(x => x.UserId == id)!;
        }

        public void Save(User temp)
        {
            _context.Update(temp);
            _context.SaveChanges();
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JWTSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string generateJwtTokenforPassword(User user)
        {
            // generate token that is valid for 3 minutes
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JWTSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", user.UserId.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}
