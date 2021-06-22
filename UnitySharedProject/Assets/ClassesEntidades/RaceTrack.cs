using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Entidade RaceTrack tem como objetivo gerir e armazenar dados sobre as pistas de corriada existentes no jogo.
/// Relacionamentos
/// Relacionamento com a entidade User. Uma RaceTrack pode ser corrida por 0 ou muitos User e um User pode Correr em 0 ou muitas RaceTracks. Este relacionamento da origem a uma 3� tabela que tem o nome de DadosCorrida e tem como objetivo armazenar e gerir os dados obtidos pela corrida
/// </summary>
public class RaceTrack : MonoBehaviour
{
	/// <summary>
	/// numero de identifica��o do objecto
	/// requisitos :
	/// deve conter pelo menos um numero
	/// restri��es :
	///  n�o pode ser um numero racional
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
	/// descri��o da RaceTrack
	/// requisitos
	///  deve conter uma descri��o simples
	/// restri��es
	///  n�o pode ser vazio
	/// </summary>
	private string Descri;
	public string descri
	{
		get
		{
			return Descri;
		}
		set
		{
			Descri = value;
		}
	}
	/// <summary>
	/// reputa��o necess�ria para desbloquear uma RaceTrack
	/// requisitos
	/// deve ser maior que 0
	/// restri��es
	/// n�o pode ser maior que 999 999 
	/// </summary>
	private int ReputationRequiered;
	public int reputationRequiered
	{
		get
		{
			return ReputationRequiered;
		}
		set
		{
			ReputationRequiered = value;
		}
	}
	/// <summary>
	/// quantidade de Dinheiro ganho de base na corrida
	/// requesitos
	/// o valor deve ser maior que 0
	/// restri��es
	///  n�o pode conter letras
	/// </summary>
	private int BaseMoneyReward;
	public int baseMoneyReward
	{
		get
		{
			return BaseMoneyReward;
		}
		set
		{
			BaseMoneyReward = value;
		}
	}
	/// <summary>
	/// quantidade de Reputa��o base (sem os valores adicionais) ganha na corrida
	/// requesitos
	/// o valor deve ser maior que 0
	/// restri��es
	///  n�o pode conter letras
	/// </summary>
	private int BaseReputationReward;
	public int baseReputationReward
	{
		get
		{
			return BaseReputationReward;
		}
		set
		{
			BaseReputationReward = value;
		}
	}

	private ArrayList conectado;



	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
