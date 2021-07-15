using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Windows;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;

namespace CRUD.ClassesEntidades.SQL
{
    class SqlUserCar
    {


        #region Dados Locais

        private const bool DEBUG_LOCAL = false;       // Ativa debug local

        #endregion

        #region Create

        /// <summary>
        /// Adiciona um novo registo à tabela
        /// </summary>
        /// <param name="userCar"></param>
        static public void Add(UserCar userCar)
        {
            // Imprime DEBUG para a consola, se DEBUG local e GERAL estiverem ativos
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserCar - add() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLuserCar - add() - DBMS ATIVO: " + DBMS_ACTIVE);
            }

            //Execução do SQL DML sob controlo do try catch
            try
            {
                // Abre ligação ao DBMS Ativo
                using (DbConnection conn = OpenConnection())
                {

                    // Prepara e executa o SQL DML
                    using (MySqlCommand sqlCommand = ((MySqlConnection)conn).CreateCommand())
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "INSERT INTO userCar"
                            + "(UserID,CarID,PowerUpID,WheelID,CarBodyID,DateUnlocked,IsUnlocked)"
                            + "VALUES(@userID, @CarID, @PowerUpID, @WheelID, @CarBodyID, @DateUnlocked, @IsUnlocked);";
                        sqlCommand.Parameters.Add(new MySqlParameter("@userID", userCar.User.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@CarID", userCar.Car.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@PowerUpID", userCar.PowerUp.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@WheelID", userCar.Roda.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@CarBodyID", userCar.CarBody.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@DateUnlocked", userCar.DateUnlocked));
                        sqlCommand.Parameters.Add(new MySqlParameter("@IsUnlocked", userCar.IsUnlocked));

                        // Tenta Executar e Se diferente de 1, provoca excessão saltanto para o catch
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            throw new InvalidProgramException("SQLuserCar - add() - mysql: ");
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLuserCar_mors - add() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLturma - Add() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLturma - Add() - Catch", // Título
                    MessageBoxButton.OK,        // Botões
                    MessageBoxImage.Information // Icon
                );
            }
        }
        #endregion

        #region Read

        /// <summary>
        /// Recolhe todos os registos da tabela, converte para obj e adiciona numa lista a devolver
        /// Há várias ligações - Processo:
        /// 1 - Ligação BD - Extrai os registos BD para a lista principal
        /// 2 - Completa a lista principal, preenchendo os obj FK, 
        /// </summary>
        /// <returns>Lista de objetos</returns>
        static public List<UserCar> GetAll(string fromUserName, string untilUserName, string fromCar, string untilCar)
        {
            List<UserCar> listaUserCars = new List<UserCar>();   // Lista Principal
            String query = "";

            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserCar - getAll() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLuserCar - getAll() - DBMS ATIVO: " + DBMS_ACTIVE);
            }

            //Execução do SQL DML sob controlo do try catch
            try
            {
                // Abre ligação ao DBMS Ativo
                using (DbConnection conn = OpenConnection())
                {
                    query = "SELECT * FROM usercar";
                    if (fromUserName != null || untilUserName != null || fromCar != null || untilCar != null)
                    {
                        query += " INNER JOIN user ON usercar.UserID = user.ID\n" +
                            "INNER JOIN car ON usercar.CarID = Car.ID"
                            + " AND user.UserName >= '" + fromUserName + "' AND user.UserName <= '" + untilUserName +
                            "~' AND Car.Descri >= '" + fromCar + "' AND Car.Descri <= '" + untilCar + "~'";
                    }

                    // Prepara e executa o SQL DML
                    using (MySqlCommand sqlCommand = new MySqlCommand())
                    {
                        // Config da ligação
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.Connection = ((MySqlConnection)conn);

                        // DEBUG
                        if (DEBUG_LOCAL)
                        {
                            Console.WriteLine("Debug: SQLuserCar - getAll() - MYSQL - SQLcommand OK");
                        }

                        // Reader recebe os dados da execução da query
                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (DEBUG_LOCAL)
                            {
                                Console.WriteLine("Debug: SQLuserCar - getAll() - MYSQL - DATAREADER CRIADO");
                            }

                            // Extração dos dados do reader para a lista, um a um: registo tabela -> new Obj ->Lista<Objs>
                            while (reader.Read())
                            {
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine("Debug: SQLuserCar - getAll(): MYSQL - DATAREADER TEM REGISTOS!");
                                }

                                // Construção do objeto
                                // Se objeto tem FKs, Não usar SQL***.get() para construir o fk dentro do construtor. gera exceção.
                                // Criar o obj FK com o Construtor de Id e depois completar o objeto fora do domínio da Connection.
                                UserCar use = new UserCar(reader.GetDateTime(reader.GetOrdinal("DateUnlocked")),
                                    reader.GetBoolean(reader.GetOrdinal("IsUnlocked")),
                                    new Car(reader.GetInt32(reader.GetOrdinal("CarID"))),
                                    new User(reader.GetInt32(reader.GetOrdinal("UserID"))),
                                    new Wheel(reader.GetInt32(reader.GetOrdinal("WheelID"))),
                                    new CarBody(reader.GetInt32(reader.GetOrdinal("CarBodyID"))),
                                    new PowerUp(reader.GetInt32(reader.GetOrdinal("PowerUpID")))
                                );

                                listaUserCars.Add(use);       //adiciona o obj à lista

                                //Debug para Output: Interessa ver o que está a sair do datareader
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine(
                                        "Debug: SQLuserCar - getAll() - DataReader - MYSQL:"
                                        + "\n Id->" + reader.GetInt32(reader.GetOrdinal("Id")).ToString()
                                    );
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLuserCar - getAll() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLuserCar - GetAll() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLuserCar - GetAll() - Catch",  // Título
                    MessageBoxButton.OK,            // Botões
                    MessageBoxImage.Error           // Icon
                );
                return null;
            }

            return listaUserCars;
        }

        /// <summary>
        /// Obtem um registo completo da tabela através do seu id, converte para obj e devolve.
        /// Há várias ligações - Processo:
        /// 1 - Ligação BD - Extrai o registo BD e preenche um Objeto.
        /// 2 - Completa o objeto, construindo os obj FK existentes
        /// </summary>
        /// <returns>Devolve um objeto preenchido ou NULL</returns>
        static public UserCar Get(int id1, int id2)
        {
            UserCar userCar = null;

            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserCar - get() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLuserCar - get() - DBMS ATIVO: " + DBMS_ACTIVE);
            }

            //Execução do SQL DML sob controlo do try catch
            try
            {
                // Abre ligação ao DBMS Ativo
                using (DbConnection conn = OpenConnection())
                {

                    // Prepara e executa o SQL DML
                    using (MySqlCommand sqlCommand = new MySqlCommand())
                    {
                        // Config da ligação
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.Connection = ((MySqlConnection)conn);

                        // SQL DDL
                        sqlCommand.CommandText = "SELECT * FROM UserCar where UserID=@userId AND CarID=@carID;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@userId", id1));
                        sqlCommand.Parameters.Add(new MySqlParameter("@carID", id2));

                        // Reader recebe os dados da execução da query
                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (DEBUG_LOCAL)
                            {
                                Console.WriteLine("Debug: SQLuserCar - get() - MYSQL - DataReader CRIADO: ");
                            }

                            // Extração dos dados do reader para a lista, um a um: registo tabela -> new Obj ->Lista<Objs>
                            if (reader.Read())
                            {
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine("Debug: SQLuserCar - get() MYSQL: DATAREADER TEM REGISTO!");
                                }

                                // Construção do objeto
                                // Se objeto tem FKs, Não usar SQL***.get() para construir o fk dentro do construtor. gera exceção.
                                // Criar o obj FK com o Construtor de Id e depois completar o objeto fora do domínio da Connection.
                                UserCar use = new UserCar(reader.GetDateTime(reader.GetOrdinal("DateUnlocked")),
                                    reader.GetBoolean(reader.GetOrdinal("IsUnlocked")),
                                    new Car(reader.GetInt32(reader.GetOrdinal("CarID"))),
                                    new User(reader.GetInt32(reader.GetOrdinal("UserID"))),
                                    new Wheel(reader.GetInt32(reader.GetOrdinal("WheelID"))),
                                    new CarBody(reader.GetInt32(reader.GetOrdinal("CarBodyID"))),
                                    new PowerUp(reader.GetInt32(reader.GetOrdinal("PowerUpID")))
                                );

                                userCar = use;

                                //Debug para Output: Interessa ver o que está a sair do datareader
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine(
                                        "Debug: SQLuserCar - get() - DataReader - MYSQL:"
                                        + "\n Id->" + reader.GetInt32(reader.GetOrdinal("Id")).ToString()
                                        + "\n Descri-> " + reader["Descri"].ToString()
                                        + "\n Obs-> " + reader["Obs"].ToString()
                                    );
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLuserCar - get() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLuserCar - Get() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLuserCar - Get() - Catch", // Título
                    MessageBoxButton.OK,        // Botões
                    MessageBoxImage.Error       // Icon
                );
                return null;
            }

