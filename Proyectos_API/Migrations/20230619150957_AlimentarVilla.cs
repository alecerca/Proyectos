using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Proyectos_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenURL", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "#", "Detalle de la Villa", new DateTime(2023, 6, 19, 11, 9, 57, 208, DateTimeKind.Local).AddTicks(4156), new DateTime(2023, 6, 19, 11, 9, 57, 208, DateTimeKind.Local).AddTicks(4353), "#", 5, "Villa nueva", 5, 200.0 },
                    { 2, "#", "Detalle de la Villa", new DateTime(2023, 6, 19, 11, 9, 57, 208, DateTimeKind.Local).AddTicks(4357), new DateTime(2023, 6, 19, 11, 9, 57, 208, DateTimeKind.Local).AddTicks(4359), "#", 16, "Villa real", 10, 400.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
