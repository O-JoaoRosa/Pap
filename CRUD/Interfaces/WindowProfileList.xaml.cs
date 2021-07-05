using CRUD.ClassesEntidades.SQL;
using System.Collections.Generic;
using System.Windows;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowProfileList.xaml
    /// </summary>
    public partial class WindowProfileList : Window
    {
        #region load

        public WindowProfileList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<Profile> lista = SqlProfile.GetAll();
            List<Profile> listatemp = new List<Profile>();
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

        /// <summary>
        /// faz refresh a list View
        /// </summary>
        private void RefreshListView()
        {
            ListView.ItemsSource = null;                  // Elimina a associação da List à listView
            ListView.Items.Clear();                       // Limpa a ListView

            List<Profile> lista = SqlProfile.GetAll();
            List<Profile> listatemp = new List<Profile>();
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
        #endregion

    }
}
