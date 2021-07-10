using CRUD.ClassesEntidades.SQL;
using Desktop___interfaces.ClassesEntidades;
using System.Windows;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowUserTypeList.xaml
    /// </summary>
    public partial class WindowUserTypeList : Window
    {
        int listOrder = LIST_NULL;

        /// <summary>
        /// metodo que é executade quando a window é chamada 
        /// </summary>
        public WindowUserTypeList()
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
            ListView.ItemsSource = SqlUserType.GetAll(listOrder);
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
            //verifica se algum item foi selecionado 
            if (ListView.SelectedItems.Count > 0)
            {
                //vai buscar o item selecionado
                UserType userTypeClicked = (UserType)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (userTypeClicked != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowUserTypeInsert( userTypeClicked, SQL_Connection.SQL_UPDATE);
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

            RefreshListView(listOrder);
        }
        #endregion

        /// <summary>
        /// faz refresh a list View
        /// </summary>
        private void RefreshListView(int order)
        {
            ListView.ItemsSource = null;                  // Elimina a associação da List à listView
            ListView.Items.Clear();                       // Limpa a ListView
            ListView.ItemsSource = SqlUserType.GetAll(order);    // Reassocia a listAlunos à ListView
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
    }
}
