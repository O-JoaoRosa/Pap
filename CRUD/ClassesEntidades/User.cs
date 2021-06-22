using System;
using System.Collections.Generic;

/// <summary>
/// Entidade User, tem como objectivo gerir e armazenar todos os dados dos utilizadores
/// Relacionamentos
/// Relacionamento com a entidade Car. Um User pode desbloquerar 1 ou muitos Car e um Car pode ser desbloqueado por 0 ou muitos users. Este relacionamento deu origem a uma 3ª entidade com o nome de garagem que irá armazenar e gerir os carros que o user desbloqueo
/// Relacionamento com o UserType. Um User pode conter um ou muitos UserTypes, e um UserType é contido por 0 ou muitos Users. Este relacionamento deu origem a uma 3ª entidade com o nome de profile que irá armazenar e gerir os perfis dos Users
/// Relacionamento com a entidade UserConfig. Um User Contem 1 ou muitas UserConfigs e uma UserConfig é contidade por 1 user apenas
/// Relacionamento com a entidade statistic. Um User contem 0 ou muitas statistic e uma Statistic é contida por 0 ou 1 user
/// Relacionamento consigo mesmo. O User pode conhexer 0 ou muitos User e um User pode ser conhecido por 0 ou muitos User. Este relacionamento deu origem a uma 3ª entidade com o nome de Firend, que tem como objectivo armazemar e os users conhecidos
/// Relacionamento com a entidade RaceTrack. Um User pode ter corrido em 0 ou muitas RaceTrack e uma race track pode ter sido corrida por 0 ou muitos Users. Este relacionamento deu origem a uma 3ªentidade com o nome de DadosCorrida que tem como objectivo gerir e armazenar os dados da corrida
/// Relacionamento com a entidade Server. Um User pode aceder a um 0 ou muitos servers e um server pode ser acedido por 0 ou muitos Users. Este relacionamento vai dar origem a uma 3ªentidade que tem como objectivo gerir e armazenar os dados dos utilizadores que entram e saem
/// </summary>
public class User {

    #region Properties

    /// <summary> Pk
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
	/// variavel que guarda o nome que o utilizador escolher
	/// requisitos
	/// deve conter pelo menos 4 caráteres
	/// restrições
	/// não pode conter caráteres especiais
	/// </summary>
	private string userName;
	public string UserName 
	{
		get 
		{
			return userName;
		}
		set 
		{
			userName = value;
		}
	}

	/// <summary>
	/// variavel usada para guardar a palavra-pass do user. So pode ser guardado com encriptação
	/// requisitos
	/// deve ter no minimo 6 caráteres
	/// restrições
	///  não pode conter mais de 20 caráteres
	/// </summary>
	private string password;
	public string Password 
	{
		get 
		{
			return password;
		}
		set 
		{
			password = value;
		}
	}

	/// <summary>
	/// var utilizada para guardar o email do user
	/// requisitos
	/// deve sempre conter o character @
	/// restrições
	/// não é valido se não tiver o character "."
	/// </summary>
	private string email;
	public string Email 
	{
		get
		{
			return email;
		}
		set 
		{
			email = value;
		}
	}

	/// <summary>
	/// Variavel que guarda a quantidade de dinheiro que o utilizador tem de momento
	/// requesitos
	/// o valor default deve ser 0
	/// restrições
	///  não pode sair mais que 99 999
	/// </summary>
	private int money;
	public int Money 
	{
		get 
		{
			return money;
		}
		set 
		{
			money = value;
		}
	}

	/// <summary>
	/// variável responsavel por guardar o valor da reputação que o utilizador tem de momento
	/// requisistos
	/// o valor default deve ser 100
	/// restições
	///  não pode ser um valor maior que 999 999
	/// </summary>
	private int reputation;
	public int Reputation 
	{
		get 
		{
			return reputation;
		}
		set 
		{
			reputation = value;
		}
	}

	/// <summary>
	/// variavel que recebe o url de uma imagem
	/// requisitos
	///  deve ter um valor default
	/// restrições
	///  não pode ser null
	/// </summary>
	private string image;
	public string Image 
	{
		get 
		{
			return image;
		}
		set 
		{
			image = value;
		}
	}

	/// <summary>
	/// data em que o utilizador esteve online pela ultima vez
	/// requisitos
	/// deve ser uma data valida
	/// restrições
	///  não pode conter letras
	/// </summary>
	private DateTime lastTimeOnline;
	public DateTime LastTimeOnline
	{
		get 
		{
			return lastTimeOnline;
		}
		set 
		{
			lastTimeOnline = value;
		}
	}

	/// <summary> Fk
	/// Lista de ServerUsers
	/// </summary>
	private List<ServerUser> listaServerUsers;
	public List<ServerUser> ListaServerUsers { get => listaServerUsers; set => listaServerUsers = value; }

	/// <summary> Fk
	/// Lista de UserCars
	/// </summary>
	private List<UserCar> listaUserCars;
	public List<UserCar> ListaUserCars { get => listaUserCars; set => listaUserCars = value; }

	/// <summary> Fk
	/// Lista de Profiles
	/// </summary>
	private List<Profile> listaProfiles;
	public List<Profile> ListaProfiles { get => listaProfiles; set => listaProfiles = value; }

	/// <summary> Fk
	/// Lista de UserConfigs
	/// </summary>
	private List<UserConfig> listaUserConfigs;
	public List<UserConfig> ListaUserConfigs { get => listaUserConfigs; set => listaUserConfigs = value; }

	/// <summary> Fk
	/// Lista de UserFriends
	/// </summary>
	private List<UserFriend> ListaUserFriends;
	public List<UserFriend> ListaUserFriends1 { get => ListaUserFriends; set => ListaUserFriends = value; }

	/// <summary> Fk
	/// Lista de UserRaces
	/// </summary>
	private List<UserRace> listaUserRaces;
	public List<UserRace> ListaUserRaces { get => listaUserRaces; set => listaUserRaces = value; }

	/// <summary> Fk
	/// Lista de Users
	/// </summary>
	private List<User> listaUsers;
	public List<User> ListaUsers { get => listaUsers; set => listaUsers = value; }

	#endregion

	#region Constructor

	/// <summary>
	/// Construtor completo
	/// </summary>
	/// <param name="id">numero de identificação do objecto</param>
	/// <param name="userName">variavel que guarda o nome que o utilizador escolher</param>
	/// <param name="password">variavel usada para guardar a palavra-pass do user. So pode ser guardado com encriptação</param>
	/// <param name="email">var utilizada para guardar o email do user</param>
	/// <param name="money">Variavel que guarda a quantidade de dinheiro que o utilizador tem de momento</param>
	/// <param name="reputation">variável responsavel por guardar o valor da reputação que o utilizador tem de momento</param>
	/// <param name="image">variavel que recebe o url de uma imagem</param>
	/// <param name="lastTimeOnline">data em que o utilizador esteve online pela ultima vez</param>
	public User(int id, string userName, string password, string email, int money, int reputation, string image, DateTime lastTimeOnline)
	{
		Id = id;
		UserName = userName;
		Password = password;
		Email = email;
		Money = money;
		Reputation = reputation;
		Image = image;
		LastTimeOnline = lastTimeOnline;
	}

	/// <summary>
	/// Construtor identitário
	/// </summary>
	/// <param name="id">numero de identificação do objecto</param>
	public User(int id)
	{
		Id = id;
	}

	#endregion
}
