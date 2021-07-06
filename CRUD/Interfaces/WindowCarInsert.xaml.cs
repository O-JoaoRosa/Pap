using CRUD.ClassesEntidades;
using System;
using System.Windows;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;
using System.Windows.Media;
using CRUD.ClassesEntidades.SQL;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowCarInsert.xaml
    /// </summary>
    public partial class WindowCarInsert : Window
    {
        int dbAction;
        Car carTemp;
        int openWarning = 0;

        #region loads
        public WindowCarInsert(int action, Car car)
        {
            InitializeComponent();
            dbAction = action;
            carTemp = car;
        }

        /// <summary>
        /// metodo execudado aseguir á window ser inicializada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //altera a interface para encaixar com a action 
            switch (dbAction)
            {
                case SQL_DELETE:
                    LabelTitle.Content = "Eliminar o Carro";
                    TextBoxDescri.Text = carTemp.Descri;
                    TextBoxRepReq.Text = carTemp.ReputationRequired.ToString();
                    TextBoxPrice.Text = carTemp.Price.ToString();
                    TextBoxMaxSpeed.Text = carTemp.MaxSpeed.ToString();
                    TextBoxAcceleration.Text = carTemp.Acceleration.ToString();
                    TextBoxDriftForce.Text = carTemp.DriftForce.ToString();
                    TextBoxMobility.Text = carTemp.Mobility.ToString();
                    ButtonAction.Content = "Eliminar";
                    TextBoxDescri.IsEnabled = false;
                    TextBoxRepReq.IsEnabled = false;
                    TextBoxPrice.IsEnabled = false;
                    TextBoxMaxSpeed.IsEnabled = false;
                    TextBoxAcceleration.IsEnabled = false;
                    TextBoxDriftForce.IsEnabled = false;
                    TextBoxMobility.IsEnabled = false;
                    break;

                case SQL_UPDATE:
                    LabelTitle.Content = "Editar o Carro";
                    TextBoxDescri.Text = carTemp.Descri;
                    TextBoxRepReq.Text = carTemp.ReputationRequired.ToString();
                    TextBoxPrice.Text = carTemp.Price.ToString();
                    TextBoxMaxSpeed.Text = carTemp.MaxSpeed.ToString();
                    TextBoxAcceleration.Text = carTemp.Acceleration.ToString();
                    TextBoxDriftForce.Text = carTemp.DriftForce.ToString();
                    TextBoxMobility.Text = carTemp.Mobility.ToString();
                    ButtonAction.Content = "Editar";
                    break;

            }
        }
        #endregion

        #region metodos

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAction_Click(object sender, RoutedEventArgs e)
        {
            //muda a operação dependendo da action escolhida
            switch (dbAction)
            {
                case SQL_INSERT:

                    //verifica se os dados estão validos ou não
                    if (ValidaDados())
                    {
                        //insere um novo user na bd
                        Car user = new Car(-1, TextBoxDescri.Text, Convert.ToInt32(TextBoxRepReq.Text), 
                            Convert.ToInt32(TextBoxPrice.Text), Convert.ToDouble(TextBoxMaxSpeed.Text),
                            Convert.ToDouble(TextBoxAcceleration.Text), Convert.ToInt32(TextBoxDriftForce.Text),
                            Convert.ToInt32(TextBoxMobility.Text)
                        );
                        SqlCar.Add(user);
                    }
                    break;

                case SQL_DELETE:
                    ValidaDados();

                    //elimina o utilizador escolhido
                    SqlCar.Del(carTemp);
                    this.Close();
                    break;

                case SQL_UPDATE:

                    //verifica se os dados estão validos ou não
                    if (ValidaDados())
                    {
                        //altera os atributos da entidade
                        carTemp.Descri = TextBoxDescri.Text;
                        carTemp.Acceleration = Convert.ToDouble(TextBoxAcceleration.Text);
                        carTemp.ReputationRequired = Convert.ToInt32(TextBoxRepReq.Text);
                        carTemp.Price = Convert.ToInt32(TextBoxPrice.Text);
                        carTemp.DriftForce = Convert.ToInt32(TextBoxDriftForce.Text);
                        carTemp.Mobility = Convert.ToInt32(TextBoxMobility.Text);
                        carTemp.MaxSpeed = Convert.ToDouble(TextBoxMaxSpeed.Text);

                        SqlCar.Set(carTemp);
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
            if (Validações.ValidaNumero(TextBoxAcceleration.Text))
            {
                TextBoxAcceleration.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                TextBoxAcceleration.Background = Brushes.White;
            }

            //verifica se existem caracteres especiais
            if (Validações.ValidaNumero(TextBoxDriftForce.Text))
            {
                TextBoxDriftForce.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                TextBoxDriftForce.Background = Brushes.White;
            }

            //verifica se existem caracteres especiais
            if (Validações.ValidaNumero(TextBoxMobility.Text))
            {
                TextBoxMobility.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                TextBoxMobility.Background = Brushes.White;
            }

            if (Validações.ValidaNumero(TextBoxPrice.Text))
            {
                TextBoxPrice.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                TextBoxPrice.Background = Brushes.White;
            }

            if (Validações.ValidaNumero(TextBoxRepReq.Text))
            {
                TextBoxRepReq.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                TextBoxRepReq.Background = Brushes.White;
            }
            
            //verifica se existem caracteres especiais
            if (Validações.ValidaNumero(TextBoxMaxSpeed.Text))
            {
                openWarning++;
                TextBoxMaxSpeed.Background = Brushes.Red;
            }
            else
            {
                TextBoxMaxSpeed.Background = Brushes.White;
                if (openWarning == 0)
                {
                    switch (dbAction)
                    {
                        case SQL_INSERT:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("Carro inserido com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_DELETE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("Carro eliminado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_UPDATE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("Carro atualizado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
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
