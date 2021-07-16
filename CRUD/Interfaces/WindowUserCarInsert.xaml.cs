using CRUD.ClassesEntidades.SQL;
using System.Windows;
using System.Windows.Media;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;
using static CRUD.ClassesEntidades.Settings;

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
            if (us != null)
            {
                userTemp = us.User;
                carBodyTemp = us.CarBody;
                carTemp = us.Car;
                wheelTemp = us.Roda;
                powerUpTemp = us.PowerUp;
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
                    LabelTitle.Content = "Eliminar entidade User - Car";
                    CheckBoxUnlocked.IsChecked = userCar.IsUnlocked;
                    ButtonSelectUser.Content = userCar.User.UserName;
                    ButtonSelectCar.Content = userCar.Car.Descri;
                    ButtonSelectWheel.Content = userCar.Roda.Descri;
                    ButtonSelectCarBody.Content = userCar.CarBody.Descri;
                    ButtonSelectPowerUp.Content = userCar.PowerUp.Descri;
                    DatePicker.SelectedDate = userCar.DateUnlocked;

                    ButtonSelectUser.IsEnabled = false;
                    ButtonSelectCar.IsEnabled = false;
                    ButtonSelectWheel.IsEnabled = false;
                    ButtonSelectCarBody.IsEnabled = false;
                    ButtonSelectPowerUp.IsEnabled = false;
                    CheckBoxUnlocked.IsEnabled = false;
                    DatePicker.IsEnabled = false;
                    ButtonAction.Content = "Eliminar";

                    break;
                case SQL_UPDATE:
                    LabelTitle.Content = "Editar entidade User - Car";
                    CheckBoxUnlocked.IsChecked = userCar.IsUnlocked;
                    ButtonSelectUser.Content = userCar.User.UserName;
                    ButtonSelectCar.Content = userCar.Car.Descri;
                    ButtonSelectWheel.Content = userCar.Roda.Descri;
                    ButtonSelectCarBody.Content = userCar.CarBody.Descri;
                    ButtonSelectPowerUp.Content = userCar.PowerUp.Descri;
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
            userTemp = null;
            carBodyTemp = null;
            carTemp = null;
            wheelTemp = null;
            powerUpTemp = null;
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
                        if (carTemp == null)
                        {
                            ButtonSelectCarBody.Background = Brushes.Red;
                            openWarning++;
                        }
                        else
                        {
                            //verifica se existem caracteres especiais
                            if (carBodyTemp == null)
                            {
                                ButtonSelectCarBody.Background = Brushes.Red;
                                openWarning++;
                            }
                            else
                            {
                                //verifica se existem caracteres especiais
                                if (wheelTemp == null)
                                {
                                    ButtonSelectWheel.Background = Brushes.Red;
                                    openWarning++;
                                }
                                else
                                {
                                    UserCar p = SqlUserCar.Get(userTemp.Id, carTemp.Id);
                                    if (p != null)
                                    {
                                        MessageBox.Show("Erro: o perfil que tentou cirar já existe", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                    else
                                    {
                                        if (ValidaDados())
                                        {
                                            UserCar profileTemp = new UserCar(DatePicker.SelectedDate.Value, (bool)CheckBoxUnlocked.IsChecked,
                                                carTemp, userTemp, wheelTemp, carBodyTemp, powerUpTemp);
                                            SqlUserCar.Add(profileTemp);

                                            userTemp = null;
                                            carBodyTemp = null;
                                            carTemp = null;
                                            wheelTemp = null;
                                            powerUpTemp = null;
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
                    if (userTemp == null)
                    {
                        openWarning++;
                        ButtonSelectUser.Background = Brushes.Red;
                    }
                    else
                    {
                        //verifica se existem caracteres especiais
                        if (carTemp == null)
                        {
                            ButtonSelectCar.Background = Brushes.Red;
                            openWarning++;
                        }
                        else
                        {
                            //verifica se existem caracteres especiais
                            if (carBodyTemp == null)
                            {
                                ButtonSelectCarBody.Background = Brushes.Red;
                                openWarning++;
                            }
                            else
                            {
                                //verifica se existem caracteres especiais
                                if (wheelTemp == null)
                                {
                                    ButtonSelectWheel.Background = Brushes.Red;
                                    openWarning++;
                                }
                                else
                                {
                                    UserCar p = SqlUserCar.Get(userTemp.Id, carTemp.Id);
                                    if (p == null || p != userCar)
                                    {
                                        if (ValidaDados())
                                        {
                                            userCar.DateUnlocked = DatePicker.SelectedDate.Value;
                                            userCar.IsUnlocked = (bool)CheckBoxUnlocked.IsChecked;
                                            userCar.PowerUp = powerUpTemp;
                                            userCar.CarBody = carBodyTemp;
                                            userCar.Roda = wheelTemp;
                                            SqlUserCar.Set(userCar, userTemp.Id, carTemp.Id);
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

        #region entity selection

        /// <summary>
        /// metodo que é executado quando o buttão é clickado, serve para escolher que entidade irá ser usada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSelectCar_Click(object sender, RoutedEventArgs e)
        {
            WindowCarListar w = new WindowCarListar(LIST_ACTION_ID);
            w.ShowDialog();
            if (carTemp != null)
            {
                ButtonSelectCar.Content = carTemp.Descri;
            }
        }

        /// <summary>
        /// metodo que é executado quando o buttão é clickado, serve para escolher que entidade irá ser usada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSelectPowerUp_Click(object sender, RoutedEventArgs e)
        {
            WindowPowerUpList w = new WindowPowerUpList(LIST_ACTION_ID);
            w.ShowDialog();
            if (powerUpTemp != null)
            {
                ButtonSelectPowerUp.Content = powerUpTemp.Descri;
            }
        }

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
        private void ButtonSelectCarBody_Click(object sender, RoutedEventArgs e)
        {
            WindowCarBodyListar w = new WindowCarBodyListar(LIST_ACTION_ID);
            w.ShowDialog();
            if (carBodyTemp != null)
            {
                ButtonSelectCarBody.Content = carBodyTemp.Descri;
            }
        }

        /// <summary>
        /// metodo que é executado quando o buttão é clickado, serve para escolher que entidade irá ser usada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSelectWheel_Click(object sender, RoutedEventArgs e)
        {
            WindowWheelList w = new WindowWheelList(LIST_ACTION_ID);
            w.ShowDialog();
            if (wheelTemp != null)
            {
                ButtonSelectWheel.Content = wheelTemp.Descri;
            }
        }
        #endregion

        #endregion


    }
}
