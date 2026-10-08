using System.Data;
using Biblioteca.Web.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Biblioteca.Web.Repositorios
{
    public class PrestamoRepositorio
    {
        private readonly string _connectionString;

        public PrestamoRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BibliotecaDB")
                ?? throw new InvalidOperationException(
                    "No se encontro la cadena de conexion 'BibliotecaDB' en appsettings.json");
        }

        private SqlConnection CrearConexion() => new SqlConnection(_connectionString);

        // ---------------------------------------------------------------
        // Reporte de prestamos por intervalo de fechas
        // Devuelve una fila por libro prestado
        // ---------------------------------------------------------------
        public async Task<IEnumerable<PrestamoReporte>> ReporteAsync(DateTime desde, DateTime hasta)
        {
            using var connection = CrearConexion();
            return await connection.QueryAsync<PrestamoReporte>(
                "sp_Prestamos_Reporte",
                new { Desde = desde, Hasta = hasta },
                commandType: CommandType.StoredProcedure);
        }
    }
}