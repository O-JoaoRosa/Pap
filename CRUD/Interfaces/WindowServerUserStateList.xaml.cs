using CRUD.ClassesEntidades.SQL;
using System.Windows;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;
using static CRUD.ClassesEntidades.Settings;


namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowServerUserStateList.xaml
    /// </summary>
    public partial class WindowServerUserStateList : Window
    {
        int listOrder = LIST_NULL;
        int listAction = -1;
        #region Loading
        public WindowServerUserStateList(int action)
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
            ListView.ItemsSource = SqlUserServerState.GetAll(listOrder,null,null);
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
        #endregion

        #region Button Clicks
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
            if (listAction == LIST_ACTION_NULL)
            {
                //verifica se algum item foi selecionado 
                if (ListView.SelectedItems.Count > 0)
                {
                    //vai buscar o item selecionado
                    ServerUserState serverUserTypeClicked = (ServerUserState)ListView.SelectedItems[0];

                    //verifica se o item selecionado está vazio ou não
                    if (serverUserTypeClicked != null)
                    {
                        //abre a janela de edição com a informação necessaria para definir o que fazer
                        Window w = new WindowServerUserStateInsert(serverUserTypeClicked, SQL_Connection.SQL_UPDATE);
                        w.ShowDialog();
                    }
                }
                else
                {
                    //avisa que nenhum item foi selecionado
                    MessageBox.Show("Erro : nenhum item selecionado", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                RefreshListView(listOrder);
            }
            else
            {
                //verifica se algum item foi selecionado 
                if (ListView.SelectedItems.Count > 0)
                {
                    //vai buscar o item selecionado
                    ServerUserState serverUserTypeClicked = (ServerUserState)ListView.SelectedItems[0];

                    //verifica se o item selecionado está vazio ou não
                    if (serverUserTypeClicked != null)
                    {
                        serverUserStateTemp = serverUserTypeClicked;
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
                ServerUserState serverUserTypeClicked = (ServerUserState)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (serverUserTypeClicked != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowServerUserStateInsert(serverUserTypeClicked, SQL_Connection.SQL_DELETE);
                    w.ShowDialog();
                }
            }
            else
            {
                //avisa que nenhum item foi selecionado
                MessageBox.Show("Erro : nenhum item selecionado", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            RefreshListView(listOrder);
        }
        #endregion

        #region organização

        /// <summary>
        /// faz refresh a list View
        /// </summary>
        private void RefreshListView(int order)
        {
            ListView.ItemsSource = null;                  // Elimina a associação da List à listView
            ListView.Items.Clear();                       // Limpa a ListView
            ListView.ItemsSource = SqlUserServerState.GetAll(order, TextBoxFrom.Text, TextBoxUntil.Text);    // Reassocia a listAlunos à ListView
        }

        /// <summary>
        /// metodo usado para organizar a lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            RefreshListView(listOrder);
        }

        /// <summary>
        /// metodo usado para pesquisar a lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            RefreshListView(listOrder);
        }
        #endregion
    }
}
