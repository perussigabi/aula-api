namespace LojaApi.Model;

/* 
    Representa uma venda realizada pela aplicação.

    A entidade Venda concentra os dados principais
    da operação de venda e protege o estado do objeto
    por meio de propriedades com validação.

    Ela também mantém em memória a lista de itens
    associados à venda.

    Essa classe não executa comandos SQL
    e não acessa diretamente o banco de dados.
*/
public class Venda
{
/* 
    Atributo privado responsável por armazenar
    o identificador da venda.

    O acesso acontece por meio da propriedade IdVenda.
*/
    private int _idVenda;

/* 
    Armazena a data e o horário
    em que a venda foi registrada.
*/
    private DateTime _dataHora;

/* 
    Armazena o valor total da venda.
*/
    private decimal _valorTotal;

/* 
    Indica se a venda está ativa.

    Esse campo será utilizado posteriormente
    para permitir a ocultação lógica da venda
    sem apagar seu histórico do banco.
*/
    private bool _ativa;

/* 
    Armazena os itens associados à venda.

    List<ItemVenda> permite manter vários itens
    dentro de um único objeto Venda.

    readonly impede que a variável _itens
    seja substituída por outra lista
    depois da criação do objeto.

    Entretanto, os elementos da lista
    ainda podem ser adicionados normalmente.
*/
    private readonly List<ItemVenda> _itens = new();

/* 
    Construtor utilizado para criar
    uma nova venda dentro da aplicação.

    Nesse momento, o identificador ainda
    não é informado porque será gerado
    automaticamente pelo PostgreSQL.

    A data e o horário são definidos
    com o momento atual da aplicação.

    A venda inicia como ativa.

    O valor total recebido já deve ter sido
    calculado anteriormente pelo Service.
*/
    public Venda(decimal valorTotal)
    {
        this.DataHora = DateTime.Now;
        this.ValorTotal = valorTotal;
        this.Ativa = true;
    }

/* 
    Construtor utilizado para criar
    um objeto Venda com dados
    que já existem no banco.

    Esse construtor é usado principalmente
    pelo Repository durante a leitura
    dos registros do PostgreSQL.

    Nesse caso, todos os dados já existem:
    - identificador;
    - data e horário;
    - valor total;
    - situação da venda.
*/
    public Venda(
        int idVenda,
        DateTime dataHora,
        decimal valorTotal,
        bool ativa)
    {
        this.IdVenda = idVenda;
        this.DataHora = dataHora;
        this.ValorTotal = valorTotal;
        this.Ativa = ativa;
    }

/* 
    Propriedade responsável pelo identificador
    da venda.

    O get permite consultar o valor.

    O private set impede que outras classes
    alterem o identificador diretamente.

    A própria entidade controla
    quando esse valor pode ser definido.
*/
    public int IdVenda
    {
        get
        {
            return _idVenda;
        }

        private set
        {
/* 
    Impede que o objeto receba
    um identificador negativo.

    O valor 0 é permitido porque pode representar
    uma venda criada em memória
    que ainda não foi persistida no banco.

    Valores negativos não são considerados válidos
    para o identificador da venda.
*/
            if (value < 0)
            {
                throw new ArgumentException(
                    "O identificador não pode ser negativo."
                );
            }

            _idVenda = value;
        }
    }

/* 
    Propriedade responsável pela data
    e horário da venda.

    O private set impede alterações diretas
    por outras classes.
*/
    public DateTime DataHora
    {
        get
        {
            return _dataHora;
        }

        private set
        {
/* 
    Impede que a venda seja criada
    com uma data muito à frente
    do horário atual da aplicação.

    DateTime.Now representa
    a data e o horário atuais.

    AddMinutes(1) cria uma tolerância
    de um minuto.

    Essa pequena margem evita erros
    causados por diferenças mínimas
    de tempo durante a execução.
*/
            if (value > DateTime.Now.AddMinutes(1))
            {
                throw new ArgumentException(
                    "A data da venda não pode ser futura."
                );
            }

            _dataHora = value;
        }
    }

/* 
    Propriedade responsável por armazenar
    o valor total da venda.

    O valor somente pode ser alterado
    internamente pela própria entidade.
*/
    public decimal ValorTotal
    {
        get
        {
            return _valorTotal;
        }

        private set
        {
/* 
    Impede que uma venda possua
    valor total igual ou menor que zero.

    Essa validação protege o estado
    do objeto Venda.

    O cálculo do valor total acontece
    anteriormente no Service.
*/
            if (value <= 0)
            {
                throw new ArgumentException(
                    "O total deve ser maior que zero."
                );
            }

            _valorTotal = value;
        }
    }

/* 
    Propriedade que indica
    se a venda está ativa.

    true  → venda ativa.
    false → venda ocultada.

    O private set impede que outras classes
    alterem diretamente esse estado.
*/
    public bool Ativa
    {
        get
        {
            return _ativa;
        }

        private set
        {
            _ativa = value;
        }
    }

/* 
    Disponibiliza os itens pertencentes
    à venda apenas para consulta.

    IReadOnlyList<ItemVenda> permite:
    - percorrer os itens;
    - consultar os elementos.

    Entretanto, não disponibiliza diretamente
    métodos como Add ou Remove.

    AsReadOnly cria uma visão somente de leitura
    da lista interna.

    Dessa forma, outras classes não conseguem
    alterar diretamente a coleção _itens.
*/
    public IReadOnlyList<ItemVenda> Itens
    {
        get
        {
            return _itens.AsReadOnly();
        }
    }

/* 
    Método responsável por adicionar
    um ItemVenda ao objeto Venda.

    Ele é utilizado principalmente
    quando o Repository está montando
    o objeto em memória após uma consulta.

    Esse método não executa INSERT
    e não altera o banco de dados.

    Ele apenas associa um objeto ItemVenda
    à venda que já está sendo construída.
*/
    public void AdicionarItem(ItemVenda item)
    {
/* 
    Verifica se o objeto recebido
    é uma referência nula.

    Uma venda não deve possuir
    um item inexistente.

    ArgumentNullException indica especificamente
    que um argumento obrigatório foi recebido
    com valor null.

    nameof(item) informa o nome
    do parâmetro relacionado ao erro.
*/
        if (item is null)
        {
            throw new ArgumentNullException(
                nameof(item),
                "O item da venda não pode ser nulo."
            );
        }

/* 
    Adiciona o ItemVenda
    à lista interna da venda.

    Como _itens é private,
    essa alteração acontece de forma controlada
    por meio deste método.
*/
        _itens.Add(item);
    }
}