using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProgrammingLanguages.Db.Contex.Migrations
{
    public partial class fixIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_languages_category_CategoryId",
                table: "languages");

            migrationBuilder.DropForeignKey(
                name: "FK_operator_languages_LanguageId",
                table: "operator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_operator",
                table: "operator");

            migrationBuilder.DropPrimaryKey(
                name: "PK_category",
                table: "category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "operator",
                newName: "operators");

            migrationBuilder.RenameTable(
                name: "category",
                newName: "categories");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "user_tokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "user_role_owners");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "user_logins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "user_claims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "user_roles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "user_role_claims");

            migrationBuilder.RenameIndex(
                name: "IX_operator_Uid",
                table: "operators",
                newName: "IX_operators_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_operator_Name",
                table: "operators",
                newName: "IX_operators_Name");

            migrationBuilder.RenameIndex(
                name: "IX_operator_LanguageId",
                table: "operators",
                newName: "IX_operators_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_category_Uid",
                table: "categories",
                newName: "IX_categories_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_category_Name",
                table: "categories",
                newName: "IX_categories_Name");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "user_role_owners",
                newName: "IX_user_role_owners_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "user_logins",
                newName: "IX_user_logins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "user_claims",
                newName: "IX_user_claims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "user_role_claims",
                newName: "IX_user_role_claims_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_operators",
                table: "operators",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categories",
                table: "categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_tokens",
                table: "user_tokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_role_owners",
                table: "user_role_owners",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_logins",
                table: "user_logins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_claims",
                table: "user_claims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_role_claims",
                table: "user_role_claims",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_languages_categories_CategoryId",
                table: "languages",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_operators_languages_LanguageId",
                table: "operators",
                column: "LanguageId",
                principalTable: "languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_claims_users_UserId",
                table: "user_claims",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_logins_users_UserId",
                table: "user_logins",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_claims_user_roles_RoleId",
                table: "user_role_claims",
                column: "RoleId",
                principalTable: "user_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_owners_user_roles_RoleId",
                table: "user_role_owners",
                column: "RoleId",
                principalTable: "user_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_role_owners_users_UserId",
                table: "user_role_owners",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_tokens_users_UserId",
                table: "user_tokens",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_languages_categories_CategoryId",
                table: "languages");

            migrationBuilder.DropForeignKey(
                name: "FK_operators_languages_LanguageId",
                table: "operators");

            migrationBuilder.DropForeignKey(
                name: "FK_user_claims_users_UserId",
                table: "user_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_user_logins_users_UserId",
                table: "user_logins");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_claims_user_roles_RoleId",
                table: "user_role_claims");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_owners_user_roles_RoleId",
                table: "user_role_owners");

            migrationBuilder.DropForeignKey(
                name: "FK_user_role_owners_users_UserId",
                table: "user_role_owners");

            migrationBuilder.DropForeignKey(
                name: "FK_user_tokens_users_UserId",
                table: "user_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_tokens",
                table: "user_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_role_owners",
                table: "user_role_owners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_role_claims",
                table: "user_role_claims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_logins",
                table: "user_logins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_claims",
                table: "user_claims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_operators",
                table: "operators");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categories",
                table: "categories");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "user_tokens",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "user_roles",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "user_role_owners",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "user_role_claims",
                newName: "AspNetRoleClaims");

            migrationBuilder.RenameTable(
                name: "user_logins",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "user_claims",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "operators",
                newName: "operator");

            migrationBuilder.RenameTable(
                name: "categories",
                newName: "category");

            migrationBuilder.RenameIndex(
                name: "IX_user_role_owners_RoleId",
                table: "AspNetUserRoles",
                newName: "IX_AspNetUserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_user_role_claims_RoleId",
                table: "AspNetRoleClaims",
                newName: "IX_AspNetRoleClaims_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_user_logins_UserId",
                table: "AspNetUserLogins",
                newName: "IX_AspNetUserLogins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_user_claims_UserId",
                table: "AspNetUserClaims",
                newName: "IX_AspNetUserClaims_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_operators_Uid",
                table: "operator",
                newName: "IX_operator_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_operators_Name",
                table: "operator",
                newName: "IX_operator_Name");

            migrationBuilder.RenameIndex(
                name: "IX_operators_LanguageId",
                table: "operator",
                newName: "IX_operator_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_categories_Uid",
                table: "category",
                newName: "IX_category_Uid");

            migrationBuilder.RenameIndex(
                name: "IX_categories_Name",
                table: "category",
                newName: "IX_category_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserTokens",
                table: "AspNetUserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoles",
                table: "AspNetRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserRoles",
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRoleClaims",
                table: "AspNetRoleClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserLogins",
                table: "AspNetUserLogins",
                columns: new[] { "LoginProvider", "ProviderKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserClaims",
                table: "AspNetUserClaims",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_operator",
                table: "operator",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_category",
                table: "category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_languages_category_CategoryId",
                table: "languages",
                column: "CategoryId",
                principalTable: "category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_operator_languages_LanguageId",
                table: "operator",
                column: "LanguageId",
                principalTable: "languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
