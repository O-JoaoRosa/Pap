using CRUD.ClassesEntidades.SQL;
using System.Windows;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;


namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowServerUserStateList.xaml
    /// </summary>
    public partial class WindowServerUserStateList : Window
    {
        int listOrder = LIST_NULL;

        #region Loading
        public WindowServerUserStateList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// metodo que é executado quando a window é loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListView.ItemsSource = SqlUserServerState.GetAll(listOrder);
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
            ListView.ItemsSource = SqlUserServerState.GetAll(order);    // Reassocia a listAlunos à ListView
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
            RefreshListView(listOrder);
        }
        #endregion
    }
}
