using System;

/// <summary>
/// 3ª entidade que surgio do relacionamento do User consigo mesmo, tem como objetivo gerir e armazenar as amizades que os Users têm com outros users
/// Relacionamentos
/// Relacionamento com o User. Um Friend pode conhecer 0 ou muitos User e um user pode ser conhecido por 0 ou muitos Friends
/// </summary>
public class UserFriend {

    #region Properties

    /// <summary>
    /// variavél que avisa os outros Users se o User em questão está online
    /// requesitos
    /// o valor default deve ser false
    /// restrições
    ///  não pode ser true por mais de 5 minutos sem verificação
    /// </summary>
    private bool isOnline;
	public bool IsOnline 
	{
		get 
		{
			return isOnline;
		}
		set 
		{
			isOnline = value;
		}
	}

	/// <summary>
	/// data em que o user adicionou o outro user como amigo
	/// requisitos
	/// deve ser uma data valida
	/// restrições
	///  não pode conter letras
	/// </summary>
	private DateTime dateAdded;
	public DateTime DateAdded 
	{
		get 
		{
			return dateAdded;
		}
		set 
		{
			dateAdded = value;
		}
	}

	/// <summary> PK
	/// numero de identificação do objecto
	/// requisitos :
	/// deve conter pelo menos um numero
	/// restrições :
	/// não pode ser um numero racional
	/// </summary>
	private User userFriend;
	public User UserFriend1
	{
		get
		{
			return userFriend;
		}
		set
		{
			userFriend = value;
		}
	}

	/// <summary> PK
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

	#endregion

	#region Constructors

	/// <summary>
	/// Construtor completo
	/// </summary>
	/// <param name="isOnline"> variavél que avisa os outros Users se o User em questão está online </param>
	/// <param name="dateAdded"> data em que o user adicionou o outro user como amigo </param>
	/// <param name="userFriend"></param>
	/// <param name="user"></param>
	public UserFriend(bool isOnline, DateTime dateAdded, User userFriend, User user)
	{
		IsOnline = isOnline;
		DateAdded = dateAdded;
		UserFriend1 = userFriend;
		User = user;
	}

	/// <summary>
	/// Construtor identitário
	/// </summary>
	/// <param name="userFriend"></param>
	/// <param name="user"></param>
	public UserFriend(User userFriend, User user)
	{
		UserFriend1 = userFriend;
		User = user;
	}

	#endregion


}
