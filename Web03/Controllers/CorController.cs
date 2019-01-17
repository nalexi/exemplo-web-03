using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Repository;

namespace Web03.Controllers
{
    public class CorController : Controller
    {
        private readonly CorRepositorio repositorio;

        public CorController()
        {
            repositorio = new CorRepositorio();
        }

        [HttpGet]
        public ActionResult Index(string busca = "")
        {
            List<Cor> cores = repositorio.ObterTodos(busca);
            ViewBag.Cores = cores;
            return View();
        }

        [HttpGet]
        public ActionResult IndexAjax()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CadastrarAjax()
        {
            return View("Cadastrar");
        }

        [HttpGet]
        public ActionResult CadastroRapidp()
        {
            return View();

        }

        [HttpGet]
        public JsonResult ObterDados()
        {
            List<Cor> cores = repositorio.ObterTodos("");
            string json = JsonConvert.SerializeObject(cores);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult StoreRapido(Cor cor)
        {
            cor.RegistroAtivo = true;
            int id = repositorio.Inserir(cor);
            cor.Id = id;
            return Json(JsonConvert.SerializeObject(cor));
        }

        [HttpPost]
        public ActionResult Store(Cor cor)
        {
            cor.RegistroAtivo = true;
            int id = repositorio.Inserir(cor);
            return Redirect("/cor");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            repositorio.Apagar(id);
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult DeleteAjax(int id)
        {
            repositorio.Apagar(id);
            var retorno = new { status = "ok" };
            var json = JsonConvert.SerializeObject(retorno);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Cor cor = repositorio.ObterPeloId(id);
            ViewBag.Cor = cor;
            return View();
        }

        [HttpPost]
        public ActionResult Update(Cor cor)
        {
            Cor corPrincipal = repositorio.ObterPeloId(cor.Id);
            corPrincipal.Nome = cor.Nome;

            repositorio.Alterar(corPrincipal);
            return RedirectToAction("Editar", new { id = corPrincipal.Id });
        }

        [HttpGet]
        public ActionResult Cadastro()
        {
            return View();

        }
    }
}