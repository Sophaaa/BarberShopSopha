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
using static BarberShopSopha.ClassEntities;

namespace BarberShopSopha
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        List<EF.Client> ListClient = new List<EF.Client>();

        List<string> listForSort2 = new List<string>()
        {
            "По умолчанию",
            "По фамилии",
            "По имени",
            "По телефону",
            "По полу"

        };

        public ClientWindow()
        {
            InitializeComponent();
            AllPersonal.ItemsSource = context.Client.ToList();
            cmbSort2.SelectedIndex = 0;
            cmbSort2.ItemsSource = listForSort2;
            Filter();
        }

        private void Filter()
        {
            ListClient = ClassAppDate.context.Client.ToList();
            ListClient = ListClient.
                Where(i => i.LName.Contains(txtSearch2.Text)
                || i.FName.Contains(txtSearch2.Text)
                || i.Phone.Contains(txtSearch2.Text)).ToList();

            switch (cmbSort2.SelectedIndex)
            {
                case 0:
                    ListClient = ListClient.OrderBy(i => i.ID).ToList();
                    break;

                case 1:
                    ListClient = ListClient.OrderBy(i => i.LName).ToList();
                    break;

                case 2:
                    ListClient = ListClient.OrderBy(i => i.FName).ToList();
                    break;

                case 3:
                    ListClient = ListClient.OrderBy(i => i.Phone).ToList();
                    break;

                case 4:
                    ListClient = ListClient.OrderBy(i => i.Gender).ToList();
                    break;

                default:
                    ListClient = ListClient.OrderBy(i => i.ID).ToList();
                    break;
            }
            if (ListClient.Count == 0)
            {
                MessageBox.Show("Записей нет");
            }
            AllPersonal.ItemsSource = ListClient;

        }




        private void btnchange2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClient2_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AddClientWindow addClientWindow = new AddClientWindow();
            addClientWindow.ShowDialog();
            this.Show();
            Filter();
        }

        private void btncBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AuthWindow authWindow = new AuthWindow();
            authWindow.ShowDialog();
            this.Close();
        }

        private void txtSearch2_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cmbSort2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }



    }

}
