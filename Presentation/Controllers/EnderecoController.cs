using KCIAOGS24.NET.Application.Interfaces;
using KCIAOGS24.NET.Application.Dtos.Create;
using KCIAOGS24.NET.Application.Dtos.Edits;
using Microsoft.AspNetCore.Mvc;

namespace KCIAOGS24.NET.Presentation.Controllers
{
    public class EnderecoController : Controller
    {
        private readonly IEnderecoApplicationService _enderecoApplicationService;

        public EnderecoController(IEnderecoApplicationService enderecoApplicationService)
        {
            _enderecoApplicationService = enderecoApplicationService;
        }

        // GET: Endereco
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var enderecosEntities = _enderecoApplicationService.ObterTodasEnderecos();

            var enderecos = enderecosEntities?.Select(endereco => new EnderecoEditDto
            {
                id = endereco.id,
                tipoResidencial = endereco.tipoResidencial,
                nome = endereco.nome,
                cep = endereco.cep,
                tarifa = endereco.tarifa,
                gastoMensal = endereco.gastoMensal,
                economia = endereco.economia,
                fk_usuario = endereco.fk_usuario
                // outros campos, se necessário
            }).ToList() ?? new List<EnderecoEditDto>(); // Retorna uma lista vazia se `enderecosEntities` for null

            return View(enderecos);
        }

        // GET: Endereco/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var endereco = _enderecoApplicationService.ObterEnderecoporId(id ?? 0);

            return View(endereco);
        }

        // GET: Endereco/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Endereco/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,profissional,local_endereco,horario_endereco,fk_evento")] EnderecoDto model)
        {
            if (ModelState.IsValid)
            {
                _enderecoApplicationService.SalvarDadosEndereco(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Endereco/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var endereco = _enderecoApplicationService.ObterEnderecoporId(id ?? 0);

            if (id == 0 || id == null)
            {
                return NotFound();
            }

            return View(new EnderecoEditDto
            {
                id = endereco.id,
                tipoResidencial = endereco.tipoResidencial,
                nome = endereco.nome,
                cep = endereco.cep,
                tarifa = endereco.tarifa,
                gastoMensal = endereco.gastoMensal,
                economia = endereco.economia,
                fk_usuario = endereco.fk_usuario
            });
        }

        // POST: Endereco/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,profissional,local_endereco,horario_endereco,fk_evento")] EnderecoEditDto endereco)
        {
            if (ModelState.IsValid)
            {
                _enderecoApplicationService.EditarDadosEndereco(id, endereco);
                return RedirectToAction(nameof(Index));
            }
            return View(endereco);
        }

        // GET: Endereco/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var endereco = _enderecoApplicationService.ObterEnderecoporId(id ?? 0);


            if (id == 0 || id == null)
            {
                return NotFound();
            }

            return View(endereco);
        }

        // POST: Endereco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var endereco = _enderecoApplicationService.DeletarDadosEndereco(id);

            if (endereco != null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(endereco);
        }
    }
}
