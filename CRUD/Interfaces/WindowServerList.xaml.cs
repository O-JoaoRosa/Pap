using static CRUD.ClassesEntidades.SQL.SQL_Connection;
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
        /// <summary>
        /// metodo executado assim que a window é inicializada
        /// </summary>
        public WindowServerList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// metedo executado assim que a window é carregada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListView.ItemsSource = SqlServer.GetAll();
        }


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
            //verifica se algum item foi selecionado 
            if(ListView.SelectedItems.Count > 0)
            {
                //vai buscar o item selecionado
                Server serverClickado = (Server)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if(serverClickado != null)
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

        /// <summary>
        /// faz refresh a list View
        /// </summary>
        private void RefreshListView()
        {
            ListView.ItemsSource = null;                  // Elimina a associação da List à listView
            ListView.Items.Clear();                       // Limpa a ListView
            ListView.ItemsSource = SqlServer.GetAll();    // Reassocia a listAlunos à ListView
        }
    }
}
