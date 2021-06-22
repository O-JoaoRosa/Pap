using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 3ª entidade que surgio do relacionamento do User com a entidade Server, tem como objetivo gerir e armazenar as permições de acesso que cada utilizadro tem
/// Relacionamentos
/// Relacionamento com o User. Um Manager pode gerir 0 ou muitos Users e um user pode ser gerido por 1 manager
/// Relacionamento com a entidade Server. Um Manager pode gerir 1 server e um Server pode ser gerido por 0 ou muitos Manageres
/// </summary>
public class ServerUser : MonoBehaviour
{

	/// <summary>
	/// ultima data de asseco que o user teve com o server
	/// restrições
	/// não pode ser uma data invalida
	/// requisistos
	/// deve ser uma data valida
	/// </summary>
	private DateTime AcesseDate;
	public DateTime acesseDate
	{
		get
		{
			return AcesseDate;
		}
		set
		{
			AcesseDate = value;
		}
	}
	/// <summary>
	/// variavel que define se o User pode ou não aceder ao server
	/// requisistos
	/// o valor default deve ser true
	/// restrições
	///  não pode ser null
	/// </summary>
	private bool IsAccessible;
	public bool isAccessible
	{
		get
		{
			return IsAccessible;
		}
		set
		{
			IsAccessible = value;
		}
	}
	/// <summary>
	/// ultima data de asseco que o user teve com o server
	/// restrições
	/// não pode ser uma data invalida
	/// requisistos
	/// deve ser uma data valida
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
	/// <summary>
	/// ultima data de asseco que o user teve com o server
	/// restrições
	/// não pode ser uma data invalida
	/// requisistos
	/// deve ser uma data valida
	/// </summary>
	private DateTime DateSuspended;
	public DateTime dateSuspended
	{
		get
		{
			return DateSuspended;
		}
		set
		{
			DateSuspended = value;
		}
	}
	/// <summary>
	/// ultima data de asseco que o user teve com o server
	/// restrições
	/// não pode ser uma data invalida
	/// requisistos
	/// deve ser uma data valida
	/// </summary>
	private DateTime DateBan;
	public DateTime dateBan
	{
		get
		{
			return DateBan;
		}
		set
		{
			DateBan = value;
		}
	}

	private ServerUserState contem;

	private User acedido;
	private Server server;



	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
