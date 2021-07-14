using CRUD.ClassesEntidades.SQL;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowUserFriendInsert.xaml
    /// </summary>
    public partial class WindowUserFriendInsert : Window
    {

        int openWarning = 0;
        int dbAction = -1;
        UserFriend userFriend;

        #region inicialização

        /// <summary>
        /// metodo executado quando a window é incializada 
        /// </summary>
        public WindowUserFriendInsert(int action, UserFriend us)
        {
            InitializeComponent();
            userFriend = us;
            dbAction = action;
        }

        /// <summary>
        /// Metedo que é executado quando a window é carregada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<User> listaUser = SqlUser.GetAll(LIST_NULL, null, null, null, null);
            ComboBoxUser.ItemsSource = listaUser;
            ComboBoxUser.DisplayMemberPath = "UserName";

            List<User> listaUserFriends = SqlUser.GetAll(LIST_NULL, null, null, null, null);
            ComboBoxUserFirend.ItemsSource = listaUserFriends;
            ComboBoxUserFirend.DisplayMemberPath = "UserName";

            //altera a interface consuante a ação escolhida
            switch (dbAction)
            {
                case SQL_DELETE:
                    LabelTitle.Content = "Eliminar entidade Amigo do utilizador";
                    CheckBoxIsOnline.IsChecked = userFriend.IsOnline;
                    ComboBoxUser.Text = userFriend.User.UserName;
                    ComboBoxUserFirend.Text = userFriend.UserFriend1.UserName;
                    DatePicker.SelectedDate = userFriend.DateAdded;

                    ComboBoxUser.IsEnabled = false;
                    ComboBoxUserFirend.IsEnabled = false;
                    DatePicker.IsEnabled = false;
                    CheckBoxIsOnline.IsEnabled = false;
                    ButtonAction.Content = "Eliminar";

                    break;
                case SQL_UPDATE:
                    LabelTitle.Content = "Editar entidade Amigo do utilizador";
                    CheckBoxIsOnline.IsChecked = userFriend.IsOnline;
                    ComboBoxUser.Text = userFriend.User.UserName;
                    ComboBoxUserFirend.Text = userFriend.UserFriend1.UserName;
                    DatePicker.SelectedDate = userFriend.DateAdded;
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
                        if ((User)ComboBoxUserFirend.SelectedItem == null)
                        {
                            openWarning++;
                            ComboBoxUser.Background = Brushes.Red;
                        }
                        else
                        {
                            //verifica se existem caracteres especiais
                            if ((User)ComboBoxUserFirend.SelectedItem == (User)ComboBoxUser.SelectedItem )
                            {
                                ComboBoxUser.Background = Brushes.Red;
                                openWarning++;
                            }
                            else
                            {
                                UserFriend p = SqlUserFriend.Get(((User)ComboBoxUser.SelectedItem).Id, ((User)ComboBoxUserFirend.SelectedItem).Id);
                                if (p != null)
                                {
                                    MessageBox.Show("Erro: o perfil que tentou cirar já existe", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                else
                                {
                                    if (ValidaDados())
                                    {
                                        UserFriend profileTemp = new UserFriend((bool)CheckBoxIsOnline.IsChecked, DatePicker.SelectedDate.Value,
                                            (User)ComboBoxUser.SelectedItem, (User)ComboBoxUserFirend.SelectedItem);
                                        SqlUserFriend.Add(profileTemp);
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
                        SqlUserFriend.Del(userFriend);
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
                        if ((User)ComboBoxUserFirend.SelectedItem == null)
                        {
                            ComboBoxUser.Background = Brushes.Red;
                            openWarning++;
                        }
                        else
                        {
                            UserFriend p = SqlUserFriend.Get(((User)ComboBoxUser.SelectedItem).Id, ((User)ComboBoxUserFirend.SelectedItem).Id);
                            if (p == null || p != userFriend)
                            {
                                if (ValidaDados())
                                {
                                    userFriend.IsOnline = (bool)CheckBoxIsOnline.IsChecked;
                                    userFriend.DateAdded = DatePicker.SelectedDate.Value;
                                    SqlUserFriend.Set(userFriend, ((User)ComboBoxUser.SelectedItem).Id, ((User)ComboBoxUserFirend.SelectedItem).Id);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Erro: a entidade Amigo do utilizador que tentou cirar já existe", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (DatePicker.SelectedDate == null)
            {
                DatePicker.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                DatePicker.Background = Brushes.White;
            }

            //verifica se existem caracteres especiais
            if ((User)ComboBoxUserFirend.SelectedItem == null)
            {
                openWarning++;
                ComboBoxUserFirend.Background = Brushes.Red;
            }
            else
            {
                ComboBoxUserFirend.Background = Brushes.White;
                if (openWarning == 0)
                {
                    switch (dbAction)
                    {
                        case SQL_INSERT:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("entidade Amigo do utilizador inserido com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_DELETE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("entidade Amigo do utilizador eliminado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_UPDATE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("entidade Amigo do utilizador atualizado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;
                    }
                }
            }
            if (openWarning > 0)
            {
                //faz uma message box para avisar o utilizador
                MessageBox.Show("Erro : entidade Amigo do utilizador caracteres inválidos", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                openWarning = 0;
                return false;
            }
            return false;
        }
        #endregion

    }
}
