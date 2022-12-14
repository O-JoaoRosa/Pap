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
    class SqlUserServerState
    {
        #region Dados Locais

        private const bool DEBUG_LOCAL = false;       // Ativa debug local

        #endregion

        #region Create

        /// <summary>
        /// Adiciona um novo registo à tabela
        /// </summary>
        /// <param name="serverUserState"></param>
        static public void Add(ServerUserState serverUserState)
        {
            // Imprime DEBUG para a consola, se DEBUG local e GERAL estiverem ativos
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLserverUserState - add() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLserverUserState - add() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "INSERT INTO serveruserstate"
                            + "(Descri)"
                            + "VALUES(@descri);";
                        sqlCommand.Parameters.Add(new MySqlParameter("@descri", serverUserState.Descri));

                        // Tenta Executar e Se diferente de 1, provoca excessão saltanto para o catch
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            throw new InvalidProgramException("SQLserverUserState - add() - mysql: ");
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLserverUserState_mors - add() - \n" + e.ToString());
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
        static public List<ServerUserState> GetAll( int order, string from, string until, int nPag, int nItens)
        {
            List<ServerUserState> listaServerUserStates = new List<ServerUserState>();   // Lista Principal
            String query = "";

            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLserverUserState - getAll() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLserverUserState - getAll() - DBMS ATIVO: " + DBMS_ACTIVE);
            }

            //Execução do SQL DML sob controlo do try catch
            try
            {
                // Abre ligação ao DBMS Ativo
                using (DbConnection conn = OpenConnection())
                {
                    query = "SELECT * FROM serveruserstate";
                    if (from != null || until != null)
                    {
                        query += " WHERE Descri >= '" + from + "' AND Descri <= '" + until + "~'";
                    }
                    switch (order)
                    {
                        case LIST_DESCRI_ASC:
                            query += " ORDER BY Descri ASC";
                            break;

                        case LIST_DESCRI_DESC:
                            query += " ORDER BY Descri DESC";
                            break;
                    }

                    query += " LIMIT @nPag, @nItens;";

                    // Prepara e executa o SQL DML
                    using (MySqlCommand sqlCommand = new MySqlCommand())
                    {
                        // Config da ligação
                        sqlCommand.Parameters.Add(new MySqlParameter("@nPag", (nPag - 1) * nItens));
                        sqlCommand.Parameters.Add(new MySqlParameter("@nItens", nItens));
                        sqlCommand.CommandText = query;
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.Connection = ((MySqlConnection)conn);

                        // DEBUG
                        if (DEBUG_LOCAL)
                        {
                            Console.WriteLine("Debug: SQLserverUserState - getAll() - MYSQL - SQLcommand OK");
                        }

                        // Reader recebe os dados da execução da query
                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (DEBUG_LOCAL)
                            {
                                Console.WriteLine("Debug: SQLserverUserState - getAll() - MYSQL - DATAREADER CRIADO");
                            }

                            // Extração dos dados do reader para a lista, um a um: registo tabela -> new Obj ->Lista<Objs>
                            while (reader.Read())
                            {
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine("Debug: SQLserverUserState - getAll(): MYSQL - DATAREADER TEM REGISTOS!");
                                }

                                // Construção do objeto
                                // Se objeto tem FKs, Não usar SQL***.get() para construir o fk dentro do construtor. gera exceção.
                                // Criar o obj FK com o Construtor de Id e depois completar o objeto fora do domínio da Connection.
                                ServerUserState serverUserState = new ServerUserState(
                                    reader.GetInt32(reader.GetOrdinal("ID")),
                                    reader["Descri"].ToString()

                                );

                                listaServerUserStates.Add(serverUserState);       //adiciona o obj à lista

                                //Debug para Output: Interessa ver o que está a sair do datareader
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine(
                                        "Debug: SQLserverUserState - getAll() - DataReader - MYSQL:"
                                        + "\n Id->" + reader.GetInt32(reader.GetOrdinal("ID")).ToString()
                                    );
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLserverUserState - getAll() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLserverUserState - GetAll() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLserverUserState - GetAll() - Catch",  // Título
                    MessageBoxButton.OK,            // Botões
                    MessageBoxImage.Error           // Icon
                );
                return null;
            }

            return listaServerUserStates;
        }

        /// <summary>
        /// Obtem um registo completo da tabela através do seu id, converte para obj e devolve.
        /// Há várias ligações - Processo:
        /// 1 - Ligação BD - Extrai o registo BD e preenche um Objeto.
        /// 2 - Completa o objeto, construindo os obj FK existentes
        /// </summary>
        /// <returns>Devolve um objeto preenchido ou NULL</returns>
        static public ServerUserState Get(int id)
        {
            ServerUserState serverUserState = null;

            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLserverUserState - get() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLserverUserState - get() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "SELECT * FROM serveruserstate where ID=@Id;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@id", id));

                        // Reader recebe os dados da execução da query
                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (DEBUG_LOCAL)
                            {
                                Console.WriteLine("Debug: SQLserverUserState - get() - MYSQL - DataReader CRIADO: ");
                            }

                            // Extração dos dados do reader para a lista, um a um: registo tabela -> new Obj ->Lista<Objs>
                            if (reader.Read())
                            {
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine("Debug: SQLserverUserState - get() MYSQL: DATAREADER TEM REGISTO!");
                                }

                                // Construção do objeto
                                // Se objeto tem FKs, Não usar SQL***.get() para construir o fk dentro do construtor. gera exceção.
                                // Criar o obj FK com o Construtor de Id e depois completar o objeto fora do domínio da Connection.
                                ServerUserState use = new ServerUserState(
                                   reader.GetInt32(reader.GetOrdinal("ID")),
                                   reader["Descri"].ToString()
                               );

                                serverUserState = use;

                                //Debug para Output: Interessa ver o que está a sair do datareader
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine(
                                        "Debug: SQLserverUserState - get() - DataReader - MYSQL:"
                                        + "\n Id->" + reader.GetInt32(reader.GetOrdinal("ID")).ToString()
                                        + "\n Descri-> " + reader["Descri"].ToString()
                                    );
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLserverUserState - get() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLserverUserState - Get() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLserverUserState - Get() - Catch", // Título
                    MessageBoxButton.OK,        // Botões
                    MessageBoxImage.Error       // Icon
                );
                return null;
            }

            return serverUserState;
        }

        #endregion

        #region Update

        /// <summary>
        /// Altera um registo da tabela
        /// </summary>
        /// <param name="serverUserState">Objeto com id a alterar da tabela</param>
        static public void Set(ServerUserState serverUserState)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLserverUserState - set() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLserverUserState - set() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "UPDATE serveruserstate SET "
                        + " Descri = @descri"
                        + " WHERE Id = @id;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@id", serverUserState.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@descri", serverUserState.Descri));

                        // Tenta executar o comando, que é suposto devolver 1
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            // Se diferente, inverte o commit e Provoca a excessão saltanto para o catch
                            throw new InvalidProgramException("SQLserverUserState - set() - mysql: ");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLserverUserState - set() - \n" + e.ToString());
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
        /// <param name="serverUserState">Objeto com id a apagar da tabela</param>
        static public void Del(ServerUserState serverUserState)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLserverUserState - del() - <--Iniciar Query-->");
                Console.WriteLine("Debug: SQLserverUserState - del() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "DELETE FROM serveruserstate WHERE ID=@id;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@id", serverUserState.Id));

                        // Tenta executar o comando, que é suposto devolver 1
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            // Se diferente, inverte o commit e Provoca a excessão saltanto para o catch
                            throw new InvalidProgramException("SQLserverUserState - del() - mysql: ");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLserverUserState - del() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLserverUserState - Del() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLserverUserState - Del() - Catch", // Título
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
        /// <param name="serverUserState">Registo a testar</param>
        /// <returns></returns>
        static public bool CheckRelationalIntegrityViolation(ServerUserState serverUserState)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLserverUserState - checkRelationalIntegrityViolation() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLserverUserState - checkRelationalIntegrityViolation() - DBMS ATIVO: " + DBMS_ACTIVE);
            }


            ////////////////////////////////////////////////////////////////////////////////////////////////
            // Controlo de Violação de Inegridade Relacional:
            // Verifica se o registo em delete, existe nas tabelas relacionadas (com FK para esta tabela)
            // Analisar no DER as tabelas a tratar: MATRICULA
            ////////////////////////////////////////////////////////////////////////////////////////////////
            StringBuilder strBuilderFK = new StringBuilder();    // Recebe a info onde há violação de integridade
            strBuilderFK.AppendLine("Para eliminar este registo, precisa primeiro de eliminar os seus movimentos em:");

            // Flag de controlo de violação de interidade, para ativar as mensagens na FormAuxuliarInfo
            bool relationalViolationForFKtables = false;   // ativa-se quando o user é fk em tabelas relacionadas

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
