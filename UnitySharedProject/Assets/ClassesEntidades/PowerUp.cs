using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Entidade que gere e armazena os dados dos PoweUps dos carros e tem como objectivo atribuir efeitos aos PowerUps
/// Relacionamentos:
/// relacionamento com a entidade Car. Um PowerUp é contido por 0 ou 1 Car e um Car  contem 0 ou 1 PowerUp
/// </summary>
public class PowerUp : MonoBehaviour
{
	/// <summary>
	/// numero de identificação do objecto
	/// requisitos :
	/// deve conter pelo menos um numero
	/// restrições :
	///  não pode ser um numero racional
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
	/// descrição do power up
	/// requisitos
	///  deve conter uma descrição simples
	/// restrições
	///  não pode ser vazio
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
	/// Nome de codigo do modelo 3d ao qual a roda estará associada
	/// requisitos
	///  deve estar preenchido
	/// restrições
	/// não pode estar vazio
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
	/// variavel que irá defenir quanto tempo o power up demorará a poder ser usado
	/// requisitos
	/// deve ser maior que 0
	/// restrições
	///  não pode ser mais que 120
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
