using System.Data;

namespace Agrotech.Clientes_Cadastrados;

public partial class ClientesCadastrados : ContentPage
{
	public ClientesCadastrados()
	{
		InitializeComponent();
	}

    private async void Button_Pesquisacpfcnpj(object sender, EventArgs e) {
        // Verifica se o campo do CNPJ está preenchido
        if (string.IsNullOrWhiteSpace(Pesquisapelocpfcnpj.Text)) {
            await DisplayAlert("Erro", "Por favor, informe o CNPJ.", "OK");
            return;
        }

        try {
            // Cria a instância da classe de busca no banco de dados
            BuscaBancoDados buscaBanco = new BuscaBancoDados();

            // Chama o método para buscar os dados dos clientes pelo CNPJ
            DataTable clienteDataTable = await buscaBanco.BuscarClientesPorCnpjAsync(Pesquisapelocpfcnpj.Text);

            // Verifica se há dados retornados
            if (clienteDataTable.Rows.Count > 0) {
                List<Clientes> listaClientes = new List<Clientes>();

                foreach (DataRow row in clienteDataTable.Rows) {
                    listaClientes.Add(new Clientes {
                        id_cliente = Convert.ToInt32(row["id_cliente"]),
                        tipo_cliente = row["tipo_cliente"].ToString(),
                        nome_cliente = row["nome_cliente"].ToString(),
                        rua = row["rua"].ToString(),
                        numero = row["numero"].ToString(),
                        complemento = row["complemento"].ToString(),
                        CEP = row["CEP"].ToString(),
                        bairro = row["bairro"].ToString(),
                        estado = row["estado"].ToString(),
                        cidade = row["cidade"].ToString(),
                        cpf = row["cpf"].ToString(),
                        rg_cliente = row["rg_cliente"].ToString(),
                        data_nascimento = Convert.ToDateTime(row["data_nascimento"]).ToShortDateString(),
                        genero_cliente = row["genero_cliente"].ToString(),
                        cnpj = row["cnpj"].ToString(),
                        razao_social = row["razao_social"].ToString(),
                        nome_responsavel = row["nome_responsavel"].ToString(),
                        nm_cadastro_cliente = row["nm_cadastro_cliente"].ToString()
                    });
                }

                // Exibe os resultados na CollectionView
                ClientesCollectionView.ItemsSource = listaClientes;
            }
            else {
                await DisplayAlert("Aviso", "Nenhum cliente encontrado com esse CNPJ.", "OK");
            }
        }
        catch (Exception ex) {
            await DisplayAlert("Erro", "Erro ao buscar dados de clientes: " + ex.Message, "OK");
        }
    }

    private async void Button_Buscapelonomecliente(object sender, EventArgs e) {
        // Verifica se o campo do nome do cliente está preenchido
        if (string.IsNullOrWhiteSpace(PesquisaPeloNomeclinte.Text)) {
            await DisplayAlert("Erro", "Por favor, informe o nome do cliente.", "OK");
            return;
        }

        try {
            // Cria a instância da classe de busca no banco de dados
            BuscaBancoDados buscaBanco = new BuscaBancoDados();

            // Chama o método para buscar os dados dos clientes pelo nome
            DataTable clienteDataTable = await buscaBanco.BuscarClientesPorNomeAsync(PesquisaPeloNomeclinte.Text);

            // Verifica se há dados retornados
            if (clienteDataTable.Rows.Count > 0) {
                List<Clientes> listaClientes = new List<Clientes>();

                foreach (DataRow row in clienteDataTable.Rows) {
                    listaClientes.Add(new Clientes {
                        id_cliente = Convert.ToInt32(row["id_cliente"]),
                        tipo_cliente = row["tipo_cliente"].ToString(),
                        nome_cliente = row["nome_cliente"].ToString(),
                        rua = row["rua"].ToString(),
                        numero = row["numero"].ToString(),
                        complemento = row["complemento"].ToString(),
                        CEP = row["CEP"].ToString(),
                        bairro = row["bairro"].ToString(),
                        estado = row["estado"].ToString(),
                        cidade = row["cidade"].ToString(),
                        cpf = row["cpf"].ToString(),
                        rg_cliente = row["rg_cliente"].ToString(),
                        data_nascimento = Convert.ToDateTime(row["data_nascimento"]).ToShortDateString(),
                        genero_cliente = row["genero_cliente"].ToString(),
                        cnpj = row["cnpj"].ToString(),
                        razao_social = row["razao_social"].ToString(),
                        nome_responsavel = row["nome_responsavel"].ToString(),
                        nm_cadastro_cliente = row["nm_cadastro_cliente"].ToString()
                    });
                }

                // Exibe os resultados na CollectionView
                ClientesCollectionView.ItemsSource = listaClientes;
            }
            else {
                await DisplayAlert("Aviso", "Nenhum cliente encontrado com esse nome.", "OK");
            }
        }
        catch (Exception ex) {
            await DisplayAlert("Erro", "Erro ao buscar dados de clientes: " + ex.Message, "OK");
        }
    }

    private async void Button_Buscarpelotipocliente(object sender, EventArgs e) {
        // Verifica se o campo do tipo de cliente está preenchido
        if (string.IsNullOrWhiteSpace(PesquisaPeloTipoCliente.Text)) {
            await DisplayAlert("Erro", "Por favor, informe o tipo de cliente.", "OK");
            return;
        }

        try {
            // Cria a instância da classe de busca no banco de dados
            BuscaBancoDados buscaBanco = new BuscaBancoDados();

            // Chama o método para buscar os dados dos clientes pelo tipo (PF ou PJ)
            DataTable clienteDataTable = await buscaBanco.BuscarClientesPorTipoAsync(PesquisaPeloTipoCliente.Text);

            // Verifica se há dados retornados
            if (clienteDataTable.Rows.Count > 0) {
                List<Clientes> listaClientes = new List<Clientes>();

                foreach (DataRow row in clienteDataTable.Rows) {
                    listaClientes.Add(new Clientes {
                        id_cliente = Convert.ToInt32(row["id_cliente"]),
                        tipo_cliente = row["tipo_cliente"].ToString(),
                        nome_cliente = row["nome_cliente"].ToString(),
                        rua = row["rua"].ToString(),
                        numero = row["numero"].ToString(),
                        complemento = row["complemento"].ToString(),
                        CEP = row["CEP"].ToString(),
                        bairro = row["bairro"].ToString(),
                        estado = row["estado"].ToString(),
                        cidade = row["cidade"].ToString(),
                        cpf = row["cpf"].ToString(),
                        rg_cliente = row["rg_cliente"].ToString(),
                        data_nascimento = Convert.ToDateTime(row["data_nascimento"]).ToShortDateString(),
                        genero_cliente = row["genero_cliente"].ToString(),
                        cnpj = row["cnpj"].ToString(),
                        razao_social = row["razao_social"].ToString(),
                        nome_responsavel = row["nome_responsavel"].ToString(),
                        nm_cadastro_cliente = row["nm_cadastro_cliente"].ToString()
                    });
                }

                // Exibe os resultados na CollectionView
                ClientesCollectionView.ItemsSource = listaClientes;
            }
            else {
                await DisplayAlert("Aviso", "Nenhum cliente encontrado com esse tipo.", "OK");
            }
        }
        catch (Exception ex) {
            await DisplayAlert("Erro", "Erro ao buscar dados de clientes: " + ex.Message, "OK");
        }
    }



}