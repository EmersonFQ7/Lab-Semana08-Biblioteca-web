# Scripts SQL - BibliotecaDB

Ejecutar en SQL Server Management Studio 22, en este orden:

1. **01_BibliotecaDB.sql** → crea la base de datos `BibliotecaDB`,
   todas las tablas (Autores, Libros, Socios, Prestamos, DetallePrestamo)
   y carga los datos de prueba (8 autores, 20 libros, 10 socios,
   5 prestamos con sus detalles). (Script de la Semana 07)

2. **02_StoredProcedures.sql** → crea los 10 procedimientos almacenados
   que consume la aplicacion ASP.NET Core MVC. (Script de la Semana 08)

Despues de ejecutar ambos scripts, la base de datos queda lista para
usarse con el proyecto Biblioteca.Web.