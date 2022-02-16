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
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {

        List<EF.Order> ListOrder = new List<EF.Order>();

        List<string> listForSort2 = new List<string>()
        {
            "По умолчанию",
            "По сотрудникам",
            "По иклиентам",
            "По услугам"

        };
        public OrderWindow()
        {
            InitializeComponent();
            AllOrder.ItemsSource = context.Client.ToList();
            cmbSort2.SelectedIndex = 0;
            cmbSort2.ItemsSource = listForSort2;
            Filter();
        }



        private void Filter()
        {
            ListOrder = ClassAppDate.context.Order.ToList();
            ListOrder = ListOrder.
                Where(i => i.Employee.Contains(txtSearch2.Text)
                || i.Client.Contains(txtSearch2.Text)
                || i.Service.Contains(txtSearch2.Text)).ToList();

            switch (cmbSort2.SelectedIndex)
            {
                case 0:
                    ListOrder = ListOrder.OrderBy(i => i.ID).ToList();
                    break;

                case 1:
                    ListOrder = ListOrder.OrderBy(i => i.IDEmp).ToList();
                    break;

                case 2:
                    ListOrder = ListOrder.OrderBy(i => i.IDClient).ToList();
                    break;

                case 3:
                    ListOrder = ListOrder.OrderBy(i => i.IDService).ToList();
                    break;

                default:
                    ListOrder = ListOrder.OrderBy(i => i.ID).ToList();
                    break;

                    if (ListOrder.Count == 0)
            {
                MessageBox.Show("Записей нет");
            }
            AllOrder.ItemsSource = ListOrder;

        }


        private void btnClient2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btncBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AuthWindow authWindow = new AuthWindow();
            authWindow.ShowDialog();
            this.Close();
        }
    }
}
