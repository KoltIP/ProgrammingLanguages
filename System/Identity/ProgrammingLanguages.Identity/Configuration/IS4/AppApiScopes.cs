﻿using Duende.IdentityServer.Models;
using ProgrammingLanguages.Shared.Common.Security;

namespace ProgrammingLanguages.Identity.Configuration.IS4
{
    public static class AppApiScopes
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
            new ApiScope(AppScopes.LanguageRead, "Access to books API - Read data"),
            new ApiScope(AppScopes.LanguageWrite, "Access to books API - Write data")
            };
    }
}
