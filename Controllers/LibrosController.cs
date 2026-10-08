using Biblioteca.Web.Models;
using Biblioteca.Web.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Biblioteca.Web.Controllers
{
    public class LibrosController : Controller
    {
        private readonly LibroRepositorio _libroRepo;
        private readonly AutorRepositorio _autorRepo;

        // Inyeccion por constructor (los registramos en Program.cs)
        public LibrosController(LibroRepositorio libroRepo, AutorRepositorio autorRepo)
        {
            _libroRepo = libroRepo;
            _autorRepo = autorRepo;
        }

        // =============================================================
        // GET: /Libros   (listado con busqueda por titulo)
        // =============================================================
        public async Task<IActionResult> Index(string? busqueda)
        {
            ViewData["Title"] = "Listado de Libros";
            ViewData["Busqueda"] = busqueda;

            IEnumerable<Libro> libros = string.IsNullOrWhiteSpace(busqueda)
                ? await _libroRepo.ListarAsync()
                : await _libroRepo.BuscarPorTituloAsync(busqueda);

            return View(libros);
        }

        // =============================================================
        // GET: /Libros/Details/5
        // =============================================================
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Detalle del Libro";

            var libro = await _libroRepo.ObtenerPorIdAsync(id);
            if (libro == null)
                return NotFound();

            return View(libro);
        }

        // =============================================================
        // GET: /Libros/Create   (muestra el formulario)
        // =============================================================
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Nuevo Libro";
            await CargarAutoresAsync();
            return View();
        }

        // =============================================================
        // POST: /Libros/Create   (procesa el formulario)
        // =============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                await CargarAutoresAsync();
                ViewData["Title"] = "Nuevo Libro";
                return View(libro);
            }

            await _libroRepo.InsertarAsync(libro);
            TempData["Mensaje"] = $"Libro '{libro.Titulo}' registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // =============================================================
        // GET: /Libros/Edit/5
        // =============================================================
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Editar Libro";

            var libro = await _libroRepo.ObtenerPorIdAsync(id);
            if (libro == null)
                return NotFound();

            await CargarAutoresAsync(libro.AutorId);
            return View(libro);
        }

        // =============================================================
        // POST: /Libros/Edit/5
        // =============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Libro libro)
        {
            if (id != libro.LibroId)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                await CargarAutoresAsync(libro.AutorId);
                ViewData["Title"] = "Editar Libro";
                return View(libro);
            }

            await _libroRepo.ActualizarAsync(libro);
            TempData["Mensaje"] = $"Libro '{libro.Titulo}' actualizado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // =============================================================
        // GET: /Libros/Delete/5   (muestra confirmacion)
        // =============================================================
        public async Task<IActionResult> Delete(int id)
        {
            ViewData["Title"] = "Eliminar Libro";

            var libro = await _libroRepo.ObtenerPorIdAsync(id);
            if (libro == null)
                return NotFound();

            return View(libro);
        }

        // =============================================================
        // POST: /Libros/Delete/5   (eliminacion logica: Activo = 0)
        // =============================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _libroRepo.ObtenerPorIdAsync(id);
            if (libro == null)
                return NotFound();

            await _libroRepo.EliminarAsync(id);
            TempData["Mensaje"] = $"Libro '{libro.Titulo}' eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        // =============================================================
        // Helper privado: carga autores en ViewData para el dropdown
        // =============================================================
        private async Task CargarAutoresAsync(int? autorSeleccionado = null)
        {
            var autores = await _autorRepo.ListarAsync();
            ViewData["Autores"] = new SelectList(
                autores, "AutorId", "Nombre", autorSeleccionado);
        }
    }
}