using AutoMapper;
using ProgrammingLanguages.Api.Controllers.Account.Models;
using ProgrammingLanguages.UserAccount;
using Microsoft.AspNetCore.Mvc;
using ProgrammingLanguages.UserAccount.Models;

namespace ProgrammingLanguages.Api.Controllers.Account
{
    [Route("api/v{version:apiVersion}/accounts")]
    [ApiController]
    [ApiVersion("1.0")]
    public class AccountsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<AccountsController> logger;
        private readonly IUserAccountService userAccountService;

        public AccountsController(IMapper mapper, ILogger<AccountsController> logger, IUserAccountService userAccountService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.userAccountService = userAccountService;
        }

        [HttpPost("")]
        public async Task<UserAccountResponse> Register([FromBody] RegisterUserAccountRequest request)
        {
            var user = await userAccountService.Create(mapper.Map<RegisterUserAccountModel>(request));

            var response = mapper.Map<UserAccountResponse>(user);

            return response;
        }

        [HttpGet("confirm/email")]
        public async Task ConfirmEmail([FromQuery] string email, [FromQuery] string code)
        {            
            await userAccountService.ConfirmEmail(email,code);
        }

        [HttpGet("inspect/{email}")]
        public async Task<bool> InspectEmail([FromRoute] string email)
        {
            return await userAccountService.InspectEmail(email);
        }

        [HttpGet("find/profile/{token}")]
        public async Task<UserAccountResponse> GetUser([FromRoute] string token)
        {
            var user = await userAccountService.GetUser(token);

            var response= mapper.Map<UserAccountResponse>(user);

            return response;

        }

        [HttpGet("change/name/{token}/{name}")]
        public async Task ChangeName([FromRoute] string token, [FromRoute] string name)
        {
            await userAccountService.ChangeName(token, name);
        }

        [HttpGet("change/email/{token}/{email}")]
        public async Task ChangeEmail([FromRoute] string token, [FromRoute] string email)
        {
            await userAccountService.ChangeEmail(token, email);
        }

        [HttpPost("change/password/{token}")]
        public async Task ChangePassword([FromRoute] string token, [FromBody] PasswordRequest request)
        {
            //PasswordModel model = mapper.Map<PasswordModel>(request);
            PasswordModel model = new PasswordModel()
            {
                NewPassword = request.NewPassword,
                OldPassword = request.OldPassword
            };
            await userAccountService.ChangePassword(token, model);
        }

        [HttpPost("forgot/password")]
        public async Task ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            //ForgotPasswordRequest passwordModel = mapper.Map<ForgotPasswordModel>(request);
            ForgotPasswordModel forgotPassword = new ForgotPasswordModel()
            {
                Email = request.Email,
                Password = request.Password
            };
            
            await userAccountService.ForgotPassword(forgotPassword);
        }

        [HttpGet("confirm/reset/password")]
        public async Task ConfirmResetPassword([FromQuery] string email, [FromQuery] string code, [FromQuery] string password)
        {
            await userAccountService.ConfirmForgotPassword(email, code, password);
        }
    }
}
