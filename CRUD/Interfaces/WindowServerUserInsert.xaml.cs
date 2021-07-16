using CRUD.ClassesEntidades.SQL;
using System.Windows;
using System.Windows.Media;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;
using static CRUD.ClassesEntidades.Settings;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowServerUserInsert.xaml
    /// </summary>
    public partial class WindowServerUserInsert : Window
    {
        int openWarning = 0;
        int dbAction = -1;
        ServerUser serverUser;

        #region inicialização

        /// <summary>
        /// metodo executado quando a window é incializada 
        /// </summary>
        public WindowServerUserInsert(int action , ServerUser us)
        {
            InitializeComponent();
            serverUser = us;
            dbAction = action;

            if (us != null)
            {
                serverUserStateTemp = us.ServerUserState;
                userTemp = us.User;
                serverTemp = us.Server;
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
                    LabelTitle.Content = "Eliminar entidade server-user";
                    CheckBoxIsAcessible.IsChecked = serverUser.IsAccessible;
                    ButtonSelectUser.Content = serverUser.User.UserName;
                    ButtonSelectServer.Content = serverUser.Server.Descri;
                    ButtonSelectServerUser.Content = serverUser.ServerUserState.Descri;
                    DatePickerAcessDate.SelectedDate = serverUser.AcesseDate;
                    DatePickerCreationDate.SelectedDate = serverUser.DateCreated;
                    DatePickerBanTime.SelectedDate = serverUser.DateBan;
                    DatePickerSuspension.SelectedDate = serverUser.DateSuspended;
                    ButtonSelectUser.IsEnabled = false;
                    ButtonSelectServer.IsEnabled = false;
                    ButtonSelectServerUser.IsEnabled = false;
                    DatePickerAcessDate.IsEnabled = false;
                    DatePickerCreationDate.IsEnabled = false;
                    DatePickerBanTime.IsEnabled = false;
                    DatePickerSuspension.IsEnabled = false;
                    CheckBoxIsAcessible.IsEnabled = false;
                    ButtonAction.Content = "Eliminar";

                    break;
                case SQL_UPDATE:
                    LabelTitle.Content = "Editar entidade server-user";
                    CheckBoxIsAcessible.IsChecked = serverUser.IsAccessible; 
                    ButtonSelectUser.Content = serverUser.User.UserName;
                    ButtonSelectServer.Content = serverUser.Server.Descri;
                    ButtonSelectServerUser.Content = serverUser.ServerUserState.Descri;
                    DatePickerAcessDate.SelectedDate = serverUser.AcesseDate;
                    DatePickerCreationDate.SelectedDate = serverUser.DateCreated;
                    DatePickerBanTime.SelectedDate = serverUser.DateBan;
                    DatePickerSuspension.SelectedDate = serverUser.DateSuspended;
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
            serverUserStateTemp = null;
            userTemp = null;
            serverTemp = null;
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

                    //verifica se existem caracteres especiais
                    if (userTemp == null)
                    {
                        openWarning++;
                        ButtonSelectUser.Background = Brushes.Red;
                    }
                    else
                    {
                        //verifica se existem caracteres especiais
                        if (serverTemp == null)
                        {
                            ButtonSelectServer.Background = Brushes.Red;
                            openWarning++;
                        }
                        else
                        {
                            //verifica se existem caracteres especiais
                            if (serverUserStateTemp == null)
                            {
                                ButtonSelectServerUser.Background = Brushes.Red;
                                openWarning++;
                            }
                            else
                            {
                                ServerUser p = SqlServerUser.Get(userTemp.Id , serverTemp.Id);
                                if (p != null)
                                {
                                    MessageBox.Show("Erro: o perfil que tentou cirar já existe", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                else
                                {
                                    if (ValidaDados())
                                    {
                                        ServerUser profileTemp = new ServerUser(DatePickerAcessDate.SelectedDate.Value , (bool)CheckBoxIsAcessible.IsChecked,
                                            DatePickerCreationDate.SelectedDate.Value, DatePickerSuspension.SelectedDate.Value,
                                            DatePickerBanTime.SelectedDate.Value, serverUserStateTemp,
                                            userTemp, serverTemp);
                                        SqlServerUser.Add(profileTemp);
                                        serverUserStateTemp = null;
                                        userTemp = null;
                                        serverTemp = null;
                                    }
                                }
                            }
                        }
                    }
                    break;

                //caso a ação escolhida seja Eliminar então elimina o objeto
                case SQL_DELETE:
                    if (ValidaDados())
                    {
                        SqlServerUser.Del(serverUser);
                    }
                    break;

                //caso a ação escolhida seja Update então atualiza os atributos do objeto
                case SQL_UPDATE:

                    //verifica se existem caracteres especiais
                    if (userTemp == null)
                    {
                        openWarning++;
                        ButtonSelectUser.Background = Brushes.Red;
                    }
                    else
                    {
                        //verifica se existem caracteres especiais
                        if (serverTemp == null)
                        {
                            ButtonSelectServer.Background = Brushes.Red;
                            openWarning++;
                        }
                        else
                        {
                            //verifica se existem caracteres especiais
                            if (serverUserStateTemp == null)
                            {
                                ButtonSelectServerUser.Background = Brushes.Red;
                                openWarning++;
                            }
                            else
                            {
                                ServerUser p = SqlServerUser.Get(userTemp.Id, serverTemp.Id);
                                if (p == null || p != serverUser)
                                {
                                    if (ValidaDados())
                                    {
                                        serverUser.IsAccessible = (bool)CheckBoxIsAcessible.IsChecked;
                                        serverUser.DateBan = DatePickerBanTime.SelectedDate.Value;
                                        serverUser.DateCreated = DatePickerCreationDate.SelectedDate.Value;
                                        serverUser.AcesseDate = DatePickerAcessDate.SelectedDate.Value;
                                        serverUser.DateSuspended = DatePickerSuspension.SelectedDate.Value;
                                        serverUser.ServerUserState = serverUserStateTemp;
                                        SqlServerUser.Set(serverUser, userTemp.Id, serverTemp.Id);
                                        serverUserStateTemp = null;
                                        userTemp = null;
                                        serverTemp = null;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Erro: a entidade server-user que tentou cirar já existe", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
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
            if (userTemp == null)
            {
                ButtonSelectUser.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                ButtonSelectUser.Background = Brushes.White;
            }

            //verifica se existem caracteres especiais
            if (serverUserStateTemp == null)
            {
                ButtonSelectServerUser.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                ButtonSelectServerUser.Background = Brushes.White;
            }

            //verifica se existem caracteres especiais
            if (DatePickerSuspension.SelectedDate == null)
            {
                DatePickerSuspension.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                DatePickerSuspension.Background = Brushes.White;
            }

            //verifica se existem caracteres especiais
            if (DatePickerCreationDate.SelectedDate == null)
            {
                DatePickerCreationDate.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                DatePickerCreationDate.Background = Brushes.White;
            }

            //verifica se existem caracteres especiais
            if (DatePickerBanTime.SelectedDate == null)
            {
                DatePickerBanTime.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                DatePickerBanTime.Background = Brushes.White;
            }

            //verifica se existem caracteres especiais
            if (DatePickerAcessDate.SelectedDate == null)
            {
                DatePickerAcessDate.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                DatePickerAcessDate.Background = Brushes.White;
            }

            //verifica se existem caracteres especiais
            if (serverTemp == null)
            {
                openWarning++;
                ButtonSelectServer.Background = Brushes.Red;
            }
            else
            {
                ButtonSelectServer.Background = Brushes.White;
                if (openWarning == 0)
                {
                    switch (dbAction)
                    {
                        case SQL_INSERT:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("entidade server-user inserido com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_DELETE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("entidade server-user eliminado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_UPDATE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("entidade server-user atualizado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;
                    }
                }
            }
            if (openWarning > 0)
            {
                //faz uma message box para avisar o utilizador
                MessageBox.Show("Erro : entidade server-user caracteres inválidos", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                openWarning = 0;
                return false;
            }
            return false;
        }
        #region select buttons

        /// <summary>
        /// metodo que é executado quando o buttão é clickado, serve para escolher que entidade irá ser usada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSelectUser_Click(object sender, RoutedEventArgs e)
        {
            WindowUserList w = new WindowUserList(LIST_ACTION_ID);
            w.ShowDialog();
            if (userTemp != null)
            {
                ButtonSelectUser.Content = userTemp.UserName;
            }
        }

        /// <summary>
        /// metodo que é executado quando o buttão é clickado, serve para escolher que entidade irá ser usada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSelectServerUser_Click(object sender, RoutedEventArgs e)
        {
            WindowServerUserStateList w = new WindowServerUserStateList(LIST_ACTION_ID);
            w.ShowDialog();
            if (serverUserStateTemp != null)
            {
                ButtonSelectServerUser.Content = serverUserStateTemp.Descri;
            }
        }


        /// <summary>
        /// metodo que é executado quando o buttão é clickado, serve para escolher que entidade irá ser usada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSelectServer_Click(object sender, RoutedEventArgs e)
        {
            WindowServerList w = new WindowServerList(LIST_ACTION_ID);
            w.ShowDialog();
            if (serverTemp != null)
            {
                ButtonSelectServer.Content = serverTemp.Descri;
            }
        }

        #endregion

        #endregion

    }
}
