using System.Collections.Generic;

/// <summary>
/// Entidade que gere e armazena a informação sobre que utilizador tem acesso a que server
/// requisitos
/// Relacionamentos
/// Relaciona com a entidade User. Um Server pode ser acedido por 0 ou muitos Users e um User pode aceder a 0 ou muitos servers. Este relacionamento dá origem a uma 3ªentidade com o nome de manager que tem como objetivo gerir e armazenar dados sobre quem está no server e se pode aceder ao server 
/// </summary>
public class Server {

    #region Properties

    /// <summary> PK
    /// numero de identificação do objecto
    /// requisitos :
    /// deve conter pelo menos um numero
    /// restrições :
    ///  não pode ser um numero racional
    /// </summary>
    private int id;
	public int Id
	{
		get 
		{
			return id;
		}
		set 
		{
			id = value;
		}
	}

	/// <summary>
	/// descrição do UserType
	/// requisitos
	///  deve conter uma descrição simples
	/// restrições
	///  não pode ser vazio
	/// </summary>
	private string descri;
	public string Descri 
	{
		get
		{
			return descri;
		}
		set 
		{
			descri = value;
		}
	}

	/// <summary>
	/// variavel que guarda todas as observações necessárias
	/// requisitos
	/// deve ser descritivo
	/// restrições
	///  não pode ter mais de 200 carcateres
	/// </summary>
	private string obs;
	public string Obs 
	{
		get 
		{
			return obs;
		}
		set 
		{
			obs = value;
		}
	}

	/// <summary>
	/// lista de ServerUsers
	/// </summary>
	private List<ServerUser> listaServerUsers;
	public List<ServerUser> ListaServerUsers { get => listaServerUsers; set => listaServerUsers = value; }

	#endregion

	#region Constructor

	/// <summary>
	/// Construtor identitário
	/// </summary>
	/// <param name="id"> numero de identificação do objecto </param>
	public Server(int id)
	{
		Id = id;
	}

	/// <summary>
	/// Construtor completo
	/// </summary>
	/// <param name="id"> numero de identificação do objecto </param>
	/// <param name="descri"> descrição do UserType </param>
	/// <param name="obs"> variavel que guarda todas as observações necessárias </param>
	public Server(int id, string descri, string obs)
	{
		Id = id;
		Descri = descri;
		Obs = obs;
	}

	#endregion

}

