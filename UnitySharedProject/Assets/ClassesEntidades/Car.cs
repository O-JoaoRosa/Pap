using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    #region Vars and properties
    /// <summary>
    /// numero de identificação do objecto
    /// requisitos :
    /// deve conter pelo menos um numero
    /// restrições :
    ///  não pode ser um numero racional
    ///  
    ///  
    ///  
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
	/// descrição do Car
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
	/// atributo que especifica a quantidade de rep que é preciso para desbloquear o objecto
	/// requisitos
	/// deve ser um numero inteiro
	/// restrições
	///  não pode ser maior que 99 999
	///  
	/// </summary>
	private int ReputationRequired;
	public int reputationRequired
	{
		get
		{
			return ReputationRequired;
		}
		set
		{
			ReputationRequired = value;
		}
	}
	/// <summary>
	/// preço nessecario para desbloquear o objecto
	/// requisitos
	/// deve conter pelo menos um caracter
	/// restrições
	///  não pode ser mais que 99 999
	///  
	/// </summary>
	private int Price;
	public int price
	{
		get
		{
			return Price;
		}
		set
		{
			Price = value;
		}
	}
	/// <summary>
	/// variável que define qual é a velocidade máxima do carro
	/// requisitos
	/// deve ser pelo menos 100.0
	/// restrições 
	/// não pode ser mais que 250.0
	///  
	/// </summary>
	private double MaxSpeed;
	public double maxSpeed
	{
		get
		{
			return MaxSpeed;
		}
		set
		{
			MaxSpeed = value;
		}
	}
	/// <summary>
	/// variável que definenn quanta velocidade o carro ganha por segundo
	/// requesitos
	/// deve ser pelo menos 5
	/// restrcições
	/// não pode ser mais que 50
	///  
	/// </summary>
	private double Acceleration;
	public double acceleration
	{
		get
		{
			return Acceleration;
		}
		set
		{
			Acceleration = value;
		}
	}
	/// <summary>
	/// variável que define a facilidade de virar enquanto se faz drift
	/// requisitos
	/// tem que ter um valor
	/// restrições
	///  não pode ser maior que 50
	///  
	/// </summary>
	private int DriftForce;
	public int driftForce
	{
		get
		{
			return DriftForce;
		}
		set
		{
			DriftForce = value;
		}
	}
	/// <summary>
	/// variável que define a facilidade de virar sem o drift
	/// requisitos
	/// tem que ter um valor
	/// restrições
	///  não pode ser um valor maior que 50
	///  
	/// </summary>
	private int Mobility;
	public int mobility
	{
		get
		{
			return Mobility;
		}
		set
		{
			Mobility = value;
		}
	}

	private ArrayList garagem;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
