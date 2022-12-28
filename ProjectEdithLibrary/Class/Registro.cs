using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEdithLibrary.Class
{
    public class Registro
    {
        public int Id { get; set; }
        public int IdStatus { get; set; }
        public string Estado { get; set; }
        public int IdProduto { get; set; }
        public string Produto { get; set; }
        public int IdMarca { get; set; }
        public string Marca { get; set; }
        public int IdFornecedor { get; set; }
        public string Fornecedor { get; set; }
        public int IdFuncionario { get; set; }
        public string Funcionario { get; set; }
        public DateTime DataRegistro { get; set; }
        public int QtdProdutos { get; set; }

    }
}
