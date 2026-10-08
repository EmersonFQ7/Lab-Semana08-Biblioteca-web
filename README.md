# Biblioteca.Web

Lab Semana 08 - Desarrollo Empresarial Avanzado

## Descripción

Aplicación web ASP.NET Core MVC para la gestión de una biblioteca:
libros, socios y reporte de préstamos. Acceso a datos con Dapper
y procedimientos almacenados en SQL Server.

## Tecnologías

- ASP.NET Core 10.0 MVC
- Dapper 2.1.89
- Microsoft.Data.SqlClient 7.1.1
- SQL Server (SSMS 22)

## Requisitos

- .NET SDK 10.0
- SQL Server con la base de datos `BibliotecaDB` (ver script Semana 07)
- Los procedimientos almacenados de este lab (script `SP_Semana08.sql`)

## Configuración

1. Clonar este repositorio.
2. Abrir SSMS 22 y ejecutar en orden:
   - `Scripts/01_BibliotecaDB.sql` (crea la BD y datos de prueba)
   - `Scripts/02_StoredProcedures.sql` (crea los 10 SPs)
3. Ajustar la cadena de conexión en `appsettings.json` con tu `Server=` real.
4. Restaurar paquetes y correr:
dotnet restore
dotnet run
5. Abrir `http://localhost:5245`

## Autor

Emerson -