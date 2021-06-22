using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3�Entidade gerada a partir do relacionamento entre as entidades Car e User, server para gerir e armazenar os carros que foram desbloqueados .
/// Relacionamentos:
/// Relacionamento com a entidade Car. A garagem desbloqueia 1 Car e um Car � desbloqueado por 0 ou muitas Garagens
/// Relacionamento com a entidade User. a Garagem contem 1 user e o user desbloqueia 1 ou muitas garagens
/// </summary>
public class UserCar : MonoBehaviour
{
	/// <summary>
	/// data em que o carro foi desbloqueado
	/// requesitos:
	/// deve ser uma data valida
	/// restri��es:
	/// n�o pode conter letras
	/// </summary>
	private DateTime DateUnlocked;
	public DateTime dateUnlocked
	{
		get
		{
			return DateUnlocked;
		}
		set
		{
			DateUnlocked = value;
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

	private Car desbloqueia;
	private User user;
	private PowerUp contem;


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