            return userCar;
        }

        #endregion

        #region Update

        /// <summary>
        /// Altera um registo da tabela
        /// </summary>
        /// <param name="userCar">Objeto com id a alterar da tabela</param>
        static public void Set(UserCar userCar, int newUserID, int newCarID)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserCar - set() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLuserCar - set() - DBMS ATIVO: " + DBMS_ACTIVE);
            }

            //Execução do SQL DML sob controlo do try catch
            try
            {
                // Abre ligação ao DBMS Ativo
                using (DbConnection conn = OpenConnection())
                {
                    // Prepara e executa o SQL DML
                    using (MySqlCommand sqlCommand = ((MySqlConnection)conn).CreateCommand())
                    {
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "UPDATE userCar SET "
                        + " CarID = @newCarID,"
                        + " UserID = @newUserID,"
                        + " WheelID= @wheelID,"
                        + " CarBodyID = @carBodyID,"
                        + " PowerUpID = @powerUpID,"
                        + " IsUnlocked = @isUnlocked,"
                        + " DateUnlocked = @dateUnlocked,"
                        + " WHERE UserID = @userID AND CarID = @carID;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@carID", userCar.Car.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@userID", userCar.User.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@wheelID", userCar.Roda.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@carBodyID", userCar.CarBody.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@powerUpID", userCar.PowerUp.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@isUnlocked", userCar.IsUnlocked));
                        sqlCommand.Parameters.Add(new MySqlParameter("@dateUnlocked", userCar.DateUnlocked));
                        sqlCommand.Parameters.Add(new MySqlParameter("@newUserID", newUserID));
                        sqlCommand.Parameters.Add(new MySqlParameter("@newCarID", newCarID));

                        // Tenta executar o comando, que é suposto devolver 1
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            // Se diferente, inverte o commit e Provoca a excessão saltanto para o catch
                            throw new InvalidProgramException("SQLuserCar - set() - mysql: ");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLuserCar - set() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLServer - Set() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLServer - Set() - Catch",     // Título
                    MessageBoxButton.OK,            // Botões
                    MessageBoxImage.Error           // Icon
                );
            }
        }

        #endregion

        #region Delete
        /// <summary>
        /// Delete de um registo da tabela. 
        /// ATENÇÃO: Porque estes objetos são FK noutras tabelas, o delete aplica-se após 
        /// o método checkRelationalIntegrityViolation(), caso contrário pode gerar Exceções
        /// </summary>
        /// <param name="userCar">Objeto com id a apagar da tabela</param>
        static public void Del(UserCar userCar)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserCar - del() - <--Iniciar Query-->");
                Console.WriteLine("Debug: SQLuserCar - del() - DBMS ATIVO: " + DBMS_ACTIVE);
            }

            //Execução do SQL DML sob controlo do try catch
            try
            {
                // Abre ligação ao DBMS Ativo
                using (DbConnection conn = OpenConnection())
                {
                    using (MySqlCommand sqlCommand = ((MySqlConnection)conn).CreateCommand())
                    {
                        // Prepara e executa o SQL DML
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText = "DELETE FROM userCar WHERE UserID = @userID AND CarID = @carID;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@userID", userCar.User.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@carID", userCar.Car.Id));

                        // Tenta executar o comando, que é suposto devolver 1
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            // Se diferente, inverte o commit e Provoca a excessão saltanto para o catch
                            throw new InvalidProgramException("SQLuserCar - del() - mysql: ");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLuserCar - del() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLuserCar - Del() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLuserCar - Del() - Catch", // Título
                    MessageBoxButton.OK,        // Botões
                    MessageBoxImage.Error       // Icon
                );
            }
        }

        /// <summary>
        /// Controlo de Violação de Integridade Relacional. 
        /// Aplica-se antes do del(). 
        /// A não utilização em PAR destes métodos, vai gerar Exceções
        /// </summary>
        /// <param name="userCar">Registo a testar</param>
        /// <returns></returns>
        static public bool CheckRelationalIntegrityViolation(UserCar userCar)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserCar - checkRelationalIntegrityViolation() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLuserCar - checkRelationalIntegrityViolation() - DBMS ATIVO: " + DBMS_ACTIVE);
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////
            // Controlo de Violação de Inegridade Relacional:
            // Verifica se o registo em delete, existe nas tabelas relacionadas (com FK para esta tabela)
            // Analisar no DER as tabelas a tratar: MATRICULA
            ////////////////////////////////////////////////////////////////////////////////////////////////
            StringBuilder strBuilderFK = new StringBuilder();    // Recebe a info onde há violação de integridade
            strBuilderFK.AppendLine("Para eliminar este registo, precisa primeiro de eliminar os seus movimentos em:");

            // Flag de controlo de violação de interidade, para ativar as mensagens na FormAuxuliarInfo
            bool relationalViolationForFKtables = false;   // ativa-se quando o userCar é fk em tabelas relacionadas

            int count;  // Acumula o nº de ocorrências positivas:

            if (relationalViolationForFKtables)
            {
                MessageBox.Show(
                    strBuilderFK.ToString(),            // Corpo da msg
                    "Violação Integridade relacional",  // Título
                    MessageBoxButton.OK,                // Botões
                    MessageBoxImage.Information         // Icon
                );
                return true;    // Há violação de integridade
            }
            return false;       // Não há violação de integridade
        }
        #endregion
    }
}
