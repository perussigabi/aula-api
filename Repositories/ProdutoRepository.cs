// Importa o namespace que contém a classe ConexaoBanco.
using LojaApi.Data;

// Importa o namespace que contém a entidade Produto.
using LojaApi.Entities;

// Importa as classes utilizadas para acessar o PostgreSQL.
using Npgsql;

// Define o namespace da classe.
namespace LojaApi.Repositories;

// Declara o Repository responsável pelas consultas de produtos.
public class ProdutoRepository
{
    /*
        Armazena uma referência para a classe de conexão.

        readonly impede que o atributo receba
        outro objeto depois do construtor.
    */
    private readonly ConexaoBanco _conexaoBanco;

    /*
        Construtor da classe ProdutoRepository.

        O objeto ConexaoBanco será fornecido
        automaticamente pela injeção de dependência.
    */
    public ProdutoRepository(ConexaoBanco conexaoBanco)
    {
        _conexaoBanco = conexaoBanco;
    }

    /*
        Método público e assíncrono responsável
        por listar os produtos ativos.

        Task<List<Produto>> representa uma operação
        que devolverá uma lista de produtos.
    */
    public async Task<List<Produto>> ListarAtivosAsync()
    {
        /*
            Cria a lista que armazenará
            os produtos encontrados no banco.
        */
        List<Produto> produtos = new();

        /*
            Solicita uma conexão aberta
            com o PostgreSQL.

            await aguarda a abertura da conexão.

            await using garante que os recursos
            sejam liberados ao final da operação.
        */
        await using NpgsqlConnection conexao =
            await _conexaoBanco.CriarConexaoAsync();

        /*
            Comando SQL responsável por buscar
            os produtos cadastrados.

            WHERE ativo = TRUE retorna somente
            os produtos que estão ativos.

            ORDER BY organiza os resultados
            pelo nome do produto.
        */
        string sql = """
            SELECT
                id_produto,
                nome,
                descricao,
                preco,
                estoque,
                ativo
            FROM produto
            WHERE ativo = TRUE
            ORDER BY nome;
            """;

        /*
            Cria o comando SQL que será
            executado utilizando a conexão.
        */
        await using NpgsqlCommand comando =
            new NpgsqlCommand(sql, conexao);

        /*
            Executa o SELECT e obtém
            um leitor dos resultados retornados.
        */
        await using NpgsqlDataReader leitor =
            await comando.ExecuteReaderAsync();

        /*
            Obtém a posição das colunas
            pelo nome.

            Isso facilita a leitura dos dados
            retornados pelo PostgreSQL.
        */
        int colunaIdProduto =
            leitor.GetOrdinal("id_produto");

        int colunaNome =
            leitor.GetOrdinal("nome");

        int colunaDescricao =
            leitor.GetOrdinal("descricao");

        int colunaPreco =
            leitor.GetOrdinal("preco");

        int colunaEstoque =
            leitor.GetOrdinal("estoque");

        int colunaAtivo =
            leitor.GetOrdinal("ativo");

        /*
            Percorre todas as linhas
            retornadas pela consulta.

            ReadAsync avança para a próxima linha
            enquanto existirem resultados.
        */
        while (await leitor.ReadAsync())
        {
            /*
                Cria um objeto Produto
                utilizando os dados encontrados
                na linha atual do banco.
            */
            Produto produto = new Produto(
                leitor.GetInt32(colunaIdProduto),
                leitor.GetString(colunaNome),
                leitor.GetString(colunaDescricao),
                leitor.GetDecimal(colunaPreco),
                leitor.GetInt32(colunaEstoque),
                leitor.GetBoolean(colunaAtivo)
            );

            /*
                Adiciona o produto criado
                à lista de resultados.
            */
            produtos.Add(produto);
        }

        /*
            Devolve a lista de produtos ativos
            para a classe que chamou o Repository.
        */
        return produtos;
    }
}