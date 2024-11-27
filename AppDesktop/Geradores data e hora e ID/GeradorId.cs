using System;
using System.Threading.Tasks;

public class GeradorId
{
    // Dependência para acessar o banco de dados
    private readonly DataAccess dataAccess;

    // Construtor que recebe a instância de DataAccess
    public GeradorId(DataAccess dataAccess)
    {
        this.dataAccess = dataAccess;
    }

    // Método para gerar um novo ID aleatório e verificar no banco
    public async Task<int> GerarNovoIdAsync()
    {
        int randomId = 0;
        bool idExists = true;

        // Cria uma nova instância de Random
        Random random = new Random();

        // Gera um número aleatório e verifica se ele já existe no banco
        while (idExists)
        {
            // Gera um número aleatório de 6 dígitos
            randomId = random.Next(100000, 999999); // Gera um número entre 100000 e 999999

            // Verifica no banco de dados se o ID já existe
            idExists = await dataAccess.VerificarIdClienteExistenteAsync(randomId.ToString());
        }

        return randomId;
    }

    // Método para armazenar o ID temporariamente (implemente a lógica conforme necessário)
    public void ArmazenarIdTemporariamente(int id)
    {
        // Implemente a lógica de armazenamento temporário, se necessário
    }
}
