using System.Data;

namespace Agrotech.Produtos_cadastrados;

public partial class ProdutoselotesCadastrados : ContentPage
{
	public ProdutoselotesCadastrados()
	{
		InitializeComponent();
	}

    private async void Button_Numero_Lote(object sender, EventArgs e) {
        // Verifica se o campo do número do lote está preenchido
        if (string.IsNullOrWhiteSpace(NumeroLoteEntry.Text)) {
            await DisplayAlert("Erro", "Por favor, informe o número do lote.", "OK");
            return;
        }

        try {
            // Cria a instância da classe de busca no banco de dados
            BuscaBancoDados buscaBanco = new BuscaBancoDados();

            // Chama o método para buscar os dados do lote e do produto
            DataTable loteProduto = await buscaBanco.BuscarLotesPorNumeroLoteAsync(NumeroLoteEntry.Text);

            // Verifica se há dados retornados
            if (loteProduto.Rows.Count > 0) {
                List<LoteProduto> listaLotes = new List<LoteProduto>();

                foreach (DataRow row in loteProduto.Rows) {
                    listaLotes.Add(new LoteProduto {
                        NomeProduto = row["Nome_Produto"].ToString(),
                        Quantidade = Convert.ToInt32(row["Quantidade"]),
                        DataCadastro = Convert.ToDateTime(row["Data_Cadastro"]).ToShortDateString(),
                        DataVencimento = Convert.ToDateTime(row["Data_Vencimento"]).ToShortDateString(),
                        IntercorrenciaLote = row["Intercorrencia_lote"].ToString()
                    });
                }

                // Exibe os resultados na interface
                LotesCollectionView.ItemsSource = listaLotes;
            }
            else {
                await DisplayAlert("Aviso", "Nenhum lote encontrado com esse número.", "OK");
            }
        }
        catch (Exception ex) {
            await DisplayAlert("Erro", "Erro ao buscar dados: " + ex.Message, "OK");
        }
    }

    private async void Button_Datainicial_final(object sender, EventArgs e) {
        // Verifica se as datas inicial e final estão preenchidas
        if (string.IsNullOrWhiteSpace(DataInicialLote.Text) || string.IsNullOrWhiteSpace(DataFinalLote.Text)) {
            await DisplayAlert("Erro", "Por favor, informe a data inicial e a data final.", "OK");
            return;
        }

        try {
            // Converte as strings das datas para DateTime
            DateTime dataInicial = Convert.ToDateTime(DataInicialLote.Text);
            DateTime dataFinal = Convert.ToDateTime(DataFinalLote.Text);

            // Verifica se a data inicial não é maior que a data final
            if (dataInicial > dataFinal) {
                await DisplayAlert("Erro", "A data inicial não pode ser maior que a data final.", "OK");
                return;
            }

            // Cria a instância da classe de busca no banco de dados
            BuscaBancoDados buscaBanco = new BuscaBancoDados();

            // Chama o método para buscar os dados de lote dentro do intervalo de datas
            DataTable loteProduto = await buscaBanco.BuscarLotesPorDataIntervaloAsync(dataInicial, dataFinal);

            // Verifica se há dados retornados
            if (loteProduto.Rows.Count > 0) {
                List<LoteProduto> listaLotes = new List<LoteProduto>();

                foreach (DataRow row in loteProduto.Rows) {
                    listaLotes.Add(new LoteProduto {
                        NomeProduto = row["Nome_Produto"].ToString(),
                        Quantidade = Convert.ToInt32(row["Quantidade"]),
                        DataCadastro = Convert.ToDateTime(row["Data_Cadastro"]).ToShortDateString(),
                        DataVencimento = Convert.ToDateTime(row["Data_Vencimento"]).ToShortDateString(),
                        IntercorrenciaLote = row["Intercorrencia_lote"].ToString()
                    });
                }

                // Exibe os resultados na interface
                LotesCollectionView.ItemsSource = listaLotes;
            }
            else {
                await DisplayAlert("Aviso", "Nenhum lote encontrado no intervalo de datas informado.", "OK");
            }
        }
        catch (Exception ex) {
            await DisplayAlert("Erro", "Erro ao buscar dados: " + ex.Message, "OK");
        }
    }

