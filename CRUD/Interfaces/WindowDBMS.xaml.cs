using System.Data.Common;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;

namespace Desktop___interfaces.Interfaces

{
    /// <summary>
    /// Interaction logic for WindowDBMS.xaml
    /// </summary>
    public partial class WindowDBMS : Window
    {
        #region Dados Locais

        private const bool DEBUG_LOCAL = false;     // Ativa debug local se TRUE

        /// <summary>
        /// Objeto para guardar a cor vermelha escolhida para os botões
        /// Serve para a repôr após o fecho do teste de comunicação com o DBMS
        /// </summary>
        private Brush ButtonDBMSBackgroundBrushes { get; set; } = null;

        #endregion

        #region Construtor e Load

        public WindowDBMS()
        {
            InitializeComponent();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int Db = DBMS_ACTIVE - 1;
            DBComboBox.SelectedIndex = Db;
        }


        #region Testes BD Locais em modo ASSÍNCRONO

        /// <summary>
        /// Teste aos DBMDs em modo assíncrono para permitir a animação da ligação
        /// Funcionamento do Modo Assíncrono;
        /// Os programas podem ser executados em modo síncrono e ou assíncrono:
        /// - modo síncrono, todo o código é colocado dentro de um canal de processamento
        ///   chamado THREAD, onde é executado instrução a instrução. Se uma das instruções
        ///   demora muito tempo, a seguinte tem que esperar. No caso das interfaces, os 
        ///   gráficos são um problema porque precisam de estar sempre em execução e ativos.
        ///   Ora, se houver instruções no código que demorem muito tempo a ser executadas
        ///   ou que dependam de resposta do exterior, como é o caso das comunicações, os 
        ///   gráficos vão bloquear até que esse processamento termina. 
        /// - modo assíncrono: o programa usa várias Threads para execução paralela.
        ///   - Tudo o que é interface corre na THREAD principal
        ///   - Tudo o processamento pesado ou de comunicações corre em THREADs secundárias. 
        /// ---------------------------------------------------------------------------------
        /// Como Funciona:
        /// Todo o bloco de código que queremos que seja executado em assíncrono, é colocado
        /// dentro do argumento de um método especial: await Task.Run() {...}
        /// - tudo o que está fora do await Task.Run(), é executado na THREAD principal.
        /// - tudo o ques está dentro é executado numa nova THREAD secundária.
        /// ---------------------------------------------------------------------------------
        /// Neste caso, o método: ButtonBDtestSQLServerLocalBD_Click() é executado quando 
        /// se clica no botão para fazer a ligação, certo?
        /// 1º Até à chamada do método await Task.Run(), tudo funciona na thread principal. 
        ///    => é altura:
        ///     - desativar os botões
        ///     - Ativar a animação associada à "comunicação em curso";
        /// 2º Chama o método await Task.Run() que:
        ///     1º Coloca a thread principal em estado de espera (await), mas em execução.
        ///        Os gráficos, ou seja o que lá estiver, continuam a bombar;
        ///        Suspende o resto da execução do método ButtonBDtestSQLServerLocalBD_Click()
        ///     2º Cria uma segunda Thread para executar a ligação em paralelo e que pode 
        ///        demorar algum tempo;
        ///        Quando o processamento desta nova Thread termina, neste caso por timeout 
        ///        ou sucesso da ligação, a thread encerra e envia um sinal à thread principal; 
        /// 3º A thread principal recebe o sinal e desbloqueia o processamento do resto do 
        ///     código do método ButtonBDtestSQLServerLocalBD_Click(), ou seja, altera o estado 
        ///     dos botões e a animação de sucesso ou erro.
        /// 
        /// IMPORTANTE: todos os métodos que usam a chamado do método await Task.Run() têm que 
        ///             ter o identificador ASYNC.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonBDtestIBERWEB_Click(object sender, RoutedEventArgs e)
        {
            // THREAD 1
            // Desativa botões e substitui a gif através dos static Resources da main.XAML
            ButtonsStatus(false);

            // Cria um objeto genérico de Connection (Classe super das Connections) para 
            // receber o os dados da ligação que vai ser executada na THREAD 2
            DbConnection connectionIsSuccess = null;

            // Ativa o DBMS alvo
            DBMS_ACTIVE = DBMS_IBERWEB;

            // MODO ASSÍNCRONO: 
            // 1º AWAIT - Suspende o processamento deste método apenas.
            //            Mas tudo na Thread 1 continua a bombar como estava. 
            // 2º Cria nova THREAD para executar a ligação ao DBMS. 
            //    Ali dentro não se pode "mexer" nos objetos da THREAD 1, a não ser que tenham 
            //    características assíncrona (os gráficos não têm), sob pena de gerar exceção.
            //    O sucesso ou insucesso da ligação é devolvida ao bool connectionIsSuccess. 
            // 3º Termina a execução assíncrona:
            //    - Encerra a Thread 2 
            //    - Envia um sinal à thread 1 para continuar o processamento do resto do código
            //      deste método. Brilhante, não é? 
            await Task.Run(() =>
            {
                // Tentativa de abertura da ligação ao DBMS indicado por DBMS_ACTIVE 
                // O resultado é guardado no objeto DBconnection da THREAD 1, que tem propriedades assíncronas.
                connectionIsSuccess = OpenConnection();

            }); // fim da thread 2

            // THREAD 1: recebeu o sinal de desbloqueio => O resto do método é finalmente executado;
            // Repôe o estado nulo do DBMS_ACTIVE
            DBMS_ACTIVE = DBMS_NULL;

            // Altera os gráficos, em função do estado da ligação, atualizado na Thread 2.
            if (connectionIsSuccess != null)
            {
                // Reativa os botões e pinta o botão de verde 
                ButtonsStatus(true);
                ButtonBDtestIBERWEB.Background = Brushes.Green;

                // Ativa a imagem de sucesso

                // Repoe todas os outros botões na cor vermelha
                ButtonBDtestWLAN.Background = Brushes.Red;
                ButtonBDtestLAN.Background = Brushes.Red;
                ButtonBDtestLOCALESCOLA.Background = Brushes.Red;
                ButtonBDtestLOCALCASA.Background = Brushes.Red;
            }
            else
            {
                // repôe a imagem original

                // Copia mensagem de erro para o ClipBoard (mem). Ver porquê no catch de SQL_Connection.OpenConnection()
                Clipboard.SetText(ClipboardText);

                // Reativa os botões
                ButtonsStatus(true);
            }
        }

