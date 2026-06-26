using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIpForensicsReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IpForensicsReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    IpAddress = table.Column<string>(type: "longtext", nullable: false),
                    AbuseConfidenceScore = table.Column<string>(type: "longtext", nullable: false),
                    TotalReports = table.Column<string>(type: "longtext", nullable: false),
                    LastReportedDate = table.Column<string>(type: "longtext", nullable: false),
                    Continent = table.Column<string>(type: "longtext", nullable: false),
                    Country = table.Column<string>(type: "longtext", nullable: false),
                    Region = table.Column<string>(type: "longtext", nullable: false),
                    City = table.Column<string>(type: "longtext", nullable: false),
                    Mobile = table.Column<string>(type: "longtext", nullable: false),
                    Proxy = table.Column<string>(type: "longtext", nullable: false),
                    Hosting = table.Column<string>(type: "longtext", nullable: false),
                    Tor = table.Column<string>(type: "longtext", nullable: false),
                    CreatedDate = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpForensicsReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IpForensicsReport_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_IpForensicsReport_UserId",
                table: "IpForensicsReport",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IpForensicsReport");
        }
    }
}
