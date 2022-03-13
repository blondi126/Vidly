using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Data.Migrations
{
    public partial class SeedUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

                INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'3443cac0-a821-43a4-ae41-36824f7c8dae', N'admin@vidly.com', N'ADMIN@VIDLY.COM', N'admin@vidly.com', N'ADMIN@VIDLY.COM', 0, N'AQAAAAEAACcQAAAAEG6jw6/SC94m9e7TWxmtYVZPiSirkXj0h7USmjubAKzFoc1UeMOI1wZH27pZqm9qZw==', N'GICFDKPBKFHDDXZ4DOXB7FJDC35DCGUA', N'98896f9d-2089-4dc1-98a7-d5c3ae8326f0', NULL, 0, 0, NULL, 1, 0)
                INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd47febcf-decb-43b7-8877-673c595edf9c', N'guest@vidly.com', N'GUEST@VIDLY.COM', N'guest@vidly.com', N'GUEST@VIDLY.COM', 0, N'AQAAAAEAACcQAAAAEKgcmiz6sz92J9ThpdlSohB947olxdBfqwSXVXWQ4BcuiaHQo/i1/pmpYM2GSsEX1w==', N'6JTPHHD6ID3JEITRJS6LG4XORN77XT6I', N'8dcac962-66a3-4da7-adf3-82bed9ea51b0', NULL, 0, 0, NULL, 1, 0)

                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'ae7d235f-c0fa-4f7c-84e1-7f67f0514af1', N'CanManageMovies', N'CanManageMovies', N'bfe754cc-8fd0-4838-8ae4-ffd99c336a78')

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3443cac0-a821-43a4-ae41-36824f7c8dae', N'ae7d235f-c0fa-4f7c-84e1-7f67f0514af1')
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
