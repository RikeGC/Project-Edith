using ProjectEdithLibrary.Class;
using ProjectEdithLibrary.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_Edith
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FuncionarioRepos repo = new FuncionarioRepos();
            Funcionario funcionario;
            funcionario = repo.ConsultarPorEmail(txtEmailLogin.Text);
            if (funcionario != null && funcionario.Senha == txtSenhaLogin.Password)
            {
                MessageBox.Show("Seja Bem Vindo " + funcionario.Nome);
                Aplicacao.FuncionarioLogado = funcionario;
                janela1 janela1 = new janela1();
                this.Hide();
                janela1.ShowDialog();
                this.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Funcionario/Senha está incorreto");
            }

        }

    }
}
