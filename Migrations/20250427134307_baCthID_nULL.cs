using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkedOutApi.Migrations
{
    /// <inheritdoc />
    public partial class baCthID_nULL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Batches_BatchId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "BatchId",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Batches_BatchId",
                table: "Users",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Batches_BatchId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "BatchId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Batches_BatchId",
                table: "Users",
                column: "BatchId",
                principalTable: "Batches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
