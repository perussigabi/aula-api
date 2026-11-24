// Importa a entidade Produto.
using LojaApi.Entities;

// Importa o ProdutoService.
using LojaApi.Services;

// Importa os recursos utilizados
// para criar Controllers e rotas HTTP.
using Microsoft.AspNetCore.Mvc;

// Define o namespace da classe.
namespace LojaApi.Controllers;

/*
    Indica que a classe é um Controller de API.

    O atributo permite que o ASP.NET Core
    aplique comportamentos específicos
    para requisições HTTP.
*/
[ApiController]

/*
    Define o endereço principal das rotas.

    Todas as rotas desta classe começarão
    com api/produtos.
*/
[Route("api/produtos")]
public class ProdutosController : ControllerBase
{
    /*
        Armazena uma referência para o ProdutoService.

        O Controller utiliza o Service
        para acessar as operações de produtos.
    */
    private readonly ProdutoService _produtoService;

    /*
        Construtor da classe ProdutosController.

        O ASP.NET Core fornecerá automaticamente
        o ProdutoService por injeção de dependência.
    */
    public ProdutosController(ProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    /*
        Define uma rota HTTP GET.

        Essa rota será responsável por listar
        os produtos ativos cadastrados no banco.
    */
    [HttpGet]
    public async Task<ActionResult<List<Produto>>> Listar()
    {
        /*
            Solicita ao Service a lista
            de produtos ativos.

            await aguarda a conclusão
            da operação assíncrona.
        */
        List<Produto> produtos =
            await _produtoService.ListarAtivosAsync();

        /*
            Ok retorna o código HTTP 200,
            indicando que a requisição foi realizada
            com sucesso.

            A lista de produtos é enviada
            no corpo da resposta em formato JSON.
        */
        return Ok(produtos);
    }
}