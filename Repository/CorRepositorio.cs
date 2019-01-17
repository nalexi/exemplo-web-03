using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Repository.Database;
using Repository.Interface;
using Models;

namespace Repository
{
    public class CorRepositorio : IRepositorio<Cor>
    {

        private SqlCommand comando;

        public void Alterar(Cor cor)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE cores SET
                                    nome = @NOME
                                    WHERE id = @ID";
            comando.Parameters.AddWithValue("@NOME", cor.Nome);
            comando.Parameters.AddWithValue("@ID", cor.Id);
            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public void Apagar(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE cores SET registro_ativo = 0 WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public int Inserir(Cor cor)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"INSERT INTO cores
                                    (nome, registro_ativo)
                                    OUTPUT INSERTED.ID
                                    VALUES(@NOME, 1)";
            comando.Parameters.AddWithValue("@NOME", cor.Nome);

            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());
            comando.Connection.Close();
            return id;
        }

        public Cor ObterPeloId(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM cores WHERE id = @ID AND registro_ativo = 1";
            comando.Parameters.AddWithValue("@ID", id);

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());

            Cor cor = null;
            if (table.Rows.Count == 1)
            {
                cor = new Cor();
                DataRow row = table.Rows[0];
                cor.Id = Convert.ToInt32(row["id"].ToString());
                cor.Nome = row["nome"].ToString();
            }
            comando.Connection.Close();
            return cor != null ? cor : null;

        }

        public List<Cor> ObterTodos(string busca)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM cores WHERE registro_ativo = 1 AND nome LIKE @BUSCA ORDER BY nome";
            busca = "%" + busca + "%";
            comando.Parameters.AddWithValue("@BUSCA", busca);
            
            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());

            List<Cor> cores = new List<Cor>();
            foreach (DataRow row in table.Rows)
            {
                Cor cor = new Cor();
                cor.Id = Convert.ToInt32(row["id"].ToString());
                cor.Nome = row["nome"].ToString();

                cores.Add(cor);
            }
            comando.Connection.Close();
            return cores;
        }
        public int InserirRapido(Cor cor)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"INSERT INTO cores
                                    (nome, registro_ativo)
                                    OUTPUT INSERTED.ID
                                    VALUES(@NOME, 1)";
            comando.Parameters.AddWithValue("@NOME", cor.Nome);

            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());
            comando.Connection.Close();
            return id;
        }
    }
}
