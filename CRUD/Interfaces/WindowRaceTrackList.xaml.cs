using CRUD.ClassesEntidades.SQL;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;
using static CRUD.ClassesEntidades.Validações;
using static CRUD.ClassesEntidades.Settings;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowRaceTrackList.xaml
    /// </summary>
    public partial class WindowRaceTrackList : Window
    {
        int listOrder = LIST_NULL;
        int listAction = -1;

        #region load
        public WindowRaceTrackList(int action)
        {
            InitializeComponent();
            listAction = action;
        }

        /// <summary>
        /// metodo executado quando a window é carregada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListView.ItemsSource = SqlRaceTrack.GetAll(listOrder, null, null , null, null); 
            
            //verifica qual o objetivo da lista
            if (listAction != LIST_ACTION_ID)
            {
                LabelSubTitle.Visibility = Visibility.Hidden;
            }
            else
            {
                LabelSubTitle.Visibility = Visibility.Visible;
                ButtonDel.Visibility = Visibility.Hidden;
                ButtonEdit.Visibility = Visibility.Hidden;
            }
        }

        #endregion

        #region Buttons

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            raceTrackTemp = null;
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
            if (listAction == LIST_ACTION_NULL)
            {
                //verifica se algum item foi selecionado 
                if (ListView.SelectedItems.Count > 0)
                {
                    //vai buscar o item selecionado
                    RaceTrack RaceTrackClickado = (RaceTrack)ListView.SelectedItems[0];

                    //verifica se o item selecionado está vazio ou não
                    if (RaceTrackClickado != null)
                    {
                        //abre a janela de edição com a informação necessaria para definir o que fazer
                        Window w = new WindowRaceTrackInsert(SQL_Connection.SQL_UPDATE, RaceTrackClickado);
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
            else
            {
                //verifica se algum item foi selecionado 
                if (ListView.SelectedItems.Count > 0)
                {
                    //vai buscar o item selecionado
                    RaceTrack RaceTrackClickado = (RaceTrack)ListView.SelectedItems[0];

                    //verifica se o item selecionado está vazio ou não
                    if (RaceTrackClickado != null)
                    {
                        raceTrackTemp = RaceTrackClickado;
                        Close();
                    }
                }
                else
                {
                    //avisa que nenhum item foi selecionado
                    MessageBox.Show("Erro : nenhum item selecionado", "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                RefreshListView();
            }
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
                RaceTrack RaceTrackClickado = (RaceTrack)ListView.SelectedItems[0];

                //verifica se o item selecionado está vazio ou não
                if (RaceTrackClickado != null)
                {
                    //abre a janela de edição com a informação necessaria para definir o que fazer
                    Window w = new WindowRaceTrackInsert(SQL_Connection.SQL_DELETE, RaceTrackClickado);
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
            ListView.ItemsSource = SqlRaceTrack.GetAll(listOrder, TextBoxFrom.Text, TextBoxUntil.Text, TextBoxFromRepReq.Text, TextBoxUntilRepReq.Text);    // Reassocia a listAlunos à ListView
        }

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
            else if (((GridViewColumnHeader)e.OriginalSource).Column.Header.ToString() == "Reputação necessária")
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
            RefreshListView();
        }

        /// <summary>
        /// metodo do butão de pesquisa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            int nErros = 0;
            if (ValidaNumero(TextBoxFromRepReq.Text) && TextBoxFromRepReq.Text != "")
            {
                nErros += 1;
                TextBoxFromRepReq.Background = Brushes.Red;
            }
            if (ValidaNumero(TextBoxUntilRepReq.Text) && TextBoxUntilRepReq.Text != "")
            {
                nErros += 1;
                TextBoxUntilRepReq.Background = Brushes.Red;

            }
            if (nErros > 0)
            {
                MessageBox.Show("Erro: parametro de pesquisa tem que ser numerioco", "Erro", MessageBoxButton.OK, MessageBoxImage.Error) ;
            }
            else
            {
                TextBoxFromRepReq.Background = Brushes.White;
                TextBoxUntilRepReq.Background = Brushes.White;
                RefreshListView();
            }
        }
        #endregion
    }
}
