using Biblioteca.Web.Models;
using Biblioteca.Web.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Web.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly PrestamoRepositorio _prestamoRepo;

        public PrestamosController(PrestamoRepositorio prestamoRepo)
        {
            _prestamoRepo = prestamoRepo;
        }

        // =============================================================
        // GET: /Prestamos/Reporte?desde=2026-10-01&hasta=2026-10-31
        // =============================================================
        public async Task<IActionResult> Reporte(DateTime? desde, DateTime? hasta)
        {
            // Rango por defecto: ultimo mes
            var fechaHasta = hasta ?? DateTime.Today;
            var fechaDesde = desde ?? fechaHasta.AddMonths(-1);

            ViewData["Title"] = "Reporte de Prestamos";
            ViewData["Desde"] = fechaDesde.ToString("yyyy-MM-dd");
            ViewData["Hasta"] = fechaHasta.ToString("yyyy-MM-dd");

            var datos = await _prestamoRepo.ReporteAsync(fechaDesde, fechaHasta);
            return View(datos);
        }
    }
}