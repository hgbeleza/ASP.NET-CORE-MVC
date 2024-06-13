using Capitulo01.Data;
using Capitulo01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Capitulo01.Controllers
{
    public class InstituicaoController : Controller
    {
        private readonly IESContext _esContext;

        private static IList<Instituicao> instituicoes = new List<Instituicao>()
        {
            new Instituicao()
            {
                Id = 1,
                Nome = "UniParaná",
                Endereco = "Paraná"
            },
            new Instituicao()
            {
                Id = 2,
                Nome = "UniAmazonas",
                Endereco = "Amazonas"
            },
            new Instituicao()
            {
                Id = 3,
                Nome = "UniSãoPaulo",
                Endereco = "São Paulo"
            },
            new Instituicao()
            {
                Id = 4,
                Nome = "UniPará",
                Endereco = "Pará"
            },
            new Instituicao()
            {
                Id = 5,
                Nome = "UniCarioca",
                Endereco = "Rio de Janeiro"
            },
        };

        public InstituicaoController(IESContext esContext)
        {
            _esContext = esContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _esContext.Instituicoes.OrderBy(i => i.Id).ToListAsync());
        }

        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Instituicao instituicao)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _esContext.Add(instituicao);
                    await _esContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Não foi possível inserir os dados.");
            }
            return View(instituicao);
        }

        public IActionResult Editar(int id)
        {
            return View(instituicoes.Where(i => i.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Editar(Instituicao instituicao)
        {
            instituicoes.Remove(instituicoes.Where(i => i.Id == instituicao.Id).FirstOrDefault());
            instituicoes.Add(instituicao);
            return RedirectToAction("Index");
        }

        public IActionResult Detalhes(int id)
        {
            return View(instituicoes.Where(i => i.Id == id).FirstOrDefault());
        }

        public IActionResult Deletar(int id)
        {
            return View(instituicoes.Where(i => i.Id == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Deletar(Instituicao instituicao)
        {
            instituicoes.Remove(instituicoes.Where(i => i.Id == instituicao.Id).FirstOrDefault());
            return RedirectToAction("Index");
        }
    }
}
