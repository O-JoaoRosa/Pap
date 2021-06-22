using System.Collections.Generic;

/// <summary>
/// esta entidade destina-se a controlar os estados do utilizador com o server
/// Ativo
/// suspenso
/// banido
/// relacionamentos:
/// Relacionamento com a entidade ServerUser. Um estado pode ter 0 ou muitos ServerUsers e um ServerUser tem que ter 1 ServerUserState.
/// </summary>
public class ServerUserState {

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
	/// descrição do Car
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
	/// Lista de ServerUsers
	/// </summary>
	private List<ServerUser> listaServerUsers;
	public List<ServerUser> ListaServerUsers { get => listaServerUsers; set => listaServerUsers = value; }

	#endregion

	#region Constructor

	/// <summary>
	/// Construtor completo
	/// </summary>
	/// <param name="id">numero de identificação do objecto</param>
	/// <param name="descri">descrição do Car</param>
	public ServerUserState(int id, string descri)
	{
		Id = id;
		Descri = descri;
	}

	/// <summary>
	/// Construtor identitário
	/// </summary>
	/// <param name="id">numero de identificação do objecto</param>
	public ServerUserState(int id)
	{
		Id = id;
	}

	#endregion

}
