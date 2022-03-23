using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using HR.Models;
using HR.Models.User;
using HR.Services.Services;
using System.Reflection.Metadata;
using HR.Core;

namespace HRManagement.Controllers
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly IConfiguration _config;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IConfiguration config, ILogger<UsersController> logger, IUserService userService)
        {
            _userService = userService;
            _config = config;
            _logger = logger;
        }

        /// <summary>
        /// Authenticate
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public ApiResponse<UserCommon> Authenticate([FromBody]AuthenticateModel model)
        {
            try
            {
                var secret = _config.GetSection("JwtConfig").GetSection("secret").Value;
                string msgError;
                var result = _userService.Authenticate(model.Username.Trim(), model.Password.Trim(), secret, out msgError);

                return new ApiResponse<UserCommon>(result, msgError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.InnerException);
                return new ApiResponse<UserCommon>(null, ex.Message);
            }
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]UserModel model)
        {
            try
            {
                var secret = _config.GetSection("JwtConfig").GetSection("secret").Value;
                string msgError;
                if (string.IsNullOrEmpty(model.Password))
                {
                    msgError = "Mật khẩu không được để trống.";
                    return BadRequest(new ApiResponse<UserCommon>(null, msgError));
                }

                var user = _userService.CreateUser(model, secret, out msgError);
                var response = new ApiResponse<UserCommon>(user, msgError);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode((int)System.Net.HttpStatusCode.InternalServerError, new ApiResponse<bool>(false, HR.Core.Constants.ErrorOnProcess));
            }
        }
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="textSearch"></param>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        //[Authorize(Roles = RoleHelper.Admin)]
        [HttpGet]
        [Route("search")]
        public ApiResponse<UserSearchModel> Search(string textSearch, int currentPage, int pageSize, string sortColumn, string sortDirection)
        {
            //var claimsIdentity = User.Identity as ClaimsIdentity;
            //var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            //var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;
            int totalRecords;
            var model = new UserSearchModel
            {
                Users = _userService.SearchUser(currentPage, pageSize, textSearch, sortColumn, sortDirection, out totalRecords),
                SortColumn = sortColumn,
                SortDirection = sortDirection,
                PageIndex = currentPage,
                PageSize = pageSize,
                TotalRecords = totalRecords,
            };
            return new ApiResponse<UserSearchModel>(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("forgotPassword")]
        public IActionResult ResetPassword(string toEmail)
        {
            string message;
            if (!_userService.ValidationEmail(toEmail, out message))
            {
                return BadRequest(new ApiResponse<string>(toEmail, message));
            }

            string smtpServer = _config.GetValue<string>("EmailSettings:smtp_server");
            int smtpPort = Convert.ToInt32(_config.GetValue<string>("EmailSettings:smtp_port"));
            bool smtpusessl = Convert.ToBoolean(_config.GetValue<string>("EmailSettings:smtp_usessl"));
            string smtpusername = _config.GetValue<string>("EmailSettings:smtp_usename");
            string smtppassword = _config.GetValue<string>("EmailSettings:smtp_password");
            string emailaddress = _config.GetValue<string>("EmailSettings:smtp_emailaddress");
            var result = _userService.ForgotPassword(emailaddress, toEmail, smtpServer, smtpPort, smtpusessl, smtpusername, smtppassword, smtpServer, out message);
            if (!result)
            {
                return BadRequest(new ApiResponse<bool>(false, message));
            }
            message = Constants.ResetPasswordResult;
            var response = new ApiResponse<bool>(true, message);
            return Ok(response);
        }
    }
}