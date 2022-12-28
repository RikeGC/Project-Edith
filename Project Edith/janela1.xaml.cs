using ProjectEdithLibrary.Class;
using ProjectEdithLibrary.repository;
using ProjectEdithLibrary.Repository;
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
using System.Windows.Shapes;
using System.IO;
using System.Data;
using System.Reflection;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp;
using Paragraph = iTextSharp.text.Paragraph;

namespace Project_Edith
{
    /// <summary>
    /// Lógica interna para janela1.xaml
    /// </summary>
    public partial class janela1 : Window
    {
        public janela1()
        {
            InitializeComponent();
        }
        ////////////Classes Para Alteração////////////
        public Funcionario FuncionarioAlter { get; set; }
        public Fornecedor FornecedorAlter { get; set; }
        public Marca MarcaAlter { get; set; }
        public Produtos ProdutoAlter { get; set; }
        public Registro RegistroAlter { get; set; }
        ////////////Fim de Classes Para Alteração////////////

        /*Aplicação de Usuario Logado*/
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Funcionario funcionario = new Funcionario();
            lblLogadoNome.Content = Aplicacao.FuncionarioLogado.Nome;
            //lblLogadoID.Content = "ID: " + Aplicacao.FuncionarioLogado.Id;

            if (Aplicacao.FuncionarioLogado.Id_tipo_funcionario != 1)
            {
                JanelaFuncionarios.Visibility = Visibility.Hidden;
                dgFunc.Columns[1].Visibility = Visibility.Hidden;
                dgForn.Columns[1].Visibility = Visibility.Hidden;
                dgMarca.Columns[1].Visibility = Visibility.Hidden;
                dgProd.Columns[1].Visibility = Visibility.Hidden;
                dgRegi.Columns[0].Visibility = Visibility.Hidden;
                dgRegi.Columns[1].Visibility = Visibility.Hidden;
            }
            MarcaRepos marca = new MarcaRepos();
            marcaRegi.ItemsSource = marca.Consultar();
            marcaProd.ItemsSource = marca.Consultar();
            FornecedorRepos fornecedor = new FornecedorRepos();
            fornecedorRegi.ItemsSource = fornecedor.Consultar();
            fornecedorProd.ItemsSource = fornecedor.Consultar();
        }
        private void BtnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            MarcaRepos marca = new MarcaRepos();
            marcaRegi.ItemsSource = marca.Consultar();
            marcaProd.ItemsSource = marca.Consultar();
            FornecedorRepos fornecedor = new FornecedorRepos();
            fornecedorRegi.ItemsSource = fornecedor.Consultar();
            fornecedorProd.ItemsSource = fornecedor.Consultar();
            MessageBox.Show("ComboBox atualizado com Sucesso!");
        }

        private void MarcaRegi_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                Marca marca = new Marca();
                marca = marcaRegi.SelectedItem as Marca;
                ProdutosRepos produtos = new ProdutosRepos();
                produtoRegi.ItemsSource = produtos.Consultar(marca.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro! Messagem Original :" + ex.Message, "Erro não hà Produto relacionado à esta Marca", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ProdutoRegi_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                Produtos produto = new Produtos();
                produto = produtoRegi.SelectedItem as Produtos;
                FornecedorRepos fornecedorRepos = new FornecedorRepos();
                fornecedorRegi.ItemsSource = fornecedorRepos.Consultar2(produto.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro! Messagem Original :" + ex.Message, "Erro não hà Produtos relacionado à este Marca", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        ////////////Botão Cria/Alterar////////////
        private void BtnFunc_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nomeFunc.Text))
            {
                MessageBox.Show("Nome é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                nomeFunc.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(emailFunc.Text))
            {
                MessageBox.Show("Email é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                emailFunc.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(senhaFunc.Text))
            {
                MessageBox.Show("Senha é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                senhaFunc.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(cpfFunc.Text))
            {
                MessageBox.Show("CPF é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                cpfFunc.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(enderecoFunc.Text))
            {
                MessageBox.Show("Endereço é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                enderecoFunc.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(bairroFunc.Text))
            {
                MessageBox.Show("Bairro é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                bairroFunc.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(cidadeFunc.Text))
            {
                MessageBox.Show("Cidade é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                cidadeFunc.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(ufFunc.Text))
            {
                MessageBox.Show("Estado(UF) é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                ufFunc.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(cepFunc.Text))
            {
                MessageBox.Show("CEP é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                cepFunc.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(funcaoFunc.Text))
            {
                MessageBox.Show("Função é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                funcaoFunc.Focus();
                return;
            }
            FuncionarioRepos repo = new FuncionarioRepos();
            Funcionario funcionario = new Funcionario
            {
                Nome = nomeFunc.Text,
                Cpf = cpfFunc.Text,
                Email = emailFunc.Text,
                Senha = senhaFunc.Text,
                Endereco = enderecoFunc.Text,
                Bairro = bairroFunc.Text,
                Cidade = cidadeFunc.Text,
                Uf = ufFunc.Text,
                Cep = cepFunc.Text
            };
            if (funcaoFunc.Text == "Administrador")
            {
                funcionario.Id_tipo_funcionario = 1;
            }
            else if (funcaoFunc.Text == "Usuario")
            {
                funcionario.Id_tipo_funcionario = 2;
            }
            try
            {
                if (FuncionarioAlter != null)
                {
                    //lblCliente.Content = "Novo Cliente";
                    //preenche o id
                    funcionario.Id = FuncionarioAlter.Id;
                    //inserindo usuario
                    repo.Alterar(funcionario);
                    nomeFunc.Text = null;
                    cpfFunc.Text = null;
                    emailFunc.Text = null;
                    senhaFunc.Text = null;
                    enderecoFunc.Text = null;
                    bairroFunc.Text = null;
                    cidadeFunc.Text = null;
                    ufFunc.Text = null;
                    cepFunc.Text = null;
                    funcaoFunc.Text = null;
                    MessageBox.Show("Funcionario Alterado com Sucesso");
                    FuncionarioAlter = null;
                }
                else
                {
                    //lblCliente.Content = "Novo Cliente";
                    //inserirndo usuario
                    repo.Inserir(funcionario);
                    nomeFunc.Text = null;
                    cpfFunc.Text = null;
                    emailFunc.Text = null;
                    senhaFunc.Text = null;
                    enderecoFunc.Text = null;
                    bairroFunc.Text = null;
                    cidadeFunc.Text = null;
                    ufFunc.Text = null;
                    cepFunc.Text = null;
                    funcaoFunc.Text = null;
                    MessageBox.Show("Funcionario Cadastrado com Sucesso");
                    FuncionarioAlter = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro! Messagem Original :" + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnForn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nomeForn.Text))
            {
                MessageBox.Show("Nome é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                nomeForn.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(telefoneForn.Text))
            {
                MessageBox.Show("Telefone é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                telefoneForn.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(cnpjForn.Text))
            {
                MessageBox.Show("CNPJ é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                cnpjForn.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(enderecoForn.Text))
            {
                MessageBox.Show("Endereço é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                enderecoForn.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(bairroForn.Text))
            {
                MessageBox.Show("Bairro é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                bairroForn.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(cidadeForn.Text))
            {
                MessageBox.Show("Cidade é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                cidadeForn.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(ufForn.Text))
            {
                MessageBox.Show("Estado(UF) é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                ufForn.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(cepForn.Text))
            {
                MessageBox.Show("CEP é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                cepForn.Focus();
                return;
            }
            FornecedorRepos repo = new FornecedorRepos();
            Fornecedor fornecedor = new Fornecedor
            {
                Nome = nomeForn.Text,
                Telefone = telefoneForn.Text,
                Cnpj = cnpjForn.Text,
                Endereco = enderecoForn.Text,
                Bairro = bairroForn.Text,
                Cidade = cidadeForn.Text,
                Uf = ufForn.Text,
                Cep = cepForn.Text
            };
            try
            {
                if (FornecedorAlter != null)
                {
                    fornecedor.Id = FornecedorAlter.Id;
                    repo.Alterar(fornecedor);
                    nomeForn.Text = null;
                    telefoneForn.Text = null;
                    cnpjForn.Text = null;
                    enderecoForn.Text = null;
                    bairroForn.Text = null;
                    cidadeForn.Text = null;
                    ufForn.Text = null;
                    cepForn.Text = null;
                    MessageBox.Show("Fornecedor alterado com sucesso");
                    FornecedorAlter = null;
                }
                else
                {
                    repo.Inserir(fornecedor);
                    nomeForn.Text = null;
                    telefoneForn.Text = null;
                    cnpjForn.Text = null;
                    enderecoForn.Text = null;
                    bairroForn.Text = null;
                    cidadeForn.Text = null;
                    ufForn.Text = null;
                    cepForn.Text = null;
                    MessageBox.Show("Fornecedor criado com sucesso");
                    FornecedorAlter = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro! Messagem Original :" + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnMarca_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nomeMarca.Text))
            {
                MessageBox.Show("Nome é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                nomeMarca.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(telefoneMarca.Text))
            {
                MessageBox.Show("Telefone é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                telefoneMarca.Focus();
                return;
            }
            MarcaRepos repo = new MarcaRepos();
            Marca marca = new Marca
            {
                Nome = nomeMarca.Text,
                Telefone = telefoneMarca.Text
            };
            try
            {
                if (MarcaAlter != null)
                {
                    marca.Id = MarcaAlter.Id;
                    repo.Alterar(marca);
                    nomeMarca.Text = null;
                    telefoneMarca.Text = null;
                    MessageBox.Show("Marca alterada com sucesso");
                    MarcaAlter = null;
                }
                else
                {
                    repo.Inserir(marca);
                    nomeMarca.Text = null;
                    telefoneMarca.Text = null;
                    MessageBox.Show("Marca criada com sucesso");
                    MarcaAlter = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro! Messagem Original :" + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Prod_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nomeProd.Text))
            {
                MessageBox.Show("Nome é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                nomeProd.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(marcaProd.Text))
            {
                MessageBox.Show("Marca é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                marcaProd.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(fornecedorProd.Text))
            {
                MessageBox.Show("Fornecedor é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                fornecedorProd.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(qtdProd.Text))
            {
                MessageBox.Show("Quantidade é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                qtdProd.Focus();
                return;
            }
            ProdutosRepos repo = new ProdutosRepos();
            Produtos produtos = new Produtos
            {
                Nome = nomeProd.Text,
                Id_marca = (marcaProd.SelectedValue as Marca).Id,
                Id_fornecedor = (fornecedorProd.SelectedValue as Fornecedor).Id,
                Qtd_produtos = int.Parse(qtdProd.Text)
            };
            try
            {
                if (ProdutoAlter != null)
                {
                    produtos.Id = ProdutoAlter.Id;
                    repo.Alterar(produtos);
                    nomeProd.Text = null;
                    marcaProd.Text = null;
                    fornecedorProd.Text = null;
                    qtdProd.Text = null;
                    MessageBox.Show("Produto alterado com sucesso");
                    ProdutoAlter = null;
                }
                else
                {
                    repo.Inserir(produtos);
                    nomeProd.Text = null;
                    marcaProd.Text = null;
                    fornecedorProd.Text = null;
                    qtdProd.Text = null;
                    MessageBox.Show("Produto criada com sucesso");
                    ProdutoAlter = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro! Messagem Original :" + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnRegi_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(statusRegi.Text))
            {
                MessageBox.Show("Status é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                statusRegi.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(marcaRegi.Text))
            {
                MessageBox.Show("Marca é Obrigatoria", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                marcaRegi.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(fornecedorRegi.Text))
            {
                MessageBox.Show("Fornecedor é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                fornecedorRegi.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(produtoRegi.Text))
            {
                MessageBox.Show("Produto é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                produtoRegi.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(quantidadeRegi.Text))
            {
                MessageBox.Show("Quantidade é Obrigatorio", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                quantidadeRegi.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(dataRegi.Text))
            {
                MessageBox.Show("Data é Obrigatoria", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                dataRegi.Focus();
                return;
            }
            DateTime thisDay = DateTime.Now;
            string thisHour = thisDay.ToString("hh:mm");
            RegistroRepos repo = new RegistroRepos();
            Registro registro = new Registro
            {
                IdProduto = (produtoRegi.SelectedValue as Produtos).Id,
                IdMarca = (marcaRegi.SelectedValue as Marca).Id,
                IdFornecedor = (fornecedorRegi.SelectedValue as Fornecedor).Id,
                QtdProdutos = int.Parse(quantidadeRegi.Text),
                /*Data_registro = DateTime.Now*/
                DataRegistro = Convert.ToDateTime(dataRegi.SelectedDate.Value.ToString("dd/MM/yyyy") + " " + thisHour),
                IdFuncionario = Aplicacao.FuncionarioLogado.Id
            };
            if (statusRegi.Text == "Recebido") { registro.IdStatus = 1; }
            else if (statusRegi.Text == "Retirado") { registro.IdStatus = 2; };
            try
            {
                if (RegistroAlter != null)
                {
                    registro.Id = RegistroAlter.Id;
                    repo.Alterar(registro);
                    statusRegi.Text = null;
                    produtoRegi.Text = null;
                    marcaRegi.Text = null;
                    fornecedorRegi.Text = null;
                    quantidadeRegi.Text = null;
                    dataRegi.Text = null;
                    MessageBox.Show("Movimentação alterada com sucesso");
                    RegistroAlter = null;
                }
                else
                {
                    repo.Inserir(registro);
                    statusRegi.Text = null;
                    produtoRegi.Text = null;
                    marcaRegi.Text = null;
                    fornecedorRegi.Text = null;
                    quantidadeRegi.Text = null;
                    dataRegi.Text = null;
                    MessageBox.Show("Movimentação criada com sucesso");
                    RegistroAlter = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro! Messagem Original :" + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        ////////////Fim Botão Cria/Alterar////////////

        ////////////Iniciar Botão Alterar////////////
        private void btnAlterarFuncionario_Click(object sender, RoutedEventArgs e)
        {
            if (dgFunc.SelectedItems.Count > 0)
            {
                FuncionarioAlter = dgFunc.SelectedItem as Funcionario;
                nomeFunc.Text = FuncionarioAlter.Nome;
                emailFunc.Text = FuncionarioAlter.Email;
                senhaFunc.Text = FuncionarioAlter.Senha;
                cpfFunc.Text = FuncionarioAlter.Cpf;
                enderecoFunc.Text = FuncionarioAlter.Endereco;
                bairroFunc.Text = FuncionarioAlter.Bairro;
                cidadeFunc.Text = FuncionarioAlter.Cidade;
                ufFunc.Text = FuncionarioAlter.Uf;
                cepFunc.Text = FuncionarioAlter.Cep;
                if (FuncionarioAlter.Id_tipo_funcionario == 1)
                {
                    funcaoFunc.Text = "Administrador";
                }
                else
                {
                    funcaoFunc.Text = "Usuario";
                }
            }
        }
        private void btnAlterarFornecedor_Click(object sender, RoutedEventArgs e)
        {
            if (dgForn.SelectedItems.Count > 0)
            {
                FornecedorAlter = dgForn.SelectedItem as Fornecedor;
                nomeForn.Text = FornecedorAlter.Nome;
                telefoneForn.Text = FornecedorAlter.Telefone;
                cnpjForn.Text = FornecedorAlter.Cnpj;
                enderecoForn.Text = FornecedorAlter.Endereco;
                bairroForn.Text = FornecedorAlter.Bairro;
                cidadeForn.Text = FornecedorAlter.Cidade;
                ufForn.Text = FornecedorAlter.Uf;
                cepForn.Text = FornecedorAlter.Cep;
            }
        }
        private void btnAlterarMarca_Click(object sender, RoutedEventArgs e)
        {
            if (dgMarca.SelectedItems.Count > 0)
            {
                MarcaAlter = dgMarca.SelectedItem as Marca;
                nomeMarca.Text = MarcaAlter.Nome;
                telefoneMarca.Text = MarcaAlter.Telefone;
            }
        }
        private void btnAlterarProduto_Click(object sender, RoutedEventArgs e)
        {
            if (dgProd.SelectedItems.Count > 0)
            {
                ProdutoAlter = dgProd.SelectedItem as Produtos;
                nomeProd.Text = ProdutoAlter.Nome;
                marcaProd.SelectedValue = ProdutoAlter.Id_marca;
                fornecedorProd.SelectedValue = ProdutoAlter.Id_fornecedor;
                qtdProd.Text = ProdutoAlter.Qtd_produtos.ToString();
            }
        }
        private void btnAlterarRegistro_Click(object sender, RoutedEventArgs e)
        {
            if (dgRegi.SelectedItems.Count > 0)
            {
                RegistroAlter = dgRegi.SelectedItem as Registro;
                if (RegistroAlter.IdStatus == 1)
                {
                    statusRegi.Text = "Recebido";
                }
                else if (RegistroAlter.IdStatus == 2)
                {
                    statusRegi.Text = "Retirado";
                }
                produtoRegi.SelectedValue = RegistroAlter.IdProduto;
                marcaRegi.SelectedValue = RegistroAlter.IdMarca;
                fornecedorRegi.SelectedValue = RegistroAlter.IdFornecedor;
                quantidadeRegi.Text = RegistroAlter.QtdProdutos.ToString();
                dataRegi.Text = RegistroAlter.DataRegistro.ToString("dd/MM/aaaa");
            }
        }
        ////////////////////////Fim Botão Alterar////////////////////////

        ////////////////////////Iniciar Botão Excluir////////////////////////
        private void btnExcluirFuncionario_Click(object sender, RoutedEventArgs e)
        {
            FuncionarioRepos repo = new FuncionarioRepos();
            Funcionario funcionario = dgFunc.SelectedItem as Funcionario;

            MessageBoxResult resposta;

            resposta = MessageBox.Show("Deseja realizar a exclusão desde Funcionario?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resposta == MessageBoxResult.Yes)
            {
                repo.Excluir(funcionario.Id);
                MessageBox.Show("Funcionário excluído com sucesso!");
            }
            else
            {
                e.Handled = true; //para ignora o delete
            }
        }
        private void btnExcluirFornecedor_Click(object sender, RoutedEventArgs e)
        {
            FornecedorRepos repo = new FornecedorRepos();
            Fornecedor fornecedor = dgForn.SelectedItem as Fornecedor;

            MessageBoxResult resposta;

            resposta = MessageBox.Show("Deseja realizar a exclusão desde Fornecedor?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resposta == MessageBoxResult.Yes)
            {
                repo.Excluir(fornecedor.Id);
                MessageBox.Show("Fornecedor excluído com sucesso!");
            }
            else
            {
                e.Handled = true; //para ignora o delete
            }
        }
        private void btnExcluirMarca_Click(object sender, RoutedEventArgs e)
        {
            MarcaRepos repo = new MarcaRepos();
            Marca marca = dgMarca.SelectedItem as Marca;

            MessageBoxResult resposta;

            resposta = MessageBox.Show("Deseja realizar a exclusão desda Marca?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resposta == MessageBoxResult.Yes)
            {
                repo.Excluir(marca.Id);
                MessageBox.Show("Marca excluído com sucesso!");
            }
            else
            {
                e.Handled = true; //para ignora o delete
            }
        }
        private void btnExcluirProduto_Click(object sender, RoutedEventArgs e)
        {
            ProdutosRepos repo = new ProdutosRepos();
            Produtos produtos = dgProd.SelectedItem as Produtos;

            MessageBoxResult resposta;

            resposta = MessageBox.Show("Deseja realizar a exclusão desde Produto?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resposta == MessageBoxResult.Yes)
            {
                repo.Excluir(produtos.Id);
                MessageBox.Show("Produto excluído com sucesso!");
            }
            else
            {
                e.Handled = true; //para ignora o delete
            }
        }
        private void btnExcluirRegistro_Click(object sender, RoutedEventArgs e)
        {
            RegistroRepos repo = new RegistroRepos();
            Registro registro = dgRegi.SelectedItem as Registro;

            MessageBoxResult resposta;

            resposta = MessageBox.Show("Deseja realizar a exclusão desde Registro?", "Excluir", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resposta == MessageBoxResult.Yes)
            {
                repo.Excluir(registro.Id);
                MessageBox.Show("Registro excluído com sucesso!");
            }
            else
            {
                e.Handled = true; //para ignora o delete
            }
        }
        ////////////////////////Fim Botão Excluir////////////////////////

        ////////////////////////Iniciar DataGrid////////////////////////
        private void btnPesqFunc_Click(object sender, RoutedEventArgs e)
        {
            FuncionarioRepos repo = new FuncionarioRepos();
            dgFunc.ItemsSource = repo.Pesquisar(pesqFunc.Text);
            dgFunc.Columns[13].Header = "Função";
            dgFunc.Columns[10].Header = "Estado";
            dgFunc.Columns[11].Header = "CEP";
            dgFunc.Columns[06].Header = "CPF";
            dgFunc.Columns[02].Visibility = Visibility.Hidden;
            dgFunc.Columns[05].Visibility = Visibility.Hidden;
            dgFunc.Columns[12].Visibility = Visibility.Hidden;

        }
        private void btnPesqForn_Click(object sender, RoutedEventArgs e)
        {
            FornecedorRepos repo = new FornecedorRepos();
            dgForn.ItemsSource = repo.Pesquisar(pesqForn.Text);
            dgForn.Columns[03].Header = "Empresa";
            dgForn.Columns[05].Header = "CNPJ";
            dgForn.Columns[06].Header = "Endereço";
            dgForn.Columns[09].Header = "UF";
            dgForn.Columns[10].Header = "CEP";
            dgForn.Columns[02].Visibility = Visibility.Hidden;
        }
        private void btnPesqMarca_Click(object sender, RoutedEventArgs e)
        {
            MarcaRepos repo = new MarcaRepos();
            dgMarca.ItemsSource = repo.Pesquisar(pesqMarca.Text);
            //dgMarca.Columns[00].Header = "";
            dgMarca.Columns[02].Visibility = Visibility.Hidden;
        }
        private void btnPesqProd_Click(object sender, RoutedEventArgs e)
        {
            ProdutosRepos repo = new ProdutosRepos();
            dgProd.ItemsSource = repo.Pesquisar(pesqProd.Text);
            dgProd.Columns[03].Header = "Produtos";
            dgProd.Columns[05].Header = "Marcas";
            dgProd.Columns[07].Header = "Fornecedor";
            dgProd.Columns[08].Header = "Quantidades";
            dgProd.Columns[02].Visibility = Visibility.Hidden;
            dgProd.Columns[04].Visibility = Visibility.Hidden;
            dgProd.Columns[06].Visibility = Visibility.Hidden;
        }
        private void btnPesqRegis_Click(object sender, RoutedEventArgs e)
        {
            RegistroRepos repo = new RegistroRepos();
            dgRegi.ItemsSource = repo.Pesquisar(pesqRegi.Text);
            dgRegi.Columns[04].Header = "Status";
            //dgRegi.Columns[04].Header = "Status Registro";
            dgRegi.Columns[06].Header = "Produtos";
            dgRegi.Columns[08].Header = "Marcas";
            dgRegi.Columns[10].Header = "Fornecedor";
            dgRegi.Columns[12].Header = "Funcionario";
            dgRegi.Columns[13].Header = "Data";
            //dgRegi.Columns[13].Header = "Data do Registro";
            dgRegi.Columns[14].Header = "Quantidade";
            //dgRegi.Columns[14].Header = "Quantidade de Produtos";
            dgRegi.Columns[02].Visibility = Visibility.Hidden;
            dgRegi.Columns[03].Visibility = Visibility.Hidden;
            dgRegi.Columns[05].Visibility = Visibility.Hidden;
            dgRegi.Columns[07].Visibility = Visibility.Hidden;
            dgRegi.Columns[09].Visibility = Visibility.Hidden;
            dgRegi.Columns[11].Visibility = Visibility.Hidden;

        }
        ////////////////////////Fim DataGrid////////////////////////

        ////////////////////////Testes de Exportação////////////////////////
        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(dgRegi.Columns.Count - 7);
            pdfTable.DefaultCell.Padding = 5;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfTable.DefaultCell.BorderWidth = 1;

            //Adding Header row
            foreach (DataGridColumn column in dgRegi.Columns)
            {
                if (column.Header.ToString() == "Alterar") continue;
                if (column.Header.ToString() == "Excluir") continue;
                if (column.Header.ToString() == "IdStatus") continue;
                if (column.Header.ToString() == "IdProduto") continue;
                if (column.Header.ToString() == "IdMarca") continue;
                if (column.Header.ToString() == "IdFornecedor") continue;
                if (column.Header.ToString() == "IdFuncionario") continue;

                PdfPCell cell = new PdfPCell(new Phrase(column.Header.ToString()));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(220, 220, 250);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_CENTER;
                pdfTable.AddCell(cell);

            }

            //Adding DataRow
            foreach (Registro row in dgRegi.Items.SourceCollection)
            {
                pdfTable.AddCell(row.Id.ToString());
                pdfTable.AddCell(row.Estado);
                pdfTable.AddCell(row.Produto);
                pdfTable.AddCell(row.Marca);
                pdfTable.AddCell(row.Fornecedor);
                pdfTable.AddCell(row.Funcionario);
                pdfTable.AddCell(row.DataRegistro.ToString());
                pdfTable.AddCell(row.QtdProdutos.ToString());
            }

            //Exporting to PDF
            string folderPath = "C:\\PDFs\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (FileStream stream = new FileStream(folderPath + "RelatorioDeMovimentação.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A4, 20f, 30f, 20f, 10f);

                string dados = "";
                Paragraph paragrafo = new Paragraph(dados, new Font(Font.NORMAL, 14));
                paragrafo.Alignment = Element.ALIGN_CENTER;

                paragrafo.Add("Relatório de Movimentação de Estoque");
                paragrafo.Add("                                    ");

                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(paragrafo);
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
                MessageBox.Show("Relatório gerado com Sucesso!");
            }
        }

    }
}
