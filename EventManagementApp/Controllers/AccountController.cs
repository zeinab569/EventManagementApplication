using AutoMapper;
using Core.Identity;
using Core.Interfaces;
using EventManagementApp.Dtos.AccountDto;
using EventManagementApp.Errors;
using EventManagementApp.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Net.Mail;

namespace EventManagementApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;

        public AccountController(
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
            ITokenServices tokenServices,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices=tokenServices;
            _mapper=mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new ApiResponce(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponce(401));

            return new UserDto
            {
                Email = user.Email,
                Token = _tokenServices.CreateToken(user),
                DisplayName = user.DisplayName
            };

        }


        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                Email = registerDto.Email,
                DisplayName = registerDto.Username,
                UserName=registerDto.Email
            };

            var result= await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
             return BadRequest(new ApiResponce(400));
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenServices.CreateToken(user),
                Email = user.Email,
            };

        }
           
        


        [HttpGet("secret")]
        [Authorize]
        public string GetSecret()
        {
            return "secret string";
        }


        [HttpGet]
        [Authorize]

        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {

            //var email = HttpContext.User?.Claims?.FirstOrDefault(x=>x.Type == ClaimTypes.Email)?.Value;

            var user = await _userManager.FindUserByClaimsPrinciplsWithEmail(User);
            return new UserDto
            {
               Email=user.Email,
               Token=_tokenServices.CreateToken(user),
               DisplayName=user.DisplayName
            };
        }

        [HttpGet("emailexist")]
        public async Task<ActionResult<bool>> EmailExist([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetCurrentAddress()
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindUserByClaimsPrinciplsWithAddress(User);
            return _mapper.Map<Address,AddressDto>(user.Address);
        }
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto address)
        {
            var user = await _userManager.FindUserByClaimsPrinciplsWithAddress(User);
            user.Address = _mapper.Map<AddressDto,Address>(address);
            var result = _userManager.UpdateAsync(user);
            if(result.IsCompletedSuccessfully) return Ok(_mapper.Map<AddressDto>(user.Address));
            return BadRequest("problem update the user");
        }
    }
}
