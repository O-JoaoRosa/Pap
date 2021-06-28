using Desktop___interfaces.ClassesEntidades;
using System;
using System.Windows;
using System.Windows.Media;
using CRUD.ClassesEntidades.SQL;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowWheelInsert.xaml
    /// </summary>
    public partial class WindowWheelInsert : Window
    {
        Wheel wheelTemp;
        int dbAction = -1;
        bool openWarning = false;


        #region Load

        /// <summary>
        /// metodo executado quando chamam a window
        /// </summary>
        /// <param name="action"></param>
        /// <param name="wheel"></param>
        public WindowWheelInsert(int action, Wheel wheel)
        {
            InitializeComponent();
            wheelTemp = wheel;
            dbAction = action;
        }

        /// <summary>
        /// metodo executado quando a window é carregada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //altera a interface para encaixar com a action 
            switch (dbAction)
            {
                case SQL_Connection.SQL_DELETE:
                    LabelTitle.Content = "Eliminar o roda";
                    TextBoxDescri.Text = wheelTemp.Descri;
                    TextBoxCodeName.Text = wheelTemp.CodeName;
                    TextBoxPrice.Text = wheelTemp.Price.ToString();
                    TextBoxColor.Text = wheelTemp.Paint;
                    ButtonAction.Content = "Eliminar";
                    TextBoxDescri.IsEnabled = false;
                    TextBoxCodeName.IsEnabled = false;
                    TextBoxPrice.IsEnabled = false;
                    TextBoxColor.IsEnabled = false;
                    break;

                case SQL_Connection.SQL_UPDATE:
                    LabelTitle.Content = "Eliminar o roda";
                    TextBoxDescri.Text = wheelTemp.Descri;
                    TextBoxCodeName.Text = wheelTemp.CodeName;
                    TextBoxPrice.Text = wheelTemp.Price.ToString();
                    TextBoxColor.Text = wheelTemp.Paint;
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
                        Wheel wheel = new Wheel(-1, Convert.ToInt32(TextBoxPrice.Text),
                        TextBoxColor.Text,
                        TextBoxDescri.Text,
                        TextBoxCodeName.Text
                        );

                        SqlWheel.Add(wheel);
                    }
                    break;

                case SQL_Connection.SQL_DELETE:
                    ValidaDados();

                    //elimina o utilizador escolhido
                    SqlWheel.Del(wheelTemp);
                    this.Close();
                    break;

                case SQL_Connection.SQL_UPDATE:

                    //verifica se os dados estão validos ou não
                    if (ValidaDados())
                    {
                        //altera os atributos da entidade
                        wheelTemp.Descri = TextBoxDescri.Text;
                        wheelTemp.Paint = TextBoxColor.Text;
                        wheelTemp.CodeName = TextBoxCodeName.Text;
                        wheelTemp.Price = Convert.ToInt32(TextBoxPrice.Text);
                        SqlWheel.Set(wheelTemp);
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
            if (Validações.ValidaNumero(TextBoxPrice.Text))
            {
                TextBoxPrice.Background = Brushes.Red;
                openWarning = true;
            }
            else
            {
                TextBoxPrice.Background = Brushes.White;
                openWarning = false;
            }

            //verifica se existem caracteres especiais
            if (Validações.ValidaColor(TextBoxColor.Text))
            {
                TextBoxColor.Background = Brushes.Red;
                openWarning = true;
            }
            else
            {
                TextBoxColor.Background = Brushes.White;
                openWarning = false;
            }

            //verifica se existem caracteres especiais
            if (Validações.ValidaTexto(TextBoxCodeName.Text))
            {
                TextBoxCodeName.Background = Brushes.Red;
                openWarning = true;
            }
            else
            {
                TextBoxCodeName.Background = Brushes.White;
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
