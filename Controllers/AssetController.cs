using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using SecureAssetManager.Data;
using SecureAssetManager.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SecureAssetManager.Controllers
{
    public class AssetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AssetController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Assets.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Threats = _context.Threats.ToList();
            ViewBag.Vulnerabilities = _context.Vulnerabilities.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CodigoActivo,Nombre,Ubicacion,Tipo,Categoria")] Asset asset)
        {

            if (ModelState.IsValid)
            {
                _context.Add(asset);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            // Si el modelo no es válido, regresar a la vista de creación con el modelo invalidado
            return View(asset);
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets
                .FirstOrDefaultAsync(a => a.ID == id);

            if (asset == null)
            {
                return NotFound();
            }

            ViewBag.Threats = _context.Threats.ToList();
            ViewBag.Vulnerabilities = _context.Vulnerabilities.ToList();

            return View(asset);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CodigoActivo,Nombre,Ubicacion,Tipo,Categoria")] Asset asset, int[] selectedThreats, int[] selectedVulnerabilities)
        {
            if (id != asset.ID)
            {
                return NotFound();
            }

            try
            {
                // Obtener el asset existente incluyendo sus amenazas y vulnerabilidades
                var existingAsset = await _context.Assets

                    .FirstOrDefaultAsync(a => a.ID == asset.ID);

                if (existingAsset ==  null)
                {
                    return NotFound();
                }

                // Actualizar las amenazas y vulnerabilidades seleccionadas
                UpdateSelectedThreats(existingAsset, selectedThreats);
                UpdateSelectedVulnerabilities(existingAsset, selectedVulnerabilities);

                // Actualizar los demás campos del asset
                existingAsset.CodigoActivo = asset.CodigoActivo;
                existingAsset.Nombre = asset.Nombre;
                existingAsset.Ubicacion = asset.Ubicacion;
                existingAsset.Categoria = asset.Categoria;
                existingAsset.Tipo = asset.Tipo;



                _context.Update(existingAsset);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(asset.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index", "Home");
        }


        private void UpdateSelectedThreats(Asset asset, int[] selectedThreats)
        {
            // Lógica para actualizar las amenazas seleccionadas
            var existingThreats = _context.AssetThreats.Where(at => at.AssetId == asset.ID).ToList();
            var currentThreatIds = existingThreats.Select(at => at.ThreatId).ToList();

            var newThreats = selectedThreats.Except(currentThreatIds);
            var removedThreats = currentThreatIds.Except(selectedThreats);

            foreach (var threatId in newThreats)
            {
                _context.AssetThreats.Add(new AssetThreat { AssetId = asset.ID, ThreatId = threatId });
            }

            foreach (var threatId in removedThreats)
            {
                var threatToRemove = existingThreats.FirstOrDefault(at => at.ThreatId == threatId);
                if (threatToRemove != null)
                {
                    _context.AssetThreats.Remove(threatToRemove);
                }
            }
        }

        private void UpdateSelectedVulnerabilities(Asset asset, int[] selectedVulnerabilities)
        {

        }









        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.Assets.FirstOrDefaultAsync(a => a.ID == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asset = await _context.Assets.FindAsync(id);
            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool AssetExists(int id)
        {
            return _context.Assets.Any(e => e.ID == id);
        }
    }
}
