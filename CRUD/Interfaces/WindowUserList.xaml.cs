using CRUD.ClassesEntidades.SQL;
using System.Windows;
using System.Windows.Controls;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;
using static CRUD.ClassesEntidades.Settings;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowUserList.xaml
    /// </summary>
    public partial class WindowUserList : Window
    {
        int listOrder = LIST_NULL;
        int listAction = -1;
        int nPag = 1;

        #region load
        public WindowUserList(int action)
        {
            InitializeComponent();
            listAction = action;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListView.ItemsSource = SqlUser.GetAll(listOrder, null, null, null, null, null, null, nPag , 10);
            if(listAction != LIST_ACTION_ID && listAction != LIST_ACTION_ID_FRIEND)
            {
                LabelSubTitle.Visibility = Visibility.Hidden;
            }
            else
            {
                LabelSubTitle.Visibility = Visibility.Visible;
                ButtonDel.Visibility = Visibility.Hidden;
                ButtonEdit.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        #region Buttons
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            userTemp = null;
            this.Close();
        }

        /// <summary>
        /// Metodo que irá ser executado quando o botão edit for carregado
        /// server para editar o user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (listAction == LIST_ACTION_NULL)
            {
                //verifica se algum item foi selecionado 
                if (ListView.SelectedItems.Count > 0)
                {
                    //vai buscar o item selecionado
                    User userClickado = (User)ListView.SelectedItems[0];

                    //verifica se o item selecionado está vazio ou não
                    if (userClickado != null)
                    {
                        //abre a janela de edição com a informação necessaria para definir o que fazer
                        Window w = new WindowUserInsert(SQL_Connection.SQL_UPDATE, userClickado);
                        w.ShowDialog();
                    }
                }
                else
                {
                    //avisa que nenhum item foi selecionado
                    MessageBox.Show("Erro : nenhum item selecionado", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                RefreshListView();
            }
            else if (listAction == LIST_ACTION_ID_FRIEND)
            {
                //verifica se algum item foi selecionado 
                if (ListView.SelectedItems.Count > 0)
                {
                    //vai buscar o item selecionado
                    User userClickado = (User)ListView.SelectedItems[0];

                    //verifica se o item selecionado está vazio ou não
                    if (userClickado != null)
                    {
                        friendTemp = userClickado;
                        Close();
                    }
                }
                else
                {
                    //avisa que nenhum item foi selecionado
                    MessageBox.Show("Erro : nenhum item selecionado", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                //verifica se algum item foi selecionado 
                if (ListView.SelectedItems.Count > 0)
                {
                    //vai buscar o item selecionado
                    User userClickado = (User)ListView.SelectedItems[0];

                    //verifica se o item selecionado está vazio ou não
                    if (userClickado != null)
                    {
                        userTemp = userClickado;
                        Close();
                    }
                }
                else
                {
                    //avisa que nenhum item foi selecionado
                    MessageBox.Show("Erro : nenhum item selecionado", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Metodo que irá ser executado quando o botão de eliminar for selecionado
        /// server para eliminar um user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            //verifica se algum item foi selecionado 
            if (ListView.SelectedItems.Count > 0)
            {
                //vai buscar o item selecionado
                User userClickado = (User)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (userClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowUserInsert(SQL_Connection.SQL_DELETE, userClickado);
                    w.ShowDialog();
                }
            }
            else
            {
                //avisa que nenhum item foi selecionado
                MessageBox.Show("Erro : nenhum item selecionado", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            RefreshListView();
        }
        #endregion

        #region List Updates
        /// <summary>
        /// faz refresh a list View
        /// </summary>
        private void RefreshListView()
        {
            ListView.ItemsSource = null;                  // Elimina a associação da List à listView
            ListView.Items.Clear();                       // Limpa a ListView
            ListView.ItemsSource = SqlUser.GetAll(listOrder, TextBoxFromUserName.Text, TextBoxUntilUserName.Text, TextBoxFromEmail.Text, TextBoxUntilEmail.Text, TextBoxFromLastTimeOnline.Text, TextBoxUntilLastTimeOnline.Text, nPag , 10);    // Reassocia a listAlunos à ListView
        }

        /// <summary>
        /// metodo que dependedo do header clicado irá alterar a ordem da lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewHeader_Click(object sender, RoutedEventArgs e)
        {
            //verifica que headr foi clickado
            if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Nome de utilizador")
            {
                //if utilizado para alterar a ordem da lista 
                if (listOrder == LIST_USERNAME_ASC)
                {
                    listOrder = LIST_USERNAME_DESC;
                }
                else
                {
                    listOrder = LIST_USERNAME_ASC;
                }
            }
            else if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Email")
            {
                if (listOrder == LIST_EMAIL_ASC)
                {
                    listOrder = LIST_EMAIL_DESC;
                }
                else
                {
                    listOrder = LIST_EMAIL_ASC;
                }
            }
            else
            {
                if (listOrder == LIST_LASTTIMEONLINE_ASC)
                {
                    listOrder = LIST_LASTTIMEONLINE_DESC;
                }
                else
                {
                    listOrder = LIST_LASTTIMEONLINE_ASC;
                }
            }

            RefreshListView();
        }

        /// <summary>
        /// metodo para a mudança de pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNextPage_Click(object sender, RoutedEventArgs e)
        {
            nPag = nPag + 1; 
            ListView.ItemsSource = null;                  // Elimina a associação da List à listView
            ListView.Items.Clear();                       // Limpa a ListView
            ListView.ItemsSource = SqlUser.GetAll(listOrder, null, null, null, null, null, null, nPag, 10);
        }

        /// <summary>
        /// metodo para a mudança de pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (nPag != 1)
            {
                nPag = nPag - 1;
                ListView.ItemsSource = null;                  // Elimina a associação da List à listView
                ListView.Items.Clear();                       // Limpa a ListView
                ListView.ItemsSource = SqlUser.GetAll(listOrder, null, null, null, null, null, null, nPag, 10);
            }
        }

        /// <summary>
        /// metodo do butão de pesquisa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            RefreshListView();
        }

        #endregion

    }
}
