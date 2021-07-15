using CRUD.ClassesEntidades.SQL;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowProfileList.xaml
    /// </summary>
    public partial class WindowProfileList : Window
    {
        int listOrder = LIST_NULL;
        List<Profile> listatemp = new List<Profile>();

        #region load

        public WindowProfileList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Profile> lista = SqlProfile.GetAll(listOrder, null, null, null, null, null, null);
            Profile profile;
            listatemp = new List<Profile>();
            
            foreach (Profile p in lista)
            {
                profile = p;
                profile.UserEscolhido = SqlUser.Get(p.UserEscolhido.Id);
                profile.TipoUser = SqlUserType.Get(p.TipoUser.Id);
                listatemp.Add(profile);
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
                Profile profileClickado = (Profile)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (profileClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowProfileInsert(SQL_Connection.SQL_UPDATE, profileClickado);
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
                Profile profileClickado = (Profile)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (profileClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowProfileInsert(SQL_Connection.SQL_DELETE, profileClickado);
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
            listatemp = new List<Profile>();
            List<Profile> lista = SqlProfile.GetAll(listOrder, TextBoxFromUserName.Text, TextBoxUntilUserName.Text, TextBoxFromUserType.Text, TextBoxUntilUserType.Text, TextBoxFromCreationDate.Text, TextBoxUntilCreationDate.Text);
            Profile profile;
            foreach (Profile p in lista)
            {
                profile = p;
                profile.UserEscolhido = SqlUser.Get(p.UserEscolhido.Id);
                profile.TipoUser = SqlUserType.Get(p.TipoUser.Id);
                listatemp.Add(profile);
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
            if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Nome do User")
            {
                if (listOrder == 100)
                {
                    List<Profile> SortedList = listatemp.OrderByDescending(o => o.UserEscolhido.UserName).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 101;
                }
                else
                {
                    List<Profile> SortedList = listatemp.OrderBy(o => o.UserEscolhido.UserName).ToList();
                    ListView.ItemsSource = null;                 
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 100;
                }
            }
            else if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Tipo de user")
            {
                if (listOrder == 200)
                {
                    List<Profile> SortedList = listatemp.OrderByDescending(o => o.TipoUser.Descri).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 201;
                }
                else
                {
                    List<Profile> SortedList = listatemp.OrderBy(o => o.TipoUser.Descri).ToList();
                    ListView.ItemsSource = null;
                    ListView.Items.Clear();
                    ListView.ItemsSource = SortedList;
                    listOrder = 200;
                }
            }
            else if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Data de Criação")
            {
                if (listOrder == LIST_DATECREATE_ASC)
                {
                    listOrder = LIST_DATECREATE_DESC;
                }
                else
                {
                    listOrder = LIST_DATECREATE_ASC;
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
            RefreshListView();
        }
        #endregion
    }
}