        /// <summary>
        /// Teste ao DBMS SQLServer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonBDtestWLAN_Click(object sender, RoutedEventArgs e)
        {
            // Desativa botões e substitui a gif através dos static Resources da main.XAML
            ButtonsStatus(false);

            // Ativa o DBMS alvo
            DBMS_ACTIVE = DBMS_TGPSI_WLAN;

            // tentativa de abertura da ligação em modo assíncrono
            DbConnection connectionIsSuccess = null;        // recebe o estado da ligação na thread 2
            await Task.Run(() =>                            // início da thread 2
            {
                // tentativa de abertura da ligação ao DBMS indicado pela constante definida em Settings
                connectionIsSuccess = OpenConnection();
            });                                             // fim da thread 2

            // Repoe o estado nulo do DBMS_ACTIVE
            DBMS_ACTIVE = DBMS_NULL;

            // Altera os gráficos, em função do estado da ligação, atualizado na Thread 2.
            if (connectionIsSuccess != null)
            {
                // Reativa os botões, pinta o botão de verde e ativa a imagem de sucesso
                ButtonsStatus(true);
                ButtonBDtestWLAN.Background = Brushes.Green;

                // Repoe todas os outros botões na cor vermelha
                ButtonBDtestIBERWEB.Background = Brushes.Red;
                ButtonBDtestLAN.Background = Brushes.Red;
                ButtonBDtestLOCALESCOLA.Background = Brushes.Red;
                ButtonBDtestLOCALCASA.Background = Brushes.Red;
            }
            else
            {
                // repôe a imagem original

                // Copia mensagem de erro para o ClipBoard. Ver porquê no catch de SQL_Connection.OpenConnection()
                Clipboard.SetText(ClipboardText);

                // Reativa os botões
                ButtonsStatus(true);
            }
        }

