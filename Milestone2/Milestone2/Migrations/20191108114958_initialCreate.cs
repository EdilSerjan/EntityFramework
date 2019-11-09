using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Milestone2.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Capcity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipCards",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    MemberId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipCards_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    CoachId = table.Column<long>(nullable: false),
                    RoomId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Coaches_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Coaches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Price = table.Column<int>(nullable: false),
                    RoomId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipments_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseMembers",
                columns: table => new
                {
                    CourseId = table.Column<long>(nullable: false),
                    MemberId = table.Column<long>(nullable: false),
                    Day = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseMembers", x => new { x.CourseId, x.MemberId });
                    table.ForeignKey(
                        name: "FK_CourseMembers_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseMembers_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { 1L, "tony@gmail.com", "Tony" });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { 2L, "mike@gmail.com", "Mike" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { 1L, "yedil@gmail.com", "Yedil" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Email", "Name" },
                values: new object[] { 2L, "lisa@gmail.com", "Lisa" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capcity" },
                values: new object[] { 1L, 20 });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capcity" },
                values: new object[] { 2L, 30 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CoachId", "Name", "RoomId" },
                values: new object[] { 2L, 1L, "Upper Body Workout", 1L });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CoachId", "Name", "RoomId" },
                values: new object[] { 1L, 2L, "Yoga", 2L });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "Id", "Name", "Price", "RoomId" },
                values: new object[] { 2L, "Dumbell", 5000, 1L });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "Id", "Name", "Price", "RoomId" },
                values: new object[] { 1L, "Yoga ball", 2000, 2L });

            migrationBuilder.InsertData(
                table: "MembershipCards",
                columns: new[] { "Id", "CreatedAt", "MemberId" },
                values: new object[] { 1L, new DateTime(2019, 11, 8, 17, 49, 57, 972, DateTimeKind.Local).AddTicks(5200), 1L });

            migrationBuilder.InsertData(
                table: "MembershipCards",
                columns: new[] { "Id", "CreatedAt", "MemberId" },
                values: new object[] { 2L, new DateTime(2019, 11, 8, 17, 49, 57, 973, DateTimeKind.Local).AddTicks(7262), 2L });

            migrationBuilder.InsertData(
                table: "CourseMembers",
                columns: new[] { "CourseId", "MemberId", "Day" },
                values: new object[] { 2L, 1L, "Monday" });

            migrationBuilder.InsertData(
                table: "CourseMembers",
                columns: new[] { "CourseId", "MemberId", "Day" },
                values: new object[] { 1L, 2L, "Tuesday" });

            migrationBuilder.CreateIndex(
                name: "IX_CourseMembers_MemberId",
                table: "CourseMembers",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CoachId",
                table: "Courses",
                column: "CoachId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_RoomId",
                table: "Courses",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_RoomId",
                table: "Equipments",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipCards_MemberId",
                table: "MembershipCards",
                column: "MemberId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseMembers");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "MembershipCards");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
