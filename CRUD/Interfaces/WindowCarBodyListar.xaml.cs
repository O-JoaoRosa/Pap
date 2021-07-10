using CRUD.ClassesEntidades.SQL;
using System.Windows;
using System.Windows.Controls;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowCarBodyListar.xaml
    /// </summary>
    public partial class WindowCarBodyListar : Window
    {
        int listOrder = LIST_NULL;

        #region load
        public WindowCarBodyListar()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListView.ItemsSource = SqlCarBody.GetAll(listOrder);
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
            //verifica se algum item foi selecionado 
            if (ListView.SelectedItems.Count > 0)
            {
                //vai buscar o item selecionado
                CarBody carBodyClicked = (CarBody)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (carBodyClicked != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowCarBodyInsert(SQL_Connection.SQL_UPDATE, carBodyClicked);
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
                CarBody carBodyClicked = (CarBody)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (carBodyClicked != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowCarBodyInsert(SQL_Connection.SQL_DELETE, carBodyClicked);
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
            ListView.ItemsSource = SqlCarBody.GetAll(listOrder);    // Reassocia a listAlunos à ListView
        }

        /// <summary>
        /// metodo que dependedo do header clicado irá alterar a ordem da lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewHeader_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString());

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
        #endregion
    }
}
