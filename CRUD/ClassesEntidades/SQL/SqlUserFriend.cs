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
    class SqlUserFriend
    {
        #region Dados Locais

        private const bool DEBUG_LOCAL = false;       // Ativa debug local

        #endregion

        #region Create

        /// <summary>
        /// Adiciona um novo registo à tabela
        /// </summary>
        /// <param name="userFriend"></param>
        static public void Add(UserFriend userFriend)
        {
            // Imprime DEBUG para a consola, se DEBUG local e GERAL estiverem ativos
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserFriend - add() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLuserFriend - add() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "INSERT INTO userFriend"
                            + "(UserID,UserFriendID,IsOnline,DateAdded)"
                            + "VALUES(@userID, @userFriendID, @isOnline, @dateAdded);";
                        sqlCommand.Parameters.Add(new MySqlParameter("@userID", userFriend.User.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@userFriendID", userFriend.UserFriend1.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@isOnline", userFriend.IsOnline));
                        sqlCommand.Parameters.Add(new MySqlParameter("@dateAdded", userFriend.DateAdded));

                        // Tenta Executar e Se diferente de 1, provoca excessão saltanto para o catch
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            throw new InvalidProgramException("SQLuserFriend - add() - mysql: ");
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLuserFriend_mors - add() - \n" + e.ToString());
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
        static public List<UserFriend> GetAll(string fromUserName, string untilUserName, string fromUserFriend , string untilUserFriend)
        {
            List<UserFriend> listaUserFriends = new List<UserFriend>();   // Lista Principal
            String query = "";

            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserFriend - getAll() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLuserFriend - getAll() - DBMS ATIVO: " + DBMS_ACTIVE);
            }

            //Execução do SQL DML sob controlo do try catch
            try
            {
                // Abre ligação ao DBMS Ativo
                using (DbConnection conn = OpenConnection())
                {
                    query = "SELECT * FROM userFriend";
                    if (fromUserName != null || untilUserName != null || fromUserFriend != null || untilUserFriend != null )
                    {
                        query += " INNER JOIN user ON userFriend.UserID = user.ID\n" +
                            "INNER JOIN user as user2 ON userFriend.UserFriendID = user2.ID"
                            + " AND user.username >= '" + fromUserName + "' AND user.UserName <= '" + untilUserName +
                            "~' AND user2.username >= '" + fromUserFriend + "' AND user2.UserName <= '" + untilUserFriend + "~';";
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
                            Console.WriteLine("Debug: SQLuserFriend - getAll() - MYSQL - SQLcommand OK");
                        }

                        // Reader recebe os dados da execução da query
                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (DEBUG_LOCAL)
                            {
                                Console.WriteLine("Debug: SQLuserFriend - getAll() - MYSQL - DATAREADER CRIADO");
                            }

                            // Extração dos dados do reader para a lista, um a um: registo tabela -> new Obj ->Lista<Objs>
                            while (reader.Read())
                            {
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine("Debug: SQLuserFriend - getAll(): MYSQL - DATAREADER TEM REGISTOS!");
                                }

                                // Construção do objeto
                                // Se objeto tem FKs, Não usar SQL***.get() para construir o fk dentro do construtor. gera exceção.
                                // Criar o obj FK com o Construtor de Id e depois completar o objeto fora do domínio da Connection.
                                UserFriend use = new UserFriend(reader.GetBoolean(reader.GetOrdinal("IsOnline")),
                                    reader.GetDateTime(reader.GetOrdinal("DateAdded")),
                                    new User(reader.GetInt32(reader.GetOrdinal("UserFriendID"))),
                                    new User(reader.GetInt32(reader.GetOrdinal("UserID")))
                                );

                                listaUserFriends.Add(use);       //adiciona o obj à lista

                                //Debug para Output: Interessa ver o que está a sair do datareader
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine(
                                        "Debug: SQLuserFriend - getAll() - DataReader - MYSQL:"
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
                Console.WriteLine("Erro: SQLuserFriend - getAll() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLuserFriend - GetAll() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLuserFriend - GetAll() - Catch",  // Título
                    MessageBoxButton.OK,            // Botões
                    MessageBoxImage.Error           // Icon
                );
                return null;
            }

            return listaUserFriends;
        }

        /// <summary>
        /// Obtem um registo completo da tabela através do seu id, converte para obj e devolve.
        /// Há várias ligações - Processo:
        /// 1 - Ligação BD - Extrai o registo BD e preenche um Objeto.
        /// 2 - Completa o objeto, construindo os obj FK existentes
        /// </summary>
        /// <returns>Devolve um objeto preenchido ou NULL</returns>
        static public UserFriend Get(int userid, int userFriendId)
        {
            UserFriend userFriend = null;

            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserFriend - get() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLuserFriend - get() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "SELECT * FROM UserFriend where (UserID=@userId AND UserFriendID=@userFriendID) OR (UserFriendID=@userId AND UserID=@userFriendID);";
                        sqlCommand.Parameters.Add(new MySqlParameter("@userId", userid));
                        sqlCommand.Parameters.Add(new MySqlParameter("@userFriendID", userFriendId));

                        // Reader recebe os dados da execução da query
                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (DEBUG_LOCAL)
                            {
                                Console.WriteLine("Debug: SQLuserFriend - get() - MYSQL - DataReader CRIADO: ");
                            }

                            // Extração dos dados do reader para a lista, um a um: registo tabela -> new Obj ->Lista<Objs>
                            if (reader.Read())
                            {
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine("Debug: SQLuserFriend - get() MYSQL: DATAREADER TEM REGISTO!");
                                }

                                // Construção do objeto
                                // Se objeto tem FKs, Não usar SQL***.get() para construir o fk dentro do construtor. gera exceção.
                                // Criar o obj FK com o Construtor de Id e depois completar o objeto fora do domínio da Connection.
                                UserFriend use = new UserFriend(reader.GetBoolean(reader.GetOrdinal("IsOnline")),
                                    reader.GetDateTime(reader.GetOrdinal("DateAdded")),
                                    new User(reader.GetInt32(reader.GetOrdinal("UserFriendID"))),
                                    new User(reader.GetInt32(reader.GetOrdinal("UserID")))
                                );

                                userFriend = use;

                                //Debug para Output: Interessa ver o que está a sair do datareader
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine(
                                        "Debug: SQLuserFriend - get() - DataReader - MYSQL:"
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
                Console.WriteLine("Erro: SQLuserFriend - get() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLuserFriend - Get() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLuserFriend - Get() - Catch", // Título
                    MessageBoxButton.OK,        // Botões
                    MessageBoxImage.Error       // Icon
                );
                return null;
            }

            return userFriend;
        }

        #endregion

        #region Update

        /// <summary>
        /// Altera um registo da tabela
        /// </summary>
        /// <param name="userFriend">Objeto com id a alterar da tabela</param>
        static public void Set(UserFriend userFriend, int newUserID, int newServerID)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserFriend - set() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLuserFriend - set() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "UPDATE userFriend SET "
                        + " DateAdded = @dateAdded,"
                        + " IsOnline = @isOnline,"
                        + " UserID = @newUserID,"
                        + " UserFriendID = @newUserFriendID"
                        + " WHERE UserID = @userID AND UserFriendID = @userFriendID;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@dateAdded", userFriend.DateAdded));
                        sqlCommand.Parameters.Add(new MySqlParameter("@isOnline", userFriend.IsOnline));
                        sqlCommand.Parameters.Add(new MySqlParameter("@userID", userFriend.User.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@userFriendID", userFriend.UserFriend1.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@newUserID", newUserID));
                        sqlCommand.Parameters.Add(new MySqlParameter("@newUserFriendID", newServerID));

                        // Tenta executar o comando, que é suposto devolver 1
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            // Se diferente, inverte o commit e Provoca a excessão saltanto para o catch
                            throw new InvalidProgramException("SQLuserFriend - set() - mysql: ");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLuserFriend - set() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLServer - Set() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLServer - Set() - Catch",     // Título
                    MessageBoxButton.OK,             // Botões
                    MessageBoxImage.Error            // Icon
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
        /// <param name="userFriend">Objeto com id a apagar da tabela</param>
        static public void Del(UserFriend userFriend)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserFriend - del() - <--Iniciar Query-->");
                Console.WriteLine("Debug: SQLuserFriend - del() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "DELETE FROM userFriend WHERE UserID = @userID AND UserFriendID = @userFriendID;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@userID", userFriend.User.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@userFriendID", userFriend.UserFriend1.Id));

                        // Tenta executar o comando, que é suposto devolver 1
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            // Se diferente, inverte o commit e Provoca a excessão saltanto para o catch
                            throw new InvalidProgramException("SQLuserFriend - del() - mysql: ");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLuserFriend - del() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLuserFriend - Del() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLuserFriend - Del() - Catch", // Título
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
        /// <param name="userFriend">Registo a testar</param>
        /// <returns></returns>
        static public bool CheckRelationalIntegrityViolation(UserFriend userFriend)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserFriend - checkRelationalIntegrityViolation() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLuserFriend - checkRelationalIntegrityViolation() - DBMS ATIVO: " + DBMS_ACTIVE);
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////
            // Controlo de Violação de Inegridade Relacional:
            // Verifica se o registo em delete, existe nas tabelas relacionadas (com FK para esta tabela)
            // Analisar no DER as tabelas a tratar: MATRICULA
            ////////////////////////////////////////////////////////////////////////////////////////////////
            StringBuilder strBuilderFK = new StringBuilder();    // Recebe a info onde há violação de integridade
            strBuilderFK.AppendLine("Para eliminar este registo, precisa primeiro de eliminar os seus movimentos em:");

            // Flag de controlo de violação de interidade, para ativar as mensagens na FormAuxuliarInfo
            bool relationalViolationForFKtables = false;   // ativa-se quando o userFriend é fk em tabelas relacionadas

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
