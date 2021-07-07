using CRUD.ClassesEntidades.SQL;
using Desktop___interfaces.ClassesEntidades.SQL;
using System.Collections.Generic;
using System.Windows;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowUserCarList.xaml
    /// </summary>
    public partial class WindowUserCarList : Window
    {
        #region load

        public WindowUserCarList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<UserCar> lista = SqlUserCar.GetAll();
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

            List<UserCar> lista = SqlUserCar.GetAll();
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
        #endregion


    }
}
