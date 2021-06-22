using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3� Entidade gerada a partir do relacionamento entre a entidade RaceTrack e a entidade User, tem como objectivo gerir e armazenar os dados obtidos durante a corrida
/// Relacionamentos
/// relacionamento com a RaceTrack. Uma entidade do tipo DadosCorrida obtem os dados de uma RaceTrack, uma RaceTrack fornece os dados a 0 ou muitas entidades DadosCorrida
/// Relacionamento com a entidade User. Um DadosCorrida est� associado a 1 User e um User pode estar associado a 0 ou muitos DadosCorrida
/// </summary>
public class UserRace : MonoBehaviour
{
	/// <summary>
	/// numero de identifica��o do objecto
	/// requisitos :
	/// deve conter pelo menos um numero
	/// restri��es :
	///  n�o pode ser um numero racional
	/// </summary>
	private int ID;
	public int iD
	{
		get
		{
			return ID;
		}
		set
		{
			ID = value;
		}
	}
	/// <summary>
	/// lugar em que o jogador ficou posicionado
	/// requisitos
	///  deve ser um valor positivo
	/// restri��es
	///  n�o pode ser maior que 20
	/// </summary>
	private int FinishPosition;
	public int finishPosition
	{
		get
		{
			return FinishPosition;
		}
		set
		{
			FinishPosition = value;
		}
	}
	/// <summary>
	/// quantidade de dinheiro feito na corrida
	/// requisitos
	/// deve ser no minimo o valor da pista
	///  restri��es
	/// n�o pode ser mais que 9999
	/// </summary>
	private int MoneyMade;
	public int moneyMade
	{
		get
		{
			return MoneyMade;
		}
		set
		{
			MoneyMade = value;
		}
	}
	/// <summary>
	/// quantidade de reputa��o feita na corrida
	/// requisistos
	/// deve ser no minimo o valor indicado na pista
	/// restri��es
	///  n�o pode ser um valor maior que 999 999
	/// </summary>
	private int ReputationMade;
	public int reputationMade
	{
		get
		{
			return ReputationMade;
		}
		set
		{
			ReputationMade = value;
		}
	}
	/// <summary>
	/// data em que a corrida ocurreu
	/// requisitos
	/// deve ser uma data valida
	/// restri��es
	///  n�o pode conter letras
	/// </summary>
	private DateTime DateRace;
	public DateTime dateRace
	{
		get
		{
			return DateRace;
		}
		set
		{
			DateRace = value;
		}
	}
	/// <summary>
	/// vari�vel que decide se o objecto est� ou n�o desbloqueado
	/// requisitos
	/// deve ter o valor default de false
	/// restri��es
	/// n�o pode ter o valor default true  
	/// </summary>
	private bool IsUnlocked;
	public bool isUnlocked
	{
		get
		{
			return IsUnlocked;
		}
		set
		{
			IsUnlocked = value;
		}
	}

	private RaceTrack conecta;
	private User user;


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
