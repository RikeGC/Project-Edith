using Dapper;
using ProjectEdithLibrary.Class;
using ProjectEdithLibrary.Repos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEdithLibrary.repository
{
    public class ProdutosRepos
    {
        public List<Produtos> Pesquisar(string filtro)
        {
            List<Produtos> produtos;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " SELECT cod_produtos Id, produtos.nome Nome, produtos.cod_fornecedor Id_fornecedor, fornecedor.nome Nome_fornecedor, produtos.cod_marca Id_marca, marca.nome Nome_marca, qtd_produtos Qtd_produtos FROM produtos join fornecedor on produtos.cod_fornecedor = fornecedor.cod_fornecedor join marca on produtos.cod_marca = marca.cod_marca WHERE produtos.nome like '%'+@FILTRO+ '%' OR qtd_produtos like '%' + @FILTRO + '%' OR marca.nome like '%' + @FILTRO + '%' OR fornecedor.nome like '%' + @FILTRO + '%'; ";

                produtos = conn.Query<Produtos>(script, new { @FILTRO = filtro }).ToList();
            }
            return produtos;

        }

        public List<Produtos> Consultar(int id)
        {
            List<Produtos> produtos;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " SELECT cod_produtos Id, nome Nome, cod_fornecedor Id_fornecedor, cod_marca Id_marca, qtd_produtos Qtd_produtos FROM produtos WHERE cod_marca = @id; ";

                produtos = conn.Query<Produtos>(script, new { @id = id }).ToList();
            }
            return produtos;
        }

        public void Inserir(Produtos produtos)
        {
            //abrir a conexão
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " INSERT INTO produtos(nome, cod_marca, cod_fornecedor, qtd_produtos) VALUES(@nome, @marca, @fornecedor, @qtd); ";

                conn.Execute(script, new { @nome = produtos.Nome, @marca = produtos.Id_marca, @fornecedor = produtos.Id_fornecedor, @qtd = produtos.Qtd_produtos, @id = produtos.Id });
            }
        }

        public void Alterar(Produtos produtos)
        {
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " UPDATE produtos SET nome = @nome, cod_marca = @marca, cod_fornecedor = @fornecedor, qtd_produtos = @qtd WHERE cod_produtos = @id; ";

                conn.Execute(script, new { @nome = produtos.Nome, @marca = produtos.Id_marca, @fornecedor = produtos.Id_fornecedor, @qtd = produtos.Qtd_produtos, @id = produtos.Id });
            }
        }

        public void Excluir(int Id)
        {
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " DELETE produtos WHERE cod_produtos = @id; ";
                conn.Execute(script, new { @id = Id });
            }
        }
    }
}
