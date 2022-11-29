using AutoMapper;
using HotelListing.Data;
using HotelListing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;

        //private readonly SignInManager<ApiUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        private readonly IMapper _mapper;

        public AccountController(UserManager<ApiUser> userManager/*, SignInManager<ApiUser> signInManager*/, ILogger<AccountController> logger, IMapper mapper)
        {
            _userManager = userManager;
            //_signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            _logger.LogInformation($"Registration Attempt for {userDto.Email}");

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = _mapper.Map<ApiUser>(userDto);
                user.UserName = user.Email;
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (result.Succeeded == false)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(AccountController)}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Login([FromBody] LoginUserDto userDto)
        //{
        //    _logger.LogInformation($"Login Attempt for {userDto.Email}");

        //    if (ModelState.IsValid == false)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var result = await _signInManager.PasswordSignInAsync(userName:userDto.Email, password: userDto.Password, isPersistent: false, lockoutOnFailure: false);

        //        if (result.Succeeded == false)
        //        {
        //            return Unauthorized(userDto);
        //        }

        //        return Accepted(userDto);

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Something went wrong in the {nameof(Login)}");
        //        return Problem($"Something went wrong in the {nameof(AccountController)}", statusCode: 500);
        //    }

        //}
    }
}