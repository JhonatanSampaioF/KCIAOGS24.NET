using KCIAOGS24.NET.Application.Interfaces;
using KCIAOGS24.NET.Application.Dtos.Create;
using KCIAOGS24.NET.Application.Dtos.Edits;
using Microsoft.AspNetCore.Mvc;

namespace KCIAO.API.MVC.Presentation.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioApplicationService _usuarioApplicationService;

        public UsuarioController(IUsuarioApplicationService usuarioApplicationService)
        {
            _usuarioApplicationService = usuarioApplicationService;
        }

        // GET: Usuario
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var usuariosEntities = _usuarioApplicationService.ObterTodasUsuarios();

            var usuarios = usuariosEntities?.Select(usuario => new UsuarioEditDto
            {
                id = usuario.id,
                nome = usuario.nome,
                email = usuario.email
                // outros campos, se necessário
            }).ToList() ?? new List<UsuarioEditDto>(); // Retorna uma lista vazia se `usuariosEntities` for null

            return View(usuarios);
        }

        // GET: Usuario/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var usuario = _usuarioApplicationService.ObterUsuarioporId(id ?? 0);

            return View(usuario);
        }

        // GET: Usuario/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,profissional,local_usuario,horario_usuario,fk_evento")] UsuarioDto model)
        {
            if (ModelState.IsValid)
            {
                _usuarioApplicationService.SalvarDadosUsuario(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Usuario/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var usuario = _usuarioApplicationService.ObterUsuarioporId(id ?? 0);

            if (id == 0 || id == null)
            {
                return NotFound();
            }

            return View(new UsuarioEditDto
            {
                id = usuario.id,
                nome = usuario.nome,
                email = usuario.email
            });
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,profissional,local_usuario,horario_usuario,fk_evento")] UsuarioEditDto usuario)
        {
            if (ModelState.IsValid)
            {
                _usuarioApplicationService.EditarDadosUsuario(id, usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var usuario = _usuarioApplicationService.ObterUsuarioporId(id ?? 0);


            if (id == 0 || id == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = _usuarioApplicationService.DeletarDadosUsuario(id);

            if (usuario != null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(usuario);
        }
    }
}
