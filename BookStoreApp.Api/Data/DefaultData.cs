using Microsoft.AspNetCore.Identity;

namespace BookStoreApp.Api.Data;

public static class DefaultData
{
    public static IdentityRole[] DefaultRoles
    {
        get
        {
            return new[]
            {
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = "965ce87c-0aad-4a29-8437-e0aaa009f684",
                },

                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = "ff4ec3ff-8d44-4142-985f-41e95c46efba",
                }
            };
        }
    }

    public static ApiUser[] DefaultUsers
    {
        get
        {
            var hasher = new PasswordHasher<ApiUser>();
            return new[]
            {
                new ApiUser
                {
                    Email = "admin@bookstore.com",
                    NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                    UserName = "admin@bookstore.com",
                    NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                    Id = "a7fb1a78-daa2-4df8-b9a9-3417de79dd54",
                    FirstName = "System",
                    LastName = "Admin",
                    PasswordHash = hasher.HashPassword(null, "admin123")
                },

                new ApiUser
                {
                    Email = "user1@bookstore.com",
                    NormalizedEmail = "USER1@BOOKSTORE.COM",
                    UserName = "user1@bookstore.com",
                    NormalizedUserName = "USER1@BOOKSTORE.COM",
                    Id = "9ce9d321-d7f9-4013-8a9b-42d0163ec65c",
                    FirstName = "System",
                    LastName = "User",
                    PasswordHash = hasher.HashPassword(null, "admin123")
                }
            };
        }
    }

    public static IdentityUserRole<string>[] DefaultUserRoles
    {
        get
        {
            var hasher = new PasswordHasher<ApiUser>();
            return new[]
            {
                new IdentityUserRole<string>()
                {
                    RoleId = "ff4ec3ff-8d44-4142-985f-41e95c46efba",
                    UserId = "a7fb1a78-daa2-4df8-b9a9-3417de79dd54"

                },

                new IdentityUserRole<string>
                {
                    RoleId = "965ce87c-0aad-4a29-8437-e0aaa009f684",
                    UserId = "9ce9d321-d7f9-4013-8a9b-42d0163ec65c"
                }
            };
        }
    }
}
