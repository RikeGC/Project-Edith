using ProjectEdithLibrary.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Edith
{
    public class Aplicacao
    {
        public static Funcionario FuncionarioLogado { get; set; }
        public static bool TestarConexao()
        {
            //Busca Conexão
            string connectionString = ProjectEdithLibrary.Repos.Conexao.ConsultarConexao();

            //Criando Conexão
            SqlConnection conexao = new SqlConnection(connectionString);

            try
            {   //Abrindo Conexao
                conexao.Open();
                conexao.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
