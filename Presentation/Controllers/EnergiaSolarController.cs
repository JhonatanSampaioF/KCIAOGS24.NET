using KCIAOGS24.NET.Application.Interfaces;
using KCIAOGS24.NET.Application.Dtos.Create;
using KCIAOGS24.NET.Application.Dtos.Edits;
using Microsoft.AspNetCore.Mvc;

namespace KCIAOGS24.NET.Presentation.Controllers
{
    public class EnergiaSolarController : Controller
    {
        private readonly IEnergiaSolarApplicationService _energiaSolarApplicationService;

        public EnergiaSolarController(IEnergiaSolarApplicationService energiaSolarApplicationService)
        {
            _energiaSolarApplicationService = energiaSolarApplicationService;
        }

        // GET: EnergiaSolar
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var energiaSolarsEntities = _energiaSolarApplicationService.ObterTodasEnergiaSolars();

            var energiaSolars = energiaSolarsEntities?.Select(energiaSolar => new EnergiaSolarEditDto
            {
                id = energiaSolar.id,
                areaPlaca = energiaSolar.areaPlaca,
                irradiacaoSolar = energiaSolar.irradiacaoSolar,
                energiaEstimadaGerada = energiaSolar.energiaEstimadaGerada,
                fk_endereco = energiaSolar.fk_endereco
                // outros campos, se necessário
            }).ToList() ?? new List<EnergiaSolarEditDto>(); // Retorna uma lista vazia se `energiaSolarsEntities` for null

            return View(energiaSolars);
        }

        // GET: EnergiaSolar/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var energiaSolar = _energiaSolarApplicationService.ObterEnergiaSolarporId(id ?? 0);

            return View(energiaSolar);
        }

        // GET: EnergiaSolar/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: EnergiaSolar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,areaPlaca,irradiacaoSolar,energiaEstimadaGerada,fk_endereco")] EnergiaSolarDto model)
        {
            if (ModelState.IsValid)
            {
                _energiaSolarApplicationService.SalvarDadosEnergiaSolar(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: EnergiaSolar/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var energiaSolar = _energiaSolarApplicationService.ObterEnergiaSolarporId(id ?? 0);

            if (id == 0 || id == null)
            {
                return NotFound();
            }

            return View(new EnergiaSolarEditDto
            {
                id = energiaSolar.id,
                areaPlaca = energiaSolar.areaPlaca,
                irradiacaoSolar = energiaSolar.irradiacaoSolar,
                energiaEstimadaGerada = energiaSolar.energiaEstimadaGerada,
                fk_endereco = energiaSolar.fk_endereco
            });
        }

        // POST: EnergiaSolar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,areaPlaca,irradiacaoSolar,energiaEstimadaGerada,fk_endereco")] EnergiaSolarEditDto energiaSolar)
        {
            if (ModelState.IsValid)
            {
                _energiaSolarApplicationService.EditarDadosEnergiaSolar(id, energiaSolar);
                return RedirectToAction(nameof(Index));
            }
            return View(energiaSolar);
        }

        // GET: EnergiaSolar/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var energiaSolar = _energiaSolarApplicationService.ObterEnergiaSolarporId(id ?? 0);


            if (id == 0 || id == null)
            {
                return NotFound();
            }

            return View(energiaSolar);
        }

        // POST: EnergiaSolar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var energiaSolar = _energiaSolarApplicationService.DeletarDadosEnergiaSolar(id);

            if (energiaSolar != null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(energiaSolar);
        }
    }
}
