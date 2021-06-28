using Desktop___interfaces.Interfaces;
using static CRUD.ClassesEntidades.SQL.SQL_Connection;
using System.Windows;

namespace CRUD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        #region metodos

        private void MenuItemCarInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowCarInsert();
            w.ShowDialog();
        }

        private void MenuItemCarListar_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowCarListar();
            w.ShowDialog();
        }

        private void MenuItemCarBodyInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowCarBodyInsert(SQL_INSERT, null);
            w.ShowDialog();
        }

        private void MenuItemCarBodyListar_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowCarBodyListar();
            w.ShowDialog();
        }

        private void MenuItemPowerUpInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowPowerUpInsert();
            w.ShowDialog();
        }

        private void MenuItemPowerUpListar_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowPowerUpList();
            w.ShowDialog();

        }

        private void MenuItemProfileInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowProfileInsert();
            w.ShowDialog();
        }

        private void ManuItemProfileListar_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowProfileList();
            w.ShowDialog();
        }

        private void MenuItemRaceTrackInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowRaceTrackInsert(SQL_INSERT, null);
            w.ShowDialog();
        }

        private void MenuItemRaceTrackList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowRaceTrackList();
            w.ShowDialog();
        }

        private void MenuItemServerInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowServerInsert(SQL_INSERT, null);
            w.ShowDialog();
        }

        private void MenuItemServerList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowServerList();
            w.ShowDialog();
        }

        private void MenuItemServerUserInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowServerUserInsert();
            w.ShowDialog();
        }

        private void MenuItemServerUserList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowServerUserList();
            w.ShowDialog();
        }

        private void MenuItemServerUserStateInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowServerUserStateInsert();
            w.ShowDialog();
        }

        private void MenuItemServerUserStateList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowServerUserStateList();
            w.ShowDialog();
        }

        private void MenuItemUserInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserInsert(SQL_INSERT, null);
            w.ShowDialog();
        }

        private void MenuItemUserList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserList();
            w.ShowDialog();
        }

        private void MenuItemUserCarInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserCarInsert();
            w.ShowDialog();
        }

        private void MenuItemUserCarList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserCarList();
            w.ShowDialog();
        }

        private void MenuItemUserConfigInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserConfigInsert();
            w.ShowDialog();
        }

        private void MenuItemUserConfigList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserConfigList();
            w.ShowDialog();
        }

        private void MenuItemUserFriendInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserFriendInsert();
            w.ShowDialog();
        }

        private void MenuItemUserFriendList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserFriendList();
            w.ShowDialog();
        }

        private void MenuItemUserRaceInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserRaceInsert();
            w.ShowDialog();
        }

        private void MenuItemUserRaceList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserRaceList();
            w.ShowDialog();
        }

        private void MenuItemUserTypeInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserTypeInsert(null, SQL_INSERT);
            w.ShowDialog();
        }

        private void MenuItemUserTypeList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserTypeList();
            w.ShowDialog();
        }

        private void MenuItemWheelInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowWheelInsert(SQL_INSERT, null);
            w.ShowDialog();
        }

        private void MenuItemWheelList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowWheelList();
            w.ShowDialog();
        }

        #endregion

        private void MenuItemDBMS_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowDBMS();
            w.ShowDialog();
        }
    }

}
