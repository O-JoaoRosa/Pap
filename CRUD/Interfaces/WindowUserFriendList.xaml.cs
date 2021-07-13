using CRUD.ClassesEntidades.SQL;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowUserFriendList.xaml
    /// </summary>
    public partial class WindowUserFriendList : Window
    {
        int listOrder = LIST_NULL;
        List<UserFriend> listatemp = new List<UserFriend>();

        #region load
        public WindowUserFriendList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<UserFriend> lista = SqlUserFriend.GetAll();
            listatemp = new List<UserFriend>();
            UserFriend userFriend;
            foreach (UserFriend p in lista)
            {
                userFriend = p;
                userFriend.UserFriend1 = SqlUser.Get(p.UserFriend1.Id);
                userFriend.User = SqlUser.Get(p.User.Id);
                listatemp.Add(userFriend);
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
                UserFriend profileClickado = (UserFriend)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (profileClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowUserFriendInsert(SQL_Connection.SQL_UPDATE, profileClickado);
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
                UserFriend profileClickado = (UserFriend)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (profileClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowUserFriendInsert(SQL_Connection.SQL_DELETE, profileClickado);
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

            List<UserFriend> lista = SqlUserFriend.GetAll();
            listatemp = new List<UserFriend>();
            UserFriend userFriend;
            foreach (UserFriend p in lista)
            {
                userFriend = p;
                userFriend.UserFriend1 = SqlUser.Get(p.UserFriend1.Id);
                userFriend.User = SqlUser.Get(p.User.Id);
                listatemp.Add(userFriend);
            }
            ListView.ItemsSource = listatemp;
        }
        #endregion

        #region lista order

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
                    List<UserFriend> SortedList = listatemp.OrderByDescending(o => o.User.UserName).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 101;
                }
                else
                {
                    List<UserFriend> SortedList = listatemp.OrderBy(o => o.User.UserName).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 100;
                }
            }
            else if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Amigo")
            {
                if (listOrder == 200)
                {
                    List<UserFriend> SortedList = listatemp.OrderByDescending(o => o.UserFriend1.UserName).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 201;
                }
                else
                {
                    List<UserFriend> SortedList = listatemp.OrderBy(o => o.UserFriend1.UserName).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 200;
                }
            }
        }
        #endregion
    }
}
