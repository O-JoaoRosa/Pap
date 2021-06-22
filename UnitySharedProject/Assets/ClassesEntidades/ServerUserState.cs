using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// esta entidade destina-se a controlar os estados do utilizador com o server
/// Ativo
/// suspenso
/// banido
/// relacionamentos:
/// Relacionamento com a entidade ServerUser. Um estado pode ter 0 ou muitos ServerUsers e um ServerUser tem que ter 1 ServerUserState.
/// </summary>
public class ServerUserState : MonoBehaviour
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
	/// descri��o do Car
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

	private ArrayList contido;


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
