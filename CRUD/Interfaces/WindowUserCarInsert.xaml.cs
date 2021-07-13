using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRUD.ClassesEntidades.SQL;
using Desktop___interfaces.ClassesEntidades.SQL;
using System.Windows;
using System.Windows.Media;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowUserCarInsert.xaml
    /// </summary>
    public partial class WindowUserCarInsert : Window
    {

        int openWarning = 0;
        int dbAction = -1;
        UserCar userCar;

        #region inicialização

        /// <summary>
        /// metodo executado quando a window é incializada 
        /// </summary>
        public WindowUserCarInsert(int action, UserCar us)
        {
            InitializeComponent();
            userCar = us;
            dbAction = action;
        }


        /// <summary>
        /// Metedo que é executado quando a window é carregada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<User> listaUser = SqlUser.GetAll(LIST_NULL);
            ComboBoxUser.ItemsSource = listaUser;
            ComboBoxUser.DisplayMemberPath = "UserName";

            List<Car> listaServers = SqlCar.GetAll(LIST_NULL);
            ComboBoxCar.ItemsSource = listaServers;
            ComboBoxCar.DisplayMemberPath = "Descri";

            List<Wheel> listaWheels = SqlWheel.GetAll(LIST_NULL);
            ComboBoxWheels.ItemsSource = listaWheels;
            ComboBoxWheels.DisplayMemberPath = "Descri";

            List<CarBody> listaCarBodys = SqlCarBody.GetAll(LIST_NULL);
            ComboBoxCarBodies.ItemsSource = listaCarBodys;
            ComboBoxCarBodies.DisplayMemberPath = "Descri";

            List<PowerUp> listaPowerUps = SqlPowerUp.GetAll(LIST_NULL);
            ComboBoxPowerUp.ItemsSource = listaPowerUps;
            ComboBoxPowerUp.DisplayMemberPath = "Descri";

            //altera a interface consuante a ação escolhida
            switch (dbAction)
            {
                case SQL_DELETE:
                    LabelTitle.Content = "Eliminar entidade User - Car";
                    CheckBoxUnlocked.IsChecked = userCar.IsUnlocked;
                    ComboBoxUser.Text = userCar.User.UserName;
                    ComboBoxCar.Text = userCar.Car.Descri;
                    ComboBoxWheels.Text = userCar.Roda.Descri;
                    ComboBoxCarBodies.Text = userCar.CarBody.Descri;
                    ComboBoxPowerUp.Text = userCar.PowerUp.Descri;
                    DatePicker.SelectedDate = userCar.DateUnlocked;
                    
                    ComboBoxUser.IsEnabled = false;
                    ComboBoxCar.IsEnabled = false;
                    ComboBoxWheels.IsEnabled = false;
                    ComboBoxCarBodies.IsEnabled = false;
                    ComboBoxPowerUp.IsEnabled = false;
                    CheckBoxUnlocked.IsEnabled = false;
                    DatePicker.IsEnabled = false;
                    ButtonAction.Content = "Eliminar";

                    break;
                case SQL_UPDATE:
                    LabelTitle.Content = "Editar entidade User - Car";
                    CheckBoxUnlocked.IsChecked = userCar.IsUnlocked;
                    ComboBoxUser.Text = userCar.User.UserName;
                    ComboBoxCar.Text = userCar.Car.Descri;
                    ComboBoxWheels.Text = userCar.Roda.Descri;
                    ComboBoxCarBodies.Text = userCar.CarBody.Descri;
                    ComboBoxPowerUp.Text = userCar.PowerUp.Descri;
                    DatePicker.SelectedDate = userCar.DateUnlocked;
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
                        if ((Car)ComboBoxCar.SelectedItem == null)
                        {
                            ComboBoxUser.Background = Brushes.Red;
                            openWarning++;
                        }
                        else
                        {
                            //verifica se existem caracteres especiais
                            if ((CarBody)ComboBoxCarBodies.SelectedItem == null)
                            {
                                ComboBoxUser.Background = Brushes.Red;
                                openWarning++;
                            }
                            else
                            {
                                //verifica se existem caracteres especiais
                                if ((Wheel)ComboBoxWheels.SelectedItem == null)
                                {
                                    ComboBoxUser.Background = Brushes.Red;
                                    openWarning++;
                                }
                                else
                                {
                                    UserCar p = SqlUserCar.Get(((User)ComboBoxUser.SelectedItem).Id, ((Car)ComboBoxCar.SelectedItem).Id);
                                    if (p != null)
                                    {
                                        MessageBox.Show("Erro: o perfil que tentou cirar já existe", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                    else
                                    {
                                        if (ValidaDados())
                                        {
                                            UserCar profileTemp = new UserCar(DatePicker.SelectedDate.Value, (bool)CheckBoxUnlocked.IsChecked,
                                                (Car)ComboBoxCar.SelectedItem, (User)ComboBoxUser.SelectedItem, (Wheel)ComboBoxWheels.SelectedItem, 
                                                (CarBody)ComboBoxCarBodies.SelectedItem , (PowerUp)ComboBoxPowerUp.SelectedItem);
                                            SqlUserCar.Add(profileTemp);
                                        }
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
                        SqlUserCar.Del(userCar);
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
                        if ((Car)ComboBoxCar.SelectedItem == null)
                        {
                            ComboBoxUser.Background = Brushes.Red;
                            openWarning++;
                        }
                        else
                        {
                            //verifica se existem caracteres especiais
                            if ((CarBody)ComboBoxCarBodies.SelectedItem == null)
                            {
                                ComboBoxUser.Background = Brushes.Red;
                                openWarning++;
                            }
                            else
                            {
                                //verifica se existem caracteres especiais
                                if ((Wheel)ComboBoxWheels.SelectedItem == null)
                                {
                                    ComboBoxUser.Background = Brushes.Red;
                                    openWarning++;
                                }
                                else
                                {
                                    UserCar p = SqlUserCar.Get(((User)ComboBoxUser.SelectedItem).Id, ((Car)ComboBoxCar.SelectedItem).Id);
                                    if (p == null || p != userCar)
                                    {
                                        if (ValidaDados())
                                        {
                                            userCar.DateUnlocked = DatePicker.SelectedDate.Value;
                                            userCar.IsUnlocked = (bool)CheckBoxUnlocked.IsChecked;
                                            userCar.PowerUp = (PowerUp)ComboBoxPowerUp.SelectedItem;
                                            userCar.CarBody = (CarBody)ComboBoxCarBodies.SelectedItem;
                                            userCar.Roda = (Wheel)ComboBoxWheels.SelectedItem;
                                            SqlUserCar.Set(userCar, ((User)ComboBoxUser.SelectedItem).Id, ((Car)ComboBoxCar.SelectedItem).Id);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Erro: o perfil que tentou cirar já existe", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }

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
            if (DatePicker.SelectedDate == null)
            {
                openWarning++;
                DatePicker.Background = Brushes.Red;
            }
            else
            {
                DatePicker.Background = Brushes.White;
                if (openWarning == 0)
                {
                    switch (dbAction)
                    {
                        case SQL_INSERT:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("entidade User - Car inserido com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_DELETE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("entidade User - Car eliminado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_UPDATE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("entidade User - Car atualizado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;
                    }
                }
            }
            if (openWarning > 0)
            {
                //faz uma message box para avisar o utilizador
                MessageBox.Show("Erro : entidade User - Car caracteres inválidos", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                openWarning = 0;
                return false;
            }
            return false;
        }
        #endregion
    }
}
