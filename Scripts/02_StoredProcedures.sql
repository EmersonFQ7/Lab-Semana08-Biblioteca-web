USE BibliotecaDB;
GO

/* =====================================================================
   LIBROS
   ===================================================================== */

-- 1) Listado de libros activos con nombre del autor (INNER JOIN)
IF OBJECT_ID('sp_Libros_Listar', 'P') IS NOT NULL
    DROP PROCEDURE sp_Libros_Listar;
GO
CREATE PROCEDURE sp_Libros_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT  l.LibroId, l.Titulo, l.ISBN, l.AutorId,
            a.Nombre AS AutorNombre, l.Ejemplares, l.Activo
    FROM    Libros  l
    INNER JOIN Autores a ON a.AutorId = l.AutorId
    WHERE   l.Activo = 1
    ORDER BY l.Titulo;
END
GO

-- 2) Busqueda por titulo (parcial, LIKE)
IF OBJECT_ID('sp_Libros_BuscarPorTitulo', 'P') IS NOT NULL
    DROP PROCEDURE sp_Libros_BuscarPorTitulo;
GO
CREATE PROCEDURE sp_Libros_BuscarPorTitulo
    @Titulo VARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT  l.LibroId, l.Titulo, l.ISBN, l.AutorId,
            a.Nombre AS AutorNombre, l.Ejemplares, l.Activo
    FROM    Libros  l
    INNER JOIN Autores a ON a.AutorId = l.AutorId
    WHERE   l.Activo = 1
      AND   l.Titulo LIKE '%' + @Titulo + '%'
    ORDER BY l.Titulo;
END
GO

-- 3) Obtener libro por Id
IF OBJECT_ID('sp_Libros_ObtenerPorId', 'P') IS NOT NULL
    DROP PROCEDURE sp_Libros_ObtenerPorId;
GO
CREATE PROCEDURE sp_Libros_ObtenerPorId
    @LibroId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT  l.LibroId, l.Titulo, l.ISBN, l.AutorId,
            a.Nombre AS AutorNombre, l.Ejemplares, l.Activo
    FROM    Libros  l
    INNER JOIN Autores a ON a.AutorId = l.AutorId
    WHERE   l.LibroId = @LibroId;
END
GO

-- 4) Insertar libro
IF OBJECT_ID('sp_Libros_Insertar', 'P') IS NOT NULL
    DROP PROCEDURE sp_Libros_Insertar;
GO
CREATE PROCEDURE sp_Libros_Insertar
    @Titulo     VARCHAR(150),
    @ISBN       VARCHAR(20),
    @AutorId    INT,
    @Ejemplares INT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Libros (Titulo, ISBN, AutorId, Ejemplares, Activo)
    VALUES (@Titulo, @ISBN, @AutorId, @Ejemplares, 1);
    SELECT SCOPE_IDENTITY() AS LibroId;
END
GO

-- 5) Actualizar libro
IF OBJECT_ID('sp_Libros_Actualizar', 'P') IS NOT NULL
    DROP PROCEDURE sp_Libros_Actualizar;
GO
CREATE PROCEDURE sp_Libros_Actualizar
    @LibroId    INT,
    @Titulo     VARCHAR(150),
    @ISBN       VARCHAR(20),
    @AutorId    INT,
    @Ejemplares INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Libros
       SET Titulo = @Titulo, ISBN = @ISBN,
           AutorId = @AutorId, Ejemplares = @Ejemplares
     WHERE LibroId = @LibroId;
END
GO

-- 6) Eliminar libro (LOGICO: Activo = 0, nunca DELETE fisico)
IF OBJECT_ID('sp_Libros_Eliminar', 'P') IS NOT NULL
    DROP PROCEDURE sp_Libros_Eliminar;
GO
CREATE PROCEDURE sp_Libros_Eliminar
    @LibroId INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Libros SET Activo = 0 WHERE LibroId = @LibroId;
END
GO

/* =====================================================================
   SOCIOS
   ===================================================================== */

-- 7) Listar socios activos
IF OBJECT_ID('sp_Socios_Listar', 'P') IS NOT NULL
    DROP PROCEDURE sp_Socios_Listar;
GO
CREATE PROCEDURE sp_Socios_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT SocioId, DNI, Nombre, Email, Activo
    FROM   Socios
    WHERE  Activo = 1
    ORDER BY Nombre;
END
GO

-- 8) Insertar socio
IF OBJECT_ID('sp_Socios_Insertar', 'P') IS NOT NULL
    DROP PROCEDURE sp_Socios_Insertar;
GO
CREATE PROCEDURE sp_Socios_Insertar
    @DNI    VARCHAR(8),
    @Nombre VARCHAR(100),
    @Email  VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Socios (DNI, Nombre, Email, Activo)
    VALUES (@DNI, @Nombre, @Email, 1);
    SELECT SCOPE_IDENTITY() AS SocioId;
END
GO

/* =====================================================================
   AUTORES
   ===================================================================== */

-- 9) Listar autores activos (para la lista desplegable)
IF OBJECT_ID('sp_Autores_Listar', 'P') IS NOT NULL
    DROP PROCEDURE sp_Autores_Listar;
GO
CREATE PROCEDURE sp_Autores_Listar
AS
BEGIN
    SET NOCOUNT ON;
    SELECT AutorId, Nombre, Nacionalidad, Activo
    FROM   Autores
    WHERE  Activo = 1
    ORDER BY Nombre;
END
GO

/* =====================================================================
   PRESTAMOS
   ===================================================================== */

-- 10) Reporte de prestamos por intervalo de fechas
IF OBJECT_ID('sp_Prestamos_Reporte', 'P') IS NOT NULL
    DROP PROCEDURE sp_Prestamos_Reporte;
GO
CREATE PROCEDURE sp_Prestamos_Reporte
    @Desde DATETIME,
    @Hasta DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    SELECT  p.PrestamoId, s.SocioId,
            s.Nombre AS SocioNombre, s.DNI AS SocioDNI,
            l.LibroId, l.Titulo AS LibroTitulo,
            p.FechaPrestamo, p.FechaLimite,
            d.FechaDevolucion, p.Estado
    FROM    Prestamos        p
    INNER JOIN DetallePrestamo d ON d.PrestamoId = p.PrestamoId
    INNER JOIN Libros          l ON l.LibroId    = d.LibroId
    INNER JOIN Socios          s ON s.SocioId    = p.SocioId
    WHERE   p.FechaPrestamo >= @Desde
      AND   p.FechaPrestamo <  DATEADD(DAY, 1, @Hasta)
    ORDER BY p.FechaPrestamo DESC, s.Nombre, l.Titulo;
END
GO