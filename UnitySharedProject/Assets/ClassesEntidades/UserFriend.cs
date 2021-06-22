using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3� entidade que surgio do relacionamento do User consigo mesmo, tem como objetivo gerir e armazenar as amizades que os Users t�m com outros users
/// Relacionamentos
/// Relacionamento com o User. Um Friend pode conhecer 0 ou muitos User e um user pode ser conhecido por 0 ou muitos Friends
/// </summary>
public class UserFriend : MonoBehaviour
{
	/// <summary>
	/// variav�l que avisa os outros Users se o User em quest�o est� online
	/// requesitos
	/// o valor default deve ser false
	/// restri��es
	///  n�o pode ser true por mais de 5 minutos sem verifica��o
	/// </summary>
	private bool IsOnline;
	public bool isOnline
	{
		get
		{
			return IsOnline;
		}
		set
		{
			IsOnline = value;
		}
	}

	/// <summary>
	/// data em que o user adicionou o outro user como amigo
	/// requisitos
	/// deve ser uma data valida
	/// restri��es
	///  n�o pode conter letras
	/// </summary>
	private DateTime DateAdded;
	public DateTime dateAdded
	{
		get
		{
			return DateAdded;
		}
		set
		{
			DateAdded = value;
		}
	}

	private User userID2;
	private User conhece;



	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
