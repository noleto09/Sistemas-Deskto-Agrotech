namespace Agrotech;

public partial class SelecionarInsumo : ContentPage
{
    private readonly DataAccess dataAccess = new DataAccess();
    public SelecionarInsumo()
	{
		InitializeComponent();
        LoadInsumoAsync();
    }

    private async Task LoadInsumoAsync() {
        try {
            var produtos = await dataAccess.GetAllInsumosAsync();
            foreach (var produto in produtos) {
                AddProductFrame(produto);
            }
        }
        catch (Exception ex) {
            Console.WriteLine("Erro ao carregar produtos: " + ex.Message);
        }
    }

    private void AddProductFrame(Insumos produto) {
        Frame productFrame = new Frame {
            BackgroundColor = Color.FromRgb(255, 255, 255), // Branco usando FromRgb
            CornerRadius = 15,
            HasShadow = true,
            Padding = 15,
            Margin = 10
        };

        // Defina o BindingContext do frame para o produto
        productFrame.BindingContext = produto; // Adicione esta linha

        var tapGestureRecognizer = new TapGestureRecognizer {
            NumberOfTapsRequired = 2
        };
        tapGestureRecognizer.Tapped += OnFrameDoubleTapped;

        productFrame.GestureRecognizers.Add(tapGestureRecognizer);

        var stackLayout = new VerticalStackLayout {
            Children =
    {
            new Label
            {
                Text = $"ID: {produto.ID_Produto}",
                FontSize = 14,
                TextColor = Colors.Gray
            },
            new Label
            {
                Text = $"Nome do Produto: {produto.Nome_Produto}",
                FontSize = 16,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.Black
            },
            new Label
            {
                Text = $"Preço: R$ {produto.Valor_Insumo}",
                FontSize = 14,
                TextColor = Colors.Gray
            },
            new Label
            {
                Text = $"Unidade de Medida: {produto.Unidade_Medida}",
                FontSize = 14,
                TextColor = Colors.Gray
            }
        }
        };

        productFrame.Content = stackLayout;
        InsumoStackLayout.Children.Add(productFrame);
    }




    public event Action<Insumos> InsumoSelecionado;

    private void OnFrameDoubleTapped(object sender, EventArgs e) {
        DisplayAlert("Teste", "O evento foi acionado.", "OK");

        if (sender is Frame frame) {
            var context = frame.BindingContext;
            if (context is Insumos insumo) {
                InsumoSelecionado?.Invoke(insumo); // Dispara o evento com o produto selecionado
                Insumos.ID = insumo.ID_Produto;
                frame.BackgroundColor = Color.FromRgb(211, 211, 211);
                DisplayAlert("Produto Selecionado", $"Produto com ID {insumo.ID_Produto} foi selecionado.", "OK");
            }
            else {
                DisplayAlert("Erro", "O BindingContext não é um Produto.", "OK");
                Console.WriteLine($"Tipo do BindingContext: {context?.GetType().Name ?? "nulo"}");
            }
        }
    }
}