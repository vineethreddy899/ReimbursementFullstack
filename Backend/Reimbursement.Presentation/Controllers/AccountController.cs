using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Reimbursement.Application.DTOs;
using Reimbursement.Application.ServiceInterface;
using Reimbursement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reimbursement.Presentation.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AccountController(IUserService userService,ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("api/signup")]
        public async Task<ActionResult> SignUp([FromBody] SignUp userModel)
        {
            try
            {
                var appUser = new User
                {
                    Name = userModel.Name,
                    Email = userModel.Email
                };
                var result = await _userService.SignUp(userModel);
                if (result.Succeeded)
                {
                    return Ok(new NewUserDTO
                    {
                        Name = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser)
                    });
                }
                return Ok();
            }
            catch (Exception ep)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error ecord");
            }
        }

        [HttpPost]
        [Route("api/signin")]
        public async Task<IActionResult> SignIn([FromBody] SignIn userModel)
        {
            try
            {
                var appUser = new User
                {
                    Email = userModel.Email,
                    Name = userModel.Email.Split('@')[0]
                };
                var result = await _userService.SignIn(userModel);
                if (result.Succeeded)
                {
                    var r = await _userService.GetUserByEmail(userModel.Email);

                    return Ok(JsonConvert.SerializeObject(new { id = r.Id, isApprover = r.isApprover, token = _tokenService.CreateToken(appUser) }));
                }
                return Ok();
            }
            catch (Exception ep)
            {
                return BadRequest(new { message = ep.Message })
                ;
            }
        }

        [HttpPost]
        [Route("api/IsUniqueEmail")]
        public async Task<IActionResult> IsUniqueEmail([FromBody] object mail)
        {
            try
            {
                var q = JsonDocument.Parse(mail.ToString());

                var result = await _userService.GetUserByEmail(q.RootElement.GetProperty("mail").ToString());
                if (result == null)
                    return Ok(true);
                else return Ok(false);
            }
            catch (Exception ep)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
               "Error ecord");

            }
        }
    }
}
