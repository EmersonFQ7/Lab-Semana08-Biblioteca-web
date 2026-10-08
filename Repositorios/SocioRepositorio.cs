using System.Data;
using Biblioteca.Web.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Biblioteca.Web.Repositorios
{
    public class SocioRepositorio
    {
        private readonly string _connectionString;

        public SocioRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BibliotecaDB")
                ?? throw new InvalidOperationException(
                    "No se encontro la cadena de conexion 'BibliotecaDB' en appsettings.json");
        }

        private SqlConnection CrearConexion() => new SqlConnection(_connectionString);

        // ---------------------------------------------------------------
        // Listar socios activos
        // ---------------------------------------------------------------
        public async Task<IEnumerable<Socio>> ListarAsync()
        {
            using var connection = CrearConexion();
            return await connection.QueryAsync<Socio>(
                "sp_Socios_Listar",
                commandType: CommandType.StoredProcedure);
        }

        // ---------------------------------------------------------------
        // Insertar socio (devuelve el nuevo SocioId)
        // ---------------------------------------------------------------
        public async Task<int> InsertarAsync(Socio socio)
        {
            using var connection = CrearConexion();
            return await connection.ExecuteScalarAsync<int>(
                "sp_Socios_Insertar",
                new
                {
                    socio.DNI,
                    socio.Nombre,
                    socio.Email
                },
                commandType: CommandType.StoredProcedure);
        }

        // ---------------------------------------------------------------
        // Verifica si un DNI ya existe (para evitar duplicados)
        // Usa una consulta parametrizada directa (no requiere un SP propio)
        // ---------------------------------------------------------------
        public async Task<bool> ExisteDniAsync(string dni)
        {
            using var connection = CrearConexion();
            const string sql = "SELECT COUNT(1) FROM Socios WHERE DNI = @DNI;";
            var cantidad = await connection.ExecuteScalarAsync<int>(sql, new { DNI = dni });
            return cantidad > 0;
        }
    }
}