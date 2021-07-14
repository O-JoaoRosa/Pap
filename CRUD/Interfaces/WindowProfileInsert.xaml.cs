using CRUD.ClassesEntidades.SQL;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;


namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowProfileInsert.xaml
    /// </summary>
    public partial class WindowProfileInsert : Window
    {

        int openWarning = 0;
        int dbAction = -1;
        Profile profile;

        #region inicialização

        public WindowProfileInsert(int action, Profile s)
        {
            InitializeComponent();
            dbAction = action;
            profile = s;
        }

        /// <summary>
        /// Metedo que é executado quando a window é carregada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<UserType> listaUserTypes = SqlUserType.GetAll(LIST_NULL, null, null);
            ComboBoxUserType.ItemsSource = listaUserTypes;
            ComboBoxUserType.DisplayMemberPath = "Descri";
            List<User> listaUsers = SqlUser.GetAll(LIST_NULL, null, null, null, null);
            ComboBoxUser.ItemsSource = listaUsers;
            ComboBoxUser.DisplayMemberPath = "UserName";

            //altera a interface consuante a ação escolhida
            switch (dbAction)
            {
                case SQL_DELETE:
                    LabelTitle.Content = "Eliminar prefil";
                    ComboBoxUser.Text = profile.UserEscolhido.UserName;
                    ComboBoxUserType.Text = profile.TipoUser.Descri;
                    DatePicker.SelectedDate = profile.DateCreated;
                    ComboBoxUser.IsEnabled = false;
                    ComboBoxUserType.IsEnabled = false;
                    DatePicker.IsEnabled = false;
                    ButtonAction.Content = "Eliminar";
                    break;
                case SQL_UPDATE:
                    LabelTitle.Content = "Editar prefil";
                    ComboBoxUser.Text = profile.UserEscolhido.UserName;
                    ComboBoxUserType.Text = profile.TipoUser.Descri;
                    DatePicker.SelectedDate = profile.DateCreated;
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
                    if ((UserType)ComboBoxUserType.SelectedItem == null)
                    {
                        openWarning++;
                        ComboBoxUser.Background = Brushes.Red;
                    }
                    else
                    {
                        //verifica se existem caracteres especiais
                        if ((User)ComboBoxUser.SelectedItem == null)
                        {
                            ComboBoxUser.Background = Brushes.Red;
                            openWarning++;
                        }
                        else
                        {
                            Profile p = SqlProfile.Get(((User)ComboBoxUser.SelectedItem).Id, ((UserType)ComboBoxUserType.SelectedItem).Id);
                            if (p != null)
                            {
                                MessageBox.Show("Erro: o perfil que tentou cirar já existe", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                if (ValidaDados())
                                {
                                    Profile profileTemp = new Profile(DatePicker.SelectedDate.Value, (User)ComboBoxUser.SelectedItem, (UserType)ComboBoxUserType.SelectedItem);
                                    SqlProfile.Add(profileTemp);
                                }
                            }
                        }
                    }
                    break;

                //caso a ação escolhida seja Eliminar então elimina o objeto
                case SQL_DELETE:
                    if (ValidaDados())
                    {
                        SqlProfile.Del(profile);
                    }
                    break;

                //caso a ação escolhida seja Update então atualiza os atributos do objeto
                case SQL_UPDATE:
                    //verifica se existem caracteres especiais
                    if ((UserType)ComboBoxUserType.SelectedItem == null)
                    {
                        openWarning++;
                        ComboBoxUser.Background = Brushes.Red;
                    }
                    else
                    {
                        //verifica se existem caracteres especiais
                        if ((User)ComboBoxUser.SelectedItem == null)
                        {
                            ComboBoxUser.Background = Brushes.Red;
                            openWarning++;
                        }
                        else
                        {
                            Profile p = SqlProfile.Get(((User)ComboBoxUser.SelectedItem).Id, ((UserType)ComboBoxUserType.SelectedItem).Id);
                            if (p != null)
                            {
                                MessageBox.Show("Erro: o perfil que tentou cirar já existe", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                if (ValidaDados())
                                {
                                    profile.DateCreated = DatePicker.SelectedDate.Value;
                                    SqlProfile.Set(profile, ((User)ComboBoxUser.SelectedItem).Id, ((UserType)ComboBoxUserType.SelectedItem).Id);
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
            if ( (User)ComboBoxUser.SelectedItem == null)
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
                ComboBoxUser.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                ComboBoxUser.Background = Brushes.White;
            }
            //verifica se existem caracteres especiais
            if ((UserType)ComboBoxUserType.SelectedItem == null)
            {
                openWarning++;
                ComboBoxUser.Background = Brushes.Red;
            }
            else
            {
                ComboBoxUserType.Background = Brushes.White;
                if (openWarning == 0)
                {
                    switch (dbAction)
                    {
                        case SQL_INSERT:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("Perfil inserido com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_DELETE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("Perfil eliminado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_UPDATE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("Perfil atualizado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;
                    }
                }
            }
            if (openWarning > 0)
            {
                //faz uma message box para avisar o utilizador
                MessageBox.Show("Erro : caracteres inválidos", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                openWarning = 0;
                return false;
            }
            return false;
        }
        #endregion
    }
}
