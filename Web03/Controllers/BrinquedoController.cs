using Models;
using Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web03.Controllers
{
    public class BrinquedoController : Controller
    {
        private readonly BrinquedoRepositorio repositorio;

        public BrinquedoController()
        {
            repositorio = new BrinquedoRepositorio();
        }

        [HttpGet]
        public ActionResult Index(string busca = "")
        {
            List<Brinquedo> brinquedos = repositorio.ObterTodos(busca);
            ViewBag.Brinquedos = brinquedos;
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
            List<Brinquedo> brinquedos = repositorio.ObterTodos("");
            string json = JsonConvert.SerializeObject(brinquedos);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult StoreRapido(Brinquedo brinquedo)
        {
            /*Brinquedo brinquedo = new Brinquedo();
            brinquedo.Nome = nome;
            brinquedo.Marca = marca;
            brinquedo.Preco = preco;
            brinquedo.Estoque = estoque;*/
            brinquedo.RegistroAtivo = true;
            int id = repositorio.Inserir(brinquedo);
            brinquedo.Id = id;
            return Json(JsonConvert.SerializeObject(brinquedo));
        }

        [HttpPost]
        public ActionResult Store (Brinquedo brinquedo)
        {
            brinquedo.RegistroAtivo = true;
            int id = repositorio.Inserir(brinquedo);
            return Redirect("/brinquedo");
        }

        [HttpGet]
        public ActionResult Delete (int id)
        {

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
            Brinquedo brinquedo = repositorio.ObterPeloId(id);
            ViewBag.Brinquedo = brinquedo;
            return View();
        }

        [HttpPost]
        public ActionResult Update(Brinquedo brinquedo)
        {
            Brinquedo brinquedoPrincipal = repositorio.ObterPeloId(brinquedo.Id);
            brinquedoPrincipal.Nome = brinquedo.Nome;
            brinquedoPrincipal.Marca = brinquedo.Marca;
            brinquedoPrincipal.Preco = brinquedo.Preco;
            brinquedoPrincipal.Estoque = brinquedo.Estoque;

            repositorio.Alterar(brinquedoPrincipal);
            return RedirectToAction("Editar", new { id = brinquedoPrincipal.Id });
        }
    }
}