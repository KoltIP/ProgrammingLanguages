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
        public async Task<UserAccountResponse> Register([FromQuery] RegisterUserAccountRequest request)
        {
            var user = await userAccountService.Create(mapper.Map<RegisterUserAccountModel>(request));

            var response = mapper.Map<UserAccountResponse>(user);

            return response;
        }

        [HttpDelete("")]
        public async Task Delete([FromQuery] string email)
        {
            await userAccountService.Delete(email);
        }
    }
}
