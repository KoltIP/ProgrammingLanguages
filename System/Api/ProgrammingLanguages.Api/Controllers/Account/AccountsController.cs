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

        [HttpDelete("")]
        public async Task Delete([FromQuery] string email)
        {
            await userAccountService.Delete(email);
        }
    }
}
