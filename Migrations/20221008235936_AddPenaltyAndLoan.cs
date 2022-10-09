using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class AddPenaltyAndLoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoanDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeadlineDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DevolutionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Returned = table.Column<bool>(type: "bit", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_books_BookId",
                        column: x => x.BookId,
                        principalTable: "books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Loans_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "penalties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PenaltyPrice = table.Column<float>(type: "real", nullable: false),
                    Settled = table.Column<bool>(type: "bit", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_penalties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_penalties_books_BookId",
                        column: x => x.BookId,
                        principalTable: "books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_penalties_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "books",
                columns: new[] { "Id", "Author", "PublishCompany", "Subject", "Title" },
                values: new object[,]
                {
                    { new Guid("1807e40d-18ee-4b46-9d47-6fe6d57f0534"), "J R R Tolkien", "George Allen & Unwin", "Frodo and your friends set out on an adventure", "The Lord of the rings - The two towers" },
                    { new Guid("601cac44-40c8-428e-9268-90e74466240c"), "J R R Tolkien", "George Allen & Unwin", "Frodo and your friends set out on an adventure", "The Lord of the rings - The return of the king" },
                    { new Guid("85c49318-4d06-41f0-836f-7c3032e8bd1d"), "Niccolo Machiavelli", "Antonio Blado d'Asola", "It's about Machiavelli vision", "The Prince" },
                    { new Guid("b0470a59-3ee7-4b06-aa81-e43c213f6cd5"), "J R R Tolkien", "George Allen & Unwin", "Frodo and your friends set out on an adventure", "The Lord of the rings - The fellowship of the ring" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Email", "Password", "Role", "UserName" },
                values: new object[] { new Guid("263a179f-75e4-4b98-81ae-4f1d9945753b"), "admin@library.com", "admin", "Manager", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BookId",
                table: "Loans",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_UserId",
                table: "Loans",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_penalties_BookId",
                table: "penalties",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_penalties_UserId",
                table: "penalties",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "penalties");

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("1807e40d-18ee-4b46-9d47-6fe6d57f0534"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("601cac44-40c8-428e-9268-90e74466240c"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("85c49318-4d06-41f0-836f-7c3032e8bd1d"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("b0470a59-3ee7-4b06-aa81-e43c213f6cd5"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("263a179f-75e4-4b98-81ae-4f1d9945753b"));
        }
    }
}
