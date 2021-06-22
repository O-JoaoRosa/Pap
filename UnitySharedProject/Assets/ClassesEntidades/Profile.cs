using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3ª entidade criada a partir do relacionamento entre a entidade User e a entidade UserType, tem como objectivo gerir e armazenar os perfis dos Users
/// Relacionamentos
/// Relacionamento com a entidade User. Um profile pode ser contido por 1 User e um User pode conter 1 ou muitos Profile.
/// Relacionamento com a entidade UserType. Um Profile pode conter um UserType  e um user type pode conter 0 ou muitos profiles.
/// </summary>
public class Profile : MonoBehaviour
{
	/// <summary>
	/// data de criação do prefil
	/// requesitos:
	/// deve ser uma data valida
	/// restrições:
	/// não pode conter letras
	/// </summary>
	private DateTime DateCreated;
	public DateTime dateCreated
	{
		get
		{
			return DateCreated;
		}
		set
		{
			DateCreated = value;
		}
	}

	private User contido;
	private UserType userType;



	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
