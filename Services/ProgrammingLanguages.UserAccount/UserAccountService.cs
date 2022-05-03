using AutoMapper;
using ProgrammingLanguages.Db.Entities;
using Microsoft.AspNetCore.Identity;
using ProgrammingLanguages.Shared.Common.Exceptions;
using ProgrammingLanguages.RabbitMqService;
using ProgrammingLanguages.UserAccount.Models;
using ProgrammingLanguages.Shared.Common.Validator;
using ProgrammingLanguages.RabbitMqService.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ProgrammingLanguages.UserAccount
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly IRabbitMqTask rabbitMqTask;
        private readonly IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator;
        public UserAccountService(IMapper mapper, UserManager<User> userManager, IRabbitMqTask rabbitMqTask, IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.rabbitMqTask = rabbitMqTask;
            this.registerUserAccountModelValidator = registerUserAccountModelValidator;
        }

        public async Task<UserAccountModel> Create(RegisterUserAccountModel model)
        {
            registerUserAccountModelValidator.Check(model);

            // Find user by email
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
                throw new ProcessException($"User account with email {model.Email} already exist.");

            // Create user account
            user = new User()
            {
                Status = UserStatus.Active,
                FullName = model.Name,
                UserName = model.Email,  // Это логин. Мы будем его приравнивать к email, хотя это и не обязательно
                Email = model.Email,
                EmailConfirmed = false, // Так как это учебный проект, то сразу считаем, что почта подтверждена. В реальном проекте, скорее всего, надо будет ее подтвердить через ссылку в письме
                PhoneNumber = null,
                PhoneNumberConfirmed = false
                // ... Также здесь есть еще интересные свойства. Посмотрите в документации.
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new ProcessException($"Creating user account is wrong. {String.Join(", ", result.Errors.Select(s => s.Description))}");



            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var codeB = System.Text.Encoding.UTF8.GetBytes(code);
            code = System.Convert.ToBase64String(codeB);
            var url = $"http://localhost:20000/api/v1/accounts/confirm/email?email={user.Email}&code={code}";
            
            // Send email to user
            // !!! Обратите внимание, что мы не отправляем письмо, а создаем задание на его отправку. Дальше уже все сделается само другими сервисами.
            await rabbitMqTask.SendEmail(new EmailModel()
            {
                Email = model.Email,
                Subject = "ProgrammingLanguages",
                Message = $"Your account will be create successful if you move on <a href ='{url}'>Link</a>"  
            });


            // Returning the created user
            return mapper.Map<UserAccountModel>(user);
        }

        public async Task ConfirmEmail(string email, string code)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
                throw new ProcessException("The user was not found");

            var codeB = Convert.FromBase64String(code);
            code = System.Text.Encoding.UTF8.GetString(codeB);
            
            var result = await userManager.ConfirmEmailAsync(user,code);


            if (!result.Succeeded)
            { 
                throw new ProcessException("Could not confirm email.");
            }
        }

        public async Task<bool> InspectEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                throw new ProcessException("The user was not found");
            return user.EmailConfirmed;

        }

        public async Task<UserAccountModel> GetUser(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenSecurity = jsonToken as JwtSecurityToken;

            var id = tokenSecurity.Claims.First(claim => claim.Type == "sub").Value;

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                throw new ProcessException("User was not found");

            return mapper.Map<UserAccountModel>(user);
        }

        public async Task ChangeName(string token, string name)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenSecurity = jsonToken as JwtSecurityToken;

            var id = tokenSecurity.Claims.First(claim => claim.Type == "sub").Value;

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                throw new ProcessException("User was not found");

            user.FullName = name;
            await userManager.UpdateAsync(user);
        }


        public async Task ChangeEmail(string token, string email)
        {
            var userEmailExist = await userManager.FindByEmailAsync(email);
            if (userEmailExist != null)
                throw new ProcessException($"User account with email {email} already exist.");

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenSecurity = jsonToken as JwtSecurityToken;

            var id = tokenSecurity.Claims.First(claim => claim.Type == "sub").Value;

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                throw new ProcessException("User was not found");
            user.Email = email;
            user.UserName = email;
            user.EmailConfirmed = false;
            //добавить проверку почты
            await userManager.UpdateAsync(user);

            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var codeB = System.Text.Encoding.UTF8.GetBytes(code);
            code = Convert.ToBase64String(codeB);
            var url = $"http://localhost:20000/api/v1/accounts/confirm/email?email={user.Email}&code={code}";
            await rabbitMqTask.SendEmail(new EmailModel()
            {
                Email = email,
                Subject = "ProgrammingLanguages",
                Message = $"Your account will be create successful if you move on <a href ='{url}'>Link</a>"
            });
        }

        public async Task ChangePassword(string token, PasswordModel model)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenSecurity = jsonToken as JwtSecurityToken;

            var id = tokenSecurity.Claims.First(claim => claim.Type == "sub").Value;

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                throw new ProcessException("User was not found");
            var res = await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!res.Succeeded)
                throw new ProcessException("Wrong old password");
            await userManager.UpdateAsync(user);

        }

        public async Task ForgotPassword(ForgotPasswordModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                throw new ProcessException($"User was not found");

            var code = await userManager.GeneratePasswordResetTokenAsync(user);
            var codeBytes = System.Text.Encoding.UTF8.GetBytes(code);
            code = Convert.ToBase64String(codeBytes);

            var url = $"http://localhost:20000/api/v1/accounts/confirm/reset/password?email={user.Email}&code={code}&password={model.Password}";

            await rabbitMqTask.SendEmail(new EmailModel()
            {
                Email = model.Email,
                Subject = "ProgrammingLanguages",
                Message = $"Confirm password reset by following this <a href='{url}'>link</a>"
            });
        }

        public async Task ConfirmForgotPassword(string email, string code, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                throw new ProcessException("User was not found");

            var codeBase64 = Convert.FromBase64String(code);
            code = System.Text.Encoding.UTF8.GetString(codeBase64);

            var res = await userManager.ResetPasswordAsync(user, code, password);
            if (!res.Succeeded)
                throw new ProcessException("Could not confirm password reset");
        }
    }
}
