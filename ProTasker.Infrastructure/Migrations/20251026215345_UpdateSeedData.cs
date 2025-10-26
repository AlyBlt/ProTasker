using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProTasker.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskHistories",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "AssignedUserId", "CreatedAt", "Description", "DueDate", "Status", "TeamId", "Title" },
                values: new object[] { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 10, 26, 12, 0, 0, 0, DateTimeKind.Unspecified), "Initialize project repository and structure", null, "Todo", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Setup Project" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "UserName",
                value: "Admin");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "UserName",
                value: "TeamLeader");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "UserName",
                value: "Member");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Role", "TeamId", "UserName" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), "alice@protasker.com", "hashedpassword", "Member", null, "Alice" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "bob@protasker.com", "hashedpassword", "TeamLeader", null, "Bob" }
                });

            migrationBuilder.InsertData(
                table: "TaskHistories",
                columns: new[] { "Id", "Action", "CreatedAt", "NewValue", "OldValue", "PerformedByUserId", "TaskId" },
                values: new object[] { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), "Created", new DateTime(2025, 10, 26, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, new Guid("33333333-3333-3333-3333-333333333333"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc") });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "AssignedUserId", "CreatedAt", "Description", "DueDate", "Status", "TeamId", "Title" },
                values: new object[] { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 10, 26, 12, 30, 0, 0, DateTimeKind.Unspecified), "Create database schema and tables", null, "InProgress", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Design Database" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "Description", "LeaderId", "Name" },
                values: new object[] { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Second Team", new Guid("55555555-5555-5555-5555-555555555555"), "Beta Team" });

            migrationBuilder.InsertData(
                table: "TaskHistories",
                columns: new[] { "Id", "Action", "CreatedAt", "NewValue", "OldValue", "PerformedByUserId", "TaskId" },
                values: new object[] { new Guid("aaaaaaaa-ffff-ffff-ffff-ffffffffffff"), "Updated", new DateTime(2025, 10, 26, 12, 45, 0, 0, DateTimeKind.Unspecified), null, null, new Guid("44444444-4444-4444-4444-444444444444"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "AssignedUserId", "CreatedAt", "Description", "DueDate", "Status", "TeamId", "Title" },
                values: new object[] { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 10, 26, 13, 0, 0, 0, DateTimeKind.Unspecified), "Develop REST API endpoints", null, "Todo", new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "API Implementation" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TaskHistories",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-ffff-ffff-ffff-ffffffffffff"));

            migrationBuilder.DeleteData(
                table: "TaskHistories",
                keyColumn: "Id",
                keyValue: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "AssignedUserId", "CreatedAt", "Description", "DueDate", "Status", "TeamId", "Title" },
                values: new object[] { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 10, 26, 12, 0, 0, 0, DateTimeKind.Unspecified), "Initialize project repository and structure", null, "Todo", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Setup Project" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "UserName",
                value: "admin");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "UserName",
                value: "teamleader");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "UserName",
                value: "member");

            migrationBuilder.InsertData(
                table: "TaskHistories",
                columns: new[] { "Id", "Action", "CreatedAt", "NewValue", "OldValue", "PerformedByUserId", "TaskId" },
                values: new object[] { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Created", new DateTime(2025, 10, 26, 12, 0, 0, 0, DateTimeKind.Unspecified), null, null, new Guid("33333333-3333-3333-3333-333333333333"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") });
        }
    }
}
