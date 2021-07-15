using CRUD.ClassesEntidades.SQL;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;
using static CRUD.ClassesEntidades.Validações;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowUserConfigList.xaml
    /// </summary>
    public partial class WindowUserConfigList : Window
    {
        int listOrder = LIST_NULL;
        List<UserConfig> listatemp = new List<UserConfig>();

        #region load

        public WindowUserConfigList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<UserConfig> lista = SqlUserConfig.GetAll(listOrder, null, null, null, null, null, null);
            listatemp = new List<UserConfig>();
            UserConfig userConfig;
            foreach (UserConfig p in lista)
            {
                userConfig = p;
                userConfig.User = SqlUser.Get(p.User.Id);
                listatemp.Add(userConfig);
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
                UserConfig userClickado = (UserConfig)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (userClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowUserConfigInsert(SQL_Connection.SQL_UPDATE, userClickado);
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
                UserConfig userClickado = (UserConfig)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (userClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowUserConfigInsert(SQL_Connection.SQL_DELETE, userClickado);
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

            List<UserConfig> lista = SqlUserConfig.GetAll(listOrder, TextBoxFromDescri.Text, TextBoxUntilDescri.Text, TextBoxFromUserName.Text, TextBoxUntilUserName.Text, TextBoxFromValue.Text, TextBoxUntilValue.Text);
            listatemp = new List<UserConfig>();
            UserConfig userConfig;
            foreach (UserConfig p in lista)
            {
                userConfig = p;
                userConfig.User = SqlUser.Get(p.User.Id);
                listatemp.Add(userConfig);
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
                    List<UserConfig> SortedList = listatemp.OrderByDescending(o => o.User.UserName).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 101;
                }
                else
                {
                    List<UserConfig> SortedList = listatemp.OrderBy(o => o.User.UserName).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 100;
                }
            }
            else if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Descrição")
            {
                if (listOrder == LIST_DESCRI_ASC)
                {
                    listOrder = LIST_DESCRI_DESC;
                }
                else
                {
                    listOrder = LIST_DESCRI_ASC;
                }
                RefreshListView();
            }
            else if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Valor")
            {
                if (listOrder == LIST_VALUE_ASC)
                {
                    listOrder = LIST_VALUE_DESC;
                }
                else
                {
                    listOrder = LIST_VALUE_ASC;
                }
                RefreshListView();
            }
        }

        /// <summary>
        /// metodo do butão de pesquisa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            int nErros = 0;
            if (ValidaNumero(TextBoxFromValue.Text) && TextBoxFromValue.Text != "")
            {
                nErros += 1;
                TextBoxFromValue.Background = Brushes.Red;
            }
            if (ValidaNumero(TextBoxUntilValue.Text) && TextBoxUntilValue.Text != "")
            {
                nErros += 1;
                TextBoxUntilValue.Background = Brushes.Red;

            }
            if (nErros > 0)
            {
                MessageBox.Show("Erro: parametro de pesquisa tem que ser numerioco", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                TextBoxFromValue.Background = Brushes.White;
                TextBoxUntilValue.Background = Brushes.White;
                RefreshListView();
            }
        }
        #endregion

    }
}
