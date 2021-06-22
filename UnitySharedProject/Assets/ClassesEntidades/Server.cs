using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Entidade que gere e armazena a informa��o sobre que utilizador tem acesso a que server
/// requisitos
/// Relacionamentos
/// Relaciona com a entidade User. Um Server pode ser acedido por 0 ou muitos Users e um User pode aceder a 0 ou muitos servers. Este relacionamento d� origem a uma 3�entidade com o nome de manager que tem como objetivo gerir e armazenar dados sobre quem est� no server e se pode aceder ao server 
/// </summary>
public class Server : MonoBehaviour
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

	/// <summary>
	/// variavel que guarda todas as observa��es necess�rias
	/// requisitos
	/// deve ser descritivo
	/// restri��es
	///  n�o pode ter mais de 200 carcateres
	/// </summary>
	private string Obs;
	public string obs
	{
		get
		{
			return Obs;
		}
		set
		{
			Obs = value;
		}
	}

	private ArrayList acedido;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
