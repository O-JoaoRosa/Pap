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
            Window w = new WindowCarInsert(SQL_INSERT, null);
            w.ShowDialog();
        }

        private void MenuItemCarListar_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowCarListar(LIST_ACTION_NULL);
            w.ShowDialog();
        }

        private void MenuItemCarBodyInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowCarBodyInsert(SQL_INSERT, null);
            w.ShowDialog();
        }

        private void MenuItemCarBodyListar_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowCarBodyListar(LIST_ACTION_NULL);
            w.ShowDialog();
        }

        private void MenuItemPowerUpInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowPowerUpInsert(SQL_INSERT, null);
            w.ShowDialog();
        }

        private void MenuItemPowerUpListar_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowPowerUpList(LIST_ACTION_NULL);
            w.ShowDialog();

        }

        private void MenuItemProfileInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowProfileInsert(SQL_INSERT , null);
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
            Window w = new WindowRaceTrackList(LIST_ACTION_NULL);
            w.ShowDialog();
        }

        private void MenuItemServerInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowServerInsert(SQL_INSERT, null);
            w.ShowDialog();
        }

        private void MenuItemServerList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowServerList(LIST_ACTION_NULL);
            w.ShowDialog();
        }

        private void MenuItemServerUserInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowServerUserInsert(SQL_INSERT, null);
            w.ShowDialog();
        }

        private void MenuItemServerUserList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowServerUserList();
            w.ShowDialog();
        }

        private void MenuItemServerUserStateInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowServerUserStateInsert(null , SQL_INSERT);
            w.ShowDialog();
        }

        private void MenuItemServerUserStateList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowServerUserStateList(LIST_ACTION_NULL);
            w.ShowDialog();
        }

        private void MenuItemUserInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserInsert(SQL_INSERT, null);
            w.ShowDialog();
        }

        private void MenuItemUserList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserList(LIST_ACTION_NULL);
            w.ShowDialog();
        }

        private void MenuItemUserCarInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserCarInsert(SQL_INSERT , null);
            w.ShowDialog();
        }

        private void MenuItemUserCarList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserCarList();
            w.ShowDialog();
        }

        private void MenuItemUserConfigInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserConfigInsert(SQL_INSERT, null);
            w.ShowDialog();
        }

        private void MenuItemUserConfigList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserConfigList();
            w.ShowDialog();
        }

        private void MenuItemUserFriendInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserFriendInsert(SQL_INSERT, null);
            w.ShowDialog();
        }

        private void MenuItemUserFriendList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserFriendList();
            w.ShowDialog();
        }

        private void MenuItemUserRaceInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowUserRaceInsert(SQL_INSERT, null);
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
            Window w = new WindowUserTypeList(LIST_ACTION_NULL);
            w.ShowDialog();
        }

        private void MenuItemWheelInserir_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowWheelInsert(SQL_INSERT, null);
            w.ShowDialog();
        }

        private void MenuItemWheelList_Click(object sender, RoutedEventArgs e)
        {
            Window w = new WindowWheelList(LIST_ACTION_NULL);
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
