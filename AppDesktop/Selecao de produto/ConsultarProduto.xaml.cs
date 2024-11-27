using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;

namespace Agrotech
{
    public partial class ConsultarProduto : ContentPage
    {
        private readonly DataAccess dataAccess = new DataAccess();

        public ConsultarProduto()
        {
            InitializeComponent();
            LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                var produtos = await dataAccess.GetAllProdutosAsync();
                foreach (var produto in produtos)
                {
                    AddProductFrame(produto);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao carregar produtos: " + ex.Message);
            }
        }

        private void AddProductFrame(Produto produto)
        {
            Frame productFrame = new Frame
            {
                BackgroundColor = Color.FromRgb(255, 255, 255), // Branco usando FromRgb
                CornerRadius = 15,
                HasShadow = true,
                Padding = 15,
                Margin = 10
            };

            // Defina o BindingContext do frame para o produto
            productFrame.BindingContext = produto; // Adicione esta linha

            var tapGestureRecognizer = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 2
            };
            tapGestureRecognizer.Tapped += OnFrameDoubleTapped;

            productFrame.GestureRecognizers.Add(tapGestureRecognizer);

            var stackLayout = new VerticalStackLayout
            {
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
                Text = $"Pre�o: R$ {produto.Valor}",
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
            ProductsStackLayout.Children.Add(productFrame);
        }


       

        public event Action<Produto> ProdutoSelecionado;

        private void OnFrameDoubleTapped(object sender, EventArgs e)
        {
            DisplayAlert("Teste", "O evento foi acionado.", "OK");

            if (sender is Frame frame)
            {
                var context = frame.BindingContext;
                if (context is Produto produto)
                {
                    ProdutoSelecionado?.Invoke(produto); // Dispara o evento com o produto selecionado
                    Produto.ID = produto.ID_Produto;
                    frame.BackgroundColor = Color.FromRgb(211, 211, 211);
                    DisplayAlert("Produto Selecionado", $"Produto com ID {produto.ID_Produto} foi selecionado.", "OK");
                }
                else
                {
                    DisplayAlert("Erro", "O BindingContext n�o � um Produto.", "OK");
                    Console.WriteLine($"Tipo do BindingContext: {context?.GetType().Name ?? "nulo"}");
                }
            }
        }

    }
}
