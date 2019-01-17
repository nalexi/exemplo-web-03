using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Repository.Interface
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
