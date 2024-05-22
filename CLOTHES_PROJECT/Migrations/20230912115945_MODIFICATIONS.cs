using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLOTHES_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class MODIFICATIONS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                   name: "FK_Employees_Employees_SUPERSSN",
                   table: "Employees",
                   column: "SUPERSSN",
                   principalTable: "Employees",
                   principalColumn: "SSN",
                   onDelete: ReferentialAction.NoAction);


            migrationBuilder.AddForeignKey(
                    name: "FK_Departments_Employees_MGRSSN",
                    table: "Departments",
                    column: "MGRSSN",
                    principalTable: "Employees",
                    principalColumn: "SSN",
                    onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
