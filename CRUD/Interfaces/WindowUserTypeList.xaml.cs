using CRUD.ClassesEntidades.SQL;
using System.Windows;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;
using static CRUD.ClassesEntidades.Settings;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowUserTypeList.xaml
    /// </summary>
    public partial class WindowUserTypeList : Window
    {
        int listOrder = LIST_NULL;
        int listAction = -1;
        int nPag = 1;

        /// <summary>
        /// metodo que é executade quando a window é chamada 
        /// </summary>
        public WindowUserTypeList(int action)
        {
            InitializeComponent();
            listAction = action;
        }

        /// <summary>
        /// metodo que é executado quando a window é loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListView.ItemsSource = SqlUserType.GetAll(listOrder, null, null, nPag, 10); 
            if (listAction != LIST_ACTION_ID && listAction != LIST_ACTION_ID_FRIEND)
            {
                LabelSubTitle.Visibility = Visibility.Hidden;
            }
            else
            {
                LabelSubTitle.Visibility = Visibility.Visible;
                ButtonEliminar.Visibility = Visibility.Hidden;
                ButtonEdit.Visibility = Visibility.Hidden;
            }
        }


        #region buttons

        /// <summary>
        /// metodo que é executado quando o botão é carregado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// metodo que é executado quando o botão é carregado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            if (listAction != LIST_ACTION_ID)
            {
                //verifica se algum item foi selecionado 
                if (ListView.SelectedItems.Count > 0)
                {
                    //vai buscar o item selecionado
                    UserType userTypeClicked = (UserType)ListView.SelectedItems[0];

                    //verifica se o item selecionado está vazio ou não
                    if (userTypeClicked != null)
                    {
                        //abre a janela de edição com a informação necessaria para definir o que fazer
                        Window w = new WindowUserTypeInsert(userTypeClicked, SQL_Connection.SQL_UPDATE);
                        w.ShowDialog();
                    }
                }
                else
                {
                    //avisa que nenhum item foi selecionado
                    MessageBox.Show("Erro : nenhum item selecionado", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                RefreshListView(listOrder, null, null);
            }
            else
            {
                //verifica se algum item foi selecionado 
                if (ListView.SelectedItems.Count > 0)
                {
                    //vai buscar o item selecionado
                    UserType userTypeClicked = (UserType)ListView.SelectedItems[0];

                    //verifica se o item selecionado está vazio ou não
                    if (userTypeClicked != null)
                    {
                        userTypeTemp = userTypeClicked;
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
        /// metodo que é executado quando o botão é carregado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEliminar_Click(object sender, RoutedEventArgs e)
        {
            //verifica se algum item foi selecionado 
            if (ListView.SelectedItems.Count > 0)
            {
                //vai buscar o item selecionado
                UserType userTypeClicked = (UserType)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (userTypeClicked != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowUserTypeInsert(userTypeClicked,SQL_Connection.SQL_DELETE);
                    w.ShowDialog();
                }
            }
            else
            {
                //avisa que nenhum item foi selecionado
                MessageBox.Show("Erro : nenhum item selecionado", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            RefreshListView(listOrder, null, null);
        }
        #endregion

        /// <summary>
        /// faz refresh a list View
        /// </summary>
        private void RefreshListView(int order, string from, string until)
        {
            ListView.ItemsSource = null;                  // Elimina a associação da List à listView
            ListView.Items.Clear();                       // Limpa a ListView
            ListView.ItemsSource = SqlUserType.GetAll(order, from, until, nPag, 10);    // Reassocia a listAlunos à ListView
        }

        private void ListViewHeader_Click(object sender, RoutedEventArgs e)
        {
            if (listOrder == LIST_DESCRI_ASC)
            {
                listOrder = LIST_DESCRI_DESC;
            }
            else
            {
                listOrder = LIST_DESCRI_ASC;
            }
            RefreshListView(listOrder, null, null);
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            RefreshListView(listOrder, TextBoxFrom.Text, TextBoxUntil.Text);
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
            ListView.ItemsSource = SqlUserType.GetAll(listOrder, null, null, nPag, 10);
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
                ListView.ItemsSource = SqlUserType.GetAll(listOrder, null, null, nPag, 10);
            }
        }
    }
}
