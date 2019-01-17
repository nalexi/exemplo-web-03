using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web03.Models;

namespace Web03.Interface
{
    interface ICarroRepositorio
    {
        List<Carro> ObterTodos(string busca);

        Carro ObterPeloId(int id);

        int Inserir(Carro carro);

        void Alterar(Carro carro);

        void Apagar(int id);

    }
}
