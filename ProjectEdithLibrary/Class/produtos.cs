using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEdithLibrary.Class
{
    public class Produtos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Id_marca { get; set; }
        public string Nome_marca { get; set; }
        public int Id_fornecedor { get; set; }
        public string Nome_fornecedor { get; set; }
        public int Qtd_produtos { get; set; }
    }
}
