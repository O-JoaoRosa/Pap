using System.Collections.Generic;

/// <summary>
/// Entidade que gere e armazena os dados dos PoweUps dos carros e tem como objectivo atribuir efeitos aos PowerUps
/// Relacionamentos:
/// relacionamento com a entidade Car. Um PowerUp é contido por 0 ou 1 Car e um Car  contem 0 ou 1 PowerUp
/// </summary>
public class PowerUp {

    #region Properties

    /// <summary> PK
    /// numero de identificação do objecto
    /// requisitos :
    ///  deve conter pelo menos um numero
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
	/// descrição do power up
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
	private string imageUrl;
	public string ImageUrl
	{
		get
		{
			return imageUrl;
		}
		set
		{
			imageUrl = value;
		}
	}

	/// <summary>
	/// variavel que irá defenir quanto tempo o power up demorará a poder ser usado
	/// requisitos
	/// deve ser maior que 0
	/// restrições
	///  não pode ser mais que 120
	/// </summary>
	private int timeCharge;
	public int TimeCharge 
	{
		get 
		{
			return timeCharge;
		}
		set 
		{
			timeCharge = value;
		}
	}

	/// <summary>
	/// lista de UserCars
	/// </summary>
	private List<UserCar> listaUserCars;
	public List<UserCar> ListaUserCars { get => listaUserCars; set => listaUserCars = value; }

	#endregion

	#region Constructor

	/// <summary>
	/// Construtor Completo
	/// </summary>
	/// <param name="id"> numero de identificação do objecto </param>
	/// <param name="descri"> descrição do power up </param>
	/// <param name="imageUrl"> Nome de codigo do modelo 3d ao qual a roda estará associada </param>
	/// <param name="timeCharge">  variavel que irá defenir quanto tempo o power up demorará a poder ser usado </param>
	public PowerUp(int id, string descri, string imageUrl, int timeCharge)
	{
		Id = id;
		Descri = descri;
		ImageUrl = imageUrl;
		TimeCharge = timeCharge;
	}

	/// <summary>
	/// Construtor identitário
	/// </summary>
	/// <param name="id"> numero de identificação do objecto  </param>
	public PowerUp(int id)
	{
		Id = id;
	}

	#endregion

}
