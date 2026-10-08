using System.Data;
using Biblioteca.Web.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Biblioteca.Web.Repositorios
{
    public class LibroRepositorio
    {
        private readonly string _connectionString;

        // IConfiguration se inyecta automaticamente, lee appsettings.json
        public LibroRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("BibliotecaDB")
                ?? throw new InvalidOperationException(
                    "No se encontro la cadena de conexion 'BibliotecaDB' en appsettings.json");
        }

        // Factory para abrir una nueva conexion cada vez que se necesita
        private SqlConnection CrearConexion() => new SqlConnection(_connectionString);

        // ---------------------------------------------------------------
        // Listar todos los libros activos (con nombre del autor)
        // ---------------------------------------------------------------
        public async Task<IEnumerable<Libro>> ListarAsync()
        {
            using var connection = CrearConexion();
            return await connection.QueryAsync<Libro>(
                "sp_Libros_Listar",
                commandType: CommandType.StoredProcedure);
        }

        // ---------------------------------------------------------------
        // Buscar libros por titulo (coincidencia parcial)
        // ---------------------------------------------------------------
        public async Task<IEnumerable<Libro>> BuscarPorTituloAsync(string titulo)
        {
            using var connection = CrearConexion();
            return await connection.QueryAsync<Libro>(
                "sp_Libros_BuscarPorTitulo",
                new { Titulo = titulo },
                commandType: CommandType.StoredProcedure);
        }

        // ---------------------------------------------------------------
        // Obtener un libro por su Id
        // ---------------------------------------------------------------
        public async Task<Libro?> ObtenerPorIdAsync(int libroId)
        {
            using var connection = CrearConexion();
            return await connection.QuerySingleOrDefaultAsync<Libro>(
                "sp_Libros_ObtenerPorId",
                new { LibroId = libroId },
                commandType: CommandType.StoredProcedure);
        }

        // ---------------------------------------------------------------
        // Insertar libro (devuelve el nuevo LibroId)
        // ---------------------------------------------------------------
        public async Task<int> InsertarAsync(Libro libro)
        {
            using var connection = CrearConexion();
            return await connection.ExecuteScalarAsync<int>(
                "sp_Libros_Insertar",
                new
                {
                    libro.Titulo,
                    libro.ISBN,
                    libro.AutorId,
                    libro.Ejemplares
                },
                commandType: CommandType.StoredProcedure);
        }

        // ---------------------------------------------------------------
        // Actualizar libro
        // ---------------------------------------------------------------
        public async Task ActualizarAsync(Libro libro)
        {
            using var connection = CrearConexion();
            await connection.ExecuteAsync(
                "sp_Libros_Actualizar",
                new
                {
                    libro.LibroId,
                    libro.Titulo,
                    libro.ISBN,
                    libro.AutorId,
                    libro.Ejemplares
                },
                commandType: CommandType.StoredProcedure);
        }

        // ---------------------------------------------------------------
        // Eliminar logico (UPDATE Activo = 0, nunca DELETE fisico)
        // ---------------------------------------------------------------
        public async Task EliminarAsync(int libroId)
        {
            using var connection = CrearConexion();
            await connection.ExecuteAsync(
                "sp_Libros_Eliminar",
                new { LibroId = libroId },
                commandType: CommandType.StoredProcedure);
        }
    }
}