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
    class SqlRaceTrack
    {
        #region Dados Locais

        private const bool DEBUG_LOCAL = false;       // Ativa debug local

        #endregion

        #region Create

        /// <summary>
        /// Adiciona um novo registo à tabela
        /// </summary>
        /// <param name="raceTrack"></param>
        static public void Add(RaceTrack raceTrack)
        {
            // Imprime DEBUG para a consola, se DEBUG local e GERAL estiverem ativos
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLraceTrack - add() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLraceTrack - add() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "INSERT INTO racetrack"
                            + "(Descri,BaseReputationReward,BaseMoneyReward,ReputationRequiered)"
                            + "VALUES(@descri, @baseReputationReward, @baseMoneyReward, @reputationRequiered);";
                        sqlCommand.Parameters.Add(new MySqlParameter("@reputationRequiered", raceTrack.ReputationRequiered));
                        sqlCommand.Parameters.Add(new MySqlParameter("@baseMoneyReward", raceTrack.BaseMoneyReward));
                        sqlCommand.Parameters.Add(new MySqlParameter("@baseReputationReward", raceTrack.BaseReputationReward));
                        sqlCommand.Parameters.Add(new MySqlParameter("@descri", raceTrack.Descri));

                        // Tenta Executar e Se diferente de 1, provoca excessão saltanto para o catch
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            throw new InvalidProgramException("SQLraceTrack - add() - mysql: ");
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLraceTrack_mors - add() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLraceTrack - Add() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLraceTrack - Add() - Catch", // Título
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
        static public List<RaceTrack> GetAll(int order)
        {
            List<RaceTrack> listaRaceTracks = new List<RaceTrack>();   // Lista Principal
            String query = "";

            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLraceTrack - getAll() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLraceTrack - getAll() - DBMS ATIVO: " + DBMS_ACTIVE);
            }

            //Execução do SQL DML sob controlo do try catch
            try
            {
                // Abre ligação ao DBMS Ativo
                using (DbConnection conn = OpenConnection())
                {
                    query = "SELECT * FROM racetrack";
                    switch (order)
                    {
                        case LIST_REPREQ_ASC:
                            query += " ORDER BY ReputationRequiered ASC;";
                            break;

                        case LIST_REPREQ_DESC:
                            query += " ORDER BY ReputationRequiered DESC;";
                            break;

                        case LIST_DESCRI_ASC:
                            query += " ORDER BY Descri ASC;";
                            break;

                        case LIST_DESCRI_DESC:
                            query += " ORDER BY Descri DESC;";
                            break;

                        default:
                            query += ";";
                            break;
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
                            Console.WriteLine("Debug: SQLraceTrack - getAll() - MYSQL - SQLcommand OK");
                        }

                        // Reader recebe os dados da execução da query
                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (DEBUG_LOCAL)
                            {
                                Console.WriteLine("Debug: SQLraceTrack - getAll() - MYSQL - DATAREADER CRIADO");
                            }

                            // Extração dos dados do reader para a lista, um a um: registo tabela -> new Obj ->Lista<Objs>
                            while (reader.Read())
                            {
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine("Debug: SQLraceTrack - getAll(): MYSQL - DATAREADER TEM REGISTOS!");
                                }

                                // Construção do objeto
                                // Se objeto tem FKs, Não usar SQL***.get() para construir o fk dentro do construtor. gera exceção.
                                // Criar o obj FK com o Construtor de Id e depois completar o objeto fora do domínio da Connection.
                                RaceTrack raceTrack = new RaceTrack(
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader["Descri"].ToString(),
                                    reader.GetInt32(reader.GetOrdinal("ReputationRequiered")),
                                    reader.GetInt32(reader.GetOrdinal("BaseMoneyReward")),
                                    reader.GetInt32(reader.GetOrdinal("BaseReputationReward"))

                                );

                                listaRaceTracks.Add(raceTrack);       //adiciona o obj à lista

                                //Debug para Output: Interessa ver o que está a sair do datareader
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine(
                                        "Debug: SQLraceTrack - getAll() - DataReader - MYSQL:"
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
                Console.WriteLine("Erro: SQLraceTrack - getAll() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLuser - GetAll() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLuser - GetAll() - Catch",  // Título
                    MessageBoxButton.OK,            // Botões
                    MessageBoxImage.Error           // Icon
                );
                return null;
            }

            return listaRaceTracks;
        }

        /// <summary>
        /// Obtem um registo completo da tabela através do seu id, converte para obj e devolve.
        /// Há várias ligações - Processo:
        /// 1 - Ligação BD - Extrai o registo BD e preenche um Objeto.
        /// 2 - Completa o objeto, construindo os obj FK existentes
        /// </summary>
        /// <returns>Devolve um objeto preenchido ou NULL</returns>
        static public RaceTrack Get(int id)
        {
            RaceTrack racetrack = null;

            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLraceTrack - get() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLraceTrack - get() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "SELECT * FROM racetrack where Id=@id;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@id", id));

                        // Reader recebe os dados da execução da query
                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (DEBUG_LOCAL)
                            {
                                Console.WriteLine("Debug: SQLraceTrack - get() - MYSQL - DataReader CRIADO: ");
                            }

                            // Extração dos dados do reader para a lista, um a um: registo tabela -> new Obj ->Lista<Objs>
                            if (reader.Read())
                            {
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine("Debug: SQLraceTrack - get() MYSQL: DATAREADER TEM REGISTO!");
                                }

                                // Construção do objeto
                                // Se objeto tem FKs, Não usar SQL***.get() para construir o fk dentro do construtor. gera exceção.
                                // Criar o obj FK com o Construtor de Id e depois completar o objeto fora do domínio da Connection.
                                RaceTrack raceTrack = new RaceTrack(
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader["Descri"].ToString(),
                                    reader.GetInt32(reader.GetOrdinal("ReputationRequiered")),
                                    reader.GetInt32(reader.GetOrdinal("BaseMoneyReward")),
                                    reader.GetInt32(reader.GetOrdinal("BaseReputationReward"))
                               );
                                racetrack = raceTrack;

                                //Debug para Output: Interessa ver o que está a sair do datareader
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine(
                                        "Debug: SQLraceTrack - get() - DataReader - MYSQL:"
                                        + "\n Id->" + reader.GetInt32(reader.GetOrdinal("Id")).ToString()
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
                Console.WriteLine("Erro: SQLraceTrack - get() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLuser - Get() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLuser - Get() - Catch", // Título
                    MessageBoxButton.OK,        // Botões
                    MessageBoxImage.Error       // Icon
                );
                return null;
            }

            return racetrack;
        }

        #endregion

        #region Update

        /// <summary>
        /// Altera um registo da tabela
        /// </summary>
        /// <param name="raceTrack">Objeto com id a alterar da tabela</param>
        static public void Set(RaceTrack raceTrack)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLraceTrack - set() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLraceTrack - set() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "UPDATE raceTrack SET "
                        + " Descri = @descri,"
                        + " ReputationRequiered = @reputationRequiered,"
                        + " BaseMoneyReward = @baseMoneyReward,"
                        + " BaseReputationReward = @baseReputationReward"
                        + " WHERE Id = @id;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@id", raceTrack.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@descri", raceTrack.Descri));
                        sqlCommand.Parameters.Add(new MySqlParameter("@reputationRequiered", raceTrack.ReputationRequiered));
                        sqlCommand.Parameters.Add(new MySqlParameter("@baseMoneyReward", raceTrack.BaseMoneyReward));
                        sqlCommand.Parameters.Add(new MySqlParameter("@baseReputationReward", raceTrack.BaseReputationReward));

                        // Tenta executar o comando, que é suposto devolver 1
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            // Se diferente, inverte o commit e Provoca a excessão saltanto para o catch
                            throw new InvalidProgramException("SQLraceTrack - set() - mysql: ");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLraceTrack - set() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLraceTrack - Set() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLraceTrack - Set() - Catch",     // Título
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
        /// <param name="raceTrack">Objeto com id a apagar da tabela</param>
        static public void Del(RaceTrack raceTrack)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLraceTrack - del() - <--Iniciar Query-->");
                Console.WriteLine("Debug: SQLraceTrack - del() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "DELETE FROM raceTrack WHERE Id=@id;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@id", raceTrack.Id));

                        // Tenta executar o comando, que é suposto devolver 1
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            // Se diferente, inverte o commit e Provoca a excessão saltanto para o catch
                            throw new InvalidProgramException("SQLraceTrack - del() - mysql: ");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLraceTrack - del() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLraceTrack - Del() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLraceTrack - Del() - Catch", // Título
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
        /// <param name="raceTrack">Registo a testar</param>
        /// <returns></returns>
        static public bool CheckRelationalIntegrityViolation(RaceTrack raceTrack)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLraceTrack - checkRelationalIntegrityViolation() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLraceTrack - checkRelationalIntegrityViolation() - DBMS ATIVO: " + DBMS_ACTIVE);
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
