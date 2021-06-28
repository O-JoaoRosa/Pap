using CRUD.ClassesEntidades.SQL;
using Desktop___interfaces.ClassesEntidades;
using System;
using System.Windows;
using System.Windows.Media;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowRaceTrackInsert.xaml
    /// </summary>
    public partial class WindowRaceTrackInsert : Window
    {
        RaceTrack raceTarckTemp;
        int dbAction = -1;
        bool openWarning = false;

        public WindowRaceTrackInsert(int action, RaceTrack raceTarck)
        {
            InitializeComponent();
            raceTarckTemp = raceTarck;
            dbAction = action;
        }

        #region Load

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //altera a interface para encaixar com a action 
            switch (dbAction)
            {
                case SQL_Connection.SQL_DELETE:
                    LabelTitle.Content = "Eliminar o User";
                    TextBoxDescri.Text = raceTarckTemp.Descri;
                    TextBoxMoneyBase.Text = raceTarckTemp.BaseMoneyReward.ToString();
                    TextBoxRepBase.Text = raceTarckTemp.BaseReputationReward.ToString();
                    TextBoxRepReq.Text = raceTarckTemp.ReputationRequiered.ToString();
                    ButtonAction.Content = "Eliminar";
                    TextBoxDescri.IsEnabled = false;
                    TextBoxMoneyBase.IsEnabled = false;
                    TextBoxRepBase.IsEnabled = false;
                    TextBoxRepReq.IsEnabled = false;
                    break;

                case SQL_Connection.SQL_UPDATE:
                    LabelTitle.Content = "Editar o User"; 
                    TextBoxDescri.Text = raceTarckTemp.Descri;
                    TextBoxMoneyBase.Text = raceTarckTemp.BaseMoneyReward.ToString();
                    TextBoxRepBase.Text = raceTarckTemp.BaseReputationReward.ToString();
                    TextBoxRepReq.Text = raceTarckTemp.ReputationRequiered.ToString();
                    ButtonAction.Content = "Editar";
                    break;

            }
        }
        #endregion

        #region Button Metodos
        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAction_Click(object sender, RoutedEventArgs e)
        {
            //muda a operação dependendo da action escolhida
            switch (dbAction)
            {
                case SQL_Connection.SQL_INSERT:

                    //verifica se os dados estão validos ou não
                    if (ValidaDados())
                    {
                        //insere um novo user na bd
                        RaceTrack raceTrack = new RaceTrack(-1,
                        TextBoxDescri.Text,
                        Convert.ToInt32(TextBoxRepReq.Text),
                        Convert.ToInt32(TextBoxMoneyBase.Text), 
                        Convert.ToInt32(TextBoxRepBase.Text)
                        );
                        SqlRaceTrack.Add(raceTrack);
                    }
                    break;

                case SQL_Connection.SQL_DELETE:
                    ValidaDados();

                    //elimina o utilizador escolhido
                    SqlRaceTrack.Del(raceTarckTemp);
                    this.Close();
                    break;

                case SQL_Connection.SQL_UPDATE:

                    //verifica se os dados estão validos ou não
                    if (ValidaDados())
                    {
                        //altera os atributos da entidade
                        raceTarckTemp.Descri = TextBoxDescri.Text;
                        raceTarckTemp.ReputationRequiered = Convert.ToInt32(TextBoxRepReq.Text);
                        raceTarckTemp.BaseReputationReward = Convert.ToInt32(TextBoxRepBase.Text);
                        raceTarckTemp.BaseMoneyReward = Convert.ToInt32(TextBoxMoneyBase.Text);

                        SqlRaceTrack.Set(raceTarckTemp);
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
                openWarning = true;
            }
            else
            {
                TextBoxDescri.Background = Brushes.White;
                openWarning = false;
            }

            //verifica se existem caracteres especiais
            if (Validações.ValidaNumero(TextBoxRepReq.Text))
            {
                TextBoxRepReq.Background = Brushes.Red;
                openWarning = true;
            }
            else
            {
                TextBoxRepReq.Background = Brushes.White;
                openWarning = false;
            }

            //verifica se existem caracteres especiais
            if (Validações.ValidaNumero(TextBoxRepBase.Text))
            {
                TextBoxRepBase.Background = Brushes.Red;
                openWarning = true;
            }
            else
            {
                TextBoxRepBase.Background = Brushes.White;
                openWarning = false;
            }

            //verifica se existem caracteres especiais
            if (Validações.ValidaNumero(TextBoxMoneyBase.Text))
            {
                openWarning = true;
                TextBoxMoneyBase.Background = Brushes.Red;
            }
            else
            {
                TextBoxMoneyBase.Background = Brushes.White;
                if (!openWarning)
                {
                    switch (dbAction)
                    {
                        case SQL_Connection.SQL_INSERT:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("RaceTrack inserido com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_Connection.SQL_DELETE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("RaceTrack eliminado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_Connection.SQL_UPDATE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("RaceTrack atualizado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
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
