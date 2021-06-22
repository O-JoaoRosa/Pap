using Desktop___interfaces.ClassesEntidades;
using Desktop___interfaces.ClassesEntidades.SQL;
using System.Windows;
using System.Windows.Media;
using static Desktop___interfaces.ClassesEntidades.SQL_Connection;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowServerInsert.xaml
    /// </summary>
    public partial class WindowServerInsert : Window
    {
        bool openWarning = false;
        int dbAction = -1;
        Server server;

        #region inicialização
        public WindowServerInsert()
        {
            InitializeComponent();
        }

        public WindowServerInsert(int action , Server s)
        {
            InitializeComponent();
            dbAction = action;
            server = s;
        }

        /// <summary>
        /// Metedo que é executado quando a window é carregada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //altera a interface consuante a ação escolhida
            switch (dbAction)
            {
                case SQL_DELETE:
                    TextBoxDescri.Text = server.Descri;
                    TextBoxObvs.Text = server.Obs;
                    TextBoxDescri.IsEnabled = false;
                    TextBoxObvs.IsEnabled = false;
                    ButtonAction.Content = "Eliminar";
                    break;
                case SQL_UPDATE:
                    TextBoxDescri.Text = server.Descri;
                    TextBoxObvs.Text = server.Obs;
                    TextBoxDescri.IsEnabled = true;
                    TextBoxObvs.IsEnabled = true;
                    ButtonAction.Content = "Editar";
                    break;
                case SQL_INSERT:
                    TextBoxDescri.Text = "descrição";
                    TextBoxObvs.Text = "observações";
                    TextBoxDescri.IsEnabled = true;
                    TextBoxObvs.IsEnabled = true;
                    ButtonAction.Content = "Inserir";
                    break;
            }
        }
        #endregion

        #region metodos dos butoes

        /// <summary>
        /// metodo executado quando o butão cancelar é carregado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Metodo do butão principal que irá decidir o que fazer dependendo da flag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAction_Click(object sender, RoutedEventArgs e)
        {
            // Executar o comando SQL DML. É a flag SqlDml que decide.
            switch (dbAction)
            {
                //caso a opção escolhida seja inserir então inser um novo objeto
                case SQL_INSERT:
                    if (ValidaDados())
                    {
                        Server serverTemp = new Server(-1, TextBoxDescri.Text, TextBoxObvs.Text);
                        SqlServer.Add(serverTemp);
                    }
                    break;

                //caso a ação escolhida seja Eliminar então elimina o objeto
                case SQL_DELETE:
                    if (ValidaDados())
                    {
                        SqlServer.Del(server);
                    }
                    break;

                //caso a ação escolhida seja Update então atualiza os atributos do objeto
                case SQL_UPDATE:
                    if (ValidaDados())
                    {
                        server.Descri = TextBoxDescri.Text;
                        server.Obs = TextBoxObvs.Text;
                        SqlServer.Set(server);
                    }
                    break;
                default:
                    break;
            }            
        }

        /// <summary>
        /// Metodo que serve para verificar se os dados estão correctos
        /// </summary>
        /// <returns></returns>
        private bool ValidaDados()
        {
            //verifica se existem caracteres especiais
            if (Validações.ValidaTexto(TextBoxDescri.Text))
            {
                TextBoxDescri.Background = Brushes.Red;
                openWarning = true;
            }
            else
            {
                TextBoxDescri.Background = Brushes.White;
                openWarning = false;
            }

            //verifica se existem caracteres especiais
            if (Validações.ValidaTexto(TextBoxObvs.Text))
            {
                openWarning = true;
                TextBoxObvs.Background = Brushes.Red;
            }
            else
            {
                TextBoxObvs.Background = Brushes.White;
                if (!openWarning)
                {
                    switch (dbAction)
                    {
                        case SQL_INSERT:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("Server inserido com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_DELETE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("Server eliminado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_UPDATE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("Server atualizado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;
                    }
                }
            }
            if (openWarning)
            {
                //faz uma message box para avisar o utilizador
                MessageBox.Show("Erro : caracteres inválidos", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                openWarning = false;
                return false;
            }
            return false;
        }


        #endregion

    }
}
