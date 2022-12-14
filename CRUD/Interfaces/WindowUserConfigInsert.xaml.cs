using CRUD.ClassesEntidades.SQL;
using System;
using System.Windows;
using System.Windows.Media;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;
using static CRUD.ClassesEntidades.Settings;
using CRUD.ClassesEntidades;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowUserConfigInsert.xaml
    /// </summary>
    public partial class WindowUserConfigInsert : Window
    {
        int openWarning = 0;
        int dbAction = -1;
        UserConfig userConfig;

        #region inicialização

        /// <summary>
        /// metodo executado quando a window é incializada 
        /// </summary>
        public WindowUserConfigInsert(int action, UserConfig us)
        {
            InitializeComponent();
            userConfig = us;
            dbAction = action;
            if (us != null)
            {
                userTemp = us.User;
            }
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
                    LabelTitle.Content = "Eliminar a configuração";
                    TextBoxDescri.Text = userConfig.Descri;
                    ButtonUserSelect.Content = userConfig.User.UserName;
                    TextBoxValue.Text = userConfig.Value.ToString();
                    ButtonUserSelect.IsEnabled = false;
                    TextBoxDescri.IsEnabled = false;
                    TextBoxValue.IsEnabled = false;
                    ButtonAction.Content = "Eliminar";

                    break;
                case SQL_UPDATE:
                    LabelTitle.Content = "Editar entidade server-user";
                    TextBoxDescri.Text = userConfig.Descri;
                    ButtonUserSelect.Content = userConfig.User.UserName;
                    userTemp = userConfig.User;
                    TextBoxValue.Text = userConfig.Value.ToString();
                    ButtonAction.Content = "Editar";
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

            userTemp = null;
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
                        UserConfig profileTemp = new UserConfig(-1, TextBoxDescri.Text,
                             Convert.ToInt32(TextBoxValue.Text),
                            userTemp);
                        userTemp = null;
                        SqlUserConfig.Add(profileTemp);
                    }
                    break;

                //caso a ação escolhida seja Eliminar então elimina o objeto
                case SQL_DELETE:
                    if (ValidaDados())
                    {
                        SqlUserConfig.Del(userConfig);
                    }
                    break;

                //caso a ação escolhida seja Update então atualiza os atributos do objeto
                case SQL_UPDATE:

                    if (ValidaDados())
                    {
                        userConfig.Descri = TextBoxDescri.Text;
                        userConfig.Value = Convert.ToInt32(TextBoxValue.Text);
                        userConfig.User = userTemp;
                        userTemp = null;
                        SqlUserConfig.Set(userConfig);
                    }
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
            if (userTemp == null)
            {
                ButtonUserSelect.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                ButtonUserSelect.Background = Brushes.White;
            }

            //verifica se existem caracteres especiais
            if (Validações.ValidaTexto(TextBoxDescri.Text))
            {
                TextBoxDescri.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                TextBoxDescri.Background = Brushes.White;
            }

            //verifica se existem caracteres especiais
            if (Validações.ValidaNumero(TextBoxValue.Text))
            {
                TextBoxValue.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                TextBoxValue.Background = Brushes.White;
                if (openWarning == 0)
                {
                    switch (dbAction)
                    {
                        case SQL_INSERT:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("Configuração do utilizador inserido com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_DELETE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("Configuração do utilizador eliminado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_UPDATE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("Configuração do utilizador atualizado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;
                    }
                }
            }
            if (openWarning > 0)
            {
                //faz uma message box para avisar o utilizador
                MessageBox.Show("Erro : Configuração do utilizador caracteres inválidos", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                openWarning = 0;
                return false;
            }
            return false;
        }

        /// <summary>
        /// butão utilizado para escolher 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonUserSelect_Click(object sender, RoutedEventArgs e)
        {
            WindowUserList w = new WindowUserList(LIST_ACTION_ID);
            w.ShowDialog();
            if (userTemp!= null)
            {
                ButtonUserSelect.Content = userTemp.UserName;
            }
        }
        #endregion

    }
}
