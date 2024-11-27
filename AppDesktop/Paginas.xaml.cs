using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using System.Globalization;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Text;
using Agrotech.Vendas_realizadas;
using Agrotech.Produtos_cadastrados;
using Agrotech.Insumos_cadastrados;
using Agrotech.Clientes_Cadastrados;
using Agrotech.Usuarios_cadastrados;

namespace Agrotech {
    public partial class Paginas : ContentPage {


        public Paginas() {
            InitializeComponent();

            paymentMethodPicker.SelectedIndexChanged += PaymentMethodPicker_SelectedIndexChanged;
            SizeChanged += OnPageSizeChanged;


        }
        private async void Button_Salvar_Clicked(object sender, EventArgs e) {
            // Obter os dados dos campos
            string nome = nomeEntry.Text; // Ajuste os nomes dos Entry conforme sua implementa��o
            string cpf = cpfEntry.Text;
            string nomeUsuario = nomeUsuarioEntry.Text;
            string senha = senhaEntry.Text;

            nomeEntry.IsEnabled = false;
            cpfEntry.IsEnabled = false;
            nomeUsuarioEntry.IsEnabled = false;
            senhaEntry.IsEnabled = false;

            // Criar uma inst�ncia da classe DataAccess
            var dataAccess = new DataAccess();

            // Inserir dados no banco de dados
            bool success = await dataAccess.InserirUsuarioAsync(nome, cpf, nomeUsuario, senha);

            if (success) {
                await DisplayAlert("Sucesso", "Dados inseridos com sucesso.", "OK");
                // Limpar os campos ou realizar outras a��es conforme necess�rio
                nomeEntry.Text = "";
                cpfEntry.Text = "";
                nomeUsuarioEntry.Text = "";
                senhaEntry.Text = "";
            }
            else {
                await DisplayAlert("Erro", "N�o foi poss�vel inserir os dados. Por favor, tente novamente.", "OK");
            }
        }


        private readonly Ajuste_Paginas ajustePaginas = new Ajuste_Paginas();

        private void Button_Cadastrar_Usuario(object sender, EventArgs e) {
            // Garante que a p�gina de configura��o esteja vis�vel antes de exibir a de cadastro de usu�rio
            ShowconfiguracaoPage();
            ajustePaginas.MostrarPagina(Pagina_Cadastrar_Usuario, MainContent);
        }

        private void Botao_cadastro_clicavel(object sender, EventArgs e) {
            ajustePaginas.MostrarPagina(Pagina_Cadastrar_Cliente, MainContent);
            bntconfiguracao.IsEnabled = false;
            bntconfiguracao2.IsEnabled = false;
            bntvendas.IsEnabled = false;
            bntvendas2.IsEnabled = false;
            bntprodutos.IsEnabled = false;
            bntprodutos2.IsEnabled = false;
            bntinsumos.IsEnabled = false;
            bntinsumos2.IsEnabled = false;
            bntmovimentacoes.IsEnabled = false;
            bntmovimentacoes2.IsEnabled = false;
        }

        private void Botao_Vendas_clicavel(object sender, EventArgs e) {
            ajustePaginas.MostrarPagina(VendasPage, MainContent);
            bntcadastrar_cliente.IsEnabled = false;
            bntcadastrar_cliente_2.IsEnabled = false;
            bntconfiguracao.IsEnabled = false;
            bntconfiguracao2.IsEnabled = false;
            bntprodutos.IsEnabled = false;
            bntprodutos2.IsEnabled = false;
            bntinsumos.IsEnabled = false;
            bntinsumos2.IsEnabled = false;
            bntmovimentacoes.IsEnabled = false;
            bntmovimentacoes2.IsEnabled = false;
        }

        private void Botao_CadastarPI_clicavel(object sender, EventArgs e) {
            ajustePaginas.MostrarPagina(Insumos_Produtos, MainContent);
            bntconfiguracao.IsEnabled = false;
            bntconfiguracao2.IsEnabled = false;
            bntvendas.IsEnabled = false;
            bntvendas2.IsEnabled = false;
            bntcadastrar_cliente.IsEnabled = false;
            bntcadastrar_cliente_2.IsEnabled = false;
            bntinsumos.IsEnabled = false;
            bntinsumos2.IsEnabled = false;
            bntmovimentacoes.IsEnabled = false;
            bntmovimentacoes2.IsEnabled = false;
        }

        private void Botao_Relatorio_Clicavel(object sender, EventArgs e) {
            ajustePaginas.MostrarPagina(Cadastro_Isumo, MainContent);
            ajustePaginas.MostrarPagina(Insumos_Produtos, MainContent);
            bntconfiguracao.IsEnabled = false;
            bntconfiguracao2.IsEnabled = false;
            bntvendas.IsEnabled = false;
            bntvendas2.IsEnabled = false;
            bntcadastrar_cliente.IsEnabled = false;
            bntcadastrar_cliente_2.IsEnabled = false;
            bntprodutos.IsEnabled = false;
            bntprodutos2.IsEnabled = false;
            bntmovimentacoes.IsEnabled = false;
            bntmovimentacoes2.IsEnabled = false;
        }

        private void Botao_Configuracao(object sender, EventArgs e) {
            // Exibe a p�gina de configura��o
            ShowconfiguracaoPage();
            bntcadastrar_cliente.IsEnabled = false;
            bntcadastrar_cliente_2.IsEnabled = false;
            bntvendas.IsEnabled = false;    
            bntvendas2.IsEnabled = false;
            bntprodutos.IsEnabled = false;
            bntprodutos2.IsEnabled = false;
            bntinsumos.IsEnabled = false;
            bntinsumos2.IsEnabled = false;
            bntmovimentacoes.IsEnabled = false;
            bntmovimentacoes2.IsEnabled = false;

        }

        private void Button_Movimentacoes(object sender, EventArgs e) {
            ajustePaginas.MostrarPagina(Paginademovimentacoes, MainContent);
            bntconfiguracao.IsEnabled = false;
            bntconfiguracao2.IsEnabled = false;
            bntcadastrar_cliente.IsEnabled = false;
            bntcadastrar_cliente_2.IsEnabled = false;
            bntvendas.IsEnabled = false;
            bntvendas2.IsEnabled = false;
            bntprodutos.IsEnabled = false;
            bntprodutos2.IsEnabled = false;
            bntinsumos.IsEnabled = false;
            bntinsumos2.IsEnabled = false;
            bntprodutos.IsEnabled = false;
            bntprodutos2.IsEnabled = false;
        }

