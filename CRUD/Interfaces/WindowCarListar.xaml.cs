
using System.Windows;
using System.Windows.Controls;
using CRUD.ClassesEntidades.SQL;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowCarListar.xaml
    /// </summary>
    public partial class WindowCarListar : Window
    {
        int listOrder = LIST_NULL;

        #region load
        public WindowCarListar()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListView.ItemsSource = SqlCar.GetAll(LIST_NULL);
        }

        #endregion

        #region Buttons

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Metodo que irá ser executado quando o botão edit for carregado
        /// server para editar o car
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            //verifica se algum item foi selecionado 
            if (ListView.SelectedItems.Count > 0)
            {
                //vai buscar o item selecionado
                Car carClickado = (Car)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (carClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowCarInsert(SQL_Connection.SQL_UPDATE, carClickado);
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
        /// server para eliminar um car
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            //verifica se algum item foi selecionado 
            if (ListView.SelectedItems.Count > 0)
            {
                //vai buscar o item selecionado
                Car carClickado = (Car)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (carClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowCarInsert(SQL_Connection.SQL_DELETE, carClickado);
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
        /// faz refresh a list View
        /// </summary>
        private void RefreshListView()
        {
            ListView.ItemsSource = null;                  // Elimina a associação da List à listView
            ListView.Items.Clear();                       // Limpa a ListView
            ListView.ItemsSource = SqlCar.GetAll(listOrder);    // Reassocia a listAlunos à ListView
        }
        #endregion

        #region List Updates
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
            else if(((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Rep. Req")
            {
                if (listOrder == LIST_REPREQ_ASC)
                {
                    listOrder = LIST_REPREQ_DESC;
                }
                else
                {
                    listOrder = LIST_REPREQ_ASC;
                }
            }
            else if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Price")
            {
                if (listOrder == LIST_PRICE_ASC)
                {
                    listOrder = LIST_PRICE_DESC;
                }
                else
                {
                    listOrder = LIST_PRICE_ASC;
                }
            }
            else
            {
                if (listOrder == LIST_MAXSPEED_ASC)
                {
                    listOrder = LIST_MAXSPEED_DESC;
                }
                else
                {
                    listOrder = LIST_MAXSPEED_ASC;
                }
            }

            RefreshListView();
        }
        #endregion
    }
}
