using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace phone_book_app.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateLabelSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Label",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValueSql: "(getutcdate())"),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Label");
        }
    }
}
