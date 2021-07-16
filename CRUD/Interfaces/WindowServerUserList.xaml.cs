using CRUD.ClassesEntidades.SQL;
using Desktop___interfaces.ClassesEntidades.SQL;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowServerUserList.xaml
    /// </summary>
    public partial class WindowServerUserList : Window
    {
        int listOrder = LIST_NULL;
        int nPag = 1;
        List<ServerUser> listatemp = new List<ServerUser>();

        #region load

        public WindowServerUserList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<ServerUser> lista = SqlServerUser.GetAll(null,null,null,null, nPag, 10);
            List<ServerUser> listatemp = new List<ServerUser>();
            ServerUser serverUser;
            foreach (ServerUser p in lista)
            {
                serverUser = p;
                serverUser.Server = SqlServer.Get(p.Server.Id);
                serverUser.User = SqlUser.Get(p.User.Id);
                serverUser.ServerUserState = SqlUserServerState.Get(p.ServerUserState.Id);
                listatemp.Add(serverUser);
            }
            ListView.ItemsSource = listatemp;
        }

        #endregion

        #region Buttons

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Metodo que irá ser executado quando o botão edit for carregado
        /// server para editar o profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            //verifica se algum item foi selecionado 
            if (ListView.SelectedItems.Count > 0)
            {
                //vai buscar o item selecionado
                ServerUser profileClickado = (ServerUser)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (profileClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowServerUserInsert(SQL_Connection.SQL_UPDATE, profileClickado);
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
        /// server para eliminar um profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            //verifica se algum item foi selecionado 
            if (ListView.SelectedItems.Count > 0)
            {
                //vai buscar o item selecionado
                ServerUser profileClickado = (ServerUser)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (profileClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowServerUserInsert(SQL_Connection.SQL_DELETE, profileClickado);
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
        /// metodo para a mudança de pagina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNextPage_Click(object sender, RoutedEventArgs e)
        {
            nPag = nPag + 1;
            ListView.ItemsSource = null;                  // Elimina a associação da List à listView
            ListView.Items.Clear();                       // Limpa a ListView
            List<ServerUser> lista = SqlServerUser.GetAll(null, null, null, null, nPag, 10);
            List<ServerUser> listatemp = new List<ServerUser>();
            ServerUser serverUser;
            foreach (ServerUser p in lista)
            {
                serverUser = p;
                serverUser.Server = SqlServer.Get(p.Server.Id);
                serverUser.User = SqlUser.Get(p.User.Id);
                serverUser.ServerUserState = SqlUserServerState.Get(p.ServerUserState.Id);
                listatemp.Add(serverUser);
            }
            ListView.ItemsSource = listatemp;
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
                List<ServerUser> lista = SqlServerUser.GetAll(null, null, null, null, nPag, 10);
                List<ServerUser> listatemp = new List<ServerUser>();
                ServerUser serverUser;
                foreach (ServerUser p in lista)
                {
                    serverUser = p;
                    serverUser.Server = SqlServer.Get(p.Server.Id);
                    serverUser.User = SqlUser.Get(p.User.Id);
                    serverUser.ServerUserState = SqlUserServerState.Get(p.ServerUserState.Id);
                    listatemp.Add(serverUser);
                }
                ListView.ItemsSource = listatemp;
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

            List<ServerUser> lista = SqlServerUser.GetAll(TextBoxFromUser.Text, TextBoxUntilUser.Text, TextBoxFromServer.Text, TextBoxUntilServer.Text, nPag, 10);
            listatemp = new List<ServerUser>();
            ServerUser serverUser;
            foreach (ServerUser p in lista)
            {
                serverUser = p;
                serverUser.Server = SqlServer.Get(p.Server.Id);
                serverUser.User = SqlUser.Get(p.User.Id);
                serverUser.ServerUserState = SqlUserServerState.Get(p.ServerUserState.Id);
                listatemp.Add(serverUser);
            }
            ListView.ItemsSource = listatemp;
        }

        /// <summary>
        /// metodo que dependedo do header clicado irá alterar a ordem da lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewHeader_Click(object sender, RoutedEventArgs e)
        {
            //verifica que headr foi clickado
            if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Utilizador")
            {
                if (listOrder == 100)
                {
                    List<ServerUser> SortedList = listatemp.OrderByDescending(o => o.User.UserName).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 101;
                }
                else
                {
                    List<ServerUser> SortedList = listatemp.OrderBy(o => o.User.UserName).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 100;
                }
            }
            else if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Servidor")
            {
                if (listOrder == 200)
                {
                    List<ServerUser> SortedList = listatemp.OrderByDescending(o => o.Server.Descri).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 201;
                }
                else
                {
                    List<ServerUser> SortedList = listatemp.OrderBy(o => o.Server.Descri).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 200;
                }
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
