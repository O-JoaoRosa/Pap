using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Desktop___interfaces.Interfaces
{
    /// <summary>
    /// Interaction logic for WindowServerUserInsert.xaml
    /// </summary>
    public partial class WindowServerUserInsert : Window
    {
        User userTemp;
        int dbAction = -1;

        /// <summary>
        /// metodo executado quando a window é incializada 
        /// </summary>
        public WindowServerUserInsert()
        {
            InitializeComponent();
        }

        /// <summary>
        /// metodo executado quando a window é incializada 
        /// </summary>
        public WindowServerUserInsert(int action , User us)
        {
            InitializeComponent();
            userTemp = us;
            dbAction = action;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
