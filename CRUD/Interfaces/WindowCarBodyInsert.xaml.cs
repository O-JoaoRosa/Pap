using CRUD.ClassesEntidades;
using System;
using System.Windows;
using System.Windows.Media;
using CRUD.ClassesEntidades.SQL;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowCarBodyInsert.xaml
    /// </summary>
    public partial class WindowCarBodyInsert : Window
    {
        CarBody carBodyTemp;
        int dbAction = -1;
        int openWarning = 0;

        #region Load
        public WindowCarBodyInsert(int action, CarBody carBody)
        {
            InitializeComponent();
            carBodyTemp = carBody;
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
                    TextBoxDescri.Text = carBodyTemp.Descri;
                    TextBoxCodeName.Text = carBodyTemp.CodeName;
                    TextBoxPrice.Text = carBodyTemp.Price.ToString();
                    TextBoxColor.Text = carBodyTemp.Paint;
                    ButtonAction.Content = "Eliminar";
                    TextBoxDescri.IsEnabled = false;
                    TextBoxCodeName.IsEnabled = false;
                    TextBoxPrice.IsEnabled = false;
                    TextBoxColor.IsEnabled = false;
                    break;

                case SQL_Connection.SQL_UPDATE:
                    LabelTitle.Content = "Eliminar o roda";
                    TextBoxDescri.Text = carBodyTemp.Descri;
                    TextBoxCodeName.Text = carBodyTemp.CodeName;
                    TextBoxPrice.Text = carBodyTemp.Price.ToString();
                    TextBoxColor.Text = carBodyTemp.Paint;
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
                        CarBody carBody = new CarBody(-1, Convert.ToInt32(TextBoxPrice.Text),
                        TextBoxColor.Text,
                        TextBoxDescri.Text,
                        TextBoxCodeName.Text
                        );

                        SqlCarBody.Add(carBody);
                    }
                    break;

                case SQL_Connection.SQL_DELETE:
                    ValidaDados();

                    //elimina o utilizador escolhido
                    SqlCarBody.Del(carBodyTemp);
                    this.Close();
                    break;

                case SQL_Connection.SQL_UPDATE:

                    //verifica se os dados estão validos ou não
                    if (ValidaDados())
                    {
                        //altera os atributos da entidade
                        carBodyTemp.Descri = TextBoxDescri.Text;
                        carBodyTemp.Paint = TextBoxColor.Text;
                        carBodyTemp.CodeName = TextBoxCodeName.Text;
                        carBodyTemp.Price = Convert.ToInt32(TextBoxPrice.Text);
                        SqlCarBody.Set(carBodyTemp);
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
            if (Validações.ValidaNumero(TextBoxPrice.Text))
            {
                TextBoxPrice.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                TextBoxPrice.Background = Brushes.White;
            }

            //verifica se existem caracteres especiais
            if (Validações.ValidaColor(TextBoxColor.Text))
            {
                TextBoxColor.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                TextBoxColor.Background = Brushes.White;
            }

            //verifica se existem caracteres especiais
            if (Validações.ValidaTexto(TextBoxCodeName.Text))
            {
                TextBoxCodeName.Background = Brushes.Red;
                openWarning++;
            }
            else
            {
                TextBoxCodeName.Background = Brushes.White;
                if (openWarning == 0)
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
