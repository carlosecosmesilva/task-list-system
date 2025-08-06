using Microsoft.EntityFrameworkCore.Migrations;

public partial class RemoveDisplayOrderUniqueIndex : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_Tarefas_DisplayOrder",
            table: "Tarefas");

        migrationBuilder.CreateIndex(
            name: "IX_Tarefas_DisplayOrder",
            table: "Tarefas",
            column: "DisplayOrder",
            unique: false);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(
            name: "IX_Tarefas_DisplayOrder",
            table: "Tarefas");

        migrationBuilder.CreateIndex(
            name: "IX_Tarefas_DisplayOrder",
            table: "Tarefas",
            column: "DisplayOrder",
            unique: true);
    }
}