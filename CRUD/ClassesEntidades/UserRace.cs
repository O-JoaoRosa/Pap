using System;

/// <summary>
/// 3ª Entidade gerada a partir do relacionamento entre a entidade RaceTrack e a entidade User, tem como objectivo gerir e armazenar os dados obtidos durante a corrida
/// Relacionamentos
/// relacionamento com a RaceTrack. Uma entidade do tipo DadosCorrida obtem os dados de uma RaceTrack, uma RaceTrack fornece os dados a 0 ou muitas entidades DadosCorrida
/// Relacionamento com a entidade User. Um DadosCorrida está associado a 1 User e um User pode estar associado a 0 ou muitos DadosCorrida
/// </summary>
public class UserRace {

    #region Properties

    /// <summary> PK
    /// numero de identificação do objecto
    /// requisitos :
    ///  deve conter pelo menos um numero
    /// restrições :
    ///  não pode ser um numero racional
    /// </summary>
    private int id;
	public int ID 
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
	/// lugar em que o jogador ficou posicionado
	/// requisitos
	///  deve ser um valor positivo
	/// restrições
	///  não pode ser maior que 20
	/// </summary>
	private int finishPosition;
	public int FinishPosition 
	{
		get 
		{
			return finishPosition;
		}
		set 
		{
			finishPosition = value;
		}
	}

	/// <summary>
	/// quantidade de dinheiro feito na corrida
	/// requisitos
	/// deve ser no minimo o valor da pista
	///  restrições
	/// não pode ser mais que 9999
	/// </summary>
	private int moneyMade;
	public int MoneyMade 
	{
		get
		{
			return moneyMade;
		}
		set
		{
			moneyMade = value;
		}
	}

	/// <summary>
	/// quantidade de reputação feita na corrida
	/// requisistos
	/// deve ser no minimo o valor indicado na pista
	/// restrições
	///  não pode ser um valor maior que 999 999
	/// </summary>
	private int reputationMade;
	public int ReputationMade 
	{
		get 
		{
			return reputationMade;
		}
		set 
		{
			reputationMade = value;
		}
	}

	/// <summary>
	/// data em que a corrida ocurreu
	/// requisitos
	/// deve ser uma data valida
	/// restrições
	///  não pode conter letras
	/// </summary>
	private DateTime dateRace;
	public DateTime DateRace 
	{
		get
		{
			return dateRace;
		}
		set 
		{
			dateRace = value;
		}
	}

	/// <summary> FK
	/// numero de identificação do objecto
	/// requisitos :
	///  deve conter pelo menos um numero
	/// restrições :
	///  não pode ser um numero racional
	/// </summary>
	private RaceTrack raceTrack;
	public RaceTrack RaceTrack
	{
		get
		{
			return raceTrack;
		}
		set
		{
			raceTrack = value;
		}
	}

	/// <summary> FK
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

	#endregion

	#region Constructor

	/// <summary>
	/// Construtor Identitário
	/// </summary>
	/// <param name="id"> numero de identificação do objecto </param>
	public UserRace(int id)
	{
		ID = id;
	}

	/// <summary>
	/// Construtor Completo
	/// </summary>
	/// <param name="iD"> numero de identificação do objecto </param>
	/// <param name="finishPosition">lugar em que o jogador ficou posicionado</param>
	/// <param name="moneyMade"> quantidade de dinheiro feito na corrida </param>
	/// <param name="reputationMade"> quantidade de reputação feita na corrida</param>
	/// <param name="dateRace">data em que a corrida ocurreu</param>
	/// <param name="isUnlocked">variável que decide se o objecto está ou não desbloqueado</param>
	/// <param name="raceTrack"></param>
	/// <param name="user"></param>
	public UserRace(int iD, int finishPosition, int moneyMade, int reputationMade, DateTime dateRace, RaceTrack raceTrack, User user)
	{
		ID = iD;
		FinishPosition = finishPosition;
		MoneyMade = moneyMade;
		ReputationMade = reputationMade;
		DateRace = dateRace;
		this.raceTrack = raceTrack;
		this.user = user;
	}

	#endregion

}
