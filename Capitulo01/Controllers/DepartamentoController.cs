using Capitulo01.Data;
using Capitulo01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Capitulo01.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly IESContext _esContext;

        public DepartamentoController(IESContext esContext)
        {
            _esContext = esContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _esContext.Departamentos.Include(i => i.Instituicao).OrderBy(d => d.Id).ToListAsync());
        }

        public IActionResult Criar()
        {
            var instituicoes = _esContext.Instituicoes.OrderBy(i => i.Id).ToList();
            instituicoes.Insert(0, new Instituicao() { Id = 0, Nome = "Selecione a instituição" });
            ViewBag.Instituicoes = instituicoes;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Departamento departamento)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _esContext.Add(departamento);
                    await _esContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(departamento);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _esContext.Departamentos.SingleOrDefaultAsync(de => de.Id == id);

            if (departamento == null)
            {
                return NotFound();
            }

            ViewBag.Instituicoes = new SelectList(_esContext.Instituicoes.OrderBy(i => i.Id), "Id", "Nome", departamento.InstituicaoId);

            return View(departamento);
        }


        [HttpPost]
        public async Task<IActionResult> Editar(int id, Departamento departamento)
        {
            if (id != departamento.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _esContext.Update(departamento);
                    await _esContext.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!DepartamentoExists(departamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        public async Task<IActionResult> Detalhes(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _esContext.Departamentos.SingleOrDefaultAsync(d => d.Id == id);
            _esContext.Instituicoes.Where(i => i.Id == departamento.InstituicaoId).Load();

            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        public async Task<IActionResult> DeletarConfirmacao(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var departamento = await _esContext.Departamentos.SingleOrDefaultAsync(d => d.Id == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        [HttpPost, ActionName("Deletar")]
        public async Task<IActionResult> Deletar(int id)
        {
            var departamento = await _esContext.Departamentos.SingleOrDefaultAsync(de => de.Id == id);
            if (departamento == null)
            {
                return NotFound();
            }
            _esContext.Departamentos.Remove(departamento);
            await _esContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentoExists(int id)
        {
            return _esContext.Departamentos.Any(d => d.Id == id);
        }
    }
}
