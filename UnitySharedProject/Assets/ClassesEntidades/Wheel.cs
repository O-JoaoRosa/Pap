using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Entidade que gere e armazena os dados das rodas dos carros e tem como objectivo dar um valor (preço) a cada roda
/// Relacionamentos:
/// relacionamento com a entidade CarBody. Uma Wheel é contida por 0 ou muitos Car e um Car  contem uma wheel apenas.
/// </summary>
public class Wheel : MonoBehaviour
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
	/// preço da roda
	/// requisitos
	/// deve conter um valor maior que 1
	/// restrições
	///  não pode ser maior que 99 999
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
	/// valor cor da pintura
	/// requisitos:
	/// deve ser um código hexadecimal válido
	/// restrições
	///  não pode ter um valor inválido
	/// </summary>
	private string Paint;
	public string paint
	{
		get
		{
			return Paint;
		}
		set
		{
			Paint = value;
		}
	}
	/// <summary>
	/// descrição da RaceTrack
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
	/// Nome de codigo do modelo 3d ao qual a roda estará associada
	/// requisitos
	///  deve estar preenchido
	/// restrições
	/// não pode estar vazio
	/// </summary>
	private string CodeName;
	public string codeName
	{
		get
		{
			return CodeName;
		}
		set
		{
			CodeName = value;
		}
	}

	private ArrayList contida;


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
