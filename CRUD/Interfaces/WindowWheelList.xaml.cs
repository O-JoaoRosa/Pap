using CRUD.ClassesEntidades.SQL;
using System.Windows;
using System.Windows.Controls;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;
using static CRUD.ClassesEntidades.Settings;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowWheelList.xaml
    /// </summary>
    public partial class WindowWheelList : Window
    {
        int listOrder = LIST_NULL;
        int listAction = -1;
        int nPag = 1;

        #region load

        /// <summary>
        /// metodo executado quando a window é chamada
        /// </summary>
        public WindowWheelList(int action)
        {
            InitializeComponent();
            listAction = action;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListView.ItemsSource = SqlWheel.GetAll(listOrder, null, null, null, null, nPag, 10); 
            if (listAction != LIST_ACTION_ID)
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
                    Wheel wheelClicked = (Wheel)ListView.SelectedItems[0];

                    //verifica se o item selecionado está vazio ou não
                    if (wheelClicked != null)
                    {
                        //abre a janela de edição com a informação necessaria para definir o que fazer
                        Window w = new WindowWheelInsert(SQL_Connection.SQL_UPDATE, wheelClicked);
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
                    Wheel wheelClicked = (Wheel)ListView.SelectedItems[0];

                    //verifica se o item selecionado está vazio ou não
                    if (wheelClicked != null)
                    {
                        wheelTemp = wheelClicked;
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
                Wheel wheelClicked = (Wheel)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (wheelClicked != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowWheelInsert(SQL_Connection.SQL_DELETE, wheelClicked);
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

            ListView.ItemsSource = SqlWheel.GetAll(listOrder, 
                TextBoxFrom.Text, TextBoxUntil.Text, 
                TextBoxFromCodeName.Text, TextBoxUntilCodeName.Text, 
                nPag, 10);    // Reassocia a listAlunos à ListView
        }

        /// <summary>
        /// metodo que dependedo do header clicado irá alterar a ordem da lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewHeader_Click(object sender, RoutedEventArgs e)
        {

            if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Descrição")
            {
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
                if (listOrder == LIST_CODENAME_ASC)
                {
                    listOrder = LIST_CODENAME_DESC;
                }
                else
                {
                    listOrder = LIST_CODENAME_ASC;
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
            ListView.ItemsSource = SqlWheel.GetAll(listOrder, null, null, null, null, nPag, 10);
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
                ListView.ItemsSource = SqlWheel.GetAll(listOrder, null, null, null, null, nPag, 10);
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
