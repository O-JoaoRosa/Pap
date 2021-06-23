using Desktop___interfaces.ClassesEntidades;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Desktop___interfaces.ClassesEntidades.SQL;
using System.Windows.Media;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowUserInsert.xaml
    /// </summary>
    public partial class WindowUserInsert : Window
    {
        User userTemp;
        int dbAction = -1;
        bool openWarning = false;

        #region 
        public WindowUserInsert()
        {
            InitializeComponent();
        }
        public WindowUserInsert(int action, User us)
        {
            InitializeComponent();
            userTemp = us;
            dbAction = action;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //altera a interface para encaixar com a action 
            switch (dbAction)
            {
                case SQL_Connection.SQL_DELETE:
                    LabelTitle.Content = "Eliminar o User";
                    TextBoxUserName.Text = userTemp.UserName;
                    TextBoxMoney.Text = userTemp.Money.ToString();
                    TextBoxRep.Text = userTemp.Reputation.ToString();
                    TextBoxPassword.Password = userTemp.Password;
                    TextBoxEmail.Text = userTemp.Email;
                    ButtonAction.Content = "Eliminar";
                    ButtonImagePick.IsEnabled = false;
                    DatePickerLastTimeOnline.SelectedDate = userTemp.LastTimeOnline;
                    TextBoxUserName.IsEnabled = false;
                    TextBoxMoney.IsEnabled = false;
                    TextBoxRep.IsEnabled = false;
                    TextBoxPassword.IsEnabled = false;
                    TextBoxEmail.IsEnabled = false;
                    ButtonImagePick.IsEnabled = false;
                    DatePickerLastTimeOnline.IsEnabled = false;
                    break;

                case SQL_Connection.SQL_UPDATE:
                    LabelTitle.Content = "Editar o User";
                    TextBoxUserName.Text = userTemp.UserName;
                    TextBoxMoney.Text = userTemp.Money.ToString();
                    TextBoxRep.Text = userTemp.Reputation.ToString();
                    TextBoxPassword.Password = userTemp.Password;
                    TextBoxEmail.Text = userTemp.Email;
                    DatePickerLastTimeOnline.SelectedDate = userTemp.LastTimeOnline;
                    ButtonAction.Content = "Editar";
                    break;

            }
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Metodo que é executado quando o utilizador carrega no botão de escolher imagem
        /// abre uma window onde o utilizador pode escolher qual imagem escolher
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonImagePick_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Escolha uma imagem";
            op.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (op.ShowDialog() == true)
            {
                ImagePerfil.Source = new BitmapImage(new Uri(op.FileName));
            }
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
                        User user = new User(-1, TextBoxUserName.Text,
                        Validações.EncodePasswordToBase64(TextBoxPassword.Password),
                        TextBoxEmail.Text,
                         Convert.ToInt32(TextBoxMoney.Text),
                        Convert.ToInt32(TextBoxRep.Text),
                        "ola",
                        DatePickerLastTimeOnline.SelectedDate.Value
                        );
                        SqlUser.Add(user);
                    }
                    break;

                case SQL_Connection.SQL_DELETE:
                    ValidaDados();

                    //elimina o utilizador escolhido
                    SqlUser.Del(userTemp);
                    this.Close();
                    break;

                case SQL_Connection.SQL_UPDATE:

                    //verifica se os dados estão validos ou não
                    if (ValidaDados())
                    {
                        //altera os atributos da entidade
                        userTemp.UserName = TextBoxUserName.Text;
                        userTemp.Money = Convert.ToInt32(TextBoxMoney.Text);
                        userTemp.Reputation = Convert.ToInt32(TextBoxRep.Text);
                        userTemp.Password = TextBoxPassword.Password;
                        userTemp.Email = TextBoxEmail.Text;
                        userTemp.LastTimeOnline = (DateTime)DatePickerLastTimeOnline.SelectedDate;

                        SqlUser.Set(userTemp);
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
            if (Validações.ValidaTexto(TextBoxUserName.Text))
            {
                TextBoxUserName.Background = Brushes.Red;
                openWarning = true;
            }
            else
            {
                TextBoxUserName.Background = Brushes.White;
                openWarning = false;
            }

            //verifica se existem caracteres especiais
            if (Validações.ValidaNumero(TextBoxMoney.Text))
            {
                TextBoxMoney.Background = Brushes.Red;
                openWarning = true;
            }
            else
            {
                TextBoxMoney.Background = Brushes.White;
                openWarning = false;
            }

            //verifica se existem caracteres especiais
            if (Validações.ValidaNumero(TextBoxRep.Text))
            {
                TextBoxRep.Background = Brushes.Red;
                openWarning = true;
            }
            else
            {
                TextBoxRep.Background = Brushes.White;
                openWarning = false;
            }
            
            //verifica se existem caracteres especiais
            if (Validações.ValidaEmail(TextBoxEmail.Text))
            {
                openWarning = true;
                TextBoxEmail.Background = Brushes.Red;
            }
            else
            {
                TextBoxEmail.Background = Brushes.White;
                if (!openWarning)
                {
                    switch (dbAction)
                    {
                        case SQL_Connection.SQL_INSERT:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("User inserido com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_Connection.SQL_DELETE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("User eliminado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                            return true;

                        case SQL_Connection.SQL_UPDATE:

                            //faz uma message box para avisar o utilizador
                            MessageBox.Show("User atualizado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
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
