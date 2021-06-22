using System;

/// <summary>
/// 3ªEntidade gerada a partir do relacionamento entre as entidades Car e User, server para gerir e armazenar os carros que foram desbloqueados .
/// Relacionamentos:
/// Relacionamento com a entidade Car. A garagem desbloqueia 1 Car e um Car é desbloqueado por 0 ou muitas Garagens
/// Relacionamento com a entidade User. a Garagem contem 1 user e o user desbloqueia 1 ou muitas garagens
/// </summary>
public class UserCar {

    #region Properties

    /// <summary>
    /// data em que o carro foi desbloqueado
    /// requesitos:
    /// deve ser uma data valida
    /// restrições:
    /// não pode conter letras
    /// </summary>
    private DateTime dateUnlocked;
	public DateTime DateUnlocked
	{
		get 
		{
			return dateUnlocked;
		}
		set 
		{
			dateUnlocked = value;
		}
	}

	/// <summary>
	/// variável que decide se o objecto está ou não desbloqueado
	/// requisitos
	/// deve ter o valor default de false
	/// restrições
	/// não pode ter o valor default true
	/// </summary>
	private bool isUnlocked;
	public bool IsUnlocked 
	{
		get
		{
			return isUnlocked;
		}
		set 
		{
			isUnlocked = value;
		}
	}

	/// <summary> PK
	/// numero de identificação do objecto
	/// requisitos :
	///  deve conter pelo menos um numero
	/// restrições :
	///  não pode ser um numero racional
	/// </summary>
	private Car car;
	public Car Car
    {
		get
		{
			return car;
		}
		set
		{
			car = value;
		}
	}

	/// <summary> PK
	/// numero de identificação do objecto
	/// requisitos :
	///  deve conter pelo menos um numero
	/// restrições :
	///  não pode ser um numero racional
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

	/// <summary> FK
	/// numero de identificação do objecto
	/// requisitos :
	///  deve conter pelo menos um numero
	/// restrições :
	///  não pode ser um numero racional
	/// </summary>
	private PowerUp powerUp;
	public PowerUp PowerUp
	{
		get
		{
			return powerUp;
		}
		set
		{
			powerUp = value;
		}
	}

	/// <summary> FK
	/// numero de identificação do objecto
	/// requisitos :
	///  deve conter pelo menos um numero
	/// restrições :
	///  não pode ser um numero racional
	/// </summary>
	private Wheel roda;
	public Wheel Roda
	{
		get
		{
			return roda;
		}
		set
		{
			roda = value;
		}
	}

	/// <summary> FK
	/// numero de identificação do objecto
	/// requisitos :
	///  deve conter pelo menos um numero
	/// restrições :
	///  não pode ser um numero racional
	/// </summary>
	private CarBody carBody;
	public CarBody CarBody
	{
		get
		{
			return carBody;
		}
		set
		{
			carBody = value;
		}
	}

	#endregion

	#region Constructors

	/// <summary>
	/// construtor completo
	/// </summary>
	/// <param name="dateUnlocked"> data em que o carro foi desbloqueado</param>
	/// <param name="isUnlocked">variável que decide se o objecto está ou não desbloqueado</param>
	/// <param name="desbloqueia"></param>
	/// <param name="user"></param>
	/// <param name="contem"></param>
	public UserCar(DateTime dateUnlocked, bool isUnlocked, Car desbloqueia, User user, PowerUp contem)
	{
		DateUnlocked = dateUnlocked;
		IsUnlocked = isUnlocked;
		this.car = desbloqueia;
		this.user = user;
		this.powerUp = contem;
	}

	/// <summary>
	/// construtor minimo
	/// </summary>
	/// <param name="dateUnlocked"> data em que o carro foi desbloqueado</param>
	/// <param name="isUnlocked">variável que decide se o objecto está ou não desbloqueado</param>
	/// <param name="desbloqueia"></param>
	/// <param name="user"></param>
	public UserCar(DateTime dateUnlocked, bool isUnlocked, Car desbloqueia, User user)
	{
		DateUnlocked = dateUnlocked;
		IsUnlocked = isUnlocked;
		this.car = desbloqueia;
		this.user = user;
	}

	/// <summary>
	/// construtor identitário
	/// </summary>
	/// <param name="desbloqueia"></param>
	/// <param name="user"></param>
	public UserCar( Car desbloqueia, User user)
	{
		this.car = desbloqueia;
		this.user = user;
	}

	#endregion
}
