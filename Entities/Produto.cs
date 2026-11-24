namespace LojaApi.Entities;

/*
    Representa um produto cadastrado na aplicação.

    A entidade Produto concentra os dados principais
    de um produto e protege seu estado por meio
    de propriedades com validação.

    Essa classe não executa comandos SQL
    e não acessa diretamente o banco de dados.
*/
public class Produto
{
    /*
        Armazena o identificador do produto.
    */
    private int _idProduto;

    /*
        Armazena o nome do produto.
    */
    private string _nome = "";

    /*
        Armazena a descrição do produto.
    */
    private string _descricao = "";

    /*
        Armazena o preço do produto.
    */
    private decimal _preco;

    /*
        Armazena a quantidade disponível
        do produto no estoque.
    */
    private int _estoque;

    /*
        Indica se o produto está ativo.
    */
    private bool _ativo;

    /*
        Construtor utilizado para criar
        um objeto Produto com dados
        que já existem no banco.

        Esse construtor será utilizado
        principalmente pelo Repository
        durante a leitura dos registros.
    */
    public Produto(
        int idProduto,
        string nome,
        string descricao,
        decimal preco,
        int estoque,
        bool ativo)
    {
        this.IdProduto = idProduto;
        this.Nome = nome;
        this.Descricao = descricao;
        this.Preco = preco;
        this.Estoque = estoque;
        this.Ativo = ativo;
    }

    /*
        Propriedade responsável pelo
        identificador do produto.

        O private set impede que outras classes
        alterem diretamente esse valor.
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
                Impede que o produto receba
                um identificador inválido.
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
        Propriedade responsável pelo nome
        do produto.
    */
    public string Nome
    {
        get
        {
            return _nome;
        }

        private set
        {
            /*
                Impede que o nome seja nulo,
                vazio ou formado apenas por espaços.
            */
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "O nome do produto não pode ficar vazio."
                );
            }

            /*
                Remove espaços desnecessários
                do início e do final do nome.
            */
            _nome = value.Trim();
        }
    }

    /*
        Propriedade responsável pela
        descrição do produto.
    */
    public string Descricao
    {
        get
        {
            return _descricao;
        }

        private set
        {
            /*
                Impede que a descrição seja nula.
                Uma descrição vazia é permitida.
            */
            if (value is null)
            {
                throw new ArgumentException(
                    "A descrição não pode ser nula."
                );
            }

            _descricao = value.Trim();
        }
    }

    /*
        Propriedade responsável pelo preço
        do produto.
    */
    public decimal Preco
    {
        get
        {
            return _preco;
        }

        private set
        {
            /*
                Impede que o produto possua
                preço igual ou menor que zero.
            */
            if (value <= 0)
            {
                throw new ArgumentException(
                    "O preço deve ser maior que zero."
                );
            }

            _preco = value;
        }
    }

    /*
        Propriedade responsável pela
        quantidade disponível em estoque.
    */
    public int Estoque
    {
        get
        {
            return _estoque;
        }

        private set
        {
            /*
                Impede que o estoque
                possua uma quantidade negativa.
            */
            if (value < 0)
            {
                throw new ArgumentException(
                    "O estoque não pode ser negativo."
                );
            }

            _estoque = value;
        }
    }

    /*
        Propriedade que indica se o produto
        está ativo ou inativo.

        true  -> produto ativo.
        false -> produto inativo.
    */
    public bool Ativo
    {
        get
        {
            return _ativo;
        }

        private set
        {
            _ativo = value;
        }
    }
}