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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static BarberShopSopha.ClassEntities;

namespace BarberShopSopha
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        


        public MainWindow()
        {
            InitializeComponent();
        }

    

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var User = context.Employee.ToList().
                Where(p => p.Login == this.txtLogin.Text && p.Password == this.passbox.Password).FirstOrDefault();
            if(User != null)
            {
                this.Hide();
                AuthWindow authWindow = new AuthWindow();
                authWindow.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы ввели не верный пароль или логин");
            }

        }
    }
}
