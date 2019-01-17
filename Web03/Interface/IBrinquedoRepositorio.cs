using Web03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web03.Interface
{
    interface IBrinquedoRepositorio
    {
        List<Brinquedo> ObterTodos(string busca);

        Brinquedo ObterPeloId(int id);

        int Inserir(Brinquedo brinquedo);

        void Alterar(Brinquedo brinquedo);

        void Apagar(int id);
    }
}
