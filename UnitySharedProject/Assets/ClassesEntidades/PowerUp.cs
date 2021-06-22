using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Entidade que gere e armazena os dados dos PoweUps dos carros e tem como objectivo atribuir efeitos aos PowerUps
/// Relacionamentos:
/// relacionamento com a entidade Car. Um PowerUp � contido por 0 ou 1 Car e um Car  contem 0 ou 1 PowerUp
/// </summary>
public class PowerUp : MonoBehaviour
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
	/// descri��o do power up
	/// requisitos
	///  deve conter uma descri��o simples
	/// restri��es
	///  n�o pode ser vazio
	///  
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
	/// Nome de codigo do modelo 3d ao qual a roda estar� associada
	/// requisitos
	///  deve estar preenchido
	/// restri��es
	/// n�o pode estar vazio
	///  
	/// </summary>
	private string ImageUrl;
	public string imageUrl
	{
		get
		{
			return ImageUrl;
		}
		set
		{
			ImageUrl = value;
		}
	}
	/// <summary>
	/// variavel que ir� defenir quanto tempo o power up demorar� a poder ser usado
	/// requisitos
	/// deve ser maior que 0
	/// restri��es
	///  n�o pode ser mais que 120
	///  
	/// </summary>
	private int TimeCharge;
	public int timeCharge
	{
		get
		{
			return TimeCharge;
		}
		set
		{
			TimeCharge = value;
		}
	}

	private System.Collections.ArrayList contido;


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
