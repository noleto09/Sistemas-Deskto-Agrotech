using Org.BouncyCastle.Asn1.X509;
using System.Data;

namespace Agrotech.Usuarios_cadastrados;

public partial class UsuariosCadastrados : ContentPage
{
	public UsuariosCadastrados()
	{
		InitializeComponent();
	}

    private async void Button_Pesquisaporcpf(object sender, EventArgs e) {
        // Verifica se o CPF foi preenchido
        if (string.IsNullOrWhiteSpace(Cpfusuario.Text)) {
            await DisplayAlert("Erro", "Por favor, informe o CPF.", "OK");
            return;
        }

        try {
            // Obtém o CPF informado
            string cpf = Cpfusuario.Text;

            // Cria a instância da classe de busca no banco de dados
            BuscaBancoDados buscaBanco = new BuscaBancoDados();

            // Chama o método para buscar os dados de usuário pelo CPF
            DataTable resultado = await buscaBanco.BuscarUsuariosPorCpfAsync(cpf);

            // Verifica se há dados retornados
            if (resultado.Rows.Count > 0) {
                List<Usuario> listaUsuarios = new List<Usuario>();

                // Percorre os resultados e os adiciona à lista
                foreach (DataRow row in resultado.Rows) {
                    listaUsuarios.Add(new Usuario {
                        Nome = row["nome"].ToString(),
                        Cpf = row["cpf"].ToString(),
                        NomeUsuario = row["nome_usuario"].ToString()
                    });
                }

                // Exibe os resultados na interface
                UsuariosCollectionView.ItemsSource = listaUsuarios;
            }
            else {
                await DisplayAlert("Aviso", "Nenhum usuário encontrado com o CPF informado.", "OK");
            }
        }
        catch (Exception ex) {
            await DisplayAlert("Erro", "Erro ao buscar dados: " + ex.Message, "OK");
        }
    }

    private async void Button_PequisaPorNome(object sender, EventArgs e) {
        // Verifica se o nome foi preenchido
        if (string.IsNullOrWhiteSpace(Nomedousario.Text)) {
            await DisplayAlert("Erro", "Por favor, informe o nome.", "OK");
            return;
        }

        try {
            // Obtém o nome informado
            string nome = Nomedousario.Text;

            // Cria a instância da classe de busca no banco de dados
            BuscaBancoDados buscaBanco = new BuscaBancoDados();

            // Chama o método para buscar os dados de usuário pelo nome
            DataTable resultado = await buscaBanco.BuscarUsuariosPorNomeAsync(nome);

            // Verifica se há dados retornados
            if (resultado.Rows.Count > 0) {
                List<Usuario> listaUsuarios = new List<Usuario>();

                // Percorre os resultados e os adiciona à lista
                foreach (DataRow row in resultado.Rows) {
                    listaUsuarios.Add(new Usuario {
                        Nome = row["nome"].ToString(),
                        Cpf = row["cpf"].ToString(),
                        NomeUsuario = row["nome_usuario"].ToString()
                    });
                }

                // Exibe os resultados na interface
                UsuariosCollectionView.ItemsSource = listaUsuarios;
            }
            else {
                await DisplayAlert("Aviso", "Nenhum usuário encontrado com o nome informado.", "OK");
            }
        }
        catch (Exception ex) {
            await DisplayAlert("Erro", "Erro ao buscar dados: " + ex.Message, "OK");
        }
    }


}