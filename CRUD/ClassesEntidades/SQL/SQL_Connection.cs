using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Windows;

namespace Desktop___interfaces.ClassesEntidades
{
    class SQL_Connection
    {
        #region Dados Locais


        // Ativa debug local
        static private readonly bool DEBUG_LOCAL = false;

        #endregion

        #region Constantes Globais para comunicação

        // Constantes para definir as operações SQL DML
        public const int SQL_INSERT = 1;
        public const int SQL_UPDATE = 2;
        public const int SQL_DELETE = 3;

        // Constantes para definir os DBMS
        public const int DBMS_NULL = 0;
        public const int DBMS_IBERWEB = 1;
        public const int DBMS_TGPSI_WLAN = 2;
        public const int DBMS_TGPSI_LAN = 3;
        public const int DBMS_TGPSI_LOCAL = 4;
        public const int DBMS_TGPSI_CASA = 5;

        // Constantes para definir as Entidades
        public const int TABLE_EFFECT = 1;
        public const int TABLE_ITEM = 2;
        public const int TABLE_LEVEL = 3;
        public const int TABLE_NPC = 4;
        public const int TABLE_NPCPOWER = 5;
        public const int TABLE_POWER = 6;
        public const int TABLE_POWERLEVEL = 7;
        public const int TABLE_PROFILE = 8;
        public const int TABLE_RARITY = 9;
        public const int TABLE_USER = 10;
        public const int TABLE_USERCONFIGS = 11;
        public const int TABLE_USERITEM = 12;
        public const int TABLE_USERPOWER = 13;
        public const int TABLE_USERTYPE = 14;

        #endregion

        #region Vars Globais para Comunicação

        /// <summary>
        /// Regista o DBMS a usar durante o processo de execução SQL DML que se extende por 3 fases: 
        /// 1º  O menu ativa esta flag antes da chamada da form edit ou lista.
        /// 2º  Na form edit ou List, a flag SqlDml decide qual a classe e o método SQL_*** a ser executado
        /// 3º  As classes SQL_*** executam a SQL DML, no DBMS indicado por esta flag
        /// </summary>
        static public int DBMS_ACTIVE { get; set; } = DBMS_IBERWEB;

        /// <summary>
        /// Recebe o texto de erro da tentativa de ligação aos DBMSs 
        /// Que depois pode ser usada para pesquisar na net
        /// </summary>
        static public string ClipboardText { get; set; } = null;

        //////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Connectors para ligar à base de dados. Recebem as Connections Strings, definidas a seguir.
        //////////////////////////////////////////////////////////////////////////////////////////////////////
        static private MySqlConnection ConnectorToMysqlIBERWEB { get; set; } = null;
        static private MySqlConnection ConnectorToMysqlWLAN { get; set; } = null;
        static private MySqlConnection ConnectorToMysqlLAN { get; set; } = null;
        static private MySqlConnection ConnectorToMysqlLOCAL { get; set; } = null;
        static private MySqlConnection ConnectorToMysqlLOCALCASA { get; set; } = null;

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Connection Strings - Contêm o endereço e as credenciais de ligação aos DBMSs, para os Connectors
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // DBMS MySQL Nesta máquina
        static private String SqlConnStringMySqlIBERWEB { get; set; } = @"server=188.93.230.170; database=vigionpt_tgpsi05_jrosa; user id=vigio_t05jrosa; password=ti@Wi174; sslMode=none";

        // DBMS MySQL no WLAN 
        static private String SqlConnStringMySqlWLAN { get; set; } = @"server=193.236.61.178; database=TGPSI05_ivo; user id=tgpsi05_ivo; password=19~Eftu6; sslMode=none";

        // DBMS MySQL no LAN
        static private String SqlConnStringMySqlLAN { get; set; } = @"server=10.114.169.2; database=TGPSI05_ivo; user id=tgpsi05_ivo; password=19~Eftu6; sslMode=none";

        // DBMS MySQL no Servidor TGPSI da ESFD
        static private String SqlConnStringMySqlLOCAL { get; set; } = @"server=localhost; database=localpap; user id=root; password=tgpsi; sslMode=none";

        // DBMS MySQL no Servidor TGPSI da ESFD
        static private String SqlConnStringMySqlLOCALCASA { get; set; } = @"server=localhost; database=tgpsi05_ivo; user id=root; password=tgpsi; sslMode=none";

        #endregion

        #region SQL Connections