        private void Button_x(object sender, EventArgs e) {
            configuracaoPage.IsVisible = false;
            Pagina_Cadastrar_Cliente.IsVisible = false;
            Insumos_Produtos.IsVisible = false;
            Cadastro_Isumo.IsVisible = false;
            VendasPage.IsVisible = false;
            Paginademovimentacoes.IsVisible = false;

            bntconfiguracao.IsEnabled = true;
            bntconfiguracao2.IsEnabled = true;
            bntcadastrar_cliente.IsEnabled = true;
            bntcadastrar_cliente_2.IsEnabled = true;
            bntvendas.IsEnabled = true;
            bntvendas2.IsEnabled = true;
            bntprodutos.IsEnabled = true;
            bntprodutos2.IsEnabled = true;
            bntinsumos.IsEnabled = true;
            bntinsumos2.IsEnabled = true;
            bntprodutos.IsEnabled = true;
            bntprodutos2.IsEnabled = true;
            bntmovimentacoes.IsEnabled = true;
            bntmovimentacoes2.IsEnabled = true;

            
            NumeroVendaEntry.Text = string.Empty;
            DataEntry.Text = string.Empty;
            HoraEntry.Text = string.Empty;
            VendedorEntry.Text = string.Empty;
            ClienteEntry.Text = string.Empty;
            Vd_cpf_cnpjEntry.Text = string.Empty;

            
            ProductStackLayout.Children.Clear();
            LinhaQuantidade.Children.Clear();
            LinhaPrecoUn.Children.Clear();
            LinhaDesconto.Children.Clear();
            LinhaValorTotal.Children.Clear();

            
            framesPaginaVendas.LimparVendas();

            
            Quantidade_Entry_PI.Text = string.Empty;
            DatadevencimentoEntry.Text = string.Empty;
            Nm_Cadastro_Produto_Insumo.Text = string.Empty;
            DatadecadastroEntry.Text = string.Empty;
            IntercorrenciaEditor.Text = string.Empty;

           
            Quantidade_Entry_PI.IsEnabled = false;
            DatadevencimentoEntry.IsEnabled = false;
            Nm_Cadastro_Produto_Insumo.IsEnabled = false;
            DatadecadastroEntry.IsEnabled = false;
            IntercorrenciaEditor.IsEnabled = false;

            
            CadastroprodutoEntry.Text = string.Empty;
            CadastroUnidadeEntry.Text = string.Empty;
            ValorEntry.Text = string.Empty;

           
            CadastroprodutoEntry.IsEnabled = false;
            CadastroUnidadeEntry.IsEnabled = false;
            ValorEntry.IsEnabled = false;

           
            Quantidade_Insumo_Entry.Text = string.Empty;
            Datadevencimento_Insumo_Entry.Text = string.Empty;
            Nm_Cadastro_Insumo.Text = string.Empty;
            Datadecadastro_Insumo_Entry.Text = string.Empty;
            Intercorrencia_Insumo_Editor.Text = string.Empty;

            
            Quantidade_Insumo_Entry.IsEnabled = false;
            Datadevencimento_Insumo_Entry.IsEnabled = false;
            Nm_Cadastro_Insumo.IsEnabled = false;
            Datadecadastro_Insumo_Entry.IsEnabled = false;
            Intercorrencia_Insumo_Editor.IsEnabled = false;

            
            Quantidade_Insumo_Entry.Text = string.Empty;
            Datadevencimento_Insumo_Entry.Text = string.Empty;
            Nm_Cadastro_Insumo.Text = string.Empty;
            Datadecadastro_Insumo_Entry.Text = string.Empty;
            Intercorrencia_Insumo_Editor.Text = string.Empty;

            
            Quantidade_Insumo_Entry.IsEnabled = false;
            Datadevencimento_Insumo_Entry.IsEnabled = false;
            Nm_Cadastro_Insumo.IsEnabled = false;
            Datadecadastro_Insumo_Entry.IsEnabled = false;
            Intercorrencia_Insumo_Editor.IsEnabled = false;

            Pagina_Cadastrar_Usuario.IsVisible = false;

        }

        private void ShowconfiguracaoPage() {
            // Exibe a p�gina de configura��o
            ajustePaginas.MostrarPagina(configuracaoPage, MainContent);
        }

        private void ShowPaginademovimentacoes() {
            // Exibe a p�gina de configura��o
            ajustePaginas.MostrarPagina(Paginademovimentacoes, MainContent);
        }

        private void PaymentMethodPicker_SelectedIndexChanged(object sender, EventArgs e) {
            var selectedPaymentMethod = paymentMethodPicker.SelectedItem?.ToString();
            if (selectedPaymentMethod == "� Vista" || selectedPaymentMethod == "Cart�o de D�bito") {
                parcelasPicker.IsEnabled = false;
                parcelasPicker.SelectedIndex = -1; // Clear selection
            }
            else {
                parcelasPicker.IsEnabled = true;
            }
        }

        private readonly FramesPaginaVendas framesPaginaVendas = new FramesPaginaVendas();

        private async void Button_Confirmacao(object sender, EventArgs e) {
            // Obter os valores digitados
            string produtoDigitado = ProdutoEntry.Text;
            string quantidadeDigitado = QuantidadeEntry.Text;
            string precoUnDigitado = Preco_UnitarioEntry.Text;
            string descontoDigitado = DescontoEntry.Text;
            string valorTotalDigitado = ValorToTalEntry.Text;

            if (string.IsNullOrWhiteSpace(produtoDigitado) ||
                string.IsNullOrWhiteSpace(quantidadeDigitado) ||
                string.IsNullOrWhiteSpace(precoUnDigitado) ||
                string.IsNullOrWhiteSpace(descontoDigitado) ||
                string.IsNullOrWhiteSpace(valorTotalDigitado)) {
                await App.Current.MainPage.DisplayAlert("Aviso", "Por favor, preencha todos os campos antes de confirmar.", "OK");
                return;
            }

            framesPaginaVendas.AdicionarFramesPaginaVendas(
                ProductStackLayout, LinhaQuantidade, LinhaPrecoUn, LinhaDesconto, LinhaValorTotal,
                produtoDigitado, quantidadeDigitado, precoUnDigitado, descontoDigitado, valorTotalDigitado);

            ProdutoEntry.Text = "";
            QuantidadeEntry.Text = "";
            Preco_UnitarioEntry.Text = "";
            DescontoEntry.Text = "";
            ValorToTalEntry.Text = "";


        }

