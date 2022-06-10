#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LuckyDrawPromotion.Models;
using LuckyDrawPromotion.Services;
using LuckyDrawPromotion.Utils;
using LuckyDrawPromotion.Helpers;
using System.Text.RegularExpressions;

namespace LuckyDrawPromotion.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login(UserDTO_AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Please check your email and password then try again" });

            return Ok(response);
        }

        [HttpPost]
        public IActionResult ForgotPassword([FromBody] UserDTO_AuthenticateEmail user)
        {
            User temp = _userService.GetAll().ToList().FirstOrDefault(p => p.Email == user.Email);

            if (temp == null)
                return BadRequest(new { message = "Please enter a valid email and try again" });
            else
            {
                string tokenPassword = _userService.generateJwtTokenforPassword(temp);
                MailUtils.SendMailGoogleSmtp("thuancmag2000@gmail.com", "vuuthuan20@gmail.com", "Quên mật khẩu", "link do mat khau accesstoken(3 phut) + dia chi fontend + header: authorization ->" + tokenPassword,
                                              "thuancmag2000@gmail.com", "zbkyzldlguncymaw").Wait();

                return Ok(new { message = "We've sent an email to (entered email address) \nClick the link in the email to reset your password." });
            }
        }

        [HttpPost, Authorize]
        public IActionResult ChangePassword([FromBody] UserDTO_AuthenticatePassword user)
        {
            if (user.Password.CompareTo(user.ConfirmPassword) != 0)
            {
                return BadRequest(new { message = "Password do not match. Please check and try again." });
            }
            var regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(.{6,})$");
            if (user.Password.Length == 0 || !regex.IsMatch(user.Password))
            {
                return BadRequest(new { message = "The Password Confirm is incorrect to the pattern. Password must contain at least 6 characters, including UPPER/lowercase, numbers. Please try again." });
            }
            User userChanged = (User)HttpContext.Items["User"];
            userChanged.Password = user.Password;
            _userService.Save(userChanged);
            return Ok(new { message = "Your password has been changed. Now you can login." });
        }

    }
}