    private async void Button_Produto(object sender, EventArgs e) {
        // Verifica se o nome do produto foi informado
        if (string.IsNullOrWhiteSpace(PesquisaProduto.Text)) {
            await DisplayAlert("Erro", "Por favor, informe o nome do produto.", "OK");
            return;
        }

        try {
            // Cria a instância da classe de busca no banco de dados
            BuscaBancoDados buscaBanco = new BuscaBancoDados();

            // Chama o método para buscar os dados do lote e produto baseado no nome do produto
            DataTable loteProduto = await buscaBanco.BuscarLotesPorNomeProdutoAsync(PesquisaProduto.Text);

            // Verifica se há dados retornados
            if (loteProduto.Rows.Count > 0) {
                List<LoteProduto> listaLotes = new List<LoteProduto>();

                foreach (DataRow row in loteProduto.Rows) {
                    listaLotes.Add(new LoteProduto {
                        NomeProduto = row["Nome_Produto"].ToString(),
                        Quantidade = Convert.ToInt32(row["Quantidade"]),
                        DataCadastro = Convert.ToDateTime(row["Data_Cadastro"]).ToShortDateString(),
                        DataVencimento = Convert.ToDateTime(row["Data_Vencimento"]).ToShortDateString(),
                        IntercorrenciaLote = row["Intercorrencia_lote"].ToString()
                    });
                }

                // Exibe os resultados na interface
                LotesCollectionView.ItemsSource = listaLotes;
            }
            else {
                await DisplayAlert("Aviso", "Nenhum lote encontrado para o nome do produto informado.", "OK");
            }
        }
        catch (Exception ex) {
            await DisplayAlert("Erro", "Erro ao buscar dados: " + ex.Message, "OK");
        }
    }

    private async void Button_Datadevencimento(object sender, EventArgs e) {
        // Verifica se a data de vencimento foi preenchida
        if (string.IsNullOrWhiteSpace(Vencimento.Text)) {
            await DisplayAlert("Erro", "Por favor, informe a data de vencimento.", "OK");
            return;
        }

        try {
            // Converte a string da data para DateTime
            DateTime dataVencimento = Convert.ToDateTime(Vencimento.Text);

            // Cria a instância da classe de busca no banco de dados
            BuscaBancoDados buscaBanco = new BuscaBancoDados();

            // Chama o método para buscar os dados de lote pela data de vencimento
            DataTable loteProduto = await buscaBanco.BuscarLotesPorDataVencimentoAsync(dataVencimento);

            // Verifica se há dados retornados
            if (loteProduto.Rows.Count > 0) {
                List<LoteProduto> listaLotes = new List<LoteProduto>();

                foreach (DataRow row in loteProduto.Rows) {
                    listaLotes.Add(new LoteProduto {
                        NomeProduto = row["Nome_Produto"].ToString(),
                        Quantidade = Convert.ToInt32(row["Quantidade"]),
                        DataCadastro = Convert.ToDateTime(row["Data_Cadastro"]).ToShortDateString(),
                        DataVencimento = Convert.ToDateTime(row["Data_Vencimento"]).ToShortDateString(),
                        IntercorrenciaLote = row["Intercorrencia_Lote"].ToString()
                    });
                }

                // Exibe os resultados na interface
                LotesCollectionView.ItemsSource = listaLotes;
            }
            else {
                await DisplayAlert("Aviso", "Nenhum lote encontrado com a data de vencimento informada.", "OK");
            }
        }
        catch (Exception ex) {
            await DisplayAlert("Erro", "Erro ao buscar dados: " + ex.Message, "OK");
        }
    }


}
