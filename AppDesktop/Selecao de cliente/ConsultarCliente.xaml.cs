using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using MySql.Data.MySqlClient;

namespace Agrotech
{
    public partial class ConsultarCliente : ContentPage
    {
        private readonly DataAccess dataAccess = new DataAccess();

        // Evento para notificar quando um cliente for selecionado
        public event Action<Cliente> ClienteSelecionado;

        public ConsultarCliente()
        {
            InitializeComponent();
            LoadClientesAsync();
        }

        private async Task LoadClientesAsync()
        {
            try
            {
                var clientes = await dataAccess.GetAllClientesAsync();
                if (clientes != null && clientes.Count > 0)
                {
                    foreach (var cliente in clientes)
                    {
                        AddClientFrame(cliente);
                    }
                }
                else
                {
                    DisplayAlert("Erro", "Nenhum cliente encontrado.", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao carregar clientes: " + ex.Message);
            }
        }

        private void AddClientFrame(Cliente cliente)
        {
            Frame clientFrame = new Frame
            {
                BackgroundColor = Color.FromRgb(255, 255, 255),
                CornerRadius = 15,
                HasShadow = true,
                Padding = 15,
                Margin = 10
            };

            clientFrame.BindingContext = cliente;

            var tapGestureRecognizer = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 2
            };
            tapGestureRecognizer.Tapped += OnFrameDoubleTapped;

            clientFrame.GestureRecognizers.Add(tapGestureRecognizer);

            var stackLayout = new VerticalStackLayout
            {
                Children =
                {
                    new Label
                    {
                        Text = $"ID: {cliente.ID_Cliente}",
                        FontSize = 14,
                        TextColor = Colors.Gray
                    },
                    new Label
                    {
                        Text = $"Nome do Cliente: {cliente.Nome_Cliente}",
                        FontSize = 16,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Colors.Black
                    },
                    new Label
                    {
                        Text = $"CPF/CNPJ: {cliente.CpfOuCnpj}",
                        FontSize = 14,
                        TextColor = Colors.Gray
                    },
                    new Label
                    {
                        Text = $"Tipo de Cliente: {cliente.TipoCliente}",
                        FontSize = 14,
                        TextColor = Colors.Gray
                    }
                }
            };

            clientFrame.Content = stackLayout;
            ClientesStackLayout.Children.Add(clientFrame);
        }

        private void OnFrameDoubleTapped(object sender, EventArgs e)
        {
            if (sender is Frame frame)
            {
                var context = frame.BindingContext;
                if (context is Cliente cliente)
                {
                    // Dispara o evento para passar o cliente selecionado
                    ClienteSelecionado?.Invoke(cliente);
                    frame.BackgroundColor = Color.FromRgb(211, 211, 211);
                    DisplayAlert("Cliente Selecionado", $"Cliente com ID {cliente.ID_Cliente} foi selecionado.", "OK");
                }
            }
        }
    }
}
