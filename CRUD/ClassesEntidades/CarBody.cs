using System.Collections.Generic;

/// <summary>
/// Entidade que gere e armazena os dados do chasi dos carros e tem como objectivo dar um valor (preço) a cada chasi
/// Relacionamentos:
/// relacionamento com a entidade Car. Um CarBody é contido por 0 ou muitos Car e um Car  contem um CarBody apenas.
/// </summary>
public class CarBody {

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
	/// preço do CarBody
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
	/// Nome de codigo do modelo 3d ao qual o CarBody estará associada
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

	/// <summary>
	/// lista de carros e users que estão associados a esta entidade
	/// </summary>
	private List<UserCar> listaUserCars;
	public List<UserCar> ListaUserCars { get => listaUserCars; set => listaUserCars = value; }

	#endregion

	#region Consreuctor

	/// <summary>
	/// Constructor Completo
	/// </summary>
	/// <param name="id"> numero de identificação do objecto </param>
	/// <param name="price"> preço do CarBody </param>
	/// <param name="paint"> valor cor da pintura </param>
	/// <param name="descri"> descrição da RaceTrack </param>
	/// <param name="codeName"> Nome de codigo do modelo 3d ao qual a roda estará associada </param>
	public CarBody(int id, int price, string paint, string descri, string codeName)
	{
		Id = id;
		Price = price;
		Paint = paint;
		Descri = descri;
		CodeName = codeName;
	}

	/// <summary>
	/// Constructor Identitário
	/// </summary>
	/// <param name="id"> numero de identificação do objecto </param>
	public CarBody(int id)
	{
		Id = id;
	}

	#endregion
}
