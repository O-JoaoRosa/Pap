using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Entidade UserConfig, tem como objectivo gerir as configurações  pessoais de cada User
/// Relacionamentos
/// Relacionam com a entidade User. Um UserConfig pode ser contido por 0 ou 1 User e um User pode conter 0 ou muitos UserConfig.
/// </summary>
public class UserConfig : MonoBehaviour
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
	/// descrição do UserConfigs
	/// requisitos
	///  deve conter uma descrição simples
	/// restrições
	///  não pode ser vazio
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
	/// valor da user config
	/// requisitos
	/// deve conter algum valor
	/// restrições
	///  pode ser um numero maior qu e100
	/// </summary>
	private int Value;
	public int value
	{
		get
		{
			return Value;
		}
		set
		{
			Value = value;
		}
	}

	private User contido;


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
