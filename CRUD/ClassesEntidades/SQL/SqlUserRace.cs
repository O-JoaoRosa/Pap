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
    class SqlUserRace
    {

        #region Dados Locais

        private const bool DEBUG_LOCAL = false;       // Ativa debug local

        #endregion

        #region Create

        /// <summary>
        /// Adiciona um novo registo à tabela
        /// </summary>
        /// <param name="userRace"></param>
        static public void Add(UserRace userRace)
        {
            // Imprime DEBUG para a consola, se DEBUG local e GERAL estiverem ativos
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserRace - add() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLuserRace - add() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "INSERT INTO userRace"
                            + "(UserID,RaceTrackID,FinishPosition,MoneyMade,ReputationMade,DateRace)"
                            + "VALUES(@userID, @raceTrackID, @finishPosition, @moneyNade , @reputationMade, @dateRace);";
                        sqlCommand.Parameters.Add(new MySqlParameter("@userID", userRace.User.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@raceTrackID", userRace.RaceTrack.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@finishPosition", userRace.FinishPosition));
                        sqlCommand.Parameters.Add(new MySqlParameter("@moneyNade", userRace.MoneyMade));
                        sqlCommand.Parameters.Add(new MySqlParameter("@reputationMade", userRace.ReputationMade));
                        sqlCommand.Parameters.Add(new MySqlParameter("@dateRace", userRace.DateRace));

                        // Tenta Executar e Se diferente de 1, provoca excessão saltanto para o catch
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            throw new InvalidProgramException("SQLuserRace - add() - mysql: ");
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLuserRace_mors - add() - \n" + e.ToString());
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
        static public List<UserRace> GetAll()
        {
            List<UserRace> listaUserRaces = new List<UserRace>();   // Lista Principal
            String query = "";

            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserRace - getAll() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLuserRace - getAll() - DBMS ATIVO: " + DBMS_ACTIVE);
            }

            //Execução do SQL DML sob controlo do try catch
            try
            {
                // Abre ligação ao DBMS Ativo
                using (DbConnection conn = OpenConnection())
                {
                    query = "SELECT * FROM userRace;";

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
                            Console.WriteLine("Debug: SQLuserRace - getAll() - MYSQL - SQLcommand OK");
                        }

                        // Reader recebe os dados da execução da query
                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (DEBUG_LOCAL)
                            {
                                Console.WriteLine("Debug: SQLuserRace - getAll() - MYSQL - DATAREADER CRIADO");
                            }

                            // Extração dos dados do reader para a lista, um a um: registo tabela -> new Obj ->Lista<Objs>
                            while (reader.Read())
                            {
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine("Debug: SQLuserRace - getAll(): MYSQL - DATAREADER TEM REGISTOS!");
                                }

                                // Construção do objeto
                                // Se objeto tem FKs, Não usar SQL***.get() para construir o fk dentro do construtor. gera exceção.
                                // Criar o obj FK com o Construtor de Id e depois completar o objeto fora do domínio da Connection.
                                UserRace use = new UserRace(reader.GetInt32(reader.GetOrdinal("ID")),
                                    reader.GetInt32(reader.GetOrdinal("FinishPosition")),
                                    reader.GetInt32(reader.GetOrdinal("MoneyMade")),
                                    reader.GetInt32(reader.GetOrdinal("ReputationMade")),
                                    reader.GetDateTime(reader.GetOrdinal("DateRace")),
                                    new RaceTrack(reader.GetInt32(reader.GetOrdinal("RaceTrackID"))),
                                    new User(reader.GetInt32(reader.GetOrdinal("UserID")))
                                );

                                listaUserRaces.Add(use);       //adiciona o obj à lista

                                //Debug para Output: Interessa ver o que está a sair do datareader
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine(
                                        "Debug: SQLuserRace - getAll() - DataReader - MYSQL:"
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
                Console.WriteLine("Erro: SQLuserRace - getAll() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLuserRace - GetAll() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLuserRace - GetAll() - Catch",  // Título
                    MessageBoxButton.OK,            // Botões
                    MessageBoxImage.Error           // Icon
                );
                return null;
            }

            return listaUserRaces;
        }

        /// <summary>
        /// Obtem um registo completo da tabela através do seu id, converte para obj e devolve.
        /// Há várias ligações - Processo:
        /// 1 - Ligação BD - Extrai o registo BD e preenche um Objeto.
        /// 2 - Completa o objeto, construindo os obj FK existentes
        /// </summary>
        /// <returns>Devolve um objeto preenchido ou NULL</returns>
        static public UserRace Get(int id)
        {
            UserRace userRace = null;

            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserRace - get() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLuserRace - get() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "SELECT * FROM UserRace where ID=@Id";
                        sqlCommand.Parameters.Add(new MySqlParameter("@Id", id));

                        // Reader recebe os dados da execução da query
                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (DEBUG_LOCAL)
                            {
                                Console.WriteLine("Debug: SQLuserRace - get() - MYSQL - DataReader CRIADO: ");
                            }

                            // Extração dos dados do reader para a lista, um a um: registo tabela -> new Obj ->Lista<Objs>
                            if (reader.Read())
                            {
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine("Debug: SQLuserRace - get() MYSQL: DATAREADER TEM REGISTO!");
                                }

                                // Construção do objeto
                                // Se objeto tem FKs, Não usar SQL***.get() para construir o fk dentro do construtor. gera exceção.
                                // Criar o obj FK com o Construtor de Id e depois completar o objeto fora do domínio da Connection.
                                UserRace use = new UserRace(reader.GetInt32(reader.GetOrdinal("ID")),
                                    reader.GetInt32(reader.GetOrdinal("FinishPosition")),
                                    reader.GetInt32(reader.GetOrdinal("MoneyMade")),
                                    reader.GetInt32(reader.GetOrdinal("ReputationMade")),
                                    reader.GetDateTime(reader.GetOrdinal("DateRace")),
                                    new RaceTrack(reader.GetInt32(reader.GetOrdinal("RaceTrackID"))),
                                    new User(reader.GetInt32(reader.GetOrdinal("UserID")))
                                );
                                userRace = use;

                                //Debug para Output: Interessa ver o que está a sair do datareader
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine(
                                        "Debug: SQLuserRace - get() - DataReader - MYSQL:"
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
                Console.WriteLine("Erro: SQLuserRace - get() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLuserRace - Get() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLuserRace - Get() - Catch", // Título
                    MessageBoxButton.OK,        // Botões
                    MessageBoxImage.Error       // Icon
                );
                return null;
            }

            return userRace;
        }

        #endregion

        #region Update

        /// <summary>
        /// Altera um registo da tabela
        /// </summary>
        /// <param name="userRace">Objeto com id a alterar da tabela</param>
        static public void Set(UserRace userRace)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserRace - set() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLuserRace - set() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "UPDATE userRace SET "
                        + " FinishPosition = @finishPosition,"
                        + " MoneyMade = @moneyMade,"
                        + " ReputationMade = @reputationMade,"
                        + " DateRace = @dateRace,"
                        + " UserID = @userID,"
                        + " RaceTrackID = @raceTrackID"
                        + " WHERE ID = @id;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@finishPosition", userRace.FinishPosition));
                        sqlCommand.Parameters.Add(new MySqlParameter("@moneyMade", userRace.MoneyMade));
                        sqlCommand.Parameters.Add(new MySqlParameter("@reputationMade", userRace.ReputationMade));
                        sqlCommand.Parameters.Add(new MySqlParameter("@dateRace", userRace.DateRace));
                        sqlCommand.Parameters.Add(new MySqlParameter("@userID", userRace.User.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@raceTrackID", userRace.RaceTrack.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@id", userRace.ID));

                        // Tenta executar o comando, que é suposto devolver 1
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            // Se diferente, inverte o commit e Provoca a excessão saltanto para o catch
                            throw new InvalidProgramException("SQLuserRace - set() - mysql: ");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLuserRace - set() - \n" + e.ToString());
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
        /// <param name="userRace">Objeto com id a apagar da tabela</param>
        static public void Del(UserRace userRace)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserRace - del() - <--Iniciar Query-->");
                Console.WriteLine("Debug: SQLuserRace - del() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "DELETE FROM userRace WHERE ID = @id;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@id", userRace.ID));

                        // Tenta executar o comando, que é suposto devolver 1
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            // Se diferente, inverte o commit e Provoca a excessão saltanto para o catch
                            throw new InvalidProgramException("SQLuserRace - del() - mysql: ");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLuserRace - del() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLuserRace - Del() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLuserRace - Del() - Catch", // Título
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
        /// <param name="userRace">Registo a testar</param>
        /// <returns></returns>
        static public bool CheckRelationalIntegrityViolation(UserRace userRace)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuserRace - checkRelationalIntegrityViolation() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLuserRace - checkRelationalIntegrityViolation() - DBMS ATIVO: " + DBMS_ACTIVE);
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////
            // Controlo de Violação de Inegridade Relacional:
            // Verifica se o registo em delete, existe nas tabelas relacionadas (com FK para esta tabela)
            // Analisar no DER as tabelas a tratar: MATRICULA
            ////////////////////////////////////////////////////////////////////////////////////////////////
            StringBuilder strBuilderFK = new StringBuilder();    // Recebe a info onde há violação de integridade
            strBuilderFK.AppendLine("Para eliminar este registo, precisa primeiro de eliminar os seus movimentos em:");

            // Flag de controlo de violação de interidade, para ativar as mensagens na FormAuxuliarInfo
            bool relationalViolationForFKtables = false;   // ativa-se quando o userRace é fk em tabelas relacionadas

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
