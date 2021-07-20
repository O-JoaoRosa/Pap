using static CRUD.ClassesEntidades.SQL.SQL_Connection;
using static CRUD.ClassesEntidades.Settings;
using System.Windows;
using System.Windows.Controls;
using Desktop___interfaces.ClassesEntidades.SQL;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowServerList.xaml
    /// </summary>
    public partial class WindowServerList : Window
    {
        int listOrder = LIST_NULL;
        int listAction = -1;
        int nPag = 1;

        #region load
        /// <summary>
        /// metodo executado assim que a window é inicializada
        /// </summary>
        public WindowServerList(int action)
        {
            InitializeComponent();
            listAction = action;
        }

        /// <summary>
        /// metedo executado assim que a window é carregada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListView.ItemsSource = SqlServer.GetAll(listOrder, null ,null ,null ,null, nPag, 10);
            if (listAction != LIST_ACTION_ID && listAction != LIST_ACTION_ID_FRIEND)
            {
                LabelSubTitle.Visibility = Visibility.Hidden;
            }
            else
            {
                LabelSubTitle.Visibility = Visibility.Visible;
                ButtonDelete.Visibility = Visibility.Hidden;
                ButtonEdit.Visibility = Visibility.Hidden;
            }
        }
        #endregion

        #region buttons
        /// <summary>
        /// metodo que será executado assim que o botão de cancelar for carregado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// metodo que irá ser executado assim que o botão de editar for carregado
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
                    Server serverClickado = (Server)ListView.SelectedItems[0];

                    //verifica se o item selecionado está vazio ou não
                    if (serverClickado != null)
                    {
                        //abre a janela de edição com a informação necessaria para definir o que fazer
                        Window w = new WindowServerInsert(SQL_UPDATE, serverClickado);
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
            else
            {
                //verifica se algum item foi selecionado 
                if (ListView.SelectedItems.Count > 0)
                {
                    //vai buscar o item selecionado
                    Server serverClickado = (Server)ListView.SelectedItems[0];

                    //verifica se o item selecionado está vazio ou não
                    if (serverClickado != null)
                    {
                        serverTemp = serverClickado;
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
        /// Metodo executado sempre que o botão eliminar é carregado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            //verifica se algum item foi selecionado 
            if (ListView.SelectedItems.Count > 0)
            {
                //vai buscar o item selecionado
                Server serverClickado = (Server)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (serverClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowServerInsert(SQL_DELETE, serverClickado);
                    w.ShowDialog();
                }
            }
            else
            {
                //avisa que nenhum item foi selecionado
                MessageBox.Show("Erro : nenhum item selecionado", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            ListView.ItemsSource = SqlServer.GetAll(listOrder, TextBoxFrom.Text, TextBoxUntil.Text,
                TextBoxFromObs.Text, TextBoxUntilObs.Text, nPag, 10);    // Reassocia a listAlunos à ListView
        }


        /// <summary>
        /// metodo que dependedo do header clicado irá alterar a ordem da lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewHeader_Click(object sender, RoutedEventArgs e)
        {
            //verifica que headr foi clickado
            if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Descrição")
            {
                //if utilizado para alterar a ordem da lista 
                if (listOrder == LIST_DESCRI_ASC)
                {
                    listOrder = LIST_DESCRI_DESC;
                }
                else
                {
                    listOrder = LIST_DESCRI_ASC;
                }
            }
            else
            {
                if (listOrder == LIST_OBS_ASC)
                {
                    listOrder = LIST_OBS_DESC;
                }
                else
                {
                    listOrder = LIST_OBS_ASC;
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
            ListView.ItemsSource = SqlServer.GetAll(listOrder, null, null, null, null, nPag, 10);
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
                ListView.ItemsSource = SqlServer.GetAll(listOrder, null, null, null, null, nPag, 10);
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
