// Importa o namespace que contém a entidade Produto.
using LojaApi.Entities;

// Importa o namespace que contém o ProdutoRepository.
using LojaApi.Repositories;

// Define o namespace da classe.
namespace LojaApi.Services;

// Declara a classe responsável pelos casos de uso
// relacionados aos produtos.
public class ProdutoService
{
    /*
        Armazena uma referência para o ProdutoRepository.

        private impede o acesso direto por outras classes.

        readonly impede que o atributo receba
        outro objeto depois do construtor.
    */
    private readonly ProdutoRepository _produtoRepository;

    /*
        Construtor da classe ProdutoService.

        O ASP.NET Core fornecerá automaticamente
        um objeto ProdutoRepository por meio
        da injeção de dependência.
    */
    public ProdutoService(ProdutoRepository produtoRepository)
    {
        /*
            Armazena o Repository recebido
            no atributo privado.
        */
        _produtoRepository = produtoRepository;
    }

    /*
        Método público e assíncrono responsável
        pelo caso de uso de listar produtos ativos.

        Task indica que o método executará
        uma operação assíncrona.

        List<Produto> indica que será devolvida
        uma lista de objetos Produto.
    */
    public async Task<List<Produto>> ListarAtivosAsync()
    {
        /*
            Chama o método ListarAtivosAsync()
            existente no ProdutoRepository.

            O Repository acessará o PostgreSQL,
            executará o SELECT e montará a lista.

            await aguarda o término da consulta
            sem bloquear a aplicação.

            return devolve a lista recebida
            para quem chamou o Service.
        */
        return await _produtoRepository.ListarAtivosAsync();
    }
}