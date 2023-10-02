using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using BackendApi2.Contracts;
using BackendApi2.Entities.DTOModels;
using BackendApi2.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BackendApi2.Controllers;

 [ApiController]
public class AuthenticationController : ControllerBase
{
      private IRepositoryWrapper _repositoryWrapper;

        private IMapper _mapper;

        private IConfiguration _configuration;
        public AuthenticationController(IRepositoryWrapper repositoryWrapper, IMapper mapper,IConfiguration configuration)
        {
            _repositoryWrapper = repositoryWrapper;

            _mapper = mapper;

            _configuration = configuration;
        }

      
      [HttpPost]
      [Route("api/[controller]/Login")]
      public async Task<IActionResult> LoginUser([FromBody]LoginUserDto loginUserDto)
      {
         User userlogin = await _repositoryWrapper.user.LoginUser(loginUserDto.Username,loginUserDto.Password);  
         if(userlogin == null)
         {
            return Unauthorized("Username or Password is incorrect");
         }
        string jwttoken = CreateJWT(userlogin);
          LoginResDto loginResDto= new LoginResDto()
          {
            Username = userlogin.Username,
            token=jwttoken
           };
           return Ok(loginResDto);
      }  

      [HttpPost]
      [Route("api/[controller]/Register")]
      public async Task<IActionResult> RegisterUser([FromBody]RegisterUserDto registerUserDto)
      {
          var result = await _repositoryWrapper.user.CheckUsernameExists(registerUserDto.Username);
          if(result)
          {
            return BadRequest("UserName already Exists");
          }
          else{
            _repositoryWrapper.user.RegisterUser(registerUserDto.Username,
            registerUserDto.Password,registerUserDto.Role);
          }
          await _repositoryWrapper.save();
          return StatusCode(201);
      }

      private string CreateJWT(User user)
      {
         var key = new 
         SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:SignKey").Value));

         var claims = new Claim[]
         {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role,user.Role),
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
        };

         var signingcredentials =  new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);

         var tokendescriptor = new SecurityTokenDescriptor()
         {
             Subject = new ClaimsIdentity(claims),
             Issuer = _configuration.GetSection("JwtSettings:Issuer").Value,
             Expires = DateTime.UtcNow.AddHours(1),
             SigningCredentials = signingcredentials
         };

          var tokenhandler = new JwtSecurityTokenHandler();

          var token = tokenhandler.CreateToken(tokendescriptor);

          return tokenhandler.WriteToken(token);
      }
      
}
