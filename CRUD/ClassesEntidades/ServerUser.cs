using System;

/// <summary>
/// 3ª entidade que surgio do relacionamento do User com a entidade Server, tem como objetivo gerir e armazenar as permições de acesso que cada utilizadro tem
/// Relacionamentos
/// Relacionamento com o User. Um Manager pode gerir 0 ou muitos Users e um user pode ser gerido por 1 manager
/// Relacionamento com a entidade Server. Um Manager pode gerir 1 server e um Server pode ser gerido por 0 ou muitos Manageres
/// </summary>
public class ServerUser {

    #region 

    /// <summary>
    /// ultima data de asseco que o user teve com o server
    /// restrições
    /// não pode ser uma data invalida
    /// requisistos
    /// deve ser uma data valida
    /// </summary>
    private DateTime acesseDate;
	public DateTime AcesseDate 
	{
		get 
		{
			return acesseDate;
		}
		set
		{
			acesseDate = value;
		}
	}

	/// <summary>
	/// variavel que define se o User pode ou não aceder ao server
	/// requisistos
	/// o valor default deve ser true
	/// restrições
	///  não pode ser null
	/// </summary>
	private bool isAccessible;
	public bool IsAccessible
	{
		get 
		{
			return isAccessible;
		}
		set 
		{
			isAccessible = value;
		}
	}

	/// <summary>
	/// ultima data de asseco que o user teve com o server
	/// restrições
	/// não pode ser uma data invalida
	/// requisistos
	/// deve ser uma data valida
	/// </summary>
	private DateTime dateCreated;
	public DateTime DateCreated
	{
		get 
		{
			return dateCreated;
		}
		set 
		{
			dateCreated = value;
		}
	}

	/// <summary>
	/// ultima data de asseco que o user teve com o server
	/// restrições
	/// não pode ser uma data invalida
	/// requisistos
	/// deve ser uma data valida
	/// </summary>
	private DateTime dateSuspended;
	public DateTime DateSuspended 
	{
		get 
		{
			return dateSuspended;
		}
		set 
		{
			dateSuspended = value;
		}
	}

	/// <summary>
	/// ultima data de asseco que o user teve com o server
	/// restrições
	/// não pode ser uma data invalida
	/// requisistos
	/// deve ser uma data valida
	/// </summary>
	private DateTime dateBan;
	public DateTime DateBan 
	{
		get
		{
			return dateBan;
		}
		set 
		{
			dateBan = value;
		}
	}

	/// <summary> FK
	/// numero de identificação do objecto
	/// requisitos :
	/// deve conter pelo menos um numero
	/// restrições :
	/// não pode ser um numero racional
	/// </summary>
	private ServerUserState serverUserState;
	public ServerUserState ServerUserState
	{
		get 
		{
			return serverUserState;
		
		}
		set
		{
			serverUserState = value;
		}
	}

	/// <summary> PK FK
	/// numero de identificação do objecto
	/// requisitos :
	/// deve conter pelo menos um numero
	/// restrições :
	/// não pode ser um numero racional
	/// </summary>
	private User user;
	public User User
	{
		get
		{
			return user;
		}
		set
		{
			user = value;
		}
			
	}

	/// <summary> PK FK
	/// numero de identificação do objecto
	/// requisitos :
	/// deve conter pelo menos um numero
	/// restrições :
	/// não pode ser um numero racional
	/// </summary>
	private Server server;
	public Server Server
	{
		get
		{
			return server;
		}
		set
		{
			server = value;
		}
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Constructor Completo
	/// </summary>
	/// <param name="acesseDate"> ultima data de asseco que o user teve com o server </param>
	/// <param name="isAccessible"> variavel que define se o User pode ou não aceder ao server </param>
	/// <param name="dateCreated"> ultima data de asseco que o user teve com o server </param>
	/// <param name="dateSuspended"> ultima data de asseco que o user teve com o server </param>
	/// <param name="dateBan"> ultima data de asseco que o user teve com o server </param>
	/// <param name="contem"></param>
	/// <param name="acedido"></param>
	/// <param name="server"></param>
	public ServerUser(DateTime acesseDate, bool isAccessible, DateTime dateCreated, DateTime dateSuspended, DateTime dateBan, ServerUserState serverUserState, User user, Server server)
	{
		AcesseDate = acesseDate;
		IsAccessible = isAccessible;
		DateCreated = dateCreated;
		DateSuspended = dateSuspended;
		DateBan = dateBan;
		ServerUserState = serverUserState;
		User = user;
		this.server = server;
	}

	/// <summary>
	/// Constructor minimo
	/// </summary>
	/// <param name="acesseDate"> ultima data de asseco que o user teve com o server </param>
	/// <param name="isAccessible"> variavel que define se o User pode ou não aceder ao server </param>
	/// <param name="dateCreated"> ultima data de asseco que o user teve com o server </param>
	/// <param name="contem"></param>
	/// <param name="acedido"></param>
	/// <param name="server"></param>
	public ServerUser(DateTime acesseDate, bool isAccessible, DateTime dateCreated, ServerUserState serverUserState, User user, Server server)
	{
		AcesseDate = acesseDate;
		IsAccessible = isAccessible;
		DateCreated = dateCreated;
		ServerUserState = serverUserState;
		User = user;
		this.server = server;
	}

	/// <summary>
	/// Constructor identitário
	/// </summary>
	/// <param name="contem"></param>
	/// <param name="acedido"></param>
	/// <param name="server"></param>
	public ServerUser(User user, Server server)
	{
		User = user;
		this.server = server;
	}

	#endregion

}