        /// <summary>
        /// Teste ao DBMS Mysql
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonBDtestLAN_Click(object sender, RoutedEventArgs e)
        {
            // Desativa botões e substitui a gif através dos static Resources da main.XAML
            ButtonsStatus(false);

            // Ativa o DBMS alvo
            DBMS_ACTIVE = DBMS_TGPSI_LAN;

            // tentativa de abertura da ligação em modo assíncrono
            DbConnection connectionIsSuccess = null;        // recebe o estado da ligação na thread 2
            await Task.Run(() =>                            // início da thread 2
            {
                // tentativa de abertura da ligação ao DBMS indicado pela constante definida em Settings
                connectionIsSuccess = OpenConnection();
            });                                             // fim da thread 2

            // Repoe o estado nulo do DBMS_ACTIVE
            DBMS_ACTIVE = DBMS_NULL;

            // Altera os gráficos, em função do estado da ligação, atualizado na Thread 2.
            if (connectionIsSuccess != null)
            {
                // Reativa os botões, pinta o botão de verde e ativa a imagem de sucesso
                ButtonsStatus(true);
                ButtonBDtestLAN.Background = Brushes.Green;

                // Repoe todas os outros botões na cor vermelha
                ButtonBDtestIBERWEB.Background = Brushes.Red;
                ButtonBDtestWLAN.Background = Brushes.Red;
                ButtonBDtestLOCALESCOLA.Background = Brushes.Red;
                ButtonBDtestLOCALCASA.Background = Brushes.Red;
            }
            else
            {
                // repôe a imagem original

                // Copia mensagem de erro para o ClipBoard. Ver porquê no catch de SQL_Connection.OpenConnection()
                Clipboard.SetText(ClipboardText);

                // Reativa os botões
                ButtonsStatus(true);
            }
        }

        /// <summary>
        /// Teste ao DBMS Mysql
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonBDtestLOCALESCOLA_Click(object sender, RoutedEventArgs e)
        {
            // Desativa botões e substitui a gif através dos static Resources da main.XAML
            ButtonsStatus(false);

            // Ativa o DBMS alvo
            DBMS_ACTIVE = DBMS_TGPSI_LOCAL;

            // tentativa de abertura da ligação em modo assíncrono
            DbConnection connectionIsSuccess = null;        // recebe o estado da ligação na thread 2
            await Task.Run(() =>                            // início da thread 2
            {
                // tentativa de abertura da ligação ao DBMS indicado pela constante definida em Settings
                connectionIsSuccess = OpenConnection();
            });                                             // fim da thread 2

            // Repoe o estado nulo do DBMS_ACTIVE
            DBMS_ACTIVE = DBMS_NULL;

            // Altera os gráficos, em função do estado da ligação, atualizado na Thread 2.
            if (connectionIsSuccess != null)
            {
                // Reativa os botões, pinta o botão de verde e ativa a imagem de sucesso
                ButtonsStatus(true);
                ButtonBDtestLOCALESCOLA.Background = Brushes.Green;

                // Repoe todas os outros botões na cor vermelha
                ButtonBDtestIBERWEB.Background = Brushes.Red;
                ButtonBDtestWLAN.Background = Brushes.Red;
                ButtonBDtestLAN.Background = Brushes.Red;
                ButtonBDtestLOCALCASA.Background = Brushes.Red;
            }
            else
            {
                // repôe a imagem original

                // Copia mensagem de erro para o ClipBoard. Ver porquê no catch de SQL_Connection.OpenConnection()
                Clipboard.SetText(ClipboardText);

                // Reativa os botões
                ButtonsStatus(true);
            }
        }

