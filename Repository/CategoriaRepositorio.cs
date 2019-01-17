using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Database;
using Repository.Interface;
using Models;

namespace Repository
{
    public class CategoriaRepositorio : IRepositorio<Categoria>
    {
        private SqlCommand comando;
        public void Alterar(Categoria categoria)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE categorias SET
                                    nome = @NOME
                                    WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", categoria.Nome);
            comando.Parameters.AddWithValue("@ID", categoria.Id);
            comando.ExecuteNonQuery();
            comando.Connection.Close();

        }

        public void Apagar(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE categorias SET registro_ativo = 0 WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public int Inserir(Categoria categoria)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"INSERT INTO categorias
                                    (nome, registro_ativo)
                                    OUTPUT INSERTED.ID
                                    VALUES (@NOME, 1)";
            comando.Parameters.AddWithValue("@NOME", categoria.Nome);

            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());

            comando.Connection.Close();
            return id;
        }

        public Categoria ObterPeloId(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM categorias WHERE id = @ID AND registro_ativo =1";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());
            Categoria categoria = null;
            if (table.Rows.Count == 1)
            {
                categoria = new Categoria();
                DataRow row = table.Rows[0];
                categoria.Id = Convert.ToInt32(row["id"].ToString());
                categoria.Nome = row["nome"].ToString();
            }
            comando.Connection.Close();
            return categoria != null ? categoria : null;
        }

        public List<Categoria> ObterTodos(string busca)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM categorias WHERE registro_ativo = 1 AND nome lIKE @BUSCA ORDER BY nome";
            busca = "%" + busca + "%";
            comando.Parameters.AddWithValue("@BUSCA", busca);
            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());

            List<Categoria> categorias = new List<Categoria>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                Categoria categoria = new Categoria();
                categoria.Id = Convert.ToInt32(table.Rows[i]["id"].ToString());
                categoria.Nome = table.Rows[i]["nome"].ToString();
                categorias.Add(categoria);
            }
            comando.Connection.Close();
            return categorias;
        }
    }
}
