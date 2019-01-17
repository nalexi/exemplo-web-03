using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web03.Models;
using Web03.Repositories;

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
        public ActionResult Index(string busca = "")
        {
            List<Categoria> categorias = repositorio.ObterTodos(busca);
            ViewBag.Categorias = categorias;
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
        public ActionResult Delete(int id)
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
            Categoria categoria = repositorio.ObterPeloId(id);
            ViewBag.Categoria = categoria;
            return View();
        }

        [HttpPost]
        public ActionResult Update(Categoria categoria)
        {
            Categoria categoriaPrincipal = repositorio.ObterPeloId(categoria.Id);
            categoriaPrincipal.Nome = categoria.Nome;

            repositorio.Alterar(categoriaPrincipal);
            return RedirectToAction("Editar", new { id = categoriaPrincipal.Id });
        }
    }

}