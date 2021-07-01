using System;

/// <summary>
/// 3ª entidade criada a partir do relacionamento entre a entidade User e a entidade UserType, tem como objectivo gerir e armazenar os perfis dos Users
/// Relacionamentos
/// Relacionamento com a entidade User. Um profile pode ser contido por 1 User e um User pode conter 1 ou muitos Profile.
/// Relacionamento com a entidade UserType. Um Profile pode conter um UserType  e um user type pode conter 0 ou muitos profiles.
/// </summary>
public class Profile {

    #region Properties

    /// <summary>
    /// data de criação do prefil
    /// requesitos:
    /// deve ser uma data valida
    /// restrições:
    /// não pode conter letras
    /// </summary>
    private DateTime dateCreated;
	public DateTime DateCreated 
    {
		get 
        {
			return dateCreated;
		}
		set 
        {
			dateCreated = value;
		}
	}

    /// <summary> PK FK
    /// numero de identificação do objecto
    /// requisitos :
    ///  deve conter pelo menos um numero
    /// restrições :
    ///  não pode ser um numero racional
    /// </summary>
	private User userEscolhido;
    public User UserEscolhido
    {
        get
        {
            return userEscolhido;
        }
        set
        {
            userEscolhido = value;
        }
    }

    /// <summary> PK FK
    /// numero de identificação do objecto
    /// requisitos :
    ///  deve conter pelo menos um numero
    /// restrições :
    ///  não pode ser um numero racional
    /// </summary>
	private UserType userType;
    public UserType TipoUser
    {
        get
        {
            return userType;
        }
        set
        {
            userType = value;
        }
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor completo 
    /// </summary>
    /// <param name="dateCreated"> data de criação do prefil </param>
    /// <param name="contido"></param>
    /// <param name="userType"></param>
    public Profile(DateTime dateCreated, User contido, UserType userType)
    {
        DateCreated = dateCreated;
        this.userEscolhido = contido;
        this.userType = userType;
    }

    /// <summary>
    /// Construtor identitário
    /// </summary>
    /// <param name="contido"></param>
    /// <param name="userType"></param>
    public Profile(User contido, UserType userType)
    {
        this.userEscolhido = contido;
        this.userType = userType;
    }

    #endregion

}
