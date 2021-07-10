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
    class SqlWheel
    {
        #region Dados Locais

        private const bool DEBUG_LOCAL = false;       // Ativa debug local

        #endregion
        
        #region Create

        /// <summary>
        /// Adiciona um novo registo à tabela
        /// </summary>
        /// <param name="wheel"></param>
        static public void Add(Wheel wheel)
        {
            // Imprime DEBUG para a consola, se DEBUG local e GERAL estiverem ativos
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLwheel - add() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLwheel - add() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "INSERT INTO wheel"
                            + "(Descri,Price,Paint,CodeName)"
                            + "VALUES(@descri, @price, @paint, @codeName);";
                        sqlCommand.Parameters.Add(new MySqlParameter("@descri", wheel.Descri));
                        sqlCommand.Parameters.Add(new MySqlParameter("@price", wheel.Price));
                        sqlCommand.Parameters.Add(new MySqlParameter("@paint", wheel.Paint));
                        sqlCommand.Parameters.Add(new MySqlParameter("@codeName", wheel.CodeName));
                        // Tenta Executar e Se diferente de 1, provoca excessão saltanto para o catch
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            throw new InvalidProgramException("SQLwheel - add() - mysql: ");
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLwheel_mors - add() - \n" + e.ToString());
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
        static public List<Wheel> GetAll(int order)
        {
            List<Wheel> listaWheels = new List<Wheel>();   // Lista Principal
            String query = "";

            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLwheel - getAll() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLwheel - getAll() - DBMS ATIVO: " + DBMS_ACTIVE);
            }

            //Execução do SQL DML sob controlo do try catch
            try
            {
                // Abre ligação ao DBMS Ativo
                using (DbConnection conn = OpenConnection())
                {
                    query = "SELECT * FROM wheel";
                    switch (order)
                    {
                        case LIST_CODENAME_ASC:
                            query += " ORDER BY CodeName ASC;";
                            break;

                        case LIST_CODENAME_DESC:
                            query += " ORDER BY CodeName DESC;";
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
                            Console.WriteLine("Debug: SQLwheel - getAll() - MYSQL - SQLcommand OK");
                        }

                        // Reader recebe os dados da execução da query
                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (DEBUG_LOCAL)
                            {
                                Console.WriteLine("Debug: SQLwheel - getAll() - MYSQL - DATAREADER CRIADO");
                            }

                            // Extração dos dados do reader para a lista, um a um: registo tabela -> new Obj ->Lista<Objs>
                            while (reader.Read())
                            {
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine("Debug: SQLwheel - getAll(): MYSQL - DATAREADER TEM REGISTOS!");
                                }

                                // Construção do objeto
                                // Se objeto tem FKs, Não usar SQL***.get() para construir o fk dentro do construtor. gera exceção.
                                // Criar o obj FK com o Construtor de Id e depois completar o objeto fora do domínio da Connection.
                                Wheel wheel = new Wheel(
                                    reader.GetInt32(reader.GetOrdinal("Id")),
                                    reader.GetInt32(reader.GetOrdinal("Price")),
                                    reader["Paint"].ToString(),
                                    reader["Descri"].ToString(),
                                    reader["CodeName"].ToString()
                                );

                                listaWheels.Add(wheel);       //adiciona o obj à lista

                                //Debug para Output: Interessa ver o que está a sair do datareader
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine(
                                        "Debug: SQLwheel - getAll() - DataReader - MYSQL:"
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
                Console.WriteLine("Erro: SQLuser - getAll() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLwheel - GetAll() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLwheel - GetAll() - Catch",  // Título
                    MessageBoxButton.OK,            // Botões
                    MessageBoxImage.Error           // Icon
                );
                return null;
            }

            return listaWheels;
        }

        /// <summary>
        /// Obtem um registo completo da tabela através do seu id, converte para obj e devolve.
        /// Há várias ligações - Processo:
        /// 1 - Ligação BD - Extrai o registo BD e preenche um Objeto.
        /// 2 - Completa o objeto, construindo os obj FK existentes
        /// </summary>
        /// <returns>Devolve um objeto preenchido ou NULL</returns>
        static public Wheel Get(int id)
        {
            Wheel wheel = null;

            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLwheel - get() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLwheel - get() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "SELECT * FROM wheel where Id=@Id;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@id", id));

                        // Reader recebe os dados da execução da query
                        using (MySqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (DEBUG_LOCAL)
                            {
                                Console.WriteLine("Debug: SQLwheel - get() - MYSQL - DataReader CRIADO: ");
                            }

                            // Extração dos dados do reader para a lista, um a um: registo tabela -> new Obj ->Lista<Objs>
                            if (reader.Read())
                            {
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine("Debug: SQLwheel - get() MYSQL: DATAREADER TEM REGISTO!");
                                }

                                // Construção do objeto
                                // Se objeto tem FKs, Não usar SQL***.get() para construir o fk dentro do construtor. gera exceção.
                                // Criar o obj FK com o Construtor de Id e depois completar o objeto fora do domínio da Connection.
                                Wheel whee = new Wheel(
                                   reader.GetInt32(reader.GetOrdinal("Id")),
                                   reader.GetInt32(reader.GetOrdinal("Price")),
                                   reader["Paint"].ToString(),
                                   reader["Descri"].ToString(),
                                   reader["CodeName"].ToString()
                               );

                                wheel = whee;

                                //Debug para Output: Interessa ver o que está a sair do datareader
                                if (DEBUG_LOCAL)
                                {
                                    Console.WriteLine(
                                        "Debug: SQLwheel - get() - DataReader - MYSQL:"
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
                Console.WriteLine("Erro: SQLwheel - get() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLuser - Get() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLuser - Get() - Catch", // Título
                    MessageBoxButton.OK,        // Botões
                    MessageBoxImage.Error       // Icon
                );
                return null;
            }

            return wheel;
        }

        #endregion

        #region Update

        /// <summary>
        /// Altera um registo da tabela
        /// </summary>
        /// <param name="wheel">Objeto com id a alterar da tabela</param>
        static public void Set(Wheel wheel)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLwheel - set() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLwheel - set() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "UPDATE wheel SET "
                        + " Descri = @descri,"
                        + " CodeName = @codeName,"
                        + " Paint = @paint,"
                        + " Price = @price"
                        + " WHERE Id = @id;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@id", wheel.Id));
                        sqlCommand.Parameters.Add(new MySqlParameter("@descri", wheel.Descri));
                        sqlCommand.Parameters.Add(new MySqlParameter("@codeName", wheel.CodeName));
                        sqlCommand.Parameters.Add(new MySqlParameter("@paint", wheel.Paint));
                        sqlCommand.Parameters.Add(new MySqlParameter("@price", wheel.Price));

                        // Tenta executar o comando, que é suposto devolver 1
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            // Se diferente, inverte o commit e Provoca a excessão saltanto para o catch
                            throw new InvalidProgramException("SQLwheel - set() - mysql: ");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLwheel - set() - \n" + e.ToString());
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
        /// <param name="wheel">Objeto com id a apagar da tabela</param>
        static public void Del(Wheel wheel)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLwheel - del() - <--Iniciar Query-->");
                Console.WriteLine("Debug: SQLwheel - del() - DBMS ATIVO: " + DBMS_ACTIVE);
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
                        sqlCommand.CommandText = "DELETE FROM wheel WHERE Id=@id;";
                        sqlCommand.Parameters.Add(new MySqlParameter("@id", wheel.Id));

                        // Tenta executar o comando, que é suposto devolver 1
                        if (sqlCommand.ExecuteNonQuery() != 1)
                        {
                            // Se diferente, inverte o commit e Provoca a excessão saltanto para o catch
                            throw new InvalidProgramException("SQLwheel - del() - mysql: ");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro: SQLuser - del() - \n" + e.ToString());
                MessageBox.Show(
                    "SQLuser - Del() - \n Ocorreram problemas com a ligação à Base de Dados: \n" + e.ToString(),
                    "SQLuser - Del() - Catch", // Título
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
        /// <param name="wheel">Registo a testar</param>
        /// <returns></returns>
        static public bool CheckRelationalIntegrityViolation(Wheel wheel)
        {
            if (DEBUG_LOCAL)
            {
                Console.WriteLine("Debug: SQLuser - checkRelationalIntegrityViolation() - <----Iniciar Query---->");
                Console.WriteLine("Debug: SQLuser - checkRelationalIntegrityViolation() - DBMS ATIVO: " + DBMS_ACTIVE);
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
