using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class MovimentationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "movimentations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovimentationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movimentations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_movimentations_books_BookId",
                        column: x => x.BookId,
                        principalTable: "books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movimentations_users_UserId",
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
                    { new Guid("00ca00fd-3852-47cc-93e2-5a59385a03e1"), "J R R Tolkien", "George Allen & Unwin", "Frodo and your friends set out on an adventure", "The Lord of the rings - The return of the king" },
                    { new Guid("3792ab2a-e0c4-4fc0-b9a5-ea7d55be84a3"), "J R R Tolkien", "Mariner Books", "Bilbo and your friends fighting with a dragon", "The Hobbit: The desolation of Smaug" },
                    { new Guid("525d2f71-b70c-4e5f-9872-60163d7d6057"), "J R R Tolkien", "George Allen & Unwin", "Frodo and your friends set out on an adventure", "The Lord of the rings - The fellowship of the ring" },
                    { new Guid("6dde63d5-6f99-4184-8b3f-e3e392d968dd"), "J R R Tolkien", "George Allen & Unwin", "Frodo and your friends set out on an adventure", "The Lord of the rings - The two towers" },
                    { new Guid("e56081dc-8a7a-475e-a5a6-5f3700d28053"), "J R R Tolkien", "Mariner Books", "Bilbo and your friends fighting with a dragon", "The Hobbit: The battle of the five arms" },
                    { new Guid("f1099753-7a05-475b-a63b-47a631cb4289"), "J R R Tolkien", "Mariner Books", "Bilbo and your friends fighting with a dragon", "The Hobbit: An Unexpected Journey" },
                    { new Guid("fc99d3c1-4c71-40b3-b575-ddb2535b595d"), "Niccolo Machiavelli", "Antonio Blado d'Asola", "It's about Machiavelli vision", "The Prince" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Email", "Password", "Role", "UserName" },
                values: new object[] { new Guid("0b4cca2a-c4f6-4132-b7c5-f9c8e0f8229d"), "admin@library.com", "admin", "Manager", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_movimentations_BookId",
                table: "movimentations",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_movimentations_UserId",
                table: "movimentations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movimentations");

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("00ca00fd-3852-47cc-93e2-5a59385a03e1"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("3792ab2a-e0c4-4fc0-b9a5-ea7d55be84a3"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("525d2f71-b70c-4e5f-9872-60163d7d6057"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("6dde63d5-6f99-4184-8b3f-e3e392d968dd"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("e56081dc-8a7a-475e-a5a6-5f3700d28053"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("f1099753-7a05-475b-a63b-47a631cb4289"));

            migrationBuilder.DeleteData(
                table: "books",
                keyColumn: "Id",
                keyValue: new Guid("fc99d3c1-4c71-40b3-b575-ddb2535b595d"));

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: new Guid("0b4cca2a-c4f6-4132-b7c5-f9c8e0f8229d"));

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
        }
    }
}
