using Dapper;
using ProjectEdithLibrary.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ProjectEdithLibrary.Repos;

namespace ProjectEdithLibrary.repository
{
    public class FuncionarioRepos
    {
        public Funcionario ConsultarPorEmail(string email)
        {
            Funcionario funcionario;
            //Abrindo Conexão
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                //Gerando Script SQL
                string script = "SELECT funcionario.cod_funcionario id, funcionario.nome nome, funcionario.email email, funcionario.senha senha, funcionario.cpf cpf, funcionario.endereco endereco, funcionario.bairro bairro, funcionario.cidade cidade, funcionario.uf uf, tipo_funcionario.cod_tipo_funcionario id_tipo_funcionario, tipo_funcionario.nome_tipo_funcionario nome_tipo_funcionario FROM funcionario join tipo_funcionario on tipo_funcionario.cod_tipo_funcionario = funcionario.cod_tipo_funcionario WHERE email = @email OR cpf = @email;";
                //Buscando o primeiro
                funcionario = conn.QueryFirstOrDefault<Funcionario>(script, new { email });
            }
            return funcionario;
        }
        
        public List<Funcionario> Pesquisar(string filtro)
        {
            List<Funcionario> funcionarios;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " SELECT funcionario.cod_funcionario ID, funcionario.nome Nome, funcionario.email Email, funcionario.senha Senha, funcionario.cpf Cpf, funcionario.endereco Endereco, funcionario.bairro Bairro, funcionario.cidade Cidade, funcionario.uf Uf, funcionario.cep Cep, tipo_funcionario.cod_tipo_funcionario id_tipo_funcionario, tipo_funcionario.nome_tipo_funcionario nome_tipo_funcionario FROM funcionario join tipo_funcionario on tipo_funcionario.cod_tipo_funcionario = funcionario.cod_tipo_funcionario WHERE nome like '%'+@FILTRO+ '%' OR cpf like '%' + @FILTRO + '%' OR email like '%' + @FILTRO + '%' OR nome_tipo_funcionario like '%' + @FILTRO + '%'; ";

                funcionarios = conn.Query<Funcionario>(script, new { @FILTRO = filtro }).ToList();
            }
            return funcionarios;

        }

        public void Inserir(Funcionario funcionario)
        {
            //abrir a conexão
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " INSERT INTO funcionario( nome, email, senha, cpf, endereco, bairro, cidade, uf, cod_tipo_funcionario, cep) VALUES( @nome, @email, @senha, @cpf, @endereco, @bairro, @cidade, @uf, @tipo, @cep); ";

                conn.Execute(script, new { @nome = funcionario.Nome, @email = funcionario.Email, @senha = funcionario.Senha, @cpf = funcionario.Cpf, @endereco = funcionario.Endereco, @bairro = funcionario.Bairro, @cidade = funcionario.Cidade, @uf = funcionario.Uf, @tipo = funcionario.Id_tipo_funcionario, @cep = funcionario.Cep });
            }
        }

        public void Alterar(Funcionario funcionario)
        {
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " UPDATE funcionario SET email = @email, senha = @senha, nome = @nome, cpf = @cpf, cod_tipo_funcionario = @tipo, bairro = @bairro, cidade = @cidade, uf = @uf, cep = @cep WHERE cod_funcionario = @id ";

                conn.Execute(script, new { @email = funcionario.Email, @senha = funcionario.Senha, @nome = funcionario.Nome, @cpf = funcionario.Cpf, @cep = funcionario.Cep, @tipo = funcionario.Id_tipo_funcionario, @bairro = funcionario.Bairro, @cidade = funcionario.Cidade, @uf = funcionario.Uf, @id = funcionario.Id  });
            }
        }

        public void Excluir(int Id)
        {
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " DELETE funcionario WHERE cod_funcionario = @id; ";
                conn.Execute(script, new { @id = Id });
            }
        }
    }
}
