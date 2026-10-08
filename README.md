# Biblioteca.Web

Lab Semana 08 - Desarrollo Empresarial Avanzado
Universidad - VII Ciclo

## Descripción

Aplicación web ASP.NET Core MVC para la gestión de una biblioteca:
libros, socios y reporte de préstamos. Acceso a datos con Dapper
y procedimientos almacenados en SQL Server.

## Tecnologías

- ASP.NET Core 10.0 MVC
- Dapper 2.1.89
- Microsoft.Data.SqlClient 7.1.1
- SQL Server (SSMS 22)
- Bootstrap 5

## Requisitos

- .NET SDK 10.0
- SQL Server con la base de datos `BibliotecaDB` (ver script Semana 07)
- Los procedimientos almacenados de este lab (script `SP_Semana08.sql`)

## Configuración

1. Restaurar la base de datos `BibliotecaDB` con los datos de prueba.
2. Ejecutar el script de los stored procedures.
3. Ajustar la cadena de conexión en `appsettings.json` → `Server=TU_INSTANCIA`.
4. Ejecutar: