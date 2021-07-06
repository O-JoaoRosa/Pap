using CRUD.ClassesEntidades.SQL;
using Desktop___interfaces.ClassesEntidades.SQL;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;

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
        }


        /// <summary>
        /// Metedo que é executado quando a window é carregada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<User> listaUser = SqlUser.GetAll();
            ComboBoxUser.ItemsSource = listaUser;
            ComboBoxUser.DisplayMemberPath = "UserName";
            
            List<Server> listaServers = SqlServer.GetAll();
            ComboBoxServer.ItemsSource = listaServers;
            ComboBoxServer.DisplayMemberPath = "Descri";

            List<ServerUserState> listaServerUserStates = SqlUserServerState.GetAll();
            ComboBoxServerUserState.ItemsSource = listaServerUserStates;
            ComboBoxServerUserState.DisplayMemberPath = "Descri";

            //altera a interface consuante a ação escolhida
            switch (dbAction)
            {
                case SQL_DELETE:
                    LabelTitle.Content = "Eliminar entidade server-user";
                    CheckBoxIsAcessible.IsChecked = serverUser.IsAccessible;
                    ComboBoxUser.Text = serverUser.User.UserName;
                    ComboBoxServer.Text = serverUser.Server.Descri;
                    ComboBoxServerUserState.Text = serverUser.ServerUserState.Descri;
                    DatePickerAcessDate.SelectedDate = serverUser.AcesseDate;
                    DatePickerCreationDate.SelectedDate = serverUser.DateCreated;
                    DatePickerBanTime.SelectedDate = serverUser.DateBan;
                    DatePickerSuspension.SelectedDate = serverUser.DateSuspended;
                    ComboBoxUser.IsEnabled = false;
                    ComboBoxServer.IsEnabled = false;
                    ComboBoxServerUserState.IsEnabled = false;
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
                    ComboBoxUser.Text = serverUser.User.UserName;
                    ComboBoxServer.Text = serverUser.Server.Descri;
                    ComboBoxServerUserState.Text = serverUser.ServerUserState.Descri;
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
                    if ((User)ComboBoxUser.SelectedItem == null)
                    {
                        openWarning++;
                        ComboBoxUser.Background = Brushes.Red;
                    }
                    else
                    {
                        //verifica se existem caracteres especiais
                        if ((Server)ComboBoxServer.SelectedItem == null)
                        {
                            ComboBoxUser.Background = Brushes.Red;
                            openWarning++;
                        }
                        else
                        {
                            //verifica se existem caracteres especiais
                            if ((ServerUserState)ComboBoxServerUserState.SelectedItem == null)
                            {
                                ComboBoxUser.Background = Brushes.Red;
                                openWarning++;
                            }
                            else
                            {
                                ServerUser p = SqlServerUser.Get(((User)ComboBoxUser.SelectedItem).Id , ((Server)ComboBoxServer.SelectedItem).Id);
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
                                            DatePickerBanTime.SelectedDate.Value, (ServerUserState)ComboBoxServerUserState.SelectedItem ,
                                            (User)ComboBoxUser.SelectedItem, (Server)ComboBoxServer.SelectedItem);
                                        SqlServerUser.Add(profileTemp);
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
                    if ((User)ComboBoxUser.SelectedItem == null)
                    {
                        openWarning++;
                        ComboBoxUser.Background = Brushes.Red;
                    }
                    else
                    {
                        //verifica se existem caracteres especiais
                        if ((Server)ComboBoxServer.SelectedItem == null)
                        {
                            ComboBoxUser.Background = Brushes.Red;
                            openWarning++;
                        }
                        else
                        {
                            //verifica se existem caracteres especiais
                            if ((ServerUserState)ComboBoxServerUserState.SelectedItem == null)
                            {
                                ComboBoxUser.Background = Brushes.Red;
                                openWarning++;
                            }
                            else
                            {
                                ServerUser p = SqlServerUser.Get(((User)ComboBoxUser.SelectedItem).Id, ((Server)ComboBoxServer.SelectedItem).Id);
                                if (p == null || p != serverUser)
                                {
                                    if (ValidaDados())
                                    {
                                        serverUser.IsAccessible = (bool)CheckBoxIsAcessible.IsChecked;
                                        serverUser.DateBan = DatePickerBanTime.SelectedDate.Value;
                                        serverUser.DateCreated = DatePickerCreationDate.SelectedDate.Value;
                                        serverUser.AcesseDate = DatePickerAcessDate.SelectedDate.Value;
                                        serverUser.DateSuspended = DatePickerSuspension.SelectedDate.Value;
                                        serverUser.ServerUserState = (ServerUserState)ComboBoxServerUserState.SelectedItem;
                                        SqlServerUser.Set(serverUser, ((User)ComboBoxUser.SelectedItem).Id, ((Server)ComboBoxServer.SelectedItem).Id);
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
            if ((User)ComboBoxUser.SelectedItem == null)
            {
                ComboBoxUser.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                ComboBoxUser.Background = Brushes.White;
            }

            //verifica se existem caracteres especiais
            if ((ServerUserState)ComboBoxServerUserState.SelectedItem == null)
            {
                ComboBoxUser.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                ComboBoxUser.Background = Brushes.White;
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
            if ((Server)ComboBoxServer.SelectedItem == null)
            {
                openWarning++;
                ComboBoxUser.Background = Brushes.Red;
            }
            else
            {
                ComboBoxServer.Background = Brushes.White;
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
        #endregion
    }
}
