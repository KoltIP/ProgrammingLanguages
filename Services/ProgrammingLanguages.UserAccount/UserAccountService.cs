﻿using AutoMapper;
using ProgrammingLanguages.Db.Entities;
using Microsoft.AspNetCore.Identity;
using ProgrammingLanguages.Shared.Common.Exceptions;
using ProgrammingLanguages.RabbitMqService;
using ProgrammingLanguages.UserAccount.Models;
using ProgrammingLanguages.Shared.Common.Validator;
using ProgrammingLanguages.RabbitMqService.Models;

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
                EmailConfirmed = true, // Так как это учебный проект, то сразу считаем, что почта подтверждена. В реальном проекте, скорее всего, надо будет ее подтвердить через ссылку в письме
                PhoneNumber = null,
                PhoneNumberConfirmed = false
                // ... Также здесь есть еще интересные свойства. Посмотрите в документации.
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                throw new ProcessException($"Creating user account is wrong. {String.Join(", ", result.Errors.Select(s => s.Description))}");


            // Send email to user
            // !!! Обратите внимание, что мы не отправляем письмо, а создаем задание на его отправку. Дальше уже все сделается само другими сервисами.
            await rabbitMqTask.SendEmail(new EmailModel()
            {
                Email = model.Email,
                Subject = "ProgrammingLanguages",
                Message = "Your account was registered successful"
            });


            // Returning the created user
            return mapper.Map<UserAccountModel>(user);
        }

        public async Task Delete(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            await userManager.DeleteAsync(user);
        }
    }
}
