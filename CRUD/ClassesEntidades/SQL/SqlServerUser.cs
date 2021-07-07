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
    class SqlServerUser
    {

        #region Dados Locais

        private const bool DEBUG_LOCAL = false;       // Ativa debug local

        #endregion

        #region Create

        /// <summary>
        /// Adiciona um novo registo à tabela
        /// </summary>
        /// <param name="serverUser"></param>
        static public void Add(ServerUser serverUser)
        {
            // Imprime DEBUG para a consola, se DEBUG local e GERAL estiverem ativos
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLserverUser - add() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLserverUser - add() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "INSERT INTO serverUser"
                            + "(UserID,ServerId,ServerUserStateID,AcesseDate,IsAccessible,DateCreated,DateSuspended,DateBan)"
                            + "VALUES(@userID, @serverId, @serverUserStateID, @acesseDate, @isAcessible, @dateCreated, @dateSuspended, @dateBan);";
                        sqlCommand.Parameters.Add(new MySqlParameter("@userID", serverUser.User.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@serverId", serverUser.Server.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@serverUserStateID", serverUser.ServerUserState.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@acesseDate", serverUser.AcesseDate));
                        sqlCommand.Parameters.Add(new MySqlParameter("@isAcessible", serverUser.IsAccessible));
                        sqlCommand.Parameters.Add(new MySqlParameter("@dateCreated", serverUser.DateCreated));
                        sqlCommand.Parameters.Add(new MySqlParameter("@dateSuspended", serverUser.DateSuspended));
                        sqlCommand.Parameters.Add(new MySqlParameter("@dateBan", serverUser.DateBan));

                        // Tenta Executar e Se diferente de 1, provoca excessão saltanto para o catch
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            throw new InvalidProgramException("SQLserverUser - add() - mysql: ");
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLserverUser_mors - add() - \n" + e.ToString());
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
        static public List<ServerUser> GetAll()
        {
            List<ServerUser> listaServerUsers = new List<ServerUser>();   // Lista Principal
            String query = "";

            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLserverUser - getAll() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLserverUser - getAll() - DBMS ATIVO: " + DBMS_ACTIVE);
            }

            //Execução do SQL DML sob controlo do try catch
            try
            {
                // Abre ligação ao DBMS Ativo
                using (DbConnection conn = OpenConnection())
                {
                    query = "SELECT * FROM serveruser;";

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
                            Console.WriteLine("Debug: SQLserverUser - getAll() - MYSQL - SQLcommand OK");
                        }

                        // Reader recebe os dados da execução da query
                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (DEBUG_LOCAL)
                            {
                                Console.WriteLine("Debug: SQLserverUser - getAll() - MYSQL - DATAREADER CRIADO");
                            }

                            // Extração dos dados do reader para a lista, um a um: registo tabela -> new Obj ->Lista<Objs>
                            while (reader.Read())
                            {
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine("Debug: SQLserverUser - getAll(): MYSQL - DATAREADER TEM REGISTOS!");
                                }

                                // Construção do objeto
                                // Se objeto tem FKs, Não usar SQL***.get() para construir o fk dentro do construtor. gera exceção.
                                // Criar o obj FK com o Construtor de Id e depois completar o objeto fora do domínio da Connection.
                                ServerUser use = new ServerUser(reader.GetDateTime(reader.GetOrdinal("AcesseDate")),
                                    reader.GetBoolean(reader.GetOrdinal("IsAccessible")),
                                    reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                                    reader.GetDateTime(reader.GetOrdinal("DateSuspended")),
                                    reader.GetDateTime(reader.GetOrdinal("DateBan")),
                                    new ServerUserState(reader.GetInt32(reader.GetOrdinal("ServerUserStateID"))),
                                    new User(reader.GetInt32(reader.GetOrdinal("UserID"))),
                                    new Server(reader.GetInt32(reader.GetOrdinal("ServerID")))
                                );

                                listaServerUsers.Add(use);       //adiciona o obj à lista

                                //Debug para Output: Interessa ver o que está a sair do datareader
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine(
                                        "Debug: SQLserverUser - getAll() - DataReader - MYSQL:"
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
                Console.WriteLine("Erro: SQLserverUser - getAll() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLserverUser - GetAll() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLserverUser - GetAll() - Catch",  // Título
                    MessageBoxButton.OK,            // Botões
                    MessageBoxImage.Error           // Icon
                );
                return null;
            }

            return listaServerUsers;
        }

        /// <summary>
        /// Obtem um registo completo da tabela através do seu id, converte para obj e devolve.
        /// Há várias ligações - Processo:
        /// 1 - Ligação BD - Extrai o registo BD e preenche um Objeto.
        /// 2 - Completa o objeto, construindo os obj FK existentes
        /// </summary>
        /// <returns>Devolve um objeto preenchido ou NULL</returns>
        static public ServerUser Get(int id1, int id2)
        {
            ServerUser serverUser = null;

            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLserverUser - get() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLserverUser - get() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "SELECT * FROM ServerUser where UserID=@userId AND ServerID=@ServerId;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@userId", id1));
                        sqlCommand.Parameters.Add(new MySqlParameter("@ServerId", id2));

                        // Reader recebe os dados da execução da query
                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (DEBUG_LOCAL)
                            {
                                Console.WriteLine("Debug: SQLserverUser - get() - MYSQL - DataReader CRIADO: ");
                            }

                            // Extração dos dados do reader para a lista, um a um: registo tabela -> new Obj ->Lista<Objs>
                            if (reader.Read())
                            {
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine("Debug: SQLserverUser - get() MYSQL: DATAREADER TEM REGISTO!");
                                }

                                // Construção do objeto
                                // Se objeto tem FKs, Não usar SQL***.get() para construir o fk dentro do construtor. gera exceção.
                                // Criar o obj FK com o Construtor de Id e depois completar o objeto fora do domínio da Connection.
                                ServerUser use = new ServerUser(reader.GetDateTime(reader.GetOrdinal("AcesseDate")),
                                    reader.GetBoolean(reader.GetOrdinal("IsAccessible")),
                                    reader.GetDateTime(reader.GetOrdinal("DateCreated")),
                                    reader.GetDateTime(reader.GetOrdinal("DateSuspended")),
                                    reader.GetDateTime(reader.GetOrdinal("DateBan")),
                                    new ServerUserState(reader.GetInt32(reader.GetOrdinal("ServerUserStateID"))),
                                    new User(reader.GetInt32(reader.GetOrdinal("UserID"))),
                                    new Server(reader.GetInt32(reader.GetOrdinal("ServerID")))
                                );

                                serverUser = use;

                                //Debug para Output: Interessa ver o que está a sair do datareader
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine(
                                        "Debug: SQLserverUser - get() - DataReader - MYSQL:"
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
                Console.WriteLine("Erro: SQLserverUser - get() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLserverUser - Get() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLserverUser - Get() - Catch", // Título
                    MessageBoxButton.OK,        // Botões
                    MessageBoxImage.Error       // Icon
                );
                return null;
            }

            return serverUser;
        }

        #endregion

        #region Update

        /// <summary>
        /// Altera um registo da tabela
        /// </summary>
        /// <param name="serverUser">Objeto com id a alterar da tabela</param>
        static public void Set(ServerUser serverUser, int newUserID, int newServerID)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLserverUser - set() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLserverUser - set() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "UPDATE serverUser SET "
                        + " DateCreated = @dateCreated,"
                        + " DateBan = @dateBan," 
                        + " IsAccessible = @isAcessible,"
                        + " AcesseDate = @acesseDate," 
                        + " DateSuspended = @dateSuspended,"
                        + " ServerUserStateID = @serverUserStateID,"
                        + " UserID = @newUserID,"
                        + " ServerID = @newUserTypeID"
                        + " WHERE UserID = @userID AND ServerID = @serverID;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@dateCreated", serverUser.DateCreated));
                        sqlCommand.Parameters.Add(new MySqlParameter("@dateBan", serverUser.DateBan));
                        sqlCommand.Parameters.Add(new MySqlParameter("@isAcessible", serverUser.IsAccessible));
                        sqlCommand.Parameters.Add(new MySqlParameter("@acesseDate", serverUser.AcesseDate));
                        sqlCommand.Parameters.Add(new MySqlParameter("@dateSuspended", serverUser.DateSuspended));
                        sqlCommand.Parameters.Add(new MySqlParameter("@userID", serverUser.User.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@serverID", serverUser.Server.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@serverUserStateID", serverUser.ServerUserState.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@newUserID", newUserID));
                        sqlCommand.Parameters.Add(new MySqlParameter("@newUserTypeID", newServerID));

                        // Tenta executar o comando, que é suposto devolver 1
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            // Se diferente, inverte o commit e Provoca a excessão saltanto para o catch
                            throw new InvalidProgramException("SQLserverUser - set() - mysql: ");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLserverUser - set() - \n" + e.ToString());
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
        /// <param name="serverUser">Objeto com id a apagar da tabela</param>
        static public void Del(ServerUser serverUser)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLserverUser - del() - <--Iniciar Query-->");
                Console.WriteLine("Debug: SQLserverUser - del() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "DELETE FROM serverUser WHERE UserID = @userID AND ServerID = @serverID;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@userID", serverUser.User.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@serverID", serverUser.Server.Id));

                        // Tenta executar o comando, que é suposto devolver 1
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            // Se diferente, inverte o commit e Provoca a excessão saltanto para o catch
                            throw new InvalidProgramException("SQLserverUser - del() - mysql: ");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLserverUser - del() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLserverUser - Del() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLserverUser - Del() - Catch", // Título
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
        /// <param name="serverUser">Registo a testar</param>
        /// <returns></returns>
        static public bool CheckRelationalIntegrityViolation(ServerUser serverUser)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLserverUser - checkRelationalIntegrityViolation() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLserverUser - checkRelationalIntegrityViolation() - DBMS ATIVO: " + DBMS_ACTIVE);
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////
            // Controlo de Violação de Inegridade Relacional:
            // Verifica se o registo em delete, existe nas tabelas relacionadas (com FK para esta tabela)
            // Analisar no DER as tabelas a tratar: MATRICULA
            ////////////////////////////////////////////////////////////////////////////////////////////////
            StringBuilder strBuilderFK = new StringBuilder();    // Recebe a info onde há violação de integridade
            strBuilderFK.AppendLine("Para eliminar este registo, precisa primeiro de eliminar os seus movimentos em:");

            // Flag de controlo de violação de interidade, para ativar as mensagens na FormAuxuliarInfo
            bool relationalViolationForFKtables = false;   // ativa-se quando o serverUser é fk em tabelas relacionadas

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
