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
    public class CarroRepositorio : IRepositorio<Carro>
    {
        private SqlCommand comando;

        public void Alterar(Carro carro)
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"UPDATE carros SET
                                    id_categoria = @ID_CATEGORIA,
                                    modelo = @MODELO,
                                    preco = @PRECO,
                                    data_compra = @DATA_COMPRA
                                    WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID_CATEGORIA", carro.IdCategoria);
            comando.Parameters.AddWithValue("@MODELO", carro.Modelo);
            comando.Parameters.AddWithValue("@PRECO", carro.Preco);
            comando.Parameters.AddWithValue("@DATA_COMPRA", carro.DataCompra);
            comando.Parameters.AddWithValue("@ID", carro.Id);
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
                                    (id_categoria, modelo, preco, data_compra, registro_ativo)
                                    OUTPUT INSERTED.ID
                                    VALUES (@ID_CATEGORIA, @MODELO, @PRECO, @DATA_COMPRA, 1)";
            comando.Parameters.AddWithValue("@ID_CATEGORIA", carro.IdCategoria);
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
                carro.IdCategoria = Convert.ToInt32(row["id_categoria"].ToString());
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
            comando.CommandText = @"SELECT ca.id_categoria, cat.nome, ca.id, ca.modelo 'modeloCarro', ca.preco, ca.data_compra
                                    FROM carros ca
                                    INNER JOIN categorias cat ON(ca.id_categoria = cat.id)
                                    WHERE ca.registro_ativo = 1 AND ca.modelo LIKE @BUSCA ORDER BY ca.modelo";
            busca = "%" + busca + "%";
            comando.Parameters.AddWithValue("@BUSCA", busca);
            

            DataTable table = new DataTable();
            table.Load(comando.ExecuteReader());

            List<Carro> carros = new List<Carro>();
            foreach (DataRow row in table.Rows)
            {
                Carro carro = new Carro();

                carro.IdCategoria = Convert.ToInt32(row["id_categoria"].ToString());
                carro.Categoria = new Categoria();

                carro.Categoria.Id = Convert.ToInt32(row["id_categoria"].ToString());
                carro.Categoria.Nome = row["nome"].ToString();

                carro.Id = Convert.ToInt32(row["id"].ToString());
                carro.Modelo = row["modeloCarro"].ToString();
                carro.Preco = Convert.ToDecimal(row["preco"].ToString());
                carro.DataCompra = Convert.ToDateTime(row["data_compra"].ToString());

                carros.Add(carro);
            }
            comando.Connection.Close();
            return carros;
        }
        public int ContabilizarCarros()
        {
            comando = Conexao.ObterConexao();
            comando.CommandText = @"SELECT COUNT(id) FROM carros WHERE registro_ativo = 1";
            int quantidade = Convert.ToInt32(comando.ExecuteScalar().ToString());

            comando.Connection.Close();
            return quantidade;
        }
    }
}