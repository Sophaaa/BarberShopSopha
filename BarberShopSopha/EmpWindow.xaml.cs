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
    /// Логика взаимодействия для EmpWindow.xaml
    /// </summary>
    public partial class EmpWindow : Window
    {


        List<EF.Employee> listEmployee = new List<EF.Employee>();

        List<string> listForSort = new List<string>()
        {
            "По умолчанию",
            "По фамилии",
            "По имени",
            "По отчеству",
            "По специализации"

        };
        public EmpWindow()
        {
            InitializeComponent();
            AllPersonal.ItemsSource = context.Employee.ToList();
            cmbSort.ItemsSource = listForSort;
            cmbSort.SelectedIndex = 0;
            Filter();
        }

        private void Filter()
        {
            listEmployee = ClassAppDate.context.Employee.ToList();
            listEmployee = listEmployee.
                Where(i => i.LName.Contains(txtSearch.Text)
                || i.FName.Contains(txtSearch.Text)
                || i.MName.Contains(txtSearch.Text)).ToList();

            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    listEmployee = listEmployee.OrderBy(i => i.ID).ToList();
                    break;

                case 1:
                    listEmployee = listEmployee.OrderBy(i => i.LName).ToList();
                    break;

                case 2:
                    listEmployee = listEmployee.OrderBy(i => i.FName).ToList();
                    break;

                case 3:
                    listEmployee = listEmployee.OrderBy(i => i.MName).ToList();
                    break;

                case 4:
                    listEmployee = listEmployee.OrderBy(i => i.IdSpecialisation).ToList();
                    break;

                default:
                    listEmployee = listEmployee.OrderBy(i => i.ID).ToList();
                    break;
            }
            if (listEmployee.Count == 0)
            {
                MessageBox.Show("Записей нет");
            }
            AllPersonal.ItemsSource = listEmployee;

        }

        private void btnEmpl_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AddEmpWindow addEmpWindow = new AddEmpWindow();
            addEmpWindow.ShowDialog();
            this.Close();
        }

       

        private void btnEmplBack2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void AllPersonal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                var resClick = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                try
                {

                    if (resClick == MessageBoxResult.Yes)
                    {
                        EF.Employee userDel = new EF.Employee();

                        if (!(AllPersonal.SelectedItem is EF.Employee))
                        {
                            MessageBox.Show("Запись не выбрана!");
                            return;
                        }

                        userDel = (AllPersonal.SelectedItem as EF.Employee);

                        //MessageBox.Show(userDel.LName);
                        ClassAppDate.context.Employee.Remove(userDel);
                        ClassAppDate.context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }

        }

        private void AllPersonal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EF.Employee userEdit = AllPersonal.SelectedItem as EF.Employee;

            AddEmpWindow addEmpWindow = new AddEmpWindow(userEdit);
            addEmpWindow.ShowDialog();

            Filter();
            
        }
    }
}

