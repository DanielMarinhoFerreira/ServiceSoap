using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceSoap.Migrations
{
    /// <inheritdoc />
    public partial class teste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID_USER = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME_USER = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    SURNAME_USER = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    LOGIN_USER = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    EMAIL_USER = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PASSWORD_USER = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID_USER);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERS");
        }
    }
}
