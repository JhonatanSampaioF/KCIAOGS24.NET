using KCIAOGS24.NET.Application.Interfaces;
using KCIAOGS24.NET.Application.Dtos.Create;
using KCIAOGS24.NET.Application.Dtos.Edits;
using Microsoft.AspNetCore.Mvc;

namespace KCIAO.API.MVC.Presentation.Controllers
{
    public class EnergiaEolicaController : Controller
    {
        private readonly IEnergiaEolicaApplicationService _energiaEolicaApplicationService;

        public EnergiaEolicaController(IEnergiaEolicaApplicationService energiaEolicaApplicationService)
        {
            _energiaEolicaApplicationService = energiaEolicaApplicationService;
        }

        // GET: EnergiaEolica
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var energiaEolicasEntities = _energiaEolicaApplicationService.ObterTodasEnergiaEolicas();

            var energiaEolicas = energiaEolicasEntities?.Select(energiaEolica => new EnergiaEolicaEditDto
            {
                id = energiaEolica.id,
                potencialNominal = energiaEolica.potencialNominal,
                alturaTorre = energiaEolica.alturaTorre,
                diametroRotor = energiaEolica.diametroRotor,
                energiaEstimadaGerada = energiaEolica.energiaEstimadaGerada,
                fk_endereco = energiaEolica.fk_endereco
                // outros campos, se necessário
            }).ToList() ?? new List<EnergiaEolicaEditDto>(); // Retorna uma lista vazia se `energiaEolicasEntities` for null

            return View(energiaEolicas);
        }

        // GET: EnergiaEolica/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var energiaEolica = _energiaEolicaApplicationService.ObterEnergiaEolicaporId(id ?? 0);

            return View(energiaEolica);
        }

        // GET: EnergiaEolica/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: EnergiaEolica/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,profissional,local_energiaEolica,horario_energiaEolica,fk_evento")] EnergiaEolicaDto model)
        {
            if (ModelState.IsValid)
            {
                _energiaEolicaApplicationService.SalvarDadosEnergiaEolica(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: EnergiaEolica/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var energiaEolica = _energiaEolicaApplicationService.ObterEnergiaEolicaporId(id ?? 0);

            if (id == 0 || id == null)
            {
                return NotFound();
            }

            return View(new EnergiaEolicaEditDto
            {
                id = energiaEolica.id,
                potencialNominal = energiaEolica.potencialNominal,
                alturaTorre = energiaEolica.alturaTorre,
                diametroRotor = energiaEolica.diametroRotor,
                energiaEstimadaGerada = energiaEolica.energiaEstimadaGerada,
                fk_endereco = energiaEolica.fk_endereco
            });
        }

        // POST: EnergiaEolica/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,profissional,local_energiaEolica,horario_energiaEolica,fk_evento")] EnergiaEolicaEditDto energiaEolica)
        {
            if (ModelState.IsValid)
            {
                _energiaEolicaApplicationService.EditarDadosEnergiaEolica(id, energiaEolica);
                return RedirectToAction(nameof(Index));
            }
            return View(energiaEolica);
        }

        // GET: EnergiaEolica/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var energiaEolica = _energiaEolicaApplicationService.ObterEnergiaEolicaporId(id ?? 0);


            if (id == 0 || id == null)
            {
                return NotFound();
            }

            return View(energiaEolica);
        }

        // POST: EnergiaEolica/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var energiaEolica = _energiaEolicaApplicationService.DeletarDadosEnergiaEolica(id);

            if (energiaEolica != null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(energiaEolica);
        }
    }
}
