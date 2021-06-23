using CRUD.ClassesEntidades.SQL;
using Desktop___interfaces.ClassesEntidades;
using Desktop___interfaces.ClassesEntidades.SQL;
using System.Windows;
using System.Windows.Media;
using static Desktop___interfaces.ClassesEntidades.SQL_Connection;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowUserTypeInsert.xaml
    /// </summary>
    public partial class WindowUserTypeInsert : Window
    {
        int dbActions;
        UserType userTypeTemp;


        /// <summary>
        /// metodo que é executado quando a window é chamada
        /// </summary>
        /// <param name="userTemp"></param>
        /// <param name="action"></param>
        public WindowUserTypeInsert(UserType userTemp, int action)
        {
            InitializeComponent();
            userTypeTemp = userTemp;
            dbActions = action;
        }

        /// <summary>
        /// metodo executado quando a window da load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //muda a interface dependendo da action
            switch (dbActions)
            {
                case SQL_DELETE:
                    TextBoxDescri.Text = userTypeTemp.Descri;
                    TextBoxDescri.IsEnabled = false;
                    ButtonAction.Content = "Eliminar";
                    LabelTitulo.Content = "Eliminar tipo de utilizador";
                    break;

                case SQL_UPDATE:
                    TextBoxDescri.Text = userTypeTemp.Descri;
                    ButtonAction.Content = "Editar";
                    LabelTitulo.Content = "Editar tipo de utilizador";
                    break;
            }
        }

        /// <summary>
        /// metodo que é executado quando o botão cancelar é carregado
        /// fecha a window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// metodo que é executado quando o botão Action é carregado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAction_Click(object sender, RoutedEventArgs e)
        {
            // Executar o comando SQL DML. É a flag SqlDml que decide.
            switch (dbActions)
            {
                //caso a opção escolhida seja inserir então inser um novo objeto
                case SQL_INSERT:
                    if (ValidaDados())
                    {
                        UserType userType = new UserType(-1, TextBoxDescri.Text);
                        SqlUserType.Add(userType);
                    }
                    break;

                //caso a ação escolhida seja Eliminar então elimina o objeto
                case SQL_DELETE:
                    if (ValidaDados())
                    {
                        SqlUserType.Del(userTypeTemp);
                    }
                    break;

                //caso a ação escolhida seja Update então atualiza os atributos do objeto
                case SQL_UPDATE:
                    if (ValidaDados())
                    {
                        userTypeTemp.Descri = TextBoxDescri.Text;
                        SqlUserType.Set(userTypeTemp);
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
            bool openWarning = false;

            //verifica se existem caracteres especiais
            if (Validações.ValidaTexto(TextBoxDescri.Text))
            {
                TextBoxDescri.Background = Brushes.Red;
            }
            else
            {
                TextBoxDescri.Background = Brushes.White;
                openWarning = false;

                switch (dbActions)
                {
                    case SQL_INSERT:

                        //faz uma message box para avisar o utilizador
                        MessageBox.Show("UserType inserido com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                        return true;

                    case SQL_DELETE:

                        //faz uma message box para avisar o utilizador
                        MessageBox.Show("UserType eliminado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                        return true;

                    case SQL_UPDATE:

                        //faz uma message box para avisar o utilizador
                        MessageBox.Show("UserType atualizado com sucesso", "Sucesso!", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                        return true;
                }
            }
            
            //verifica se 
            if (openWarning)
            {
                //faz uma message box para avisar o utilizador
                MessageBox.Show("Erro : caracteres inválidos", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                openWarning = false;
                return false;
            }
            return false;
        }
    }
}
