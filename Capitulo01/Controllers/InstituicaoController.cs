using Capitulo01.Models;
using Microsoft.AspNetCore.Mvc;

namespace Capitulo01.Controllers
{
    public class InstituicaoController : Controller
    {
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

        public IActionResult Index()
        {
            return View(instituicoes);
        }

        public ActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Criar(Instituicao instituicao)
        {
            instituicoes.Add(instituicao);
            instituicao.Id = instituicoes.Select(i => i.Id).Max() + 1;
            return RedirectToAction("Index");
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