        private void OnPageSizeChanged(object sender, EventArgs e) {
            double width = this.Width;
            double height = this.Height;

            // Defina os tamanhos desejados para a tela menor
            double desiredWidth = 1100;
            double desiredHeight = 650;

            // Calcule os novos tamanhos baseados na propor��o da tela maior
            double newWidth = width > desiredWidth ? width * 0.8 : desiredWidth;
            double newHeight = height > desiredHeight ? height * 0.9 : desiredHeight;

            // Ajuste os tamanhos e posicionamento do ContentView
            AdjustContentViewLayout(VendasPage, newWidth, newHeight);
            AdjustContentViewLayout(configuracaoPage, newWidth, newHeight);
            AdjustContentViewLayout(Pagina_Cadastrar_Usuario, newWidth, newHeight);
            AdjustContentViewLayout(Pagina_Cadastrar_Cliente, newWidth, newHeight);
            AdjustContentViewLayout(Cadastro_Isumo, newWidth, newHeight);
            AdjustContentViewLayout(Insumos_Produtos, newWidth, newHeight);
            AdjustContentViewLayout(Paginademovimentacoes, newWidth, newHeight);

        }

        private void AdjustContentViewLayout(View contentView, double width, double height) {
            AbsoluteLayout.SetLayoutBounds(contentView, new Rect(0.9, 0.5, width, height));
            AbsoluteLayout.SetLayoutFlags(contentView, AbsoluteLayoutFlags.PositionProportional);

        }

        private string selectedGender;



        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e) {
            if (RadioButtonPF.IsChecked) {
                if (sender is CheckBox selectedCheckBox && selectedCheckBox.IsChecked) {
                    // Desmarcar todos os outros CheckBox
                    foreach (var checkbox in new[] { CheckBoxMale, CheckBoxFemale, CheckBoxOtherSex }) {
                        if (checkbox != selectedCheckBox) {
                            checkbox.IsChecked = false;
                        }
                    }

                    // Armazenar a escolha do usu�rio
                    if (selectedCheckBox == CheckBoxMale) {
                        selectedGender = "Masculino";
                    }
                    else if (selectedCheckBox == CheckBoxFemale) {
                        selectedGender = "Feminino";
                    }
                    else if (selectedCheckBox == CheckBoxOtherSex) {
                        selectedGender = "Outros";
                    }
                }
            }
        }


        private readonly GerenciadorCamposFormulario gerenciadorCampos = new GerenciadorCamposFormulario();

        private async void Button_Nova_venda(object sender, EventArgs e) {
            // Crie uma inst�ncia de DataAccess (assumindo que voc� j� tenha a conex�o configurada)
            var dataAccess = new DataAccess();

            // Crie uma inst�ncia do GeradorId
            var geradorId = new GeradorId(dataAccess);

            // Gere um novo n�mero de venda usando a classe GeradorId
            int numeroVenda = await geradorId.GerarNovoIdAsync();

            // Define o n�mero gerado no Entry NumeroVendaEntry
            NumeroVendaEntry.Text = numeroVenda.ToString();

            // Crie uma inst�ncia de GeradorDataHora
            var geradorDataHora = new GeradorDataHora(Application.Current.Dispatcher);

            // Obtenha a data e a hora atuais
            string dataAtual = geradorDataHora.DataAtual;
            string horaAtual = geradorDataHora.HoraAtual;

            // Define a data e a hora nos Entries DataEntry e HoraEntry
            DataEntry.Text = dataAtual;
            HoraEntry.Text = horaAtual;

            // Define o nome do usu�rio logado no VendedorEntry
            VendedorEntry.Text = UsuarioLogado.NomeUsuario;

            // Habilita os campos para a nova venda
            gerenciadorCampos.HabilitarCamposVenda(DescontoEntry, QuantidadeEntry);
        }

        private async void Button_Novo_Cliente(object sender, EventArgs e) {
            // Verifica se o idEntry j� tem um valor (se o cadastro j� foi iniciado)
            if (string.IsNullOrEmpty(idEntry.Text)) {
                // Cria uma inst�ncia de DataAccess (assumindo que voc� j� tenha a conex�o configurada)
                var dataAccess = new DataAccess();

                // Cria uma inst�ncia do GeradorId
                var geradorId = new GeradorId(dataAccess);

                // Gere um novo ID aleat�rio usando a classe GeradorId
                int randomId = await geradorId.GerarNovoIdAsync();

                // Define o valor gerado no Entry idEntry
                idEntry.Text = randomId.ToString();

                // Armazena temporariamente o ID gerado (caso necess�rio)
                geradorId.ArmazenarIdTemporariamente(randomId);
            }

            // Habilita os campos para o novo cliente
            gerenciadorCampos.HabilitarCamposCliente(
                Cl_nome_Entry, CpfCnpjEntry,
                RuaEntry, NumeroEntry, ComplementarEntry, CepEntry,
                BairroEntry, EstadoEntry, CidadeEntry,
                RadioButtonPF, RadioButtonPJ);
        }

        // M�todo para armazenar temporariamente o ID gerado
        private void ArmazenarIdTemporariamente(int id) {
            // Implemente aqui a l�gica para armazenar o ID temporariamente
            // Pode ser uma lista est�tica, cache, ou uma tabela tempor�ria no banco de dados
        }

