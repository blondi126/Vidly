using Microsoft.EntityFrameworkCore.Migrations;

namespace Vidly.Data.Migrations
{
    public partial class SetGenresAndMembershipTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO MembershipType (Id, SignUpFee, DurationInMonths, DiscountRate, Name) VALUES (1, 0, 0, 0, 'Pay as You Go' )");
            migrationBuilder.Sql("INSERT INTO MembershipType (Id, SignUpFee, DurationInMonths, DiscountRate, Name) VALUES (2, 30, 1, 10, 'Monthly')");
            migrationBuilder.Sql("INSERT INTO MembershipType (Id, SignUpFee, DurationInMonths, DiscountRate, Name) VALUES (3, 90, 3, 15, 'Quarterly')");
            migrationBuilder.Sql("INSERT INTO MembershipType (Id, SignUpFee, DurationInMonths, DiscountRate, Name) VALUES (4, 300, 12, 20, 'Annual')");

            migrationBuilder.Sql("INSERT INTO Genre (Name) VALUES ('Action')");
            migrationBuilder.Sql("INSERT INTO Genre (Name) VALUES ('Thriller')");
            migrationBuilder.Sql("INSERT INTO Genre (Name) VALUES ('Family')");
            migrationBuilder.Sql("INSERT INTO Genre (Name) VALUES ('Romance')");
            migrationBuilder.Sql("INSERT INTO Genre (Name) VALUES ('Comedy')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
