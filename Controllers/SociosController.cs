using Biblioteca.Web.Models;
using Biblioteca.Web.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Web.Controllers
{
    public class SociosController : Controller
    {
        private readonly SocioRepositorio _socioRepo;

        public SociosController(SocioRepositorio socioRepo)
        {
            _socioRepo = socioRepo;
        }

        // =============================================================
        // GET: /Socios   (listado de socios activos)
        // =============================================================
        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Listado de Socios";

            var socios = await _socioRepo.ListarAsync();
            return View(socios);
        }

        // =============================================================
        // GET: /Socios/Create
        // =============================================================
        public IActionResult Create()
        {
            ViewData["Title"] = "Nuevo Socio";
            return View();
        }

        // =============================================================
        // POST: /Socios/Create
        // Valida que el DNI no este duplicado antes de insertar
        // =============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Socio socio)
        {
            // 1. Si el DNI ya existe, agregamos el error al ModelState
            //    (punto 10 del lab)
            if (!string.IsNullOrWhiteSpace(socio.DNI) &&
                await _socioRepo.ExisteDniAsync(socio.DNI))
            {
                ModelState.AddModelError(nameof(socio.DNI),
                    "Ya existe un socio registrado con ese DNI.");
            }

            // 2. Si falla cualquier validacion, volvemos al formulario
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Nuevo Socio";
                return View(socio);
            }

            // 3. Todo OK: insertar y redirigir
            await _socioRepo.InsertarAsync(socio);
            TempData["Mensaje"] = $"Socio '{socio.Nombre}' registrado correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}