using System.Globalization;

public class FramesPaginaVendas {
   
    private int currentRedLineIndex = 0;
    private readonly List<VendaItem> vendas = new List<VendaItem>();

    public IReadOnlyList<VendaItem> Vendas => vendas.AsReadOnly(); // Apenas leitura pública

   
    public void AdicionarVenda(string produto, string quantidade, string precoUnitario, string desconto, string valorTotal)
    {
        // Converte os valores para decimal utilizando a função ConvertToDecimal
        decimal precoUnitarioDecimal = ConvertToDecimal(precoUnitario);
        decimal descontoDecimal = ConvertToDecimal(desconto);
        decimal valorTotalDecimal = ConvertToDecimal(valorTotal);

        // Adiciona a venda com os valores convertidos para decimal
        vendas.Add(new VendaItem
        {
            Produto = produto,
            Quantidade = quantidade,
            PrecoUnitario = precoUnitarioDecimal,  // Armazenando como decimal
            Desconto = descontoDecimal,            // Armazenando como decimal
            ValorTotal = valorTotalDecimal         // Armazenando como decimal
        });
    }


    // Método para criar um StackLayout com o texto fornecido
    public StackLayout CriarStackLayout(string texto) {
        return new StackLayout {
            Orientation = StackOrientation.Horizontal,
            Children = {
                new Label { Text = texto, WidthRequest = 100 }
            }
        };
    }

    // Método para criar um Frame com o layout e a cor de fundo (cinza ou branco)
    public Frame CriarFrame(StackLayout layout, bool isLightGray) {
        return new Frame {
            Content = layout,
            BackgroundColor = isLightGray ? Colors.LightGray : Colors.White,
            BorderColor = Colors.Gainsboro,
            CornerRadius = 0,
            Padding = new Thickness(5, 2),
            Margin = new Thickness(0, 0)
        };
    }

    private decimal ConvertToDecimal(string valor)
    {
        // Verifica se o valor está vazio ou nulo
        if (string.IsNullOrWhiteSpace(valor))
        {
            return 0; // Retorna 0 caso o valor esteja vazio
        }

        // Substitui vírgula por ponto para garantir formato adequado para números
        valor = valor.Replace(",", ".");

        // Tenta converter a string para decimal
        if (decimal.TryParse(valor, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
        {
            return result;
        }
        else
        {
            return 0; // Retorna 0 caso a conversão falhe
        }
    }

    // Método para limpar a lista de vendas
    public void LimparVendas() {
        vendas.Clear();
    }


    // Método para adicionar Frames à página de vendas com comportamento interativo
    public void AdicionarFramesPaginaVendas(
    StackLayout productStackLayout,
    StackLayout linhaQuantidade,
    StackLayout linhaPrecoUn,
    StackLayout linhaDesconto,
    StackLayout linhaValorTotal,
    string produtoDigitado,
    string quantidadeDigitado,
    string precoUnDigitado,
    string descontoDigitado,
    string valorTotalDigitado)
    {
        // Formata os valores para o banco de dados
        precoUnDigitado = FormatarParaBancoDeDados(precoUnDigitado);
        descontoDigitado = FormatarParaBancoDeDados(descontoDigitado);
        valorTotalDigitado = FormatarParaBancoDeDados(valorTotalDigitado);

        // Converte os valores para decimal
        decimal precoUnitario = ConvertToDecimal(precoUnDigitado);
        decimal desconto = ConvertToDecimal(descontoDigitado);
        decimal valorTotal = ConvertToDecimal(valorTotalDigitado);

        // Adiciona a venda com os valores convertidos
        AdicionarVenda(produtoDigitado, quantidadeDigitado, precoUnitario.ToString(), desconto.ToString(), valorTotal.ToString());

        // Código de criação dos Frames permanece o mesmo
        bool isRed = productStackLayout.Children.Count == currentRedLineIndex;

        var productFrame = CriarFrame(CriarStackLayout(produtoDigitado), isRed);
        var quantidadeFrame = CriarFrame(CriarStackLayout(quantidadeDigitado), isRed);
        var precoUnFrame = CriarFrame(CriarStackLayout(precoUnDigitado), isRed);
        var descontoFrame = CriarFrame(CriarStackLayout(descontoDigitado), isRed);
        var valorTotalFrame = CriarFrame(CriarStackLayout(valorTotalDigitado), isRed);

        productStackLayout.Children.Add(productFrame);
        linhaQuantidade.Children.Add(quantidadeFrame);
        linhaPrecoUn.Children.Add(precoUnFrame);
        linhaDesconto.Children.Add(descontoFrame);
        linhaValorTotal.Children.Add(valorTotalFrame);
    }

    // Função para remover o separador de milhar e garantir ponto como separador decimal
    private string FormatarParaBancoDeDados(string valor)
    {
        // Remove qualquer ponto (separador de milhar) e substitui a vírgula por ponto (separador decimal)
        valor = valor.Replace(".", "").Replace(",", ".");

        return valor;
    }



}