        /// <summary>
        /// Teste ao DBMS SQLite
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonBDtestLOCALCASA_Click(object sender, RoutedEventArgs e)
        {
            // Desativa botões e substitui a gif através dos static Resources da main.XAML
            ButtonsStatus(false);

            // Ativa o DBMS alvo
            DBMS_ACTIVE = DBMS_TGPSI_CASA;

            // tentativa de abertura da ligação em modo assíncrono
            DbConnection connectionIsSuccess = null;        // recebe o estado da ligação na thread 2
            await Task.Run(() =>                            // início da thread 2
            {
                // tentativa de abertura da ligação ao DBMS indicado pela constante definida em Settings
                connectionIsSuccess = OpenConnection();
            });                                             // fim da thread 2

            // Repoe o estado nulo do DBMS_ACTIVE
            DBMS_ACTIVE = DBMS_NULL;

            // Altera os gráficos, em função do estado da ligação, atualizado na Thread 2.
            if (connectionIsSuccess != null)
            {
                // Reativa os botões, pinta o botão de verde e ativa a imagem de sucesso
                ButtonsStatus(true);
                ButtonBDtestLOCALCASA.Background = Brushes.Green;

                // Repoe todas os outros botões na cor vermelha
                ButtonBDtestIBERWEB.Background = Brushes.Red;
                ButtonBDtestWLAN.Background = Brushes.Red;
                ButtonBDtestLAN.Background = Brushes.Red;
                ButtonBDtestLOCALESCOLA.Background = Brushes.Red;
            }
            else
            {
                // repôe a imagem original

                // Copia mensagem de erro para o ClipBoard. Ver porquê no catch de SQL_Connection.OpenConnection()
                Clipboard.SetText(ClipboardText);

                // Reativa os botões
                ButtonsStatus(true);
            }
        }

        /// <summary>
        /// Auxiliar para ativar ou desativar os botões enquanto a ligação de faz
        /// </summary>
        /// <param name="ativar"></param>
        private void ButtonsStatus(bool ativar)
        {
            if (ativar)
            {
                ButtonBDtestIBERWEB.IsEnabled = true;
                ButtonBDtestWLAN.IsEnabled = true;
                ButtonBDtestLAN.IsEnabled = true;
                ButtonBDtestLOCALESCOLA.IsEnabled = true;
                ButtonBDtestLOCALCASA.IsEnabled = true;
                ButtonConnect.IsEnabled = true;
                ButtonBack.IsEnabled = true;
            }
            else
            {
                ButtonBDtestIBERWEB.IsEnabled = false;
                ButtonBDtestWLAN.IsEnabled = false;
                ButtonBDtestLAN.IsEnabled = false;
                ButtonBDtestLOCALESCOLA.IsEnabled = false;
                ButtonBDtestLOCALCASA.IsEnabled = false;
                ButtonConnect.IsEnabled = false;
                ButtonBack.IsEnabled = false;
            }
        }

        #endregion

        #region DB Chosen Connection

        /// <summary>
        /// Vai ativar a DB que o utilizador escolheu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBDMySqlConnect_Click(object sender, RoutedEventArgs e)
        {
            string value = DBComboBox.SelectedIndex.ToString();
            switch (value)
            {
                case "0":
                    // Ativa o DBMS alvo
                    DBMS_ACTIVE = DBMS_IBERWEB;
                    break;

                case "1":
                    // Ativa o DBMS alvo
                    DBMS_ACTIVE = DBMS_TGPSI_WLAN;
                    break;

                case "2":
                    // Ativa o DBMS alvo
                    DBMS_ACTIVE = DBMS_TGPSI_LAN;
                    break;

                case "3":
                    // Ativa o DBMS alvo
                    DBMS_ACTIVE = DBMS_TGPSI_LOCAL;
                    break;
                case "4":
                    // Ativa o DBMS alvo
                    DBMS_ACTIVE = DBMS_TGPSI_CASA;
                    break;
            }
            MessageBox.Show(
                    "A BD ativa de momento é a " + DBMS_ACTIVE,   // Msg
                    "DBMS Ativo",                // Título
                    MessageBoxButton.OK,                // botões
                    MessageBoxImage.Information             // Icon
                );
            Close();
        }

        #endregion
    }
    #endregion
}
