using CRUD.ClassesEntidades.SQL;
using CRUD.ClassesEntidades;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;
namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowUserRaceInsert.xaml
    /// </summary>
    public partial class WindowUserRaceInsert : Window
    {
        int openWarning = 0;
        int dbAction = -1;
        UserRace userRace;

        #region inicialização

        /// <summary>
        /// metodo executado quando a window é incializada 
        /// </summary>
        public WindowUserRaceInsert(int action, UserRace us)
        {
            InitializeComponent();
            userRace = us;
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

            List<RaceTrack> listaUserRaces = SqlRaceTrack.GetAll(LIST_NULL);
            ComboBoxRaceTack.ItemsSource = listaUserRaces;
            ComboBoxRaceTack.DisplayMemberPath = "Descri";

            //altera a interface consuante a ação escolhida
            switch (dbAction)
            {
                case SQL_DELETE:
                    LabelTitle.Content = "Eliminar entidade Amigo do utilizador";
                    TextBoxDinheroGanho.Text= userRace.MoneyMade.ToString();
                    ComboBoxUser.Text = userRace.User.UserName;
                    ComboBoxRaceTack.Text = userRace.RaceTrack.Descri;
                    DatePicker.SelectedDate = userRace.DateRace;
                    TextBoxPosicionamento.Text = userRace.FinishPosition.ToString();
                    TextBoxRepMade.Text = userRace.ReputationMade.ToString();


                    ComboBoxUser.IsEnabled = false;
                    ComboBoxRaceTack.IsEnabled = false;
                    DatePicker.IsEnabled = false;
                    TextBoxPosicionamento.IsEnabled = false;
                    TextBoxRepMade.IsEnabled = false;
                    TextBoxDinheroGanho.IsEnabled = false;
                    ButtonAction.Content = "Eliminar";

                    break;
                case SQL_UPDATE:
                    LabelTitle.Content = "Editar entidade Amigo do utilizador";
                    TextBoxDinheroGanho.Text = userRace.MoneyMade.ToString();
                    ComboBoxUser.Text = userRace.User.UserName;
                    ComboBoxRaceTack.Text = userRace.RaceTrack.Descri;
                    DatePicker.SelectedDate = userRace.DateRace;
                    TextBoxPosicionamento.Text = userRace.FinishPosition.ToString();
                    TextBoxRepMade.Text = userRace.ReputationMade.ToString();
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
                        if ((RaceTrack)ComboBoxRaceTack.SelectedItem == null)
                        {
                            openWarning++;
                            ComboBoxUser.Background = Brushes.Red;
                        }
                        else
                        {
                            if (ValidaDados())
                            {
                                UserRace profileTemp = new UserRace(-1,Convert.ToInt32(TextBoxPosicionamento.Text),
                                    Convert.ToInt32(TextBoxDinheroGanho.Text), Convert.ToInt32(TextBoxRepMade.Text),
                                    DatePicker.SelectedDate.Value, (RaceTrack)ComboBoxRaceTack.SelectedItem, (User)ComboBoxUser.SelectedItem);
                                SqlUserRace.Add(profileTemp);
                            }
                        }
                    }
                    break;

                //caso a ação escolhida seja Eliminar então elimina o objeto
                case SQL_DELETE:
                    if (ValidaDados())
                    {
                        SqlUserRace.Del(userRace);
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
                        if ((RaceTrack)ComboBoxRaceTack.SelectedItem == null)
                        {
                            openWarning++;
                            ComboBoxUser.Background = Brushes.Red;
                        }
                        else
                        {
                            if (ValidaDados())
                            {
                                userRace.DateRace = DatePicker.SelectedDate.Value;
                                userRace.FinishPosition = Convert.ToInt32(TextBoxPosicionamento.Text);
                                userRace.MoneyMade = Convert.ToInt32(TextBoxDinheroGanho.Text);
                                userRace.ReputationMade = Convert.ToInt32(TextBoxRepMade.Text);
                                userRace.User = (User)ComboBoxUser.SelectedItem;
                                userRace.RaceTrack = (RaceTrack)ComboBoxRaceTack.SelectedItem;
                                SqlUserRace.Set(userRace);
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
            if (Validações.ValidaNumero(TextBoxDinheroGanho.Text))
            {
                TextBoxDinheroGanho.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                TextBoxDinheroGanho.Background = Brushes.White;
            }

            //verifica se existem caracteres especiais
            if (Validações.ValidaNumero(TextBoxPosicionamento.Text))
            {
                TextBoxPosicionamento.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                TextBoxPosicionamento.Background = Brushes.White;
            }
            
            //verifica se existem caracteres especiais
            if (Validações.ValidaNumero(TextBoxRepMade.Text))
            {
                TextBoxRepMade.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                TextBoxRepMade.Background = Brushes.White;
            }

            //verifica se existem caracteres especiais
            if ((RaceTrack)ComboBoxRaceTack.SelectedItem == null)
            {
                openWarning++;
                ComboBoxRaceTack.Background = Brushes.Red;
            }
            else
            {
                ComboBoxRaceTack.Background = Brushes.White;
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