        /// <summary>
        /// Cria e devolve uma connection aberta para o DBMS ativo
        /// NOTA IMPORTANTE: Nada disto é necessário quando se tem apenas um DBMS
        /// </summary>
        /// <returns>Ligação preparada e aberta</returns>
        static public DbConnection OpenConnection()
        {
            // Termina as Ligações antigas primeiro.
            CloseAllConnections();

            // Criação de um objeto de ligação comum a todos os connectors a devolver a quem pedir uma ligação
            DbConnection connection = null;

            try
            {
                // Efetua a ligação para o DBMS ativo
                switch (DBMS_ACTIVE)
                {
                    case DBMS_IBERWEB:
                        // Prepara a Ligação SQLServer com a connectionString
                        ConnectorToMysqlIBERWEB = new MySqlConnection(SqlConnStringMySqlIBERWEB);

                        // tenta abrir a Ligação
                        ConnectorToMysqlIBERWEB.Open();

                        // Se abertura com sucesso, entrega a ligação ao objeto genérico. Caso contrário gera exceção
                        connection = ConnectorToMysqlIBERWEB;
                        break;

                    case DBMS_TGPSI_WLAN:
                        //inicializa a Ligação
                        ConnectorToMysqlWLAN = new MySqlConnection(SqlConnStringMySqlWLAN);

                        //tenta abrir a Ligação
                        ConnectorToMysqlWLAN.Open();

                        // Se abertura com sucesso, entrega a ligação ao objeto genérico. Caso contrário gera exceção
                        connection = ConnectorToMysqlWLAN;
                        break;

                    case DBMS_TGPSI_LAN:
                        //inicializa a Ligação
                        ConnectorToMysqlLAN = new MySqlConnection(SqlConnStringMySqlLAN);

                        //tenta abrir a Ligação
                        ConnectorToMysqlLAN.Open();

                        // Se abertura com sucesso, entrega a ligação ao objeto genérico. Caso contrário gera exceção
                        connection = ConnectorToMysqlLAN;
                        break;

                    case DBMS_TGPSI_LOCAL:
                        //inicializa a Ligação
                        ConnectorToMysqlLOCAL = new MySqlConnection(SqlConnStringMySqlLOCAL);

                        //tenta abrir a Ligação
                        ConnectorToMysqlLOCAL.Open();

                        // Se abertura com sucesso, entrega a ligação ao objeto genérico. Caso contrário gera exceção
                        connection = ConnectorToMysqlLOCAL;
                        break;
                    case DBMS_TGPSI_CASA:
                        //inicializa a Ligação
                        ConnectorToMysqlLOCALCASA = new MySqlConnection(SqlConnStringMySqlLOCALCASA);

                        //tenta abrir a Ligação
                        ConnectorToMysqlLOCALCASA.Open();

                        // Se abertura com sucesso, entrega a ligação ao objeto genérico. Caso contrário gera exceção
                        connection = ConnectorToMysqlLOCALCASA;
                        break;

                    case DBMS_NULL:
                        MessageBox.Show(
                            "Atenção ao programador, DBMS is NULL",
                            "SQL_Connection - OpenConnectio() - case DBMS_NULL",
                            MessageBoxButton.OK,
                            MessageBoxImage.Exclamation
                        );
                        break;

                    default:
                        MessageBox.Show(
                            "Atenção ao programador, DBMS desconhecido:\n" + DBMS_ACTIVE,
                            "SQL_Connection - OpenConnectio() - Default",
                            MessageBoxButton.OK,
                            MessageBoxImage.Exclamation
                        );

                        DBMS_ACTIVE = DBMS_NULL;
                        break;
                }
            }
            catch (Exception ex)
            {
                // Mostra a mensagem de erro numa MsgBox e associa o botão ok ao Clipboard. 
                // Desta forma o utilizador poderá colar o erro na google e pesquisar soluções

                string msgtext = "Não foi possível ligar à base de dados, com o seguinte erro:\n\n\""
                    + ex.Message
                    + "\"\n\nContacte o Administrador do sistema informático"
                    + "\nO botão OK irá copiar a mensagem de erro. Google it!";

                if (MessageBox.Show(msgtext, "DBMS NÃO DISPONÍVEL", MessageBoxButton.OK, MessageBoxImage.Stop) == MessageBoxResult.OK)
                {
                    // Como não se deve pode usar o ClipBord de forma direta a partir de uma thread, 
                    // copiamos a msg de erro para um atributo desta classe static e acedemos-lhe a partir da 
                    // thread principal após o encerramento da Thread 2
                    ClipboardText = ex.Message;
                };


                DBMS_ACTIVE = DBMS_NULL;
                return null;
            }

            return connection;
        }

        /// <summary>
        /// Fecha todas as Ligações com os DBMS
        /// </summary>
        /// <param name="dbmsNull">True: passa o DBMSactive para null</param>
        static public void CloseAllConnections()
        {
            //fecha ligação MySql IBERWEB
            if (ConnectorToMysqlIBERWEB != null)
            {
                ConnectorToMysqlIBERWEB.Close();
                ConnectorToMysqlIBERWEB = null;
            }

            //fecha ligação MySql WLAN
            if (ConnectorToMysqlWLAN != null)
            {
                ConnectorToMysqlWLAN.Close();
                ConnectorToMysqlWLAN = null;
            }

            //fecha ligação MySql LAN
            if (ConnectorToMysqlLAN != null)
            {
                ConnectorToMysqlLAN.Close();
                ConnectorToMysqlLAN = null;
            }

            //fecha ligação MySql Local
            if (ConnectorToMysqlLOCAL != null)
            {
                ConnectorToMysqlLOCAL.Close();
                ConnectorToMysqlLOCAL = null;
            }
        }

        #endregion
    }
}
