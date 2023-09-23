using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodieLionApi.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "expired_at",
                table: "email_codes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 23, 7, 15, 29, 425, DateTimeKind.Utc).AddTicks(2393),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 9, 22, 18, 21, 44, 37, DateTimeKind.Utc).AddTicks(7292));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "email_codes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 23, 7, 0, 29, 425, DateTimeKind.Utc).AddTicks(2334),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 9, 22, 18, 6, 44, 37, DateTimeKind.Utc).AddTicks(7239));

            migrationBuilder.CreateTable(
                name: "home_images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    url = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_home_images", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "home_images");

            migrationBuilder.AlterColumn<DateTime>(
                name: "expired_at",
                table: "email_codes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 22, 18, 21, 44, 37, DateTimeKind.Utc).AddTicks(7292),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 9, 23, 7, 15, 29, 425, DateTimeKind.Utc).AddTicks(2393));

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "email_codes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2023, 9, 22, 18, 6, 44, 37, DateTimeKind.Utc).AddTicks(7239),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2023, 9, 23, 7, 0, 29, 425, DateTimeKind.Utc).AddTicks(2334));
        }
    }
}
