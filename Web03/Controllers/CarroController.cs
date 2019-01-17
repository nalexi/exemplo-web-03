using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;
using Web03.Models;
using Web03.Repositories;

namespace Web03.Controllers
{
    public class CarroController : Controller
    {
        private readonly CarroRepositorio repositorio;

        public CarroController()
        {
            repositorio = new CarroRepositorio();
        }

        [HttpGet]
        public ActionResult Index(string busca = "")
        {
            List<Carro> carros = repositorio.ObterTodos(busca);
            ViewBag.Carros = carros;
            ViewBag.Title = "Lista de carros";
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
        public ActionResult CadastroRapido()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObterDados()
        {
            List<Carro> carros = repositorio.ObterTodos("");
            string json = JsonConvert.SerializeObject(carros);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult StoreRapido(Carro carro)
        {
            carro.RegistroAtivo = true;
            int id = repositorio.Inserir(carro);
            carro.Id = id;
            return Json(JsonConvert.SerializeObject(carro));
        }

        [HttpPost]
        public ActionResult Store(Carro carro)
        {
            carro.RegistroAtivo = true;
            int id = repositorio.Inserir(carro);
            return Redirect("/carro");
        }

        [HttpGet]
        public ActionResult Cadastro()
        {
            return View();

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
            Carro carro = repositorio.ObterPeloId(id);
            ViewBag.Carro = carro;
            return View();
        }

        [HttpPost]
        public ActionResult Update(Carro carro)
        {
            Carro carroPrincipal = repositorio.ObterPeloId(carro.Id);
            carroPrincipal.Modelo = carro.Modelo;
            carroPrincipal.Preco = carro.Preco;
            carroPrincipal.DataCompra = carro.DataCompra;
            repositorio.Alterar(carroPrincipal);
            return RedirectToAction("Editar", new {id = carroPrincipal.Id });
        }
    }
}