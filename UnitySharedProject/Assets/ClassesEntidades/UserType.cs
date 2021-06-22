using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Entidade UserType que tem como objectivo gerir e armazenar a informa��o sobre o tipo de utilizador que o User �.
/// Relacionamento com a entidade Profile. Um UserType � contido por 0 ou muitos Users, e um User pode conter 1 ou muitos UserType. Este relacionamento deu origem a uma 3� entidade que tem como objectivo gerir e armazenar os perfis dos User.
/// </summary>
public class UserType : MonoBehaviour
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
	/// descri��o do UserType
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
