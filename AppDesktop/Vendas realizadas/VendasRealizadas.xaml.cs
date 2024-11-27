using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;

namespace Agrotech.Vendas_realizadas;

public partial class VendasRealizadas : ContentPage
{
	public VendasRealizadas()
	{
		InitializeComponent();
	}

    private async void Button_Numero_venda(object sender, EventArgs e) {
        // Verifica se o campo do número da venda está preenchido
        if (string.IsNullOrWhiteSpace(NumeroVendaEntry.Text)) {
            await DisplayAlert("Erro", "Por favor, informe o número da venda.", "OK");
            return;
        }

        try {
            // Chama o método de busca no banco de dados
            BuscaBancoDados buscaBanco = new BuscaBancoDados();
            DataTable venda = await buscaBanco.BuscarVendaPorNumeroAsync(NumeroVendaEntry.Text);

            // Verifica se há dados retornados
            if (venda.Rows.Count > 0) {
                List<Venda> listaVendas = new List<Venda>();
                foreach (DataRow row in venda.Rows) {
                    listaVendas.Add(new Venda {
                        id_Venda = row["id_Venda"].ToString(),
                        Nm_Venda = row["Nm_Venda"].ToString(),
                        data_Venda = Convert.ToDateTime(row["data_Venda"]).ToShortDateString(),
                        hora_Venda = row["hora_Venda"].ToString(),
                        Vendedor = row["Vendedor"].ToString(),
                        Nm_Cliente = row["Nm_Cliente"].ToString(),
                        cpf_cnpj_cliente = row["cpf_cnpj_cliente"].ToString()
                    });
                }
                VendasCollectionView.ItemsSource = listaVendas;
            }
            else {
                await DisplayAlert("Aviso", "Nenhuma venda encontrada com esse número.", "OK");
            }
        }
        catch (Exception ex) {
            await DisplayAlert("Erro", "Erro ao buscar dados: " + ex.Message, "OK");
        }
    }


    private async void Button_Datafinal_DataInicial(object sender, EventArgs e) {
        // Verifica se as datas estão preenchidas
        if (string.IsNullOrWhiteSpace(DataInicial.Text) || string.IsNullOrWhiteSpace(DataFinal.Text)) {
            await DisplayAlert("Erro", "Por favor, preencha as datas corretamente.", "OK");
            return;
        }

        try {
            // Chama o método de busca no banco de dados
            BuscaBancoDados buscaBanco = new BuscaBancoDados();
            DataTable vendas = await buscaBanco.BuscarVendasPorDataAsync(DataInicial.Text, DataFinal.Text);

            // Verifica se há dados retornados
            if (vendas.Rows.Count > 0) {
                List<Venda> listaVendas = new List<Venda>();
                foreach (DataRow row in vendas.Rows) {
                    listaVendas.Add(new Venda {
                        id_Venda = row["id_Venda"].ToString(),  // Atribuindo o id_Venda
                        Nm_Venda = row["Nm_Venda"].ToString(),
                        data_Venda = Convert.ToDateTime(row["data_Venda"]).ToShortDateString(),
                        hora_Venda = row["hora_Venda"].ToString(),
                        Vendedor = row["Vendedor"].ToString(),
                        Nm_Cliente = row["Nm_Cliente"].ToString(),
                        cpf_cnpj_cliente = row["cpf_cnpj_cliente"].ToString()
                    });

                }
                VendasCollectionView.ItemsSource = listaVendas;
            }
            else {
                await DisplayAlert("Aviso", "Nenhuma venda encontrada nesse período.", "OK");
            }
        }
        catch (Exception ex) {
            await DisplayAlert("Erro", "Erro ao buscar dados: " + ex.Message, "OK");
        }
    }

    private async void Button_Cliente(object sender, EventArgs e) {
        // Verifica se o campo do nome do cliente está preenchido
        if (string.IsNullOrWhiteSpace(PesquisaCliente.Text)) {
            await DisplayAlert("Erro", "Por favor, informe o nome do cliente.", "OK");
            return;
        }

        try {
            // Chama o método de busca no banco de dados
            BuscaBancoDados buscaBanco = new BuscaBancoDados();
            DataTable vendas = await buscaBanco.BuscarVendasPorClienteAsync(PesquisaCliente.Text);

            // Verifica se há dados retornados
            if (vendas.Rows.Count > 0) {
                List<Venda> listaVendas = new List<Venda>();
                foreach (DataRow row in vendas.Rows) {
                    listaVendas.Add(new Venda {
                        id_Venda = row["id_Venda"].ToString(),
                        Nm_Venda = row["Nm_Venda"].ToString(),
                        data_Venda = Convert.ToDateTime(row["data_Venda"]).ToShortDateString(),
                        hora_Venda = row["hora_Venda"].ToString(),
                        Vendedor = row["Vendedor"].ToString(),
                        Nm_Cliente = row["Nm_Cliente"].ToString(),
                        cpf_cnpj_cliente = row["cpf_cnpj_cliente"].ToString()
                    });
                }
                VendasCollectionView.ItemsSource = listaVendas;
            }
            else {
                await DisplayAlert("Aviso", "Nenhuma venda encontrada para esse cliente.", "OK");
            }
        }
        catch (Exception ex) {
            await DisplayAlert("Erro", "Erro ao buscar dados: " + ex.Message, "OK");
        }
    }

    private async void Button_CpfCnpj(object sender, EventArgs e) {
        // Verifica se o campo do CPF/CNPJ está preenchido
        if (string.IsNullOrWhiteSpace(PesquisaClientecpfcpnj.Text)) {
            await DisplayAlert("Erro", "Por favor, informe o CPF ou CNPJ do cliente.", "OK");
            return;
        }

        try {
            // Chama o método de busca no banco de dados
            BuscaBancoDados buscaBanco = new BuscaBancoDados();
            DataTable vendas = await buscaBanco.BuscarVendasPorCpfCnpjAsync(PesquisaClientecpfcpnj.Text);

            // Verifica se há dados retornados
            if (vendas.Rows.Count > 0) {
                List<Venda> listaVendas = new List<Venda>();
                foreach (DataRow row in vendas.Rows) {
                    listaVendas.Add(new Venda {
                        id_Venda = row["id_Venda"].ToString(),
                        Nm_Venda = row["Nm_Venda"].ToString(),
                        data_Venda = Convert.ToDateTime(row["data_Venda"]).ToShortDateString(),
                        hora_Venda = row["hora_Venda"].ToString(),
                        Vendedor = row["Vendedor"].ToString(),
                        Nm_Cliente = row["Nm_Cliente"].ToString(),
                        cpf_cnpj_cliente = row["cpf_cnpj_cliente"].ToString()
                    });
                }
                VendasCollectionView.ItemsSource = listaVendas;
            }
            else {
                await DisplayAlert("Aviso", "Nenhuma venda encontrada para este CPF ou CNPJ.", "OK");
            }
        }
        catch (Exception ex) {
            await DisplayAlert("Erro", "Erro ao buscar dados: " + ex.Message, "OK");
        }
    }

   


}


