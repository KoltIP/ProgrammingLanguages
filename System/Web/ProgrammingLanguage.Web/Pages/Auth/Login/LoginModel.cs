﻿namespace ProgrammingLanguage.Web.Pages.Auth
{
    public class LoginModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; } = true;
    }
}
