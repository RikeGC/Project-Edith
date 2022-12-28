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
    public class FornecedorRepos
    {
        public List<Fornecedor> Pesquisar(string filtro)
        {
            List<Fornecedor> fornecedor;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " SELECT cod_fornecedor Id, nome Nome, cnpj CNPJ, telefone Telefone, endereco Endereco, bairro Bairro, cidade Cidade, uf UF, cep CEP FROM fornecedor WHERE nome like '%'+@FILTRO+ '%' OR cnpj like '%' + @FILTRO + '%'  ; ";

                fornecedor = conn.Query<Fornecedor>(script, new { @FILTRO = filtro }).ToList();
            }
            return fornecedor;

        }

        public List<Fornecedor> Consultar()
        {
            List<Fornecedor> fornecedor;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " SELECT cod_fornecedor Id, nome Nome, cnpj CNPJ, endereco Endereco, bairro Bairro, cidade Cidade, uf UF, cep CEP FROM fornecedor ORDER BY cod_fornecedor; ";

                fornecedor = conn.Query<Fornecedor>(script/*, new {  }*/).ToList();
            }
            return fornecedor;
        }
        public List<Fornecedor> Consultar2(int id)
        {
            List<Fornecedor> fornecedor;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " SELECT fornecedor.cod_fornecedor Id, fornecedor.nome Nome, cnpj CNPJ, endereco Endereco, bairro Bairro, cidade Cidade, uf UF, cep CEP FROM fornecedor join produtos on fornecedor.cod_fornecedor = produtos.cod_fornecedor where produtos.cod_produtos = @id ORDER BY fornecedor.cod_fornecedor ; ";

                fornecedor = conn.Query<Fornecedor>(script, new { @id = id }).ToList();
            }
            return fornecedor;
        }

        public void Inserir(Fornecedor fornecedor)
        {
            //abrir a conexão
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " INSERT INTO fornecedor(nome, telefone, cnpj, endereco, bairro, cidade, uf, cep) VALUES(@nome, @telefone, @cnpj, @endereco, @bairro, @cidade, @uf, @cep); ";

                conn.Execute(script, new { @nome = fornecedor.Nome, @telefone = fornecedor.Telefone, @cnpj = fornecedor.Cnpj, @endereco = fornecedor.Endereco, @bairro = fornecedor.Bairro, @cidade = fornecedor.Cidade, @uf = fornecedor.Uf, @cep = fornecedor.Cep });
            }
        }

        public void Alterar(Fornecedor fornecedor)
        {
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " UPDATE fornecedor SET nome = @nome, telefone = @telefone, cnpj = @cnpj, endereco = @endereco, bairro = @bairro, cidade = @cidade, uf = @uf, cep = @cep WHERE cod_fornecedor = @id; ";

                conn.Execute(script, new { @nome = fornecedor.Nome, @telefone = fornecedor.Telefone, @cnpj = fornecedor.Cnpj, @endereco = fornecedor.Endereco, @bairro = fornecedor.Bairro, @cidade = fornecedor.Cidade, @uf = fornecedor.Uf, @id = fornecedor.Id, @cep = fornecedor.Cep });
            }
        }

        public void Excluir(int Id)
        {
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " DELETE fornecedor WHERE cod_fornecedor = @id; ";
                conn.Execute(script, new { @id = Id });
            }
        }
    }
}
