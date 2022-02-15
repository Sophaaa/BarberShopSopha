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

namespace BarberShopSopha
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

    

        private void btnListEmp_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            EmpWindow empWindow = new EmpWindow();
            empWindow.ShowDialog();
            this.Close();
        }

        private void btnListOrder_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            OrderWindow orderWindow = new OrderWindow();
            orderWindow.ShowDialog();
            this.Close();
        }

        private void btnListClient_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ClientWindow clientWindow = new ClientWindow();
            clientWindow.ShowDialog();
            this.Close();

        }
    }
}
