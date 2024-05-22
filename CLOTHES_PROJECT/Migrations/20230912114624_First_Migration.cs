using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CLOTHES_PROJECT.Migrations
{
    /// <inheritdoc />
    public partial class First_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DNUMBER = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MGRSSN = table.Column<int>(type: "int", nullable: true),
                    MGRStartDate = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DNUMBER);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    SSN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SUPERSSN = table.Column<int>(type: "int", nullable: true),
                    Fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Bdate = table.Column<DateTime>(type: "Date", nullable: false),
                    DNUM = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.SSN);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DNUM",
                        column: x => x.DNUM,
                        principalTable: "Departments",
                        principalColumn: "DNUMBER",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    PNUMBER = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNUM = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.PNUMBER);
                    table.ForeignKey(
                        name: "FK_Projects_Departments_DNUM",
                        column: x => x.DNUM,
                        principalTable: "Departments",
                        principalColumn: "DNUMBER",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Dependents",
                columns: table => new
                {
                    ESSN = table.Column<int>(type: "int", nullable: false),
                    Dependent_Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Bdate = table.Column<DateTime>(type: "Date", nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependents", x => new { x.ESSN, x.Dependent_Name });
                    table.ForeignKey(
                        name: "FK_Dependents_Employees_ESSN",
                        column: x => x.ESSN,
                        principalTable: "Employees",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WORKS_ON",
                columns: table => new
                {
                    ESSN = table.Column<int>(type: "int", nullable: false),
                    PNO = table.Column<int>(type: "int", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WORKS_ON", x => new { x.ESSN, x.PNO });
                    table.ForeignKey(
                        name: "FK_WORKS_ON_Employees_ESSN",
                        column: x => x.ESSN,
                        principalTable: "Employees",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WORKS_ON_Projects_PNO",
                        column: x => x.PNO,
                        principalTable: "Projects",
                        principalColumn: "PNUMBER",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dependents_ESSN",
                table: "Dependents",
                column: "ESSN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DNUM",
                table: "Employees",
                column: "DNUM");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_DNUM",
                table: "Projects",
                column: "DNUM");

            migrationBuilder.CreateIndex(
                name: "IX_WORKS_ON_PNO",
                table: "WORKS_ON",
                column: "PNO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dependents");

            migrationBuilder.DropTable(
                name: "WORKS_ON");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
