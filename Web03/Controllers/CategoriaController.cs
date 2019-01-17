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
    public class CategoriaController : Controller
    {
        private readonly CategoriaRepositorio repositorio;

        public CategoriaController()
        {
            repositorio = new CategoriaRepositorio();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObterDados()
        {
            List<Categoria> categorias = repositorio.ObterTodos("");
            string json = JsonConvert.SerializeObject(categorias);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult StoreRapido(Categoria categoria)
        {
            categoria.RegistroAtivo = true;
            int id = repositorio.Inserir(categoria);
            categoria.Id = id;
            return Json(JsonConvert.SerializeObject(categoria));
        }

        [HttpPost]
        public ActionResult Store(Categoria categoria)
        {
            categoria.RegistroAtivo = true;
            int id = repositorio.Inserir(categoria);
            return Redirect("/categoria");
        }

        [HttpGet]
        public ActionResult DeleteAjax(int id)
        {
            repositorio.Apagar(id);
            var retorno = new { status = "ok" };
            var json = JsonConvert.SerializeObject(retorno);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(Categoria categoria)
        {
            Categoria categoriaPrincipal = repositorio.ObterPeloId(categoria.Id);
            categoriaPrincipal.Nome = categoria.Nome;

            repositorio.Alterar(categoriaPrincipal);
            return Json(JsonConvert.SerializeObject(categoriaPrincipal));
        }
        
        [HttpGet]
        public ActionResult ObterPeloId(int id)
        {
            var categoria = repositorio.ObterPeloId(id);
            var json = JsonConvert.SerializeObject(categoria);
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }

}