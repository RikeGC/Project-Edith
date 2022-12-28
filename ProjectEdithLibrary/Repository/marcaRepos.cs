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
    public class MarcaRepos
    {
        public List<Marca> Pesquisar(string filtro)
        {
            List<Marca> marca;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " SELECT cod_marca Id, nome Nome, telefone Telefone FROM marca WHERE nome like '%'+@FILTRO+ '%' OR telefone like '%' + @FILTRO + '%' ; ";

                marca = conn.Query<Marca>(script, new { @FILTRO = filtro }).ToList();
            }
            return marca;

        }

        public List<Marca> Consultar()
        {
            List<Marca> marca;
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " SELECT cod_marca Id, nome Nome, telefone Telefone FROM marca ORDER BY cod_marca ; ";

                marca = conn.Query<Marca>(script/*, new {  }*/).ToList();
            }
            return marca;
        }

        public void Inserir(Marca marca)
        {
            //abrir a conexão
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " INSERT INTO marca(nome, telefone) VALUES(@nome, @telefone); ";

                conn.Execute(script, new { @nome = marca.Nome, @telefone = marca.Telefone });
            }
        }

        public void Alterar(Marca marca)
        {
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " UPDATE marca SET nome = @nome, telefone = @telefone WHERE cod_marca = @id; ";

                conn.Execute(script, new { @nome = marca.Nome, @telefone = marca.Telefone, @id = marca.Id });
            }
        }

        public void Excluir(int Id)
        {
            using (SqlConnection conn = new SqlConnection(Conexao.ConsultarConexao()))
            {
                string script = " DELETE marca WHERE cod_marca = @id; ";
                conn.Execute(script, new { @id = Id });
            }
        }
    }
}
