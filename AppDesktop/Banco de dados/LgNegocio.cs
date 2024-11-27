using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

public class DataAccess {
    private readonly string connStr = "server=localhost;user=root;database=sys_agrotech;port=3306;password=AGROtech78@%24";

    public async Task<bool> InserirUsuarioAsync(string nome, string cpf, string nomeUsuario, string senha) {
        try {
            using (var conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();

                string sql = "INSERT INTO usuario (nome, cpf, nome_usuario, senha_usuario) VALUES (@nome, @cpf, @nome_usuario, @senha)";
                using (var cmd = new MySqlCommand(sql, conn)) {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                    cmd.Parameters.AddWithValue("@nome_usuario", nomeUsuario);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return true;
        } catch (Exception ex) {
            // Logar o erro ou exibir uma mensagem de erro
            Console.WriteLine("Erro: " + ex.Message);
            return false;
        }
    }

    public async Task<bool> VerificarIdClienteExistenteAsync(string id) {
        try {
            using (var conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();

                string sql = "SELECT COUNT(*) FROM cliente WHERE Id_cliente = @id_cliente";
                using (var cmd = new MySqlCommand(sql, conn)) {
                    cmd.Parameters.AddWithValue("@id_cliente", id);

                    var result = await cmd.ExecuteScalarAsync();
                    return Convert.ToInt32(result) > 0;
                }
            }
        } catch (Exception ex) {
            // Logar o erro ou exibir uma mensagem de erro
            Console.WriteLine("Erro: " + ex.Message);
            return false;
        }
    }


    public async Task<bool> InserirClienteAsync(string cl_id, string tipoCliente, string cl_nome, string cl_cpf_cnpj, string cl_rg, string cl_data_nascimento, string genero, string Ed_cliente_rua, string Ed_cliente_numero,
    string Ed_cliente_complemento, string Ed_cliente_cep, string Ed_cliente_bairro, string Ed_cliente_estado, string Ed_cliente_cidade) {
        try {
            using (var conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();

                // Iniciar uma transação para garantir a consistência dos dados
                using (var transaction = await conn.BeginTransactionAsync()) {
                    try {
                        // Primeiro, inserir os dados na tabela `cliente`
                        string sqlCliente = "INSERT INTO cliente (tipo_cliente, nome_cliente, rua, numero, complemento, CEP, bairro, estado, cidade, nm_cadastro_cliente) " +
                                            "VALUES (@tipocliente, @nome, @ed_rua, @ed_numero, @ed_complementar, @ed_cep, @ed_bairro, @ed_estado, @ed_cidade, @nm_cadastro_cliente)";

                        long idCliente;
                        using (var cmdCliente = new MySqlCommand(sqlCliente, conn, transaction)) {
                            cmdCliente.Parameters.AddWithValue("@tipocliente", tipoCliente);
                            cmdCliente.Parameters.AddWithValue("@nome", cl_nome);
                            cmdCliente.Parameters.AddWithValue("@ed_rua", Ed_cliente_rua);
                            cmdCliente.Parameters.AddWithValue("@ed_numero", Ed_cliente_numero);
                            cmdCliente.Parameters.AddWithValue("@ed_complementar", Ed_cliente_complemento);
                            cmdCliente.Parameters.AddWithValue("@ed_cep", Ed_cliente_cep);
                            cmdCliente.Parameters.AddWithValue("@ed_bairro", Ed_cliente_bairro);
                            cmdCliente.Parameters.AddWithValue("@ed_estado", Ed_cliente_estado);
                            cmdCliente.Parameters.AddWithValue("@ed_cidade", Ed_cliente_cidade);
                            cmdCliente.Parameters.AddWithValue("@nm_cadastro_cliente", cl_id);

                            // Executar a inserção e obter o ID gerado
                            await cmdCliente.ExecuteNonQueryAsync();
                            idCliente = cmdCliente.LastInsertedId;
                        }

                        // Verificar o tipo de cliente e inserir na tabela correspondente
                        if (tipoCliente == "Pessoa Física") {
                            string sqlPF = "INSERT INTO cliente_pf (id_cliente, cpf, rg_cliente, data_nascimento, genero_cliente) " +
                                           "VALUES (@id_cliente, @cpf, @rg, @data_nascimento, @genero)";

                            using (var cmdPF = new MySqlCommand(sqlPF, conn, transaction)) {
                                cmdPF.Parameters.AddWithValue("@id_cliente", idCliente);
                                cmdPF.Parameters.AddWithValue("@cpf", cl_cpf_cnpj);
                                cmdPF.Parameters.AddWithValue("@rg", cl_rg);
                                cmdPF.Parameters.AddWithValue("@data_nascimento", cl_data_nascimento);
                                cmdPF.Parameters.AddWithValue("@genero", genero);

                                await cmdPF.ExecuteNonQueryAsync();
                            }
                        } else if (tipoCliente == "Pessoa Jurídica") {
                            string sqlPJ = "INSERT INTO cliente_pj (id_cliente, cnpj, razao_social) " +
                                           "VALUES (@id_cliente, @cnpj, @razao_social)";

                            using (var cmdPJ = new MySqlCommand(sqlPJ, conn, transaction)) {
                                cmdPJ.Parameters.AddWithValue("@id_cliente", idCliente);
                                cmdPJ.Parameters.AddWithValue("@cnpj", cl_cpf_cnpj);
                                cmdPJ.Parameters.AddWithValue("@razao_social", cl_nome);

                                await cmdPJ.ExecuteNonQueryAsync();
                            }
                        }

                        // Confirmar a transação
                        await transaction.CommitAsync();
                    } catch (Exception) {
                        // Em caso de erro, reverter a transação
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            return true;
        } catch (Exception ex) {
            // Logar o erro ou exibir uma mensagem de erro
            Console.WriteLine("Erro: " + ex.Message);
            return false;
        }
    }

    public async Task<bool> InserirProdutoAsync(string nomeProduto, string unidade, decimal valorProduto)
    {
        try
        {
            using (var conn = new MySqlConnection(connStr))
            {
                await conn.OpenAsync();

                string sql = "INSERT INTO Produtos (Nome_Produto, Unidade_Medida, Valor) VALUES (@nomeProduto, @unidade, @valorProduto)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nomeProduto", nomeProduto);
                    cmd.Parameters.AddWithValue("@unidade", unidade);
                    cmd.Parameters.AddWithValue("@valorProduto", valorProduto);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
            return false;
        }
    }

    public async Task<bool> InserirLoteAsync(int idProduto, int nLote, int quantidade, DateTime dataCadastro, DateTime dataVencimento, string intercorrencia)
    {
        try
        {
            using (var conn = new MySqlConnection(connStr))
            {
                await conn.OpenAsync();
                string sql = "INSERT INTO Lotes (ID_Produto, N_Lote, Data_Cadastro, Quantidade, Data_Vencimento, Intercorrencia_lote) " +
                    "VALUES (@idProduto, @nLote, @dataCadastro, @quantidade, @dataVencimento, @intercorrencia)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idProduto", idProduto);
                    cmd.Parameters.AddWithValue("@nLote", nLote);
                    cmd.Parameters.AddWithValue("@quantidade", quantidade);
                    cmd.Parameters.AddWithValue("@dataCadastro", dataCadastro);
                    cmd.Parameters.AddWithValue("@dataVencimento", dataVencimento);
                    cmd.Parameters.AddWithValue("@intercorrencia", intercorrencia);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro ao inserir lote: " + ex.Message);
            return false;
        }
    }

    public async Task<bool> InserirInsumoAsync(string nomeInsumo, string unidadeInsumo, decimal valorInsumo) {
        try {
            using (var conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();

                string sql = "INSERT INTO Insumos (Nome_Produto, Unidade_Medida, Valor_Insumo) VALUES (@nomeProduto, @unidade, @valorInsumo)";
                using (var cmd = new MySqlCommand(sql, conn)) {
                    cmd.Parameters.AddWithValue("@nomeProduto", nomeInsumo);
                    cmd.Parameters.AddWithValue("@unidade", unidadeInsumo);
                    cmd.Parameters.AddWithValue("@valorInsumo", valorInsumo);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return true;
        }
        catch (Exception ex) {
            Console.WriteLine("Erro: " + ex.Message);
            return false;
        }
    }

    public async Task<List<Insumos>> GetAllInsumosAsync() {
        var produtos = new List<Insumos>();
        try {
            using (var conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();

                string sql = "SELECT ID_Insumo, Nome_Produto, Unidade_Medida, Valor_Insumo FROM Insumos";
                using (var cmd = new MySqlCommand(sql, conn)) {
                    using (var reader = await cmd.ExecuteReaderAsync()) {
                        while (await reader.ReadAsync()) {
                            produtos.Add(new Insumos {
                                ID_Produto = reader.GetInt32("ID_Insumo"),
                                Nome_Produto = reader.GetString("Nome_Produto"),
                                Unidade_Medida = reader.GetString("Unidade_Medida"),
                                Valor_Insumo = reader.GetDecimal("Valor_Insumo")
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex) {
            Console.WriteLine("Erro: " + ex.Message);
        }
        return produtos;
    }

    public async Task<List<Produto>> GetAllProdutosAsync()
    {
        var produtos = new List<Produto>();
        try
        {
            using (var conn = new MySqlConnection(connStr))
            {
                await conn.OpenAsync();

                string sql = "SELECT ID_Produto, Nome_Produto, Unidade_Medida, Valor FROM Produtos";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            produtos.Add(new Produto
                            {
                                ID_Produto = reader.GetInt32("ID_Produto"),
                                Nome_Produto = reader.GetString("Nome_Produto"),
                                Unidade_Medida = reader.GetString("Unidade_Medida"),
                                Valor = reader.GetDecimal("Valor")
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
        }
        return produtos;
    }

    public async Task<bool> InserirLoteInsumoAsync(int idProduto, int nLoteInsumo, int quantidadeInsumo, DateTime dataCadastroInsumo, DateTime dataVencimentoInsumo, string intercorrenciaInsumo) {
        try {
            using (var conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();
                string sql = "INSERT INTO Lotes_Insumos (ID_Insumo, N_Lote, Data_Cadastro, Quantidade, Data_Vencimento, Intercorrencia_lote) " +
                    "VALUES (@idProduto, @nLote, @dataCadastro, @quantidade, @dataVencimento, @intercorrencia)";
                using (var cmd = new MySqlCommand(sql, conn)) {
                    cmd.Parameters.AddWithValue("@idProduto", idProduto);
                    cmd.Parameters.AddWithValue("@nLote", nLoteInsumo);
                    cmd.Parameters.AddWithValue("@quantidade", quantidadeInsumo);
                    cmd.Parameters.AddWithValue("@dataCadastro", dataCadastroInsumo);
                    cmd.Parameters.AddWithValue("@dataVencimento", dataVencimentoInsumo);
                    cmd.Parameters.AddWithValue("@intercorrencia", intercorrenciaInsumo);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return true;
        }
        catch (Exception ex) {
            Console.WriteLine("Erro ao inserir lote: " + ex.Message);
            return false;
        }
    }

    public async Task<List<Cliente>> GetAllClientesAsync()
    {
        var clientes = new List<Cliente>();
        try
        {
            using (var conn = new MySqlConnection(connStr))
            {
                await conn.OpenAsync();

                // Consulta para obter informações básicas de clientes, incluindo o tipo de cliente
                string sql = @"
                SELECT 
                    cliente.id_cliente AS ID_Cliente,
                    cliente.nome_cliente AS Nome_Cliente,
                    cliente.tipo_cliente AS TipoCliente,
                    COALESCE(cliente_pf.cpf, cliente_pj.cnpj) AS CpfOuCnpj
                FROM cliente
                LEFT JOIN cliente_pf ON cliente.id_cliente = cliente_pf.id_cliente
                LEFT JOIN cliente_pj ON cliente.id_cliente = cliente_pj.id_cliente";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            clientes.Add(new Cliente
                            {
                                ID_Cliente = reader.GetInt32("ID_Cliente"),
                                Nome_Cliente = reader.GetString("Nome_Cliente"),
                                TipoCliente = reader.GetString("TipoCliente"),
                                CpfOuCnpj = reader.GetString("CpfOuCnpj")
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro: " + ex.Message);
        }
        return clientes;
    }


    public async Task<int> InserirVendaAsync(int numeroVenda, DateTime dataVenda, TimeSpan horaVenda, string vendedor, string nomeCliente, string cpfCnpjCliente) {
        try {
            using (var conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();
                string sql = @"
                INSERT INTO venda (Nm_Venda, data_Venda, hora_Venda, Vendedor, Nm_Cliente, cpf_cnpj_cliente) 
                VALUES (@numeroVenda, @dataVenda, @horaVenda, @vendedor, @nomeCliente, @cpfCnpjCliente);
                SELECT LAST_INSERT_ID();";

                using (var cmd = new MySqlCommand(sql, conn)) {
                    cmd.Parameters.AddWithValue("@numeroVenda", numeroVenda);
                    cmd.Parameters.AddWithValue("@dataVenda", dataVenda);
                    cmd.Parameters.AddWithValue("@horaVenda", horaVenda);
                    cmd.Parameters.AddWithValue("@vendedor", vendedor);
                    cmd.Parameters.AddWithValue("@nomeCliente", nomeCliente);
                    cmd.Parameters.AddWithValue("@cpfCnpjCliente", cpfCnpjCliente);

                    var idVenda = await cmd.ExecuteScalarAsync();
                    return Convert.ToInt32(idVenda);
                }
            }
        }
        catch (Exception ex) {
            Console.WriteLine("Erro ao inserir venda: " + ex.Message);
            return -1; // Retorna -1 em caso de erro
        }
    }

    public async Task<bool> InserirItensVendaAsync(int idVenda, string tipoProduto, string quantidadeSaida, decimal valorUn, decimal desconto, decimal valorTotal) {
        try {
            using (var conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();
                string sql = @"
                INSERT INTO item_venda (id_Venda, tipo_Produto, quantidade_Saida, valor_Un, desconto, valor_Total) 
                VALUES (@idVenda, @tipoProduto, @quantidadeSaida, @valorUn, @desconto, @valorTotal)";

                using (var cmd = new MySqlCommand(sql, conn)) {
                    cmd.Parameters.AddWithValue("@idVenda", idVenda);
                    cmd.Parameters.AddWithValue("@tipoProduto", tipoProduto);
                    cmd.Parameters.AddWithValue("@quantidadeSaida", quantidadeSaida);
                    cmd.Parameters.AddWithValue("@valorUn", valorUn);
                    cmd.Parameters.AddWithValue("@desconto", desconto);
                    cmd.Parameters.AddWithValue("@valorTotal", valorTotal);

                    await cmd.ExecuteNonQueryAsync();
                    return true; // Retorna true se a execução foi bem-sucedida
                }
            }
        }
        catch (Exception ex) {
            Console.WriteLine("Erro ao inserir item de venda: " + ex.Message);
            return false; // Retorna false em caso de erro
        }
    }




    public async Task<string> VerificarUsuarioAsync(string nomeUsuario, string senha)
    {
        try
        {
            using (var conn = new MySqlConnection(connStr))
            {
                await conn.OpenAsync();

                string sql = "SELECT nome_usuario FROM usuario WHERE nome_usuario = @nome_usuario AND senha_usuario = @senha";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome_usuario", nomeUsuario);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    var result = await cmd.ExecuteScalarAsync();
                    return result?.ToString(); // Retorna o nome do usuário ou null se não encontrado
                }
            }
        }
        catch (Exception ex)
        {
            // Logar o erro ou exibir uma mensagem de erro
            Console.WriteLine("Erro: " + ex.Message);
            return null; // Retorna null em caso de erro
        }
    }

}
