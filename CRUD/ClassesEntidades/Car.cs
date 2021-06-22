using System.Collections.Generic;

/// <summary>
/// Entidade que gere e armazena os dados dos carros, tem como objectivo atribuir valores destintos a cada carro para haver divercidade
/// relacionamentos:
/// Relacionamento com a entidade Wheel. Um car tem que conter uma roda e cada roda pode ter 0 ou muitos carros
/// Relacionamento com a entidade CarBody. Um Car tem que conter um CarBody e o CarBody deve conter 0 ou muitos Cars.
/// Relacionamento com a entidade PowerUp. Um Car é afetado por 0 ou 1 PowerUp e um PowerUp pode ser contido por 0 ou 1 Cars
/// Relcionamento com o User. Um Car pode ser desbloquado por 0 ou muitos users e um user pode desbloquear 1 ou muitos Cars. Este relacionamento vai gerar uma 3ª entidade com o nome de Garagem que vai gerir e armazenar o que carros estão associados a que users
/// </summary>
public class Car 
{

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
	/// descrição do Car
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
	/// atributo que especifica a quantidade de rep que é preciso para desbloquear o objecto
	/// requisitos
	/// deve ser um numero inteiro
	/// restrições
	///  não pode ser maior que 99 999
	/// </summary>
	private int reputationRequired;
	public int ReputationRequired 
	{
		get 
		{
			return reputationRequired;
		}
		set 
		{
			reputationRequired = value;
		}
	}

	/// <summary>
	/// preço nessecario para desbloquear o objecto
	/// requisitos
	/// deve conter pelo menos um caracter
	/// restrições
	///  não pode ser mais que 99 999
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
	/// variável que define qual é a velocidade máxima do carro
	/// requisitos
	/// deve ser pelo menos 100.0
	/// restrições 
	/// não pode ser mais que 250.0
	/// </summary>
	private double maxSpeed;
	public double MaxSpeed 
	{
		get
		{
			return maxSpeed;
		}
		set
		{
			maxSpeed = value;
		}
	}

	/// <summary>
	/// variável que definenn quanta velocidade o carro ganha por segundo
	/// requesitos
	/// deve ser pelo menos 5
	/// restrcições
	/// não pode ser mais que 50
	/// </summary>
	private double acceleration;
	public double Acceleration
	{
		get
		{
			return acceleration;
		}
		set 
		{
			acceleration = value;
		}
	}

	/// <summary>
	/// variável que define a facilidade de virar enquanto se faz drift
	/// requisitos
	/// tem que ter um valor
	/// restrições
	///  não pode ser maior que 50
	/// </summary>
	private int driftForce;
	public int DriftForce 
	{
		get 
		{
			return driftForce;
		}
		set 
		{
			driftForce = value;
		}
	}

	/// <summary>
	/// variável que define a facilidade de virar sem o drift
	/// requisitos
	/// tem que ter um valor
	/// restrições
	///  não pode ser um valor maior que 50
	/// </summary>
	private int mobility;
	public int Mobility 
	{
		get 
		{
			return mobility;
		}
		set 
		{
			mobility = value;
		}
	}

	/// <summary>
	/// lista de carros associados a esta entidade
	/// </summary>
	private List<Car> listaCars;
	public List<Car> ListaCars { get => listaCars; set => listaCars = value; }

	#endregion

	#region Constructors
	/// <summary>
	/// Construtor Identitário
	/// </summary>
	/// <param name="id"> numero de identificação do objecto </param>
	public Car(int id)
	{
		Id = id;
	}

	/// <summary>
	/// Construtor Completo
	/// </summary>
	/// <param name="id"> numero de identificação do objecto </param>
	/// <param name="descri"> descrição do Car </param>
	/// <param name="reputationRequired"> atributo que especifica a quantidade de rep que é preciso para desbloquear o objecto </param>
	/// <param name="price"> preço nessecario para desbloquear o objecto </param>
	/// <param name="maxSpeed"> variável que define qual é a velocidade máxima do carro </param>
	/// <param name="acceleration"> variável que definenn quanta velocidade o carro ganha por segundo </param>
	/// <param name="driftForce"> variável que define a facilidade de virar enquanto se faz drift </param>
	/// <param name="mobility"> variável que define a facilidade de virar sem o drift </param>
	public Car(int id, string descri, int reputationRequired, int price, double maxSpeed, double acceleration, int driftForce, int mobility)
	{
		Id = id;
		Descri = descri;
		ReputationRequired = reputationRequired;
		Price = price;
		MaxSpeed = maxSpeed;
		Acceleration = acceleration;
		DriftForce = driftForce;
		Mobility = mobility;
	}
	#endregion

}
