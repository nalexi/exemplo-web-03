using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Web03.Database;
using Web03.Interface;
using Web03.Models;

namespace Web03.Repositories
{
    public class CarroRepositorio : IRepositorio<Carro>
    {
        private SqlCommand comando;

        public void Alterar(Carro carro)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE carros SET
                                    modelo = @MODELO,
                                    preco = @PRECO,
                                    data_compra = @DATA_COMPRA
                                    WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", carro.Id);
            comando.Parameters.AddWithValue("@MODELO", carro.Modelo);
            comando.Parameters.AddWithValue("@PRECO", carro.Preco);
            comando.Parameters.AddWithValue("@DATA_COMPRA", carro.DataCompra);
            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public void Apagar(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE carros SET registro_ativo = 0 WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            comando.ExecuteNonQuery();
            comando.Connection.Close();
        }

        public int Inserir(Carro carro)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"INSERT INTO carros
                                    (modelo, preco, data_compra, registro_ativo)
                                    OUTPUT INSERTED.ID
                                    VALUES (@MODELO, @PRECO, @DATA_COMPRA, 1)";
            comando.Parameters.AddWithValue("@ID", carro.Id);
            comando.Parameters.AddWithValue("@MODELO", carro.Modelo);
            comando.Parameters.AddWithValue("@PRECO", carro.Preco);
            comando.Parameters.AddWithValue("@DATA_COMPRA", carro.DataCompra);

            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());
            comando.Connection.Close();
            return id;
        }

        public Carro ObterPeloId(int id)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM carros WHERE id = @ID AND registro_ativo = 1";

            comando.Parameters.AddWithValue("@ID", id);
            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());
            Carro carro = null;
            if (table.Rows.Count == 1)
            {
                carro = new Carro();
                DataRow row = table.Rows[0];

                carro.Id = Convert.ToInt32(row["id"].ToString());
                carro.Modelo = row["modelo"].ToString();
                carro.Preco = Convert.ToDecimal(row["preco"].ToString());
                carro.DataCompra = Convert.ToDateTime(row["data_compra"].ToString());
            }
            comando.Connection.Close();
            return carro != null ? carro : null; 

        }

        public List<Carro> ObterTodos(string busca)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT * FROM carros WHERE registro_ativo = 1 AND modelo LIKE @BUSCA ORDER BY modelo";
            busca = "%" + busca + "%";
            comando.Parameters.AddWithValue("@BUSCA", busca);

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());

            List<Carro> carros = new List<Carro>();
            foreach (DataRow row in table.Rows)
            {
                Carro carro = new Carro();

                carro.Id = Convert.ToInt32(row["id"].ToString());
                carro.Modelo = row["modelo"].ToString();
                carro.Preco = Convert.ToDecimal(row["preco"].ToString());
                carro.DataCompra = Convert.ToDateTime(row["data_compra"].ToString());

                carros.Add(carro);
            }
            comando.Connection.Close();
            return carros;
        }
        public int InserirRapido(Carro carro)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"INSERT INTO carros
                                    (modelo, preco, data_compra, registro_ativo)
                                    OUTPUT INSERTED.ID
                                    VALUES (@MODELO, @PRECO, @DATA_COMPRA, 1)";
            comando.Parameters.AddWithValue("@ID", carro.Id);
            comando.Parameters.AddWithValue("@MODELO", carro.Modelo);
            comando.Parameters.AddWithValue("@PRECO", carro.Preco);
            comando.Parameters.AddWithValue("@DATA_COMPRA", carro.DataCompra);

            int id = Convert.ToInt32(comando.ExecuteScalar().ToString());
            comando.Connection.Close();
            return id;
        }
    }
}