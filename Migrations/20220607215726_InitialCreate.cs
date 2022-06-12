using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace psw_ftn.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Admins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Expertise = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Doctors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Patients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckUps",
                columns: table => new
                {
                    CheckUpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientUserId = table.Column<int>(type: "int", nullable: true),
                    DoctorUserId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CancellationTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckUps", x => x.CheckUpId);
                    table.ForeignKey(
                        name: "FK_CheckUps_Doctors_DoctorUserId",
                        column: x => x.DoctorUserId,
                        principalTable: "Doctors",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckUps_Patients_PatientUserId",
                        column: x => x.PatientUserId,
                        principalTable: "Patients",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsForDisplay = table.Column<bool>(type: "bit", nullable: false),
                    Incognito = table.Column<bool>(type: "bit", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CancelledCheckUps",
                columns: table => new
                {
                    CancelledCheckUpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckUpId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    CancelationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancelledCheckUps", x => x.CancelledCheckUpId);
                    table.ForeignKey(
                        name: "FK_CancelledCheckUps_CheckUps_CheckUpId",
                        column: x => x.CheckUpId,
                        principalTable: "CheckUps",
                        principalColumn: "CheckUpId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CancelledCheckUps_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryCheckUps",
                columns: table => new
                {
                    CheckUpId = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryCheckUps", x => x.CheckUpId);
                    table.ForeignKey(
                        name: "FK_HistoryCheckUps_CheckUps_CheckUpId",
                        column: x => x.CheckUpId,
                        principalTable: "CheckUps",
                        principalColumn: "CheckUpId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CancelledCheckUps_CheckUpId",
                table: "CancelledCheckUps",
                column: "CheckUpId");

            migrationBuilder.CreateIndex(
                name: "IX_CancelledCheckUps_PatientId",
                table: "CancelledCheckUps",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckUps_DoctorUserId",
                table: "CheckUps",
                column: "DoctorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckUps_PatientUserId",
                table: "CheckUps",
                column: "PatientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_PatientId",
                table: "Feedbacks",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "CancelledCheckUps");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "HistoryCheckUps");

            migrationBuilder.DropTable(
                name: "CheckUps");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
