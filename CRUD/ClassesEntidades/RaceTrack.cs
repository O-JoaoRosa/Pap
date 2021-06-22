using System.Collections.Generic;

/// <summary>
/// Entidade RaceTrack tem como objetivo gerir e armazenar dados sobre as pistas de corriada existentes no jogo.
/// Relacionamentos
/// Relacionamento com a entidade User. Uma RaceTrack pode ser corrida por 0 ou muitos User e um User pode Correr em 0 ou muitas RaceTracks. Este relacionamento da origem a uma 3ª tabela que tem o nome de DadosCorrida e tem como objetivo armazenar e gerir os dados obtidos pela corrida
/// </summary>
public class RaceTrack {

    #region Properties

    /// <summary> pk
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
	/// descrição da RaceTrack
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
	/// reputação necessária para desbloquear uma RaceTrack
	/// requisitos
	/// deve ser maior que 0
	/// restrições
	/// não pode ser maior que 999 999 
	/// </summary>
	private int reputationRequiered;
	public int ReputationRequiered 
	{
		get 
		{
			return reputationRequiered;
		}
		set 
		{
			reputationRequiered = value;
		}
	}

	/// <summary>
	/// quantidade de Dinheiro ganho de base na corrida
	/// requesitos
	/// o valor deve ser maior que 0
	/// restrições
	///  não pode conter letras
	/// </summary>
	private int baseMoneyReward;
	public int BaseMoneyReward
	{
		get 
		{
			return baseMoneyReward;
		}
		set 
		{
			baseMoneyReward = value;
		}
	}

	/// <summary>
	/// quantidade de Reputação base (sem os valores adicionais) ganha na corrida
	/// requesitos
	/// o valor deve ser maior que 0
	/// restrições
	///  não pode conter letras
	/// </summary>
	private int baseReputationReward;
	public int BaseReputationReward 
	{
		get 
		{
			return baseReputationReward;
		}
		set
		{
			baseReputationReward = value;
		}
	}

	/// <summary>
	/// lista de userRaces
	/// </summary>
	private List<UserRace> listaUserRaces;
	public List<UserRace> ListaUserRaces { get => listaUserRaces; set => listaUserRaces = value; }

	#endregion

	#region Constructors

	/// <summary>
	/// construtor identitário
	/// </summary>
	/// <param name="id"> numero de identificação do objecto </param>
	public RaceTrack(int id)
	{
		Id = id;
	}

	/// <summary>
	/// Construtor completo
	/// </summary>
	/// <param name="id"> numero de identificação do objecto </param>
	/// <param name="descri"> descrição da RaceTrack </param>
	/// <param name="reputationRequiered"> reputação necessária para desbloquear uma RaceTrack </param>
	/// <param name="baseMoneyReward"> quantidade de Dinheiro ganho de base na corrida </param>
	/// <param name="baseReputationReward"> quantidade de Reputação base (sem os valores adicionais) ganha na corrida </param>
	public RaceTrack(int id, string descri, int reputationRequiered, int baseMoneyReward, int baseReputationReward)
	{
		Id = id;
		Descri = descri;
		ReputationRequiered = reputationRequiered;
		BaseMoneyReward = baseMoneyReward;
		BaseReputationReward = baseReputationReward;
	}

	#endregion

}
