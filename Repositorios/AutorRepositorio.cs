using System.Data;
using Biblioteca.Web.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Biblioteca.Web.Repositorios
{
    public class AutorRepositorio
    {
        private readonly string _connectionString;

        public AutorRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BibliotecaDB")
                ?? throw new InvalidOperationException(
                    "No se encontro la cadena de conexion 'BibliotecaDB' en appsettings.json");
        }

        private SqlConnection CrearConexion() => new SqlConnection(_connectionString);

        // ---------------------------------------------------------------
        // Listar autores activos (para la lista desplegable en Libros)
        // ---------------------------------------------------------------
        public async Task<IEnumerable<Autor>> ListarAsync()
        {
            using var connection = CrearConexion();
            return await connection.QueryAsync<Autor>(
                "sp_Autores_Listar",
                commandType: CommandType.StoredProcedure);
        }
    }
}