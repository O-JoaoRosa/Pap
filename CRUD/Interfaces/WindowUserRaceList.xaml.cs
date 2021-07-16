using CRUD.ClassesEntidades.SQL;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowUserRaceList.xaml
    /// </summary>
    public partial class WindowUserRaceList : Window
    {
        int listOrder = LIST_NULL;
        List<UserRace> listatemp = new List<UserRace>();
        int nPag =1;

        #region load
        public WindowUserRaceList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //cria uma lista temporaria e uma lista que irá ser assiciada a listView
            List<UserRace> lista = SqlUserRace.GetAll(null, null,null,null,null,null, nPag, 10);
            listatemp = new List<UserRace>();
            UserRace userRace;

            //vai a cada elemneto da lista GetAll e controi as fks de maneira completa
            foreach (UserRace p in lista)
            {
                userRace = p;
                userRace.User = SqlUser.Get(p.User.Id);
                userRace.RaceTrack = SqlRaceTrack.Get(p.RaceTrack.Id);
                listatemp.Add(userRace);
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
                UserRace profileClickado = (UserRace)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (profileClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowUserRaceInsert(SQL_Connection.SQL_UPDATE, profileClickado);
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
                UserRace profileClickado = (UserRace)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (profileClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowUserRaceInsert(SQL_Connection.SQL_DELETE, profileClickado);
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

            //cria uma lista temporaria e uma lista que irá ser assiciada a listView
            List<UserRace> lista = SqlUserRace.GetAll(TextBoxFromUserName.Text, TextBoxUntilUserName.Text, TextBoxFromRaceTrack.Text, TextBoxUntilRaceTrack.Text, TextBoxFromDate.Text, TextBoxUntilDate.Text, nPag, 10);
            listatemp = new List<UserRace>();
            UserRace userRace;

            //vai a cada elemneto da lista GetAll e controi as fks de maneira completa
            foreach (UserRace p in lista)
            {
                userRace = p;
                userRace.User = SqlUser.Get(p.User.Id);
                userRace.RaceTrack = SqlRaceTrack.Get(p.RaceTrack.Id);
                listatemp.Add(userRace);
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
                    List<UserRace> SortedList = listatemp.OrderByDescending(o => o.User.UserName).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 101;
                }
                else
                {
                    List<UserRace> SortedList = listatemp.OrderBy(o => o.User.UserName).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 100;
                }
            }
            else if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Pista de Corrida")
            {
                if (listOrder == 200)
                {
                    List<UserRace> SortedList = listatemp.OrderByDescending(o => o.RaceTrack.Descri).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 201;
                }
                else
                {
                    List<UserRace> SortedList = listatemp.OrderBy(o => o.RaceTrack.Descri).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 200;
                }
            }else if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Data da corrida")
            {
                if (listOrder == 300)
                {
                    List<UserRace> SortedList = listatemp.OrderByDescending(o => o.DateRace).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 301;
                }
                else
                {
                    List<UserRace> SortedList = listatemp.OrderBy(o => o.DateRace).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 300;
                }
            }
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
            //cria uma lista temporaria e uma lista que irá ser assiciada a listView
            List<UserRace> lista = SqlUserRace.GetAll(null, null, null, null, null, null, nPag, 10);
            listatemp = new List<UserRace>();
            UserRace userRace;

            //vai a cada elemneto da lista GetAll e controi as fks de maneira completa
            foreach (UserRace p in lista)
            {
                userRace = p;
                userRace.User = SqlUser.Get(p.User.Id);
                userRace.RaceTrack = SqlRaceTrack.Get(p.RaceTrack.Id);
                listatemp.Add(userRace);
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
                                                              //cria uma lista temporaria e uma lista que irá ser assiciada a listView
                List<UserRace> lista = SqlUserRace.GetAll(null, null, null, null, null, null, nPag, 10);
                listatemp = new List<UserRace>();
                UserRace userRace;

                //vai a cada elemneto da lista GetAll e controi as fks de maneira completa
                foreach (UserRace p in lista)
                {
                    userRace = p;
                    userRace.User = SqlUser.Get(p.User.Id);
                    userRace.RaceTrack = SqlRaceTrack.Get(p.RaceTrack.Id);
                    listatemp.Add(userRace);
                }
                ListView.ItemsSource = listatemp;
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
