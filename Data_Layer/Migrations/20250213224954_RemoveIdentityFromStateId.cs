using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Layer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIdentityFromStateId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
           name: "FK_VacationRequests_RequestStates_RequestStateId",
           table: "VacationRequests");

            // Step 2: Drop the primary key constraint in RequestStates
            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestStates",
                table: "RequestStates");

            // Step 3: Create a temporary column to store the data (nullable)
            migrationBuilder.AddColumn<int>(
                name: "TempStateId",
                table: "RequestStates",
                nullable: true); // Allow null values

            // Step 4: Copy data from the identity column to the temporary column
            migrationBuilder.Sql("UPDATE RequestStates SET TempStateId = StateId");

            // Step 5: Drop the existing identity column
            migrationBuilder.DropColumn(
                name: "StateId",
                table: "RequestStates");

            // Step 6: Recreate the column without the identity property (nullable initially)
            migrationBuilder.AddColumn<int>(
      name: "StateId",
      table: "RequestStates",
      nullable: false,
      defaultValue: 0); // Allow null values initially

            // Step 7: Copy data back from the temporary column to the new column
            migrationBuilder.Sql("UPDATE RequestStates SET StateId = TempStateId");

            // Step 8: Update StateId to be non-nullable after copying data
            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "RequestStates",
                nullable: false);

            // Step 9: Drop the temporary column
            migrationBuilder.DropColumn(
                name: "TempStateId",
                table: "RequestStates");

            // Step 10: Add the primary key constraint back
            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestStates",
                table: "RequestStates",
                column: "StateId");

            // Step 11: Recreate the foreign key constraint in VacationRequests
            migrationBuilder.AddForeignKey(
                name: "FK_VacationRequests_RequestStates_RequestStateId",
                table: "VacationRequests",
                column: "RequestStateId",
                principalTable: "RequestStates",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
