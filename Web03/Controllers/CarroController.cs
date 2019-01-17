using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;
using Models;
using Repository;

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
            List<Categoria> categorias = new CategoriaRepositorio().ObterTodos("");
            ViewBag.Categorias = categorias;
            return View();

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            repositorio.Apagar(id);
            return RedirectToAction("index");
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            Carro carro = repositorio.ObterPeloId(id);
            ViewBag.Carro = carro;

            List<Categoria> categorias = new CategoriaRepositorio().ObterTodos("");
            ViewBag.Categorias = categorias;
            return View();
        }

        [HttpPost]
        public ActionResult Update(Carro carro)
        {
            Carro carroPrincipal = repositorio.ObterPeloId(carro.Id);
            carroPrincipal.IdCategoria = carro.IdCategoria;
            carroPrincipal.Modelo = carro.Modelo;
            carroPrincipal.Preco = carro.Preco;
            carroPrincipal.DataCompra = carro.DataCompra;
            repositorio.Alterar(carroPrincipal);
            return RedirectToAction("Editar", new {id = carroPrincipal.Id });
        }
    }
}