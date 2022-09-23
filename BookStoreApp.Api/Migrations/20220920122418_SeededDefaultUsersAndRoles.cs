using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.Api.Migrations
{
    public partial class SeededDefaultUsersAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "965ce87c-0aad-4a29-8437-e0aaa009f684", "d9515694-f572-4133-9eed-83b065ac8418", "User", "USER" },
                    { "ff4ec3ff-8d44-4142-985f-41e95c46efba", "b1029ae2-d989-455a-9737-cdb8ac827123", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "9ce9d321-d7f9-4013-8a9b-42d0163ec65c", 0, "7dd68ebd-d3f9-4e30-88e7-a25f59f27b81", "user1@bookstore.com", false, "System", "User", false, null, "USER1@BOOKSTORE.COM", "USER1@BOOKSTORE.COM", "AQAAAAEAACcQAAAAELYIwA8+cTOBLsMxCSkT2Jd9kgT8YVJ4iQKp8tt9LLZO8xC5gYm6v/NHG7eqJiibBg==", null, false, "ea9c0145-cb63-4738-be3f-1553b79994d5", false, "user1@bookstore.com" },
                    { "a7fb1a78-daa2-4df8-b9a9-3417de79dd54", 0, "be3af002-b108-486a-8c72-d11032b56b14", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEDGNYD9U6oOYYEmGg/WxFae0xTqofjXoIpc41h6OmvBUVYMrlJHilkQa80K52Hzy2A==", null, false, "13ff2293-b579-4ab6-899b-b8fe66ef2469", false, "admin@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "965ce87c-0aad-4a29-8437-e0aaa009f684", "9ce9d321-d7f9-4013-8a9b-42d0163ec65c" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ff4ec3ff-8d44-4142-985f-41e95c46efba", "a7fb1a78-daa2-4df8-b9a9-3417de79dd54" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "965ce87c-0aad-4a29-8437-e0aaa009f684", "9ce9d321-d7f9-4013-8a9b-42d0163ec65c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ff4ec3ff-8d44-4142-985f-41e95c46efba", "a7fb1a78-daa2-4df8-b9a9-3417de79dd54" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "965ce87c-0aad-4a29-8437-e0aaa009f684");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff4ec3ff-8d44-4142-985f-41e95c46efba");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ce9d321-d7f9-4013-8a9b-42d0163ec65c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a7fb1a78-daa2-4df8-b9a9-3417de79dd54");
        }
    }
}
