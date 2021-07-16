using CRUD.ClassesEntidades.SQL;
using System.Windows;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;
using static CRUD.ClassesEntidades.Settings;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowPowerUpList.xaml
    /// </summary>
    public partial class WindowPowerUpList : Window
    {
        int listOrder = LIST_NULL;
        int listAction = -1;

        #region load
        public WindowPowerUpList(int action)
        {
            InitializeComponent();
            listAction = action;
        }

        /// <summary>
        /// metodo executdada quando a window é carregada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListView.ItemsSource = SqlPowerUp.GetAll(listOrder, null, null);
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
            if (listAction != LIST_ACTION_ID)
            {
                //verifica se algum item foi selecionado 
                if (ListView.SelectedItems.Count > 0)
                {
                    //vai buscar o item selecionado
                    PowerUp userClickado = (PowerUp)ListView.SelectedItems[0];

                    //verifica se o item selecionado está vazio ou não
                    if (userClickado != null)
                    {
                        //abre a janela de edição com a informação necessaria para definir o que fazer
                        Window w = new WindowPowerUpInsert(SQL_Connection.SQL_UPDATE, userClickado);
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
                    PowerUp userClickado = (PowerUp)ListView.SelectedItems[0];

                    //verifica se o item selecionado está vazio ou não
                    if (userClickado != null)
                    {
                        powerUpTemp = userClickado;
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
                PowerUp userClickado = (PowerUp)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (userClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowPowerUpInsert(SQL_Connection.SQL_DELETE, userClickado);
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

        #region List Updates
        /// <summary>
        /// faz refresh a list View
        /// </summary>
        private void RefreshListView(int order)
        {
            ListView.ItemsSource = null;                  // Elimina a associação da List à listView
            ListView.Items.Clear();                       // Limpa a ListView
            ListView.ItemsSource = SqlPowerUp.GetAll(order, TextBoxFrom.Text, TextBoxUntil.Text);    // Reassocia a listAlunos à ListView
        }
        
        /// <summary>
        /// metodo que muda a ordenação da lista
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
        /// metodo do butão de pesquisa
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
