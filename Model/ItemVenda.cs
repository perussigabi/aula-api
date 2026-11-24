namespace LojaApi.Model;

/* 
    Representa um item pertencente a uma venda.

    Cada objeto ItemVenda corresponde
    a um produto incluído em uma venda,
    juntamente com:
    - o identificador do item;
    - o identificador do produto;
    - o nome do produto;
    - a quantidade vendida;
    - o valor unitário utilizado na venda.

    Essa entidade representa os dados
    de um item já registrado.

    Ela não executa comandos SQL
    e não acessa diretamente o banco de dados.
*/
public class ItemVenda
{
/* 
    Armazena o identificador
    do item da venda.
*/
    private int _idItemVenda;

/* 
    Armazena o identificador
    do produto associado ao item.
*/
    private int _idProduto;

/* 
    Armazena o nome do produto.

    A string é inicializada com ""
    para evitar que o atributo
    comece com valor nulo.
*/
    private string _nomeProduto = "";

/* 
    Armazena a quantidade
    vendida daquele produto.
*/
    private int _quantidade;

/* 
    Armazena o valor unitário
    utilizado no momento da venda.

    Esse valor representa o preço
    registrado na venda,
    e não necessariamente o preço atual
    do produto no cadastro.
*/
    private decimal _valorUnitario;

/* 
    Construtor utilizado para criar
    um objeto ItemVenda com dados
    que já existem no banco.

    Esse construtor é usado principalmente
    pelo Repository durante a leitura
    dos registros do PostgreSQL.

    São recebidos:
    - identificador do item;
    - identificador do produto;
    - nome do produto;
    - quantidade vendida;
    - valor unitário utilizado na venda.
*/
    public ItemVenda(
        int idItemVenda,
        int idProduto,
        string nomeProduto,
        int quantidade,
        decimal valorUnitario)
    {
        this.IdItemVenda = idItemVenda;
        this.IdProduto = idProduto;
        this.NomeProduto = nomeProduto;
        this.Quantidade = quantidade;
        this.ValorUnitario = valorUnitario;
    }

/* 
    Propriedade responsável
    pelo identificador do item.

    O get permite consultar o valor.

    O private set impede que outras classes
    alterem diretamente esse identificador.
*/
    public int IdItemVenda
    {
        get
        {
            return _idItemVenda;
        }

        private set
        {
/* 
    Um item carregado do banco
    deve possuir um identificador válido.

    Como o id_item_venda é gerado
    pelo PostgreSQL,
    esperamos um valor maior que zero.

    Valores iguais a zero ou negativos
    são considerados inválidos.
*/
            if (value <= 0)
            {
                throw new ArgumentException(
                    "O identificador do item deve ser maior que zero."
                );
            }

            _idItemVenda = value;
        }
    }

/* 
    Propriedade responsável
    pelo identificador do produto
    associado ao item da venda.
*/
    public int IdProduto
    {
        get
        {
            return _idProduto;
        }

        private set
        {
/* 
    Impede que o item seja associado
    a um identificador de produto inválido.

    O valor deve ser maior que zero.
*/
            if (value <= 0)
            {
                throw new ArgumentException(
                    "O identificador do produto deve ser maior que zero."
                );
            }

            _idProduto = value;
        }
    }

/* 
    Propriedade responsável
    pelo nome do produto.

    O nome é utilizado principalmente
    para apresentar os dados da venda
    de forma mais compreensível.
*/
    public string NomeProduto
    {
        get
        {
            return _nomeProduto;
        }

        private set
        {
/* 
    Impede que o item possua
    um nome vazio, nulo
    ou formado apenas por espaços.

    IsNullOrWhiteSpace verifica
    exatamente essas situações.
*/
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "O nome do produto não pode ficar vazio."
                );
            }

/* 
    Trim remove espaços em branco
    do início e do final do texto
    antes de armazená-lo.
*/
            _nomeProduto = value.Trim();
        }
    }

/* 
    Propriedade responsável
    pela quantidade vendida
    daquele produto.
*/
    public int Quantidade
    {
        get
        {
            return _quantidade;
        }

        private set
        {
/* 
    A quantidade vendida deve
    ser sempre maior que zero.

    Valores iguais a zero ou negativos
    não representam uma quantidade válida.
*/
            if (value <= 0)
            {
                throw new ArgumentException(
                    "A quantidade deve ser maior que zero."
                );
            }

            _quantidade = value;
        }
    }

/* 
    Propriedade responsável
    pelo valor unitário utilizado
    no item da venda.

    O private set impede alterações
    diretas por outras classes.
*/
    public decimal ValorUnitario
    {
        get
        {
            return _valorUnitario;
        }

        private set
        {
/* 
    Impede que o item possua
    valor unitário igual ou menor que zero.

    Esse valor representa o preço
    utilizado no momento da venda.
*/
            if (value <= 0)
            {
                throw new ArgumentException(
                    "O valor unitário deve ser maior que zero."
                );
            }

            _valorUnitario = value;
        }
    }

/* 
    Propriedade calculada responsável por obter o valor total do item.

    Ela não possui atributo privado e não possui set porque seu valor
    depende diretamente de:
    - Quantidade;
    - ValorUnitario.

    O cálculo realizado é:

    quantidade × valor unitário

    Exemplo:
    quantidade = 3
    valor unitário = 50

    valor total do item = 150

    O cálculo utiliza o preço registrado
    no item da venda, preservando
    o valor utilizado naquele momento,
    mesmo que o preço atual do produto mude.
*/
    public decimal ValorTotalItem
    {
        get
        {
            return Quantidade * ValorUnitario;
        }
    }
}