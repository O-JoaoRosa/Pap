using CRUD.ClassesEntidades.SQL;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowUserCarList.xaml
    /// </summary>
    public partial class WindowUserCarList : Window
    {
        int listOrder = LIST_NULL;
        int nPag = 1;
        List<UserCar> listatemp = new List<UserCar>();

        #region load

        public WindowUserCarList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<UserCar> lista = SqlUserCar.GetAll(null,null,null,null, nPag, 10);
            UserCar userCar;
            foreach (UserCar p in lista)
            {
                userCar = p;
                userCar.Car = SqlCar.Get(p.Car.Id);
                userCar.User = SqlUser.Get(p.User.Id);
                userCar.PowerUp = SqlPowerUp.Get(p.PowerUp.Id);
                userCar.Roda = SqlWheel.Get(p.Roda.Id);
                listatemp.Add(userCar);
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
                UserCar profileClickado = (UserCar)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (profileClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowUserCarInsert(SQL_Connection.SQL_UPDATE, profileClickado);
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
                UserCar profileClickado = (UserCar)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (profileClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowUserCarInsert(SQL_Connection.SQL_DELETE, profileClickado);
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

            List<UserCar> lista = SqlUserCar.GetAll(TextBoxFrom.Text, TextBoxUntil.Text, TextBoxFromObs.Text, TextBoxUntilObs.Text, nPag, 10);
            List<UserCar> listatemp = new List<UserCar>();
            UserCar userCar;
            foreach (UserCar p in lista)
            {
                userCar = p;
                userCar.Car = SqlCar.Get(p.Car.Id);
                userCar.User = SqlUser.Get(p.User.Id);
                userCar.PowerUp = SqlPowerUp.Get(p.PowerUp.Id);
                userCar.Roda = SqlWheel.Get(p.Roda.Id);
                listatemp.Add(userCar);
            }
            ListView.ItemsSource = listatemp;
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
            List<UserCar> lista = SqlUserCar.GetAll(null,null,null,null, nPag, 10);
            UserCar userCar;
            foreach (UserCar p in lista)
            {
                userCar = p;
                userCar.Car = SqlCar.Get(p.Car.Id);
                userCar.User = SqlUser.Get(p.User.Id);
                userCar.PowerUp = SqlPowerUp.Get(p.PowerUp.Id);
                userCar.Roda = SqlWheel.Get(p.Roda.Id);
                listatemp.Add(userCar);
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
                List<UserCar> lista = SqlUserCar.GetAll(null, null, null, null, nPag, 10);
                UserCar userCar;
                foreach (UserCar p in lista)
                {
                    userCar = p;
                    userCar.Car = SqlCar.Get(p.Car.Id);
                    userCar.User = SqlUser.Get(p.User.Id);
                    userCar.PowerUp = SqlPowerUp.Get(p.PowerUp.Id);
                    userCar.Roda = SqlWheel.Get(p.Roda.Id);
                    listatemp.Add(userCar);
                }
            }
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
            if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Nome de utilizador")
            {
                if (listOrder == 100)
                {
                    List<UserCar> SortedList = listatemp.OrderByDescending(o => o.User.UserName).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 101;
                }
                else
                {
                    List<UserCar> SortedList = listatemp.OrderBy(o => o.User.UserName).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 100;
                }
            }
            else if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Carro")
            {
                if (listOrder == 200)
                {
                    List<UserCar> SortedList = listatemp.OrderByDescending(o => o.Car.Descri).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 201;
                }
                else
                {
                    List<UserCar> SortedList = listatemp.OrderBy(o => o.Car.Descri).ToList();
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
