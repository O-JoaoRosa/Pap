using System.Collections.Generic;

/// <summary>
/// Entidade UserType que tem como objectivo gerir e armazenar a informação sobre o tipo de utilizador que o User é.
/// Relacionamento com a entidade Profile. Um UserType é contido por 0 ou muitos Users, e um User pode conter 1 ou muitos UserType. Este relacionamento deu origem a uma 3ª entidade que tem como objectivo gerir e armazenar os perfis dos User.
/// </summary>
public class UserType {

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
	/// descrição do UserType
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

	/// <summary> FK
	/// linsta de profiles associados a este User Type
	/// </summary>
	private List<Profile> listaProfiles;
	public List<Profile> ListaProfiles { get => listaProfiles; set => listaProfiles = value; }

	#endregion

	#region Constructors

	/// <summary>
	///Construtor completo 
	/// </summary>
	/// <param name="id">numero de identificação do objecto</param>
	/// <param name="descri">descrição do UserType</param>
	public UserType(int id, string descri)
	{
		Id = id;
		Descri = descri;
	}

	/// <summary>
	/// Construtor Identitário
	/// </summary>
	/// <param name="id">numero de identificação do objecto</param>
	public UserType(int id)
	{
		Id = id;
	}

	#endregion

}
