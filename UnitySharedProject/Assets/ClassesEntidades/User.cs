using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
public class User : MonoBehaviour
{
	/// <summary>
	/// numero de identificação do objecto
	/// requisitos :
	/// deve conter pelo menos um numero
	/// restrições :
	///  não pode ser um numero racional
	///  
	///  
	///  
	/// </summary>
	private int Id;
	public int id
	{
		get
		{
			return Id;
		}
		set
		{
			Id = value;
		}
	}
	/// <summary>
	/// variavel que guarda o nome que o utilizador escolher
	/// requisitos
	/// deve conter pelo menos 4 caráteres
	/// restrições
	/// não pode conter caráteres especiais
	///  
	/// </summary>
	private string UserName;
	public string userName
	{
		get
		{
			return UserName;
		}
		set
		{
			UserName = value;
		}
	}
	/// <summary>
	/// variavel usada para guardar a palavra-pass do user. So pode ser guardado com encriptação
	/// requisitos
	/// deve ter no minimo 6 caráteres
	/// restrições
	///  não pode conter mais de 20 caráteres
	///  
	/// </summary>
	private string Password;
	public string password
	{
		get
		{
			return Password;
		}
		set
		{
			Password = value;
		}
	}
	/// <summary>
	/// var utilizada para guardar o email do user
	/// requisitos
	/// deve sempre conter o character @
	/// restrições
	/// não é valido se não tiver o character "."
	///  
	/// </summary>
	private string Email;
	public string email
	{
		get
		{
			return Email;
		}
		set
		{
			Email = value;
		}
	}
	/// <summary>
	/// Variavel que guarda a quantidade de dinheiro que o utilizador tem de momento
	/// requesitos
	/// o valor default deve ser 0
	/// restrições
	///  não pode sair mais que 99 999
	///  
	/// </summary>
	private int Money;
	public int money
	{
		get
		{
			return Money;
		}
		set
		{
			Money = value;
		}
	}
	/// <summary>
	/// variável responsavel por guardar o valor da reputação que o utilizador tem de momento
	/// requisistos
	/// o valor default deve ser 100
	/// restições
	///  não pode ser um valor maior que 999 999
	///  
	/// </summary>
	private int Reputation;
	public int reputation
	{
		get
		{
			return Reputation;
		}
		set
		{
			Reputation = value;
		}
	}
	/// <summary>
	/// variavel que recebe o url de uma imagem
	/// requisitos
	///  deve ter um valor default
	/// restrições
	///  não pode ser null
	///  
	/// </summary>
	private string Image;
	public string image
	{
		get
		{
			return Image;
		}
		set
		{
			Image = value;
		}
	}
	/// <summary>
	/// data em que o utilizador esteve online pela ultima vez
	/// requisitos
	/// deve ser uma data valida
	/// restrições
	///  não pode conter letras
	///  
	/// </summary>
	private DateTime LastTimeOnline;
	public DateTime lastTimeOnline
	{
		get
		{
			return LastTimeOnline;
		}
		set
		{
			LastTimeOnline = value;
		}
	}

	private ArrayList conectado;
	private ArrayList conhece;
	private ArrayList Friend;
	private ArrayList contem;
	private ArrayList profile;
	private ArrayList desbloqueia;
	private ArrayList user_Server;



	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
