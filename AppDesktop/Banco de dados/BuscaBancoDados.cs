using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

public class BuscaBancoDados {
    // String de conexão com o banco de dados
    private readonly string connStr = "server=localhost;user=root;database=sys_agrotech;port=3306;password=AGROtech78@%24";


    public async Task<DataTable> BuscarVendaPorNumeroAsync(string numeroVenda) {
        DataTable dataTable = new DataTable();
        string query = "SELECT * FROM venda WHERE Nm_Venda = @numeroVenda";  // Ajuste conforme sua tabela

        using (MySqlConnection conn = new MySqlConnection(connStr)) {
            using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                cmd.Parameters.AddWithValue("@numeroVenda", numeroVenda);

                try {
                    await conn.OpenAsync();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd)) {
                        adapter.Fill(dataTable);
                    }
                }
                catch (Exception ex) {
                    throw new Exception("Erro na busca pelo número da venda: " + ex.Message);
                }
            }
        }
        return dataTable;
    }


    // Método para buscar dados com base em data inicial e final
    public async Task<DataTable> BuscarVendasPorDataAsync(string dataInicial, string dataFinal) {
        DataTable resultado = new DataTable();

        try {
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();

                // Verificar e formatar datas corretamente
                DateTime dtInicial = DateTime.Parse(dataInicial);
                DateTime dtFinal = DateTime.Parse(dataFinal);

                string sql = @"SELECT id_Venda, Nm_Venda, data_Venda, hora_Venda, Vendedor, Nm_Cliente, cpf_cnpj_cliente 
               FROM venda 
               WHERE data_Venda BETWEEN @DataInicial AND @DataFinal";


                using (MySqlCommand cmd = new MySqlCommand(sql, conn)) {
                    cmd.Parameters.AddWithValue("@DataInicial", dtInicial.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@DataFinal", dtFinal.ToString("yyyy-MM-dd"));

                    // Usar DbDataReader e fazer cast para MySqlDataReader
                    using (DbDataReader reader = await cmd.ExecuteReaderAsync()) {
                        // Convertendo explicitamente para MySqlDataReader
                        MySqlDataReader mySqlReader = reader as MySqlDataReader;
                        if (mySqlReader != null) {
                            resultado.Load(mySqlReader);
                        }
                        else {
                            // Caso o cast falhe, exibe um erro ou faz algum tratamento
                            Console.WriteLine("Erro: O reader não é um MySqlDataReader.");
                        }
                    }
                }
            }
        }
        catch (Exception ex) {
            // Exibe a mensagem completa do erro
            Console.WriteLine("Erro ao buscar vendas: " + ex.ToString());
        }

        return resultado;
    }

    public async Task<DataTable> BuscarVendasPorClienteAsync(string nomeCliente) {
        DataTable dataTable = new DataTable();
        string query = "SELECT * FROM venda WHERE Nm_Cliente LIKE @nomeCliente";  // Ajuste o nome da tabela conforme necessário

        using (MySqlConnection conn = new MySqlConnection(connStr)) {
            using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                cmd.Parameters.AddWithValue("@nomeCliente", "%" + nomeCliente + "%");  // Busca parcial (LIKE)

                try {
                    await conn.OpenAsync();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd)) {
                        adapter.Fill(dataTable);
                    }
                }
                catch (Exception ex) {
                    throw new Exception("Erro na busca pelo cliente: " + ex.Message);
                }
            }
        }
        return dataTable;
    }

    public async Task<DataTable> BuscarVendasPorCpfCnpjAsync(string cpfCnpj) {
        DataTable dataTable = new DataTable();
        string query = "SELECT * FROM venda WHERE cpf_cnpj_cliente = @cpfCnpj";  // Ajuste o nome da coluna conforme necessário

        using (MySqlConnection conn = new MySqlConnection(connStr)) {
            using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                cmd.Parameters.AddWithValue("@cpfCnpj", cpfCnpj);

                try {
                    await conn.OpenAsync();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd)) {
                        adapter.Fill(dataTable);
                    }
                }
                catch (Exception ex) {
                    throw new Exception("Erro na busca por CPF ou CNPJ: " + ex.Message);
                }
            }
        }
        return dataTable;
    }


    public async Task<DataTable> BuscarLotesPorNumeroLoteAsync(string numeroLote) {
        try {
            // Comando SQL para buscar as informações de produtos e lotes
            string query = @"
            SELECT 
                p.Nome_Produto, 
                p.Unidade_Medida, 
                p.Valor, 
                l.N_Lote, 
                l.Data_Cadastro, 
                l.Quantidade, 
                l.Data_Vencimento, 
                l.Intercorrencia_Lote
            FROM 
                Lotes l
            INNER JOIN 
                Produtos p ON l.ID_Produto = p.ID_Produto
            WHERE 
                l.N_Lote = @NumeroLote";

            // Definindo a conexão e comando
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                    cmd.Parameters.AddWithValue("@NumeroLote", numeroLote);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                        DataTable dataTable = new DataTable();
                        await Task.Run(() => da.Fill(dataTable));
                        return dataTable;
                    }
                }
            }
        }
        catch (Exception ex) {
            // Trate exceções de banco de dados aqui
            throw new Exception("Erro ao buscar dados: " + ex.Message);
        }
    }

    public async Task<DataTable> BuscarLotesPorDataIntervaloAsync(DateTime dataInicial, DateTime dataFinal) {
        try {
            // Comando SQL para buscar as informações de produtos e lotes dentro do intervalo de datas
            string query = @"
        SELECT 
            p.Nome_Produto, 
            p.Unidade_Medida, 
            p.Valor, 
            l.N_Lote, 
            l.Data_Cadastro, 
            l.Quantidade, 
            l.Data_Vencimento, 
            l.Intercorrencia_Lote
        FROM 
            Lotes l
        INNER JOIN 
            Produtos p ON l.ID_Produto = p.ID_Produto
        WHERE 
            l.Data_Cadastro BETWEEN @DataInicial AND @DataFinal";

            // Definindo a conexão e comando
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                    cmd.Parameters.AddWithValue("@DataInicial", dataInicial);
                    cmd.Parameters.AddWithValue("@DataFinal", dataFinal);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                        DataTable dataTable = new DataTable();
                        await Task.Run(() => da.Fill(dataTable));
                        return dataTable;
                    }
                }
            }
        }
        catch (Exception ex) {
            // Trate exceções de banco de dados aqui
            throw new Exception("Erro ao buscar dados: " + ex.Message);
        }
    }

    public async Task<DataTable> BuscarLotesPorNomeProdutoAsync(string nomeProduto) {
        try {
            // Comando SQL para buscar as informações de produtos e lotes baseado no nome do produto
            string query = @"
        SELECT 
            p.Nome_Produto, 
            p.Unidade_Medida, 
            p.Valor, 
            l.N_Lote, 
            l.Data_Cadastro, 
            l.Quantidade, 
            l.Data_Vencimento, 
            l.Intercorrencia_Lote
        FROM 
            Lotes l
        INNER JOIN 
            Produtos p ON l.ID_Produto = p.ID_Produto
        WHERE 
            p.Nome_Produto LIKE @NomeProduto";

            // Definindo a conexão e comando
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                    cmd.Parameters.AddWithValue("@NomeProduto", "%" + nomeProduto + "%");

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                        DataTable dataTable = new DataTable();
                        await Task.Run(() => da.Fill(dataTable));
                        return dataTable;
                    }
                }
            }
        }
        catch (Exception ex) {
            // Trate exceções de banco de dados aqui
            throw new Exception("Erro ao buscar dados: " + ex.Message);
        }
    }


    public async Task<DataTable> BuscarLotesPorDataVencimentoAsync(DateTime dataVencimento) {
        try {
            // Comando SQL para buscar as informações de produtos e lotes com data de vencimento
            string query = @"
        SELECT 
            p.Nome_Produto, 
            p.Unidade_Medida, 
            p.Valor, 
            l.N_Lote, 
            l.Data_Cadastro, 
            l.Quantidade, 
            l.Data_Vencimento, 
            l.Intercorrencia_Lote
        FROM 
            Lotes l
        INNER JOIN 
            Produtos p ON l.ID_Produto = p.ID_Produto
        WHERE 
            l.Data_Vencimento = @DataVencimento";

            // Definindo a conexão e comando
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                    cmd.Parameters.AddWithValue("@DataVencimento", dataVencimento);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                        DataTable dataTable = new DataTable();
                        await Task.Run(() => da.Fill(dataTable));
                        return dataTable;
                    }
                }
            }
        }
        catch (Exception ex) {
            // Trate exceções de banco de dados aqui
            throw new Exception("Erro ao buscar dados: " + ex.Message);
        }
    }

    public async Task<DataTable> BuscarLotesPorNumeroLoteInsumoAsync(string numeroLote) {
        try {
            // Comando SQL para buscar as informações de produtos e lotes
            string query = @"
            SELECT 
                p.Nome_Produto, 
                p.Unidade_Medida, 
                p.Valor_Insumo, 
                l.N_Lote, 
                l.Data_Cadastro, 
                l.Quantidade, 
                l.Data_Vencimento, 
                l.Intercorrencia_Lote
            FROM 
                Lotes_Insumos l
            INNER JOIN 
                Insumos p ON l.ID_Insumo = p.ID_Insumo
            WHERE 
                l.N_Lote = @NumeroLote";

            // Definindo a conexão e comando
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                    cmd.Parameters.AddWithValue("@NumeroLote", numeroLote);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                        DataTable dataTable = new DataTable();
                        await Task.Run(() => da.Fill(dataTable));
                        return dataTable;
                    }
                }
            }
        }
        catch (Exception ex) {
            // Trate exceções de banco de dados aqui
            throw new Exception("Erro ao buscar dados: " + ex.Message);
        }
    }

    public async Task<DataTable> BuscarLotesPorDataInsumoIntervaloAsync(DateTime dataInicial, DateTime dataFinal) {
        try {
            // Comando SQL para buscar as informações de produtos e lotes dentro do intervalo de datas
            string query = @"
        SELECT 
            p.Nome_Produto, 
            p.Unidade_Medida, 
            p.Valor_Insumo, 
            l.N_Lote, 
            l.Data_Cadastro, 
            l.Quantidade, 
            l.Data_Vencimento, 
            l.Intercorrencia_Lote
        FROM 
            Lotes_Insumos l
        INNER JOIN 
            Insumos p ON l.ID_Insumo = p.ID_Insumo
        WHERE 
            l.Data_Cadastro BETWEEN @DataInicial AND @DataFinal";

            // Definindo a conexão e comando
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                    cmd.Parameters.AddWithValue("@DataInicial", dataInicial);
                    cmd.Parameters.AddWithValue("@DataFinal", dataFinal);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                        DataTable dataTable = new DataTable();
                        await Task.Run(() => da.Fill(dataTable));
                        return dataTable;
                    }
                }
            }
        }
        catch (Exception ex) {
            // Trate exceções de banco de dados aqui
            throw new Exception("Erro ao buscar dados: " + ex.Message);
        }
    }

    public async Task<DataTable> BuscarLotesPorNomeInsumoAsync(string nomeProduto) {
        try {
            // Comando SQL para buscar as informações de produtos e lotes baseado no nome do produto
            string query = @"
        SELECT 
            p.Nome_Produto, 
            p.Unidade_Medida, 
            p.Valor_Insumo, 
            l.N_Lote, 
            l.Data_Cadastro, 
            l.Quantidade, 
            l.Data_Vencimento, 
            l.Intercorrencia_Lote
        FROM 
            Lotes_Insumos l
        INNER JOIN 
            Insumos p ON l.ID_Insumo = p.ID_Insumo
        WHERE 
            p.Nome_Produto LIKE @NomeProduto";

            // Definindo a conexão e comando
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                    cmd.Parameters.AddWithValue("@NomeProduto", "%" + nomeProduto + "%");

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                        DataTable dataTable = new DataTable();
                        await Task.Run(() => da.Fill(dataTable));
                        return dataTable;
                    }
                }
            }
        }
        catch (Exception ex) {
            // Trate exceções de banco de dados aqui
            throw new Exception("Erro ao buscar dados: " + ex.Message);
        }
    }

    public async Task<DataTable> BuscarLotesPorDataVencimentoInsumoAsync(DateTime dataVencimento) {
        try {
            // Comando SQL para buscar as informações de produtos e lotes com data de vencimento
            string query = @"
        SELECT 
            p.Nome_Produto, 
            p.Unidade_Medida, 
            p.Valor_Insumo, 
            l.N_Lote, 
            l.Data_Cadastro, 
            l.Quantidade, 
            l.Data_Vencimento, 
            l.Intercorrencia_Lote
        FROM 
            Lotes_Insumos l
        INNER JOIN 
            Insumos p ON l.ID_Insumo = p.ID_Insumo
        WHERE 
            l.Data_Vencimento = @DataVencimento";

            // Definindo a conexão e comando
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                    cmd.Parameters.AddWithValue("@DataVencimento", dataVencimento);

                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                        DataTable dataTable = new DataTable();
                        await Task.Run(() => da.Fill(dataTable));
                        return dataTable;
                    }
                }
            }
        }
        catch (Exception ex) {
            // Trate exceções de banco de dados aqui
            throw new Exception("Erro ao buscar dados: " + ex.Message);
        }
    }

    public async Task<DataTable> BuscarUsuariosPorCpfAsync(string cpf) {
        DataTable resultado = new DataTable();

        try {
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();

                string sql = @"SELECT idUsuario, nome, cpf, nome_usuario 
                           FROM usuario
                           WHERE cpf = @Cpf";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn)) {
                    cmd.Parameters.AddWithValue("@Cpf", cpf);

                    using (DbDataReader reader = await cmd.ExecuteReaderAsync()) {
                        MySqlDataReader mySqlReader = reader as MySqlDataReader;
                        if (mySqlReader != null) {
                            resultado.Load(mySqlReader);
                        }
                        else {
                            Console.WriteLine("Erro: O reader não é um MySqlDataReader.");
                        }
                    }
                }
            }
        }
        catch (Exception ex) {
            Console.WriteLine("Erro ao buscar usuários: " + ex.ToString());
        }

        return resultado;
    }

    public async Task<DataTable> BuscarUsuariosPorNomeAsync(string nome) {
        DataTable resultado = new DataTable();

        try {
            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();

                // Consulta para buscar usuários pelo nome
                string sql = @"SELECT idUsuario, nome, cpf, nome_usuario
                           FROM usuario
                           WHERE nome LIKE @Nome";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn)) {
                    // Usar o operador LIKE para buscar nomes semelhantes
                    cmd.Parameters.AddWithValue("@Nome", "%" + nome + "%");

                    using (DbDataReader reader = await cmd.ExecuteReaderAsync()) {
                        MySqlDataReader mySqlReader = reader as MySqlDataReader;
                        if (mySqlReader != null) {
                            resultado.Load(mySqlReader);
                        }
                        else {
                            Console.WriteLine("Erro: O reader não é um MySqlDataReader.");
                        }
                    }
                }
            }
        }
        catch (Exception ex) {
            Console.WriteLine("Erro ao buscar usuários: " + ex.ToString());
        }

        return resultado;
    }

    public async Task<DataTable> BuscarClientesPorCnpjAsync(string cnpj) {
        try {
            // Consulta SQL que faz o JOIN entre as tabelas cliente, cliente_pf e cliente_pj.
            string query = @"
            SELECT 
                c.id_cliente, 
                c.tipo_cliente, 
                c.nome_cliente, 
                c.rua, 
                c.numero, 
                c.complemento, 
                c.CEP, 
                c.bairro, 
                c.estado, 
                c.cidade, 
                cpf.cpf, 
                cpf.rg_cliente, 
                cpf.data_nascimento, 
                cpf.genero_cliente, 
                pj.cnpj, 
                pj.razao_social, 
                pj.nome_responsavel, 
                c.nm_cadastro_cliente
            FROM 
                cliente c
            LEFT JOIN cliente_pf cpf ON c.id_cliente = cpf.id_cliente
            LEFT JOIN cliente_pj pj ON c.id_cliente = pj.id_cliente
            WHERE 
                pj.cnpj = @Cnpj OR cpf.cpf = @Cnpj";

            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                    // Adiciona o CNPJ como parâmetro na consulta SQL
                    cmd.Parameters.AddWithValue("@Cnpj", cnpj);

                    // Executa a consulta e preenche o DataTable com os resultados
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                        DataTable dataTable = new DataTable();
                        await Task.Run(() => da.Fill(dataTable));
                        return dataTable;
                    }
                }
            }
        }
        catch (Exception ex) {
            // Exceção caso ocorra algum erro durante a consulta
            throw new Exception("Erro ao buscar dados de clientes: " + ex.Message);
        }
    }

    public async Task<DataTable> BuscarClientesPorNomeAsync(string nomeCliente) {
        try {
            // Consulta SQL para buscar clientes pelo nome
            string query = @"
            SELECT 
                c.id_cliente, 
                c.tipo_cliente, 
                c.nome_cliente, 
                c.rua, 
                c.numero, 
                c.complemento, 
                c.CEP, 
                c.bairro, 
                c.estado, 
                c.cidade, 
                cpf.cpf, 
                cpf.rg_cliente, 
                cpf.data_nascimento, 
                cpf.genero_cliente, 
                pj.cnpj, 
                pj.razao_social, 
                pj.nome_responsavel, 
                c.nm_cadastro_cliente
            FROM 
                cliente c
            LEFT JOIN cliente_pf cpf ON c.id_cliente = cpf.id_cliente
            LEFT JOIN cliente_pj pj ON c.id_cliente = pj.id_cliente
            WHERE 
                c.nome_cliente LIKE @NomeCliente";

            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                    // Adiciona o nome do cliente como parâmetro na consulta SQL
                    cmd.Parameters.AddWithValue("@NomeCliente", "%" + nomeCliente + "%");

                    // Executa a consulta e preenche o DataTable com os resultados
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                        DataTable dataTable = new DataTable();
                        await Task.Run(() => da.Fill(dataTable));
                        return dataTable;
                    }
                }
            }
        }
        catch (Exception ex) {
            throw new Exception("Erro ao buscar dados de clientes: " + ex.Message);
        }
    }

    public async Task<DataTable> BuscarClientesPorTipoAsync(string tipoCliente) {
        try {
            // Consulta SQL para buscar clientes pelo tipo
            string query = @"
            SELECT 
                c.id_cliente, 
                c.tipo_cliente, 
                c.nome_cliente, 
                c.rua, 
                c.numero, 
                c.complemento, 
                c.CEP, 
                c.bairro, 
                c.estado, 
                c.cidade, 
                cpf.cpf, 
                cpf.rg_cliente, 
                cpf.data_nascimento, 
                cpf.genero_cliente, 
                pj.cnpj, 
                pj.razao_social, 
                pj.nome_responsavel, 
                c.nm_cadastro_cliente
            FROM 
                cliente c
            LEFT JOIN cliente_pf cpf ON c.id_cliente = cpf.id_cliente
            LEFT JOIN cliente_pj pj ON c.id_cliente = pj.id_cliente
            WHERE 
                c.tipo_cliente = @TipoCliente";

            using (MySqlConnection conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();
                using (MySqlCommand cmd = new MySqlCommand(query, conn)) {
                    // Adiciona o tipo de cliente como parâmetro na consulta SQL
                    cmd.Parameters.AddWithValue("@TipoCliente", tipoCliente);

                    // Executa a consulta e preenche o DataTable com os resultados
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
                        DataTable dataTable = new DataTable();
                        await Task.Run(() => da.Fill(dataTable));
                        return dataTable;
                    }
                }
            }
        }
        catch (Exception ex) {
            throw new Exception("Erro ao buscar dados de clientes: " + ex.Message);
        }
    }


}
