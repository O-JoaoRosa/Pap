/// <summary>
/// Entidade UserConfig, tem como objectivo gerir as configurações  pessoais de cada User
/// Relacionamentos
/// Relacionam com a entidade User. Um UserConfig pode ser contido por 0 ou 1 User e um User pode conter 0 ou muitos UserConfig.
///  
/// </summary>
public class UserConfig {

    #region Properties

    /// <summary> pk
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
	/// descrição do UserConfigs
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
	/// valor da user config
	/// requisitos
	/// deve conter algum valor
	/// restrições
	///  pode ser um numero maior qu e100
	/// </summary>
	private int value;
	public int Value 
	{
		get
		{
			return value;
		}
		set 
		{
			this.value = value;
		}
	}

	/// <summary> FK
	/// numero de identificação do objecto
	/// requisitos :
	/// deve conter pelo menos um numero
	/// restrições :
	/// não pode ser um numero racional
	/// </summary>
	private User contido;
	public User Contido
	{
		get
		{
			return contido;
		}
		set
		{
			contido = value;
		}
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Construct completo
	/// </summary>
	/// <param name="id"> numero de identificação do objecto </param>
	/// <param name="descri"> descrição do UserConfigs </param>
	/// <param name="value"> valor da user config </param>
	/// <param name="contido"></param>
	public UserConfig(int id, string descri, int value, User contido)
	{
		Id = id;
		Descri = descri;
		Value = value;
		this.contido = contido;
	}

	/// <summary>
	/// Construct minimo
	/// </summary>
	/// <param name="id"> numero de identificação do objecto </param>
	/// <param name="descri"> descrição do UserConfigs </param>
	/// <param name="value"> valor da user config </param>
	public UserConfig(int id, string descri, int value)
	{
		Id = id;
		Descri = descri;
		Value = value;
	}

	/// <summary>
	/// Construct identitário
	/// </summary>
	/// <param name="id"> numero de identificação do objecto </param>
	public UserConfig(int id)
	{
		Id = id;
	}

	#endregion

}