        // Evento TextChanged no Entry
        private void Cl_dn_Entry_TextChanged(object sender, TextChangedEventArgs e) {
            var entry = sender as Entry;

            // Remove caracteres n�o num�ricos
            string text = new string(entry.Text.Where(char.IsDigit).ToArray());

            // Limita o comprimento a no m�ximo 8 d�gitos (DDMMAAAA)
            if (text.Length > 8) {
                text = text.Substring(0, 8); // Corta o texto para 8 d�gitos
            }

            // Formata o texto para o formato DD/MM/AAAA
            if (text.Length >= 2 && text.Length <= 4) {
                text = text.Insert(2, "/");
            }
            else if (text.Length > 4) {
                text = text.Insert(2, "/");
                text = text.Insert(5, "/");
            }

            // Evita loop infinito ao atualizar o texto no Entry
            if (entry.Text != text) {
                entry.Text = text;
            }

            // Converte o texto formatado para o formato yyyy-MM-dd
            if (text.Length == 10) {
                try {
                    DateTime parsedDate = DateTime.ParseExact(text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string dbDate = parsedDate.ToString("yyyy-MM-dd"); // Formato para banco de dados
                                                                       // Aqui voc� pode armazenar ou usar a data formatada para banco de dados
                                                                       // Exemplo: SaveDateToDatabase(dbDate);
                }
                catch (FormatException) {
                    // Trate o erro de formato, se necess�rio
                }
            }
        }


        // Cont�m a  l�gica da classe Gereciador Tipo Cliente ou seja o sexo do cliente pessoa f�sica
        private readonly GerenciadorTipoCliente gerenciadorTipoCliente = new GerenciadorTipoCliente();

        private void OnClientTypeChanged(object sender, CheckedChangedEventArgs e) {
            gerenciadorTipoCliente.OnClientTypeChanged(RadioButtonPF, RadioButtonPJ, CpfCnpjEntry, CheckBoxMale, CheckBoxFemale, CheckBoxOtherSex);

            if (RadioButtonPF.IsChecked) {
                // Habilitar os campos Entry
                Cl_Rg_Entry.IsEnabled = true;
                Cl_dn_Entry.IsEnabled = true;

                // Ajustar a altura dos Entry para deix�-los vis�veis
                Cl_Rg_Entry.HeightRequest = -1; // -1 define a altura autom�tica com base no conte�do
                Cl_dn_Entry.HeightRequest = -1;

                // Desabilitar os campos de PJ (se houver)
                // Exemplo:
                // Cl_PJ_Field.IsEnabled = false;
            }
            else {
                // Desabilitar os campos Entry
                Cl_Rg_Entry.IsEnabled = false;
                Cl_dn_Entry.IsEnabled = false;

                // Redefinir a altura para ocult�-los
                Cl_Rg_Entry.HeightRequest = 0;
                Cl_dn_Entry.HeightRequest = 0;
            }
        }

        private async void Button_Salvar_Cliente(object sender, EventArgs e) {

            // L�GICA PARA QUANDO O USU�RIO SALVAR O CADASTRO OS ENTRY FICA FALSE NOVAMENTE PARA DIGITAR AT� QUE O USU�RIO CLIQUE NO BOT�O NOVO
            idEntry.IsEnabled = false;
            Cl_nome_Entry.IsEnabled = false;
            CpfCnpjEntry.IsEnabled = false;
            Cl_Rg_Entry.IsEnabled = false;
            Cl_dn_Entry.IsEnabled = false;
            RuaEntry.IsEnabled = false;
            NumeroEntry.IsEnabled = false;
            ComplementarEntry.IsEnabled = false;
            CepEntry.IsEnabled = false;
            BairroEntry.IsEnabled = false;
            EstadoEntry.IsEnabled = false;
            CidadeEntry.IsEnabled = false;
            RadioButtonPF.IsEnabled = false;
            RadioButtonPJ.IsEnabled = false;


            // Obter o tipo de cliente usando o GerenciadorTipoCliente
            string tipoCliente = RadioButtonPF.IsChecked ? "Pessoa F�sica" : "Pessoa Jur�dica";
            // Obter os dados dos campos
            string cl_Id = idEntry.Text;
            string cl_nome = Cl_nome_Entry.Text;
            string cl_cpf_cnpj = CpfCnpjEntry.Text;
            string cl_rg = Cl_Rg_Entry.Text;
            string cl_data_nascimento = Cl_dn_Entry.Text;
            string genero = selectedGender;
            string Ed_cliente_rua = RuaEntry.Text;
            string Ed_cliente_numero = NumeroEntry.Text;
            string Ed_cliente_complemento = ComplementarEntry.Text;
            string Ed_cliente_cep = CepEntry.Text;
            string Ed_cliente_bairro = BairroEntry.Text;
            string Ed_cliente_estado = EstadoEntry.Text;
            string Ed_cliente_cidade = CidadeEntry.Text;


            // Criar uma inst�ncia da classe DataAccess
            var dataAccess = new DataAccess();

            // Inserir dados no banco de dados, incluindo o g�nero
            bool success = await dataAccess.InserirClienteAsync(cl_Id, tipoCliente, cl_nome, cl_cpf_cnpj, cl_rg, cl_data_nascimento, genero, Ed_cliente_rua, Ed_cliente_numero,
                Ed_cliente_complemento, Ed_cliente_cep, Ed_cliente_bairro, Ed_cliente_estado, Ed_cliente_cidade);

            if (success) {
                await DisplayAlert("Sucesso", "Dados inseridos com sucesso.", "OK");
                // Limpar os campos
                idEntry.Text = "";
                Cl_nome_Entry.Text = "";
                CpfCnpjEntry.Text = "";
                Cl_Rg_Entry.Text = "";
                Cl_dn_Entry.Text = "";
                RuaEntry.Text = "";
                NumeroEntry.Text = "";
                ComplementarEntry.Text = "";
                CepEntry.Text = "";
                BairroEntry.Text = "";
                EstadoEntry.Text = "";
                CidadeEntry.Text = "";


            }
            else {
                await DisplayAlert("Erro", "N�o foi poss�vel inserir os dados. Por favor, tente novamente.", "OK");
            }
        }


        // Cont�m a l�gica da Classe Lg_Formatacao_cpnf_cpf
        private Lg_Formatacao_cpnf_cpf formatador = new Lg_Formatacao_cpnf_cpf();

        private void CpfCnpjEntry_TextChanged(object sender, TextChangedEventArgs e) {
            var entry = sender as Entry;

            if (entry == null) return;

            string formattedText = string.Empty;

            if (RadioButtonPF.IsChecked) {
                // Formata como CPF
                formattedText = formatador.FormatarCpf(e.NewTextValue);
            }
            else if (RadioButtonPJ.IsChecked) {
                // Formata como CNPJ
                formattedText = formatador.FormatarCnpj(e.NewTextValue);
            }

            // Define o texto formatado
            if (entry.Text != formattedText) {
                entry.Text = formattedText;
            }

            // Move o cursor para o final do texto
            entry.CursorPosition = entry.Text.Length;
        }
        private void OnAddressTypeChanged(object sender, CheckedChangedEventArgs e) {

        }

        private void CheckBoxMale_CheckedChanged(object sender, CheckedChangedEventArgs e) {

        }

        private void CheckBoxFemale_CheckedChanged(object sender, CheckedChangedEventArgs e) {

        }

        private void CheckBoxOtherSex_CheckedChanged(object sender, CheckedChangedEventArgs e) {

        }

        private string operacaoEscolhida; // "lote" ou "produto"

        private async void Button_Novo_Lote_Produto(object sender, EventArgs e) {
            operacaoEscolhida = "lote";

            // Crie uma inst�ncia de DataAccess (assumindo que voc� j� tenha a conex�o configurada)
            var dataAccess = new DataAccess();

            // Crie uma inst�ncia do GeradorId
            var geradorId = new GeradorId(dataAccess);

            // Gere um novo ID para o lote usando a classe GeradorId
            int novoIdLote = await geradorId.GerarNovoIdAsync();

            // Define o ID gerado no Entry IdEntry
            Nm_Cadastro_Produto_Insumo.Text = novoIdLote.ToString();

            // Crie uma inst�ncia de GeradorDataHora para obter a data atual
            var geradorDataHora = new GeradorDataHora(Application.Current.Dispatcher);

            // Obtenha a data atual
            string dataAtual = geradorDataHora.DataAtual;

            // Define a data de cadastro no Entry correspondente
            DatadecadastroEntry.Text = dataAtual; // Altere "DataCadastroEntry" para o nome correto do seu Entry de data de cadastro

            // Habilita apenas os Entry necess�rios para o "Novo Lote"
            Nm_Cadastro_Produto_Insumo.IsEnabled = true;
            Quantidade_Entry_PI.IsEnabled = true;
            DatadevencimentoEntry.IsEnabled = true;
            IntercorrenciaEditor.IsEnabled = true;
        }

        private async void Button_Salvar_Lote(object sender, EventArgs e) {
            try {
                // Verifique se um produto foi selecionado
                int idProdutoSelecionado = Produto.ID;

                if (idProdutoSelecionado == 0) {
                    await DisplayAlert("Erro", "Nenhum produto foi selecionado. Por favor, selecione um produto antes de salvar o lote.", "OK");
                    return;
                }

                // Obtenha os valores dos Entry para inserir no lote
                int quantidade = int.Parse(Quantidade_Entry_PI.Text);
                DateTime dataVencimento = DateTime.Parse(DatadevencimentoEntry.Text);

                // Obtenha o n�mero do lote (N_Lote)
                int nLote = int.Parse(Nm_Cadastro_Produto_Insumo.Text);

                // Obtenha a data de cadastro a partir de um Entry
                DateTime dataCadastro = DateTime.Parse(DatadecadastroEntry.Text);

                // Obtenha a intercorr�ncia (se houver)
                string intercorrencia = IntercorrenciaEditor.Text;

                // Crie uma inst�ncia de DataAccess
                var dataAccess = new DataAccess();

                // Insira os dados na tabela de lotes
                bool loteInserido = await dataAccess.InserirLoteAsync(idProdutoSelecionado, nLote, quantidade, dataCadastro, dataVencimento, intercorrencia);

                if (loteInserido) {
                    await DisplayAlert("Sucesso", "Lote salvo com sucesso!", "OK");

                    // Limpa os campos ap�s o sucesso
                    Quantidade_Entry_PI.Text = string.Empty;
                    DatadevencimentoEntry.Text = string.Empty;
                    Nm_Cadastro_Produto_Insumo.Text = string.Empty;
                    DatadecadastroEntry.Text = string.Empty;
                    IntercorrenciaEditor.Text = string.Empty;

                    // Opcional: Desabilitar os campos (se necess�rio)
                    Quantidade_Entry_PI.IsEnabled = false;
                    DatadevencimentoEntry.IsEnabled = false;
                    Nm_Cadastro_Produto_Insumo.IsEnabled = false;
                    DatadecadastroEntry.IsEnabled = false;
                    IntercorrenciaEditor.IsEnabled = false;
                }
                else {
                    await DisplayAlert("Erro", "Falha ao salvar o lote. Verifique os dados e tente novamente.", "OK");
                }
            }
            catch (Exception ex) {
                await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
            }
        }



        private async void Button_Novo_Produto(object sender, EventArgs e) {
            operacaoEscolhida = "produto";

            // Crie uma inst�ncia de DataAccess (assumindo que voc� j� tenha a conex�o configurada)
            var dataAccess = new DataAccess();
            
            CadastroprodutoEntry.IsEnabled = true;
            CadastroUnidadeEntry.IsEnabled = true;
            ValorEntry.IsEnabled = true;
        }

        private void ValorProduto_TextChanged(object sender, TextChangedEventArgs e) {
            var entry = sender as Entry;

            if (entry != null) {
                // Salvar a posi��o original do cursor antes da atualiza��o
                int originalCursorPosition = entry.CursorPosition;
                int originalTextLength = entry.Text?.Length ?? 0;

                // Remover caracteres n�o num�ricos e formatar corretamente
                string unformattedText = e.NewTextValue?.Replace(".", "").Replace(",", "").Trim() ?? string.Empty;

                if (decimal.TryParse(unformattedText, out decimal value)) {
                    // Divide por 100 para considerar centavos e formata sem o s�mbolo de moeda
                    string formattedValue = (value / 100).ToString("N2", new CultureInfo("pt-BR"));

                    // Atualiza o texto formatado no Entry
                    entry.Text = formattedValue;

                    // Calcula a nova posi��o do cursor
                    int formattedTextLength = formattedValue.Length;
                    int cursorOffset = formattedTextLength - originalTextLength;
                    int newCursorPosition = Math.Max(0, Math.Min(originalCursorPosition + cursorOffset, formattedValue.Length));

                    // Ajusta a posi��o do cursor usando Dispatcher
                    entry.Dispatcher.Dispatch(() => {
                        entry.CursorPosition = newCursorPosition;
                    });


                }
            }
        }

        private async void Button_Salvar_Produtos(object sender, EventArgs e) {
            try {
                // Verifica se os campos obrigat�rios n�o est�o vazios
                if (string.IsNullOrWhiteSpace(CadastroprodutoEntry.Text) ||
                    string.IsNullOrWhiteSpace(CadastroUnidadeEntry.Text) ||
                    !decimal.TryParse(ValorEntry.Text, out decimal valorProduto)) {
                    await DisplayAlert("Erro", "Por favor, preencha todos os campos corretamente.", "OK");
                    return;
                }

                // Crie uma inst�ncia de DataAccess
                var dataAccess = new DataAccess();

                // Obtenha os valores dos Entry para inserir na tabela de produtos
                string nomeProduto = CadastroprodutoEntry.Text.Trim(); // Remove espa�os extras
                string unidade = CadastroUnidadeEntry.Text.Trim(); // Remove espa�os extras

                // Garantir que o valor do produto tenha 2 casas decimais
                valorProduto = Math.Round(valorProduto, 2); // Arredonda o valor para 2 casas decimais

                // Insira os dados na tabela de produtos
                bool produtoInserido = await dataAccess.InserirProdutoAsync(nomeProduto, unidade, valorProduto);

                if (produtoInserido) {
                    await DisplayAlert("Sucesso", "Produto salvo com sucesso!", "OK");

                    // Limpa os campos ap�s o sucesso
                    CadastroprodutoEntry.Text = string.Empty;
                    CadastroUnidadeEntry.Text = string.Empty;
                    ValorEntry.Text = string.Empty;

                    // Opcional: Desabilitar os campos (voc� pode definir como false ou como preferir)
                    CadastroprodutoEntry.IsEnabled = false;
                    CadastroUnidadeEntry.IsEnabled = false;
                    ValorEntry.IsEnabled = false;
                }
                else {
                    await DisplayAlert("Erro", "Falha ao salvar o produto. Verifique os dados e tente novamente.", "OK");
                }
            }
            catch (Exception ex) {
                await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
            }
        }




        private async void Button_Salvar_Lote_Insumos(object sender, EventArgs e) {
            try {
                // Verifique se um produto foi selecionado
                int idInsumoSelecionado = Insumos.ID;

                if (idInsumoSelecionado == 0) {
                    await DisplayAlert("Erro", "Nenhum produto foi selecionado. Por favor, selecione um produto antes de salvar o lote.", "OK");
                    return;
                }

                // Obtenha os valores dos Entry para inserir no lote
                int quantidadeInsumo = int.Parse(Quantidade_Insumo_Entry.Text);
                DateTime dataVencimentoInsumo = DateTime.Parse(Datadevencimento_Insumo_Entry.Text);

                // Obtenha o n�mero do lote (N_Lote)
                int nLoteInsumo = int.Parse(Nm_Cadastro_Insumo.Text); // Certifique-se de que NLoteEntry � o nome do seu Entry para o n�mero do lote

                // Obtenha a data de cadastro a partir de um Entry
                DateTime dataCadastroInsumo = DateTime.Parse(Datadecadastro_Insumo_Entry.Text); // Aqui, voc� pega a data do Entry

                // Obtenha a intercorr�ncia (se houver)
                string intercorrenciaInsumo = Intercorrencia_Insumo_Editor.Text; // Certifique-se de que IntercorrenciaEntry � o nome do seu Entry para intercorr�ncia

                // Crie uma inst�ncia de DataAccess
                var dataAccess = new DataAccess();

                // Insira os dados na tabela de lotes
                bool loteInserido = await dataAccess.InserirLoteInsumoAsync(idInsumoSelecionado, nLoteInsumo, quantidadeInsumo, dataCadastroInsumo, dataVencimentoInsumo, intercorrenciaInsumo);

                if (loteInserido) {
                    await DisplayAlert("Sucesso", "Lote salvo com sucesso!", "OK");

                    // Limpar os campos ap�s salvar
                    Quantidade_Insumo_Entry.Text = string.Empty;
                    Datadevencimento_Insumo_Entry.Text = string.Empty;
                    Nm_Cadastro_Insumo.Text = string.Empty;
                    Datadecadastro_Insumo_Entry.Text = string.Empty;
                    Intercorrencia_Insumo_Editor.Text = string.Empty;

                    // Desabilitar os campos ap�s salvar
                    Quantidade_Insumo_Entry.IsEnabled = false;
                    Datadevencimento_Insumo_Entry.IsEnabled = false;
                    Nm_Cadastro_Insumo.IsEnabled = false;
                    Datadecadastro_Insumo_Entry.IsEnabled = false;
                    Intercorrencia_Insumo_Editor.IsEnabled = false;

                }
                else {
                    await DisplayAlert("Erro", "Falha ao salvar o lote. Verifique os dados e tente novamente.", "OK");
                }
            }
            catch (Exception ex) {
                await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
            }
        }

        private void CadastroprodutoEntry_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private async void ProdutoListView_ItemTapped(object sender, ItemTappedEventArgs e) {

        }

        private async void Button_Consultar_Produto(object sender, EventArgs e) {
            await Navigation.PushAsync(new ConsultarProduto());
        }

        private async void Button_Selecionar_Produto(object sender, EventArgs e) {
            var consultarProdutoPage = new ConsultarProduto();

            // Inscreva-se no evento ProdutoSelecionado
            consultarProdutoPage.ProdutoSelecionado += OnProdutoSelecionado;

            await Navigation.PushAsync(consultarProdutoPage);
        }

        private string operacaoEscolhidaInsumo;
        private async void Button_Novo_Insumo(object sender, EventArgs e) {
            operacaoEscolhidaInsumo = "produto";

            // Crie uma inst�ncia de DataAccess (assumindo que voc� j� tenha a conex�o configurada)
            var dataAccess = new DataAccess();

            // Crie uma inst�ncia do GeradorId
            var geradorId = new GeradorId(dataAccess);

            // Gere um novo ID para o produto usando a classe GeradorId
            int novoIdProduto = await geradorId.GerarNovoIdAsync();

            // Define o ID gerado no Entry IdEntry
            Nm_Cadastro_Insumo.Text = novoIdProduto.ToString();

            // Habilita apenas os Entry necess�rios para o "Novo Produto"
            Nm_Cadastro_Insumo.IsEnabled = true;
            CadastroInsumoEntry.IsEnabled = true;
            Cadastro_Unidade_Insumo_Entry.IsEnabled = true;
            Valor_Insumo_Entry.IsEnabled = true;
        }

        private void Valorinsumo_TextChanged(object sender, TextChangedEventArgs e) {
            var entry = sender as Entry;

            if (entry != null) {
                // Salvar a posi��o original do cursor antes da atualiza��o
                int originalCursorPosition = entry.CursorPosition;
                int originalTextLength = entry.Text?.Length ?? 0;

                // Remover caracteres n�o num�ricos e formatar corretamente
                string unformattedText = e.NewTextValue?.Replace(".", "").Replace(",", "").Trim() ?? string.Empty;

                if (decimal.TryParse(unformattedText, out decimal value)) {
                    // Divide por 100 para considerar centavos e formata sem o s�mbolo de moeda
                    string formattedValue = (value / 100).ToString("N2", new CultureInfo("pt-BR"));

                    // Atualiza o texto formatado no Entry
                    entry.Text = formattedValue;

                    // Calcula a nova posi��o do cursor
                    int formattedTextLength = formattedValue.Length;
                    int cursorOffset = formattedTextLength - originalTextLength;
                    int newCursorPosition = Math.Max(0, Math.Min(originalCursorPosition + cursorOffset, formattedValue.Length));

                    // Ajusta a posi��o do cursor usando Dispatcher
                    entry.Dispatcher.Dispatch(() => {
                        entry.CursorPosition = newCursorPosition;
                    });

                    
                }
            }
        }

        private async void Button_Novo_Lote_Insumo(object sender, EventArgs e) {
            operacaoEscolhidaInsumo = "lote";

            // Crie uma inst�ncia de DataAccess (assumindo que voc� j� tenha a conex�o configurada)
            var dataAccess = new DataAccess();

            // Crie uma inst�ncia do GeradorId
            var geradorId = new GeradorId(dataAccess);

            // Gere um novo ID para o lote usando a classe GeradorId
            int novoIdLote = await geradorId.GerarNovoIdAsync();

            // Define o ID gerado no Entry IdEntry
            Nm_Cadastro_Insumo.Text = novoIdLote.ToString();

            // Crie uma inst�ncia de GeradorDataHora para obter a data atual
            var geradorDataHora = new GeradorDataHora(Application.Current.Dispatcher);

            // Obtenha a data atual
            string dataAtual = geradorDataHora.DataAtual;

            // Define a data de cadastro no Entry correspondente
            Datadecadastro_Insumo_Entry.Text = dataAtual; // Altere "DataCadastroEntry" para o nome correto do seu Entry de data de cadastro

            // Habilita apenas os Entry necess�rios para o "Novo Lote"
            Nm_Cadastro_Insumo.IsEnabled = true;
            Quantidade_Insumo_Entry.IsEnabled = true;
            Datadevencimento_Insumo_Entry.IsEnabled = true;
            Intercorrencia_Insumo_Editor.IsEnabled = true;
        }

        private async void Button_Salvar_Insumos(object sender, EventArgs e) {
            try {
                // Verifica se os campos obrigat�rios n�o est�o vazios e se o valor do insumo � v�lido
                if (string.IsNullOrWhiteSpace(CadastroInsumoEntry.Text) ||
                    string.IsNullOrWhiteSpace(Cadastro_Unidade_Insumo_Entry.Text) ||
                    !decimal.TryParse(Valor_Insumo_Entry.Text, out decimal valorInsumo)) {
                    await DisplayAlert("Erro", "Por favor, preencha todos os campos corretamente.", "OK");
                    return;
                }

                // Crie uma inst�ncia de DataAccess
                var dataAccess = new DataAccess();

               
                string nomeInsumo = CadastroInsumoEntry.Text.Trim(); // Remove espa�os extras
                string unidadeInsumo = Cadastro_Unidade_Insumo_Entry.Text.Trim(); // Remove espa�os extras

                // Arredonda o valor do insumo para 2 casas decimais
                valorInsumo = Math.Round(valorInsumo, 2);

               
                bool insumoInserido = await dataAccess.InserirInsumoAsync(nomeInsumo, unidadeInsumo, valorInsumo);

                if (insumoInserido) {
                    await DisplayAlert("Sucesso", "Insumo salvo com sucesso!", "OK");

                    // Limpa os campos ap�s o sucesso
                    CadastroInsumoEntry.Text = string.Empty;
                    Cadastro_Unidade_Insumo_Entry.Text = string.Empty;
                    Valor_Insumo_Entry.Text = string.Empty;

                    // Opcional: Desabilitar os campos (se necess�rio)
                    CadastroInsumoEntry.IsEnabled = false;
                    Cadastro_Unidade_Insumo_Entry.IsEnabled = false;
                    Valor_Insumo_Entry.IsEnabled = false;
                }
                else {
                    await DisplayAlert("Erro", "Falha ao salvar o insumo. Verifique os dados e tente novamente.", "OK");
                }
            }
            catch (Exception ex) {
                await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
            }
        }



        private async void Button_Consultar_Insumo(object sender, EventArgs e) {
            await Navigation.PushAsync(new SelecionarInsumo());
        }


        private void OnProdutoSelecionado(Produto produto) {
            // Define os valores dos Entry de Produto e Pre�o Unit�rio com o produto selecionado
            ProdutoEntry.Text = produto.Nome_Produto;
            Preco_UnitarioEntry.Text = produto.Valor.ToString();

            // Volta para a p�gina anterior
            Navigation.PopAsync();
        }

        private async void Button_Selecionar_Cliente(object sender, EventArgs e) {
            var consultarClientePage = new ConsultarCliente();

            // Inscreva-se no evento ClienteSelecionado para receber o cliente selecionado
            consultarClientePage.ClienteSelecionado += OnClienteSelecionado;

            await Navigation.PushAsync(consultarClientePage);
        }

        private void OnClienteSelecionado(Cliente cliente) {
            // Preenche o Entry com o nome do cliente selecionado
            ClienteEntry.Text = cliente.Nome_Cliente;
            Vd_cpf_cnpjEntry.Text = cliente.CpfOuCnpj;

            // Volta para a p�gina anterior
            Navigation.PopAsync();
        }


        private void ValorToTalEntry_TextChanged(object sender, TextChangedEventArgs e) {
            var entry = sender as Entry;

            if (entry != null) {
                // Remove caracteres n�o num�ricos e formata corretamente
                string unformattedText = e.NewTextValue?.Replace(".", "").Replace(",", "").Trim() ?? string.Empty;

                if (decimal.TryParse(unformattedText, out decimal value)) {
                    // Divide por 100 para considerar centavos e formata sem o s�mbolo de moeda
                    string formattedValue = (value / 100).ToString("N2", new CultureInfo("pt-BR"));

                    // Atualiza o texto formatado no Entry
                    entry.Text = formattedValue;

                    // Movimenta o cursor para o final
                    entry.CursorPosition = formattedValue.Length;
                }
            }
        }


        private void Preco_UnitarioEntry_TextChanged(object sender, TextChangedEventArgs e) {
            CalcularValorTotal();

            if (sender is Entry entry) {
                // Remove a formata��o, mantendo apenas os n�meros e a v�rgula como separador decimal
                string input = e.NewTextValue?.Replace(".", "").Replace("R$", "") ?? string.Empty;

                // Verificar se a entrada est� vazia ou cont�m caracteres n�o num�ricos
                if (decimal.TryParse(input, out decimal valor)) {
                    // Agora, formatar corretamente com a v�rgula como separador decimal
                    entry.Text = valor.ToString("#,0.00", new CultureInfo("pt-BR"));

                    // Move o cursor para o final do texto
                    entry.CursorPosition = entry.Text.Length;
                }
                else {
                    // Caso a convers�o falhe, podemos limpar ou manter o texto, conforme necess�rio
                    entry.Text = string.Empty;
                }
            }
        }


        private void QuantidadeEntry_TextChanged(object sender, TextChangedEventArgs e) {
            CalcularValorTotal();
        }


        private void DescontoEntry_TextChanged(object sender, TextChangedEventArgs e) {
            var entry = sender as Entry;

            if (entry != null) {
                // Salvar a posi��o original do cursor antes da atualiza��o
                int originalCursorPosition = entry.CursorPosition;
                int originalTextLength = entry.Text?.Length ?? 0;

                // Remover caracteres n�o num�ricos e formatar corretamente
                string unformattedText = e.NewTextValue?.Replace(".", "").Replace(",", "").Trim() ?? string.Empty;

                if (decimal.TryParse(unformattedText, out decimal value)) {
                    // Divide por 100 para considerar centavos e formata sem o s�mbolo de moeda
                    string formattedValue = (value / 100).ToString("N2", new CultureInfo("pt-BR"));

                    // Atualiza o texto formatado no Entry
                    entry.Text = formattedValue;

                    // Calcula a nova posi��o do cursor
                    int formattedTextLength = formattedValue.Length;
                    int cursorOffset = formattedTextLength - originalTextLength;
                    int newCursorPosition = Math.Max(0, Math.Min(originalCursorPosition + cursorOffset, formattedValue.Length));

                    // Ajusta a posi��o do cursor usando Dispatcher
                    entry.Dispatcher.Dispatch(() => {
                        entry.CursorPosition = newCursorPosition;
                    });

                    // Recalcula o valor total com desconto
                    CalcularValorTotal();
                }
            }
        }


        private void CalcularValorTotal() {
            
            string precoTexto = Preco_UnitarioEntry.Text?.Replace("R$", "").Trim() ?? string.Empty;
            string quantidadeTexto = QuantidadeEntry.Text?.Trim() ?? string.Empty;
            string descontoTexto = DescontoEntry.Text?.Trim() ?? string.Empty;

            bool precoValido = decimal.TryParse(precoTexto, NumberStyles.Any, new CultureInfo("pt-BR"), out decimal precoUnitario);
            bool quantidadeValida = decimal.TryParse(quantidadeTexto, NumberStyles.Any, new CultureInfo("pt-BR"), out decimal quantidade);
            bool descontoValido = decimal.TryParse(descontoTexto, NumberStyles.Any, new CultureInfo("pt-BR"), out decimal desconto);

            if (precoValido && quantidadeValida) {
                // Calcular o valor total sem desconto
                decimal valorTotal = precoUnitario * quantidade;

                if (descontoValido) {
                    // Subtrair o desconto
                    valorTotal -= desconto;
                }

                // Atualizar o campo de valor total com desconto
                ValorToTalEntry.Text = valorTotal.ToString("N2", new CultureInfo("pt-BR"));
            }
            else {
                // Limpar os campos se houver valores inv�lidos
                ValorToTalEntry.Text = "";
            }
        }

        private async void Button_Clicked_Salvar_Venda(object sender, EventArgs e) {
            try {
                // Dados da venda
                int numeroVenda = int.Parse(NumeroVendaEntry.Text);
                DateTime dataVenda = DateTime.Parse(DataEntry.Text);
                TimeSpan horaVenda = TimeSpan.Parse(HoraEntry.Text);
                string vendedor = VendedorEntry.Text;
                string nomeCliente = ClienteEntry.Text;
                string vdCpfCnpj = Vd_cpf_cnpjEntry.Text;

                var dataAccess = new DataAccess();
                // Inserir a venda
                int idVenda = await dataAccess.InserirVendaAsync(numeroVenda, dataVenda, horaVenda, vendedor, nomeCliente, vdCpfCnpj);

                if (idVenda > 0) {
                    // Iterar pelos itens e inserir na tabela item_venda
                    foreach (var venda in framesPaginaVendas.Vendas) {
                        bool itemInserido = await dataAccess.InserirItensVendaAsync(
                            idVenda,
                            venda.Produto,
                            venda.Quantidade,
                            venda.PrecoUnitario,
                            venda.Desconto,
                            venda.ValorTotal
                        );

                        if (!itemInserido) {
                            await DisplayAlert("Erro", "Falha ao salvar alguns itens de venda.", "OK");
                            return;
                        }
                    }

                    // Exibir mensagem de sucesso com bot�o de imprimir
                    bool imprimir = await DisplayAlert("Sucesso", "Venda e itens salvos com sucesso! Deseja imprimir a venda?", "Sim", "N�o");

                    if (imprimir) {
                        ImprimirVenda(idVenda); // M�todo para imprimir a venda
                    }

                    // Limpar os campos ap�s salvar a venda
                    NumeroVendaEntry.Text = string.Empty;
                    DataEntry.Text = string.Empty;
                    HoraEntry.Text = string.Empty;
                    VendedorEntry.Text = string.Empty;
                    ClienteEntry.Text = string.Empty;
                    Vd_cpf_cnpjEntry.Text = string.Empty;

                    // **Limpar os StackLayouts**
                    ProductStackLayout.Children.Clear();
                    LinhaQuantidade.Children.Clear();
                    LinhaPrecoUn.Children.Clear();
                    LinhaDesconto.Children.Clear();
                    LinhaValorTotal.Children.Clear();

                    // **Limpar a lista de vendas**
                    framesPaginaVendas.LimparVendas();
                }
                else {
                    await DisplayAlert("Erro", "Erro ao salvar a venda.", "OK");
                }
            }
            catch (Exception ex) {
                await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
            }
        }


        
        private void ImprimirVenda(int idVenda) {
            // L�gica para imprimir ou gerar relat�rio da venda
            Console.WriteLine($"Imprimindo a venda com ID: {idVenda}");
        }


        private async void Button_Clicked_Cancelar(object sender, EventArgs e) {

        }


        private async void Button_Clicked_Alterar(object sender, EventArgs e) {
           
        }

        private async void Button_ManualdeAjuda(object sender, EventArgs e) {
            var filePath = @"C:\Users\nolet\Documents\TELAS DO SISTEMA\Manual de Ajuda.pdf";

            if (File.Exists(filePath)) {
                await Launcher.OpenAsync(new OpenFileRequest {
                    File = new ReadOnlyFile(filePath)
                });
            }
            else {
                await DisplayAlert("Erro", "Arquivo n�o encontrado!", "OK");
            }
        }

        private async void Button_Suporte(object sender, EventArgs e) {
            await DisplayAlert("Suporte T�cnico",
                "Para qualquer d�vida ou ajuda, entre em contato com nosso suporte t�cnico!\n\nE-mail: suporteagrotech@gmail.com",
                "OK");
        }


        private async void Button_Vendas(object sender, EventArgs e) {
            await Navigation.PushAsync(new VendasRealizadas());

        }

        private async void Button_Produtos_Cadastrados(object sender, EventArgs e) {
            await Navigation.PushAsync(new ProdutoselotesCadastrados());
        }

        private async void Button_Insumos_Cadastrados(object sender, EventArgs e) {
            await Navigation.PushAsync(new InsumosCadastrados());
        }

        private async void Button_Clientes_Cadastrados(object sender, EventArgs e) {
            await Navigation.PushAsync(new ClientesCadastrados());
        }

        private async void Button_Usuarios_Cadastrados(object sender, EventArgs e) {
            await Navigation.PushAsync(new UsuariosCadastrados());
        }

        private void Button_Novousuario(object sender, EventArgs e) {
            nomeEntry.IsEnabled = true;
            cpfEntry.IsEnabled = true;
            nomeUsuarioEntry.IsEnabled = true;
            senhaEntry.IsEnabled = true;
        }
    }

}

