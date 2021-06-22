using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Entidade que gere e armazena os dados do chasi dos carros e tem como objectivo dar um valor (pre�o) a cada chasi
/// Relacionamentos:
/// relacionamento com a entidade Car. Um CarBody � contido por 0 ou muitos Car e um Car  contem um CarBody apenas.
/// </summary>
public class NewBehaviourScript : MonoBehaviour
{
	#region Vars & Properties
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
	/// pre�o do CarBody
	/// requisitos
	/// deve conter um valor maior que 1
	/// restri��es
	///  n�o pode ser maior que 99 999
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
	/// valor cor da pintura
	/// requisitos:
	/// deve ser um c�digo hexadecimal v�lido
	/// restri��es
	///  n�o pode ter um valor inv�lido
	///  
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
	/// descri��o da RaceTrack
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

	private System.Collections.ArrayList contido;
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
