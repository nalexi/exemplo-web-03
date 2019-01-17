using Web03.Database;
using Web03.Interface;
using Web03.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Web03.Repositories
{
    public class BrinquedoRepositorio : IRepositorio<Brinquedo>
    {
        private SqlCommand comando;

        public void Alterar(Brinquedo brinquedo)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE brinquedos SET
                                    nome = @NOME,
                                    marca = @MARCA,
                                    preco = @PRECO,
                                    estoque = @ESTOQUE
                                    WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", brinquedo.Id);
            comando.Parameters.AddWithValue("@NOME", brinquedo.Nome);
            comando.Parameters.AddWithValue("@MARCA", brinquedo.Marca);
            comando.Parameters.AddWithValue("@PRECO", brinquedo.Preco);
            comando.Parameters.AddWithValue("@ESTOQUE", brinquedo.Estoque);

            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public void Apagar(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE brinquedos SET registro_ativo = 0 WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public int Inserir(Brinquedo brinquedo)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"INSERT INTO brinquedos
                                    (nome, marca, preco, estoque, registro_ativo)
                                    OUTPUT INSERTED.ID
                                    VALUES (@NOME, @MARCA, @PRECO, @ESTOQUE, 1)";
            comando.Parameters.AddWithValue("@ID", brinquedo.Id);
            comando.Parameters.AddWithValue("@NOME", brinquedo.Nome);
            comando.Parameters.AddWithValue("@MARCA", brinquedo.Marca);
            comando.Parameters.AddWithValue("@PRECO", brinquedo.Preco);
            comando.Parameters.AddWithValue("@ESTOQUE", brinquedo.Estoque);
            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());
            comando.Connection.Close();
            return id;      
        }

        public Brinquedo ObterPeloId(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM brinquedos WHERE id = @ID AND registro_ativo = 1";

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());

            Brinquedo brinquedo = null;
            if (table.Rows.Count == 1)
            {
                brinquedo = new Brinquedo();
                DataRow row = table.Rows[0];
                brinquedo.Id = Convert.ToInt32(row["id"].ToString());
                brinquedo.Nome = row["nome"].ToString();
                brinquedo.Marca = row["marca"].ToString();
                brinquedo.Preco = Convert.ToDecimal(row["preco"].ToString());
                brinquedo.Estoque = Convert.ToInt16(row["estoque"].ToString());
            }
            comando.Connection.Close();
            return brinquedo == null ? brinquedo : null;

        }

        public List<Brinquedo> ObterTodos(string busca)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM brinquedos WHERE registro_ativo = 1 AND nome LIKE @BUSCA ORDER BY nome";
            busca = "%" + busca + "%";
            comando.Parameters.AddWithValue("@BUSCA", busca);

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());

            List<Brinquedo> brinquedos = new List<Brinquedo>();
            foreach (DataRow row in table.Rows)
            {
                Brinquedo brinquedo = new Brinquedo();
                brinquedo.Id = Convert.ToInt32(row["id"].ToString());
                brinquedo.Nome = row["nome"].ToString();
                brinquedo.Marca = row["marca"].ToString();
                brinquedo.Preco = Convert.ToDecimal(row["preco"].ToString());
                brinquedo.Estoque = Convert.ToInt16(row["estoque"].ToString());
                brinquedos.Add(brinquedo);
            }
            comando.Connection.Close();
            return brinquedos;
        }

        public int InserirRapido(Brinquedo brinquedo)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"INSERT INTO brinquedos (nome, marca, preco, estoque, registro_ativo)
                                    OUTPUT INSERTED.ID
                                    VALUES(@NOME, @MARCA, @PRECO, @ESTOQUE, 1";
            comando.Parameters.AddWithValue("@NOME", brinquedo.Nome);
            comando.Parameters.AddWithValue("@MARCA", brinquedo.Marca);
            comando.Parameters.AddWithValue("@PRECO", brinquedo.Preco);
            comando.Parameters.AddWithValue("@ESTOQUE", brinquedo.Estoque);

            int id = Convert.ToInt16(comando.ExecuteScalar().ToString());
            comando.Connection.Close();
            return id;


        }
    }
}