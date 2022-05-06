using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("16ec2435-9f86-4b6b-a34c-afec6d55116f"), "adminemail@gmail.com", "Admin", "$2a$11$3pcU0/38tt2LLKbj/FtxDOLyg6UMPodRuiYGZu8zbPeqcZ0f670Tq", "Admin" },
                    { new Guid("2ba1d6d4-b595-4455-8c71-96b2fd6aa24d"), "manageremail@gmail.com", "Manager", "$2a$11$Hx8SlauJqRzLLaMVoYwOoeCXNFpcko5gcKdpFBEek.qBJVwOprXVq", "Manager" },
                    { new Guid("8222c3fb-be78-4e59-b9c2-296aec2591ca"), "useremail@gmail.com", "User", "$2a$11$FF1ZvmaC6EBc0Pxo0gKXF.loFUbPcAQtwpPmkV9bIV3rlu.vO0QHO", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("16ec2435-9f86-4b6b-a34c-afec6d55116f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2ba1d6d4-b595-4455-8c71-96b2fd6aa24d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8222c3fb-be78-4e59-b9c2-296aec2591ca"));
        }
    }
}
