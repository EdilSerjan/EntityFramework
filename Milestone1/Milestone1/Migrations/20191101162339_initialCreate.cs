using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Milestone1.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    tel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    telephone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    capcity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MembershipCards",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    createdAt = table.Column<DateTime>(nullable: false),
                    memberId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipCards", x => x.id);
                    table.ForeignKey(
                        name: "FK_MembershipCards_Members_memberId",
                        column: x => x.memberId,
                        principalTable: "Members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    coachId = table.Column<long>(nullable: false),
                    roomId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.id);
                    table.ForeignKey(
                        name: "FK_Courses_Coaches_coachId",
                        column: x => x.coachId,
                        principalTable: "Coaches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Rooms_roomId",
                        column: x => x.roomId,
                        principalTable: "Rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true),
                    price = table.Column<int>(nullable: false),
                    roomId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Equipments_Rooms_roomId",
                        column: x => x.roomId,
                        principalTable: "Rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    courseId = table.Column<long>(nullable: false),
                    memberId = table.Column<long>(nullable: false),
                    day = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => new { x.courseId, x.memberId });
                    table.ForeignKey(
                        name: "FK_Schedules_Courses_courseId",
                        column: x => x.courseId,
                        principalTable: "Courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Members_memberId",
                        column: x => x.memberId,
                        principalTable: "Members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "id", "name", "tel" },
                values: new object[] { 1L, "Tony", "+13625623" });

            migrationBuilder.InsertData(
                table: "Coaches",
                columns: new[] { "id", "name", "tel" },
                values: new object[] { 2L, "Mike", "+73473827" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "id", "name", "telephone" },
                values: new object[] { 1L, "Yedil", "+77771234567" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "id", "name", "telephone" },
                values: new object[] { 2L, "Lisa", "+21233523343" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "id", "capcity" },
                values: new object[] { 1L, 20 });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "id", "capcity" },
                values: new object[] { 2L, 30 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "id", "coachId", "name", "roomId" },
                values: new object[] { 2L, 1L, "Upper Body Workout", 1L });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "id", "coachId", "name", "roomId" },
                values: new object[] { 1L, 2L, "Yoga", 2L });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "id", "name", "price", "roomId" },
                values: new object[] { 2L, "Dumbell", 5000, 1L });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "id", "name", "price", "roomId" },
                values: new object[] { 1L, "Yoga ball", 2000, 2L });

            migrationBuilder.InsertData(
                table: "MembershipCards",
                columns: new[] { "id", "createdAt", "memberId" },
                values: new object[] { 1L, new DateTime(2019, 11, 1, 22, 23, 38, 948, DateTimeKind.Local).AddTicks(5020), 1L });

            migrationBuilder.InsertData(
                table: "MembershipCards",
                columns: new[] { "id", "createdAt", "memberId" },
                values: new object[] { 2L, new DateTime(2019, 11, 1, 22, 23, 38, 960, DateTimeKind.Local).AddTicks(4380), 2L });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "courseId", "memberId", "day" },
                values: new object[] { 2L, 1L, 1 });

            migrationBuilder.InsertData(
                table: "Schedules",
                columns: new[] { "courseId", "memberId", "day" },
                values: new object[] { 1L, 2L, 4 });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_coachId",
                table: "Courses",
                column: "coachId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_roomId",
                table: "Courses",
                column: "roomId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_roomId",
                table: "Equipments",
                column: "roomId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipCards_memberId",
                table: "MembershipCards",
                column: "memberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_memberId",
                table: "Schedules",
                column: "memberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "MembershipCards");

            migrationBuilder.DropTable(
                name: "Schedules");

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
