using System.Collections.Generic;

/// <summary>
/// Entidade que gere e armazena os dados das rodas dos carros e tem como objectivo dar um valor (preço) a cada roda
/// Relacionamentos:
/// relacionamento com a entidade CarBody. Uma Wheel é contida por 0 ou muitos Car e um Car  contem uma wheel apenas.
/// </summary>
public class Wheel {

    #region Properties

    /// <summary> PK
    /// numero de identificação do objecto
    /// requisitos :
    /// deve conter pelo menos um numero
    /// restrições :
    ///  não pode ser um numero racional
    /// </summary>
    private int id;
	public int Id 
	{
		get 
		{
			return id;
		}
		set 
		{
			id = value;
		}
	}

	/// <summary>
	/// preço da roda
	/// requisitos
	/// deve conter um valor maior que 1
	/// restrições
	///  não pode ser maior que 99 999
	/// </summary>
	private int price;
	public int Price 
	{
		get 
		{
			return price;
		}
		set 
		{
			price = value;
		}
	}

	/// <summary>
	/// valor cor da pintura
	/// requisitos:
	/// deve ser um código hexadecimal válido
	/// restrições
	///  não pode ter um valor inválido
	/// </summary>
	private string paint;
	public string Paint 
	{
		get 
		{
			return paint;
		}
		set 
		{
			paint = value;
		}
	}

	/// <summary>
	/// descrição da RaceTrack
	/// requisitos
	///  deve conter uma descrição simples
	/// restrições
	///  não pode ser vazio
	/// </summary>
	private string descri;
	public string Descri 
	{
		get 
		{
			return descri;
		}
		set 
		{
			descri = value;
		}
	}

	/// <summary>
	/// Nome de codigo do modelo 3d ao qual a roda estará associada
	/// requisitos
	///  deve estar preenchido
	/// restrições
	/// não pode estar vazio
	/// </summary>
	private string codeName;
	public string CodeName 
	{
		get 
		{
			return codeName;
		}
		set 
		{
			codeName = value;
		}
	}

	/// <summary> FK
	/// Lista de UserCars que a roda esta associada
	/// </summary>
	private List<UserCar> listaUserCars;
	public List<UserCar> ListaUserCars { get => listaUserCars; set => listaUserCars = value; }

	#endregion

	#region Constructor

	/// <summary>
	/// Construtor Identitário
	/// </summary>
	/// <param name="id"></param>
	public Wheel(int id)
	{
		Id = id;
	}

	/// <summary>
	/// Construtor completo
	/// </summary>
	/// <param name="id"></param>
	/// <param name="price"></param>
	/// <param name="paint"></param>
	/// <param name="descri"></param>
	/// <param name="codeName"></param>
	public Wheel(int id, int price, string paint, string descri, string codeName)
	{
		Id = id;
		Price = price;
		Paint = paint;
		Descri = descri;
		CodeName = codeName;
	}

	#endregion
}
