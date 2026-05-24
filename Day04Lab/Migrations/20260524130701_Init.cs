using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Day04Lab.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Date_Of_Production = table.Column<DateOnly>(type: "date", nullable: false),
                    Date_Of_Expire = table.Column<DateOnly>(type: "date", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Dry Groceries" },
                    { 2, "Dairy & Eggs" },
                    { 3, "Meat & Frozen Food" },
                    { 4, "Canned Goods" },
                    { 5, "Beverages" },
                    { 6, "Snacks & Sweets" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoriesId", "Count", "Date_Of_Expire", "Date_Of_Production", "Description", "Price", "Title" },
                values: new object[,]
                {
                    { 1, 1, 150, new DateOnly(2027, 1, 10), new DateOnly(2026, 1, 10), "High-quality natural white rice, 1kg pack", 35, "Premium Egyptian Rice" },
                    { 2, 1, 200, new DateOnly(2027, 2, 1), new DateOnly(2026, 2, 1), "Made from 100% pure durum wheat semolina, 400g", 15, "Penne Pasta" },
                    { 3, 1, 80, new DateOnly(2027, 1, 5), new DateOnly(2026, 1, 5), "Pure refined sunflower oil for cooking and frying, 1L", 80, "Sunflower Oil" },
                    { 4, 2, 60, new DateOnly(2026, 11, 1), new DateOnly(2026, 5, 1), "Pasteurized and sterilized natural cow milk, 1L", 42, "Full Cream Milk" },
                    { 5, 2, 90, new DateOnly(2026, 10, 15), new DateOnly(2026, 4, 15), "Smooth vegetable oil feta cheese, 500g", 38, "Feta White Cheese" },
                    { 6, 2, 120, new DateOnly(2026, 6, 4), new DateOnly(2026, 5, 20), "Fresh plain yogurt cup, 105g", 8, "Natural Yogurt" },
                    { 7, 3, 45, new DateOnly(2026, 9, 1), new DateOnly(2026, 3, 1), "Pure beef burger patties ready for grilling, 1kg", 160, "Frozen Beef Burger" },
                    { 8, 3, 40, new DateOnly(2026, 8, 20), new DateOnly(2026, 2, 20), "Breaded and seasoned chicken breast fillets, 1kg", 180, "Frozen Chicken Pane" },
                    { 9, 4, 110, new DateOnly(2027, 7, 15), new DateOnly(2026, 1, 15), "Natural rich tomato paste jar, 360g", 22, "Tomato Paste Puree" },
                    { 10, 4, 130, new DateOnly(2029, 1, 1), new DateOnly(2026, 1, 1), "Easy-open canned solid tuna in vegetable oil, 185g", 55, "Solid Tuna" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoriesId",
                table: "Products",
                column: "CategoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
