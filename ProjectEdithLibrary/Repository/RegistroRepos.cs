using Dapper;
using ProjectEdithLibrary.Class;
using ProjectEdithLibrary.Repos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEdithLibrary.Repository
{
    public class RegistroRepos
    {
        public List<Registro> Pesquisar(string filtro)
        {
            List<Registro> registro;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " SELECT cod_registro Id, registro_de_movimentacao.cod_status_registro IdStatus, status_registro.nome_status_registro Estado, registro_de_movimentacao.cod_produtos IdProdutos, produtos.nome Produto, registro_de_movimentacao.cod_marca IdMarca, marca.nome Marca, registro_de_movimentacao.cod_fornecedor IdFornecedor, fornecedor.nome Fornecedor, registro_de_movimentacao.cod_funcionario IdFuncionario, funcionario.nome Funcionario, data_registro DataRegistro, registro_de_movimentacao.qtd_produtos QtdProdutos FROM registro_de_movimentacao join status_registro on registro_de_movimentacao.cod_status_registro = status_registro.cod_status_registro join marca on registro_de_movimentacao.cod_marca = marca.cod_marca join produtos on registro_de_movimentacao.cod_produtos = produtos.cod_produtos join fornecedor on fornecedor.cod_fornecedor = registro_de_movimentacao.cod_fornecedor join funcionario on registro_de_movimentacao.cod_funcionario = funcionario.cod_funcionario WHERE nome_status_registro like '%'+@FILTRO+ '%' OR cpf like '%' + @FILTRO + '%' OR cnpj like '%' + @FILTRO + '%' OR produtos.nome like '%' + @FILTRO + '%' OR marca.nome like '%'+@FILTRO+ '%' OR fornecedor.nome like '%' + @FILTRO + '%' OR funcionario.nome like '%' + @FILTRO + '%' OR data_registro like '%' + @FILTRO + '%'; ";

                registro = conn.Query<Registro>(script, new { @FILTRO = filtro }).ToList();
            }
            return registro;

        }

        public void Inserir(Registro registro)
        {
            //abrir a conexão
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " INSERT INTO registro_de_movimentacao(cod_status_registro, cod_produtos, cod_marca, data_registro, cod_fornecedor, cod_funcionario, qtd_produtos) VALUES(@status, @produtos, @marca, @data, @fornecedor, @funcionario, @qtd); " +
                    "if @status = 1 UPDATE produtos SET qtd_produtos = (qtd_produtos + @qtd) WHERE cod_produtos = @produtos; if @status = 2 UPDATE produtos SET qtd_produtos = (qtd_produtos - @qtd) WHERE cod_produtos = @produtos;";

                conn.Execute(script, new { @status = registro.IdStatus, @produtos = registro.IdProduto, @marca = registro.IdMarca, @data = registro.DataRegistro, @fornecedor = registro.IdFornecedor, @funcionario = registro.IdFuncionario, @qtd = registro.QtdProdutos });
            }
        }

        public void Alterar(Registro registro)
        {
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " UPDATE registro_de_movimentacao SET cod_status_registro = @status, cod_produtos = @produtos, cod_marca = @marca, data_registro = @data, cod_fornecedor = @fornecedor, cod_funcionario = @funcionario, qtd_produtos = @qtd WHERE cod_registro = @id; " +
                    "if @status = 1 UPDATE produtos SET qtd_produtos = (qtd_produtos + @qtd) WHERE cod_produtos = @produtos; if @status = 2 UPDATE produtos SET qtd_produtos = (qtd_produtos - @qtd) WHERE cod_produtos = @produtos;";

                conn.Execute(script, new { @status = registro.IdStatus, @produtos = registro.IdProduto, @marca = registro.IdMarca, @data = registro.DataRegistro, @fornecedor = registro.IdFornecedor, @funcionario = registro.IdFuncionario, @id = registro.Id, @qtd = registro.QtdProdutos });
            }
        }

        public void Excluir(int Id)
        {
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " DELETE registro_de_movimentacao WHERE cod_registro = @id; ";
                conn.Execute(script, new { @id = Id });
            }
        }
    }
}
