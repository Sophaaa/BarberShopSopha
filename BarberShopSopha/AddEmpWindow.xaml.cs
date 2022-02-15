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
using static BarberShopSopha.ClassAppDate;
using BarberShopSopha.EF;


namespace BarberShopSopha
{
    /// <summary>
    /// Логика взаимодействия для AddEmpWindow.xaml
    /// </summary>
    public partial class AddEmpWindow : Window
    {
        EF.Employee editEmployee = new EF.Employee();

        bool isEdit = true;
        public AddEmpWindow()
        {
            InitializeComponent();
            cmbSpisial.ItemsSource = context.Specialisation.ToList();
            cmbSpisial.DisplayMemberPath = "Name";
            cmbSpisial.SelectedIndex = 0;

            isEdit = false; 
        }


        public AddEmpWindow(EF.Employee employee)
        {
            InitializeComponent();
            cmbSpisial.ItemsSource = context.Specialisation.ToList();
            cmbSpisial.DisplayMemberPath = "Name";
            cmbSpisial.SelectedIndex = Convert.ToInt32(employee.IdSpecialisation - 1);

            txtLogin.Text = employee.Login;
            txtLName.Text = employee.LName;
            txtFName.Text = employee.FName;
            txtMName.Text = employee.MName;
            txtEmail.Text = employee.Email;
            (cmbSpisial.SelectedItem as Specialisation).ID = employee.IdSpecialisation;
            txtPassword.Text = employee.Password;
            txtSeries.Text = employee.PassSeries.ToString();
            txtNumber.Text = employee.PassNum.ToString();

            tbTitle.Text = "Изменить данные работника";

            btnAddEmpl.Content = "Изменить";

            editEmployee = employee;
            isEdit = true;




        }
        private void btnAddEmpl_Click(object sender, RoutedEventArgs e)

        {
            Employee employee = new Employee();


            if (string.IsNullOrWhiteSpace(txtLName.Text))
            {
                MessageBox.Show("Поле Фамилия не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFName.Text))
            {
                MessageBox.Show("Поле Имя не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Поле E-mail не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtLogin.Text))
            {
                MessageBox.Show("Поле Логин не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMName.Text))
            {
                MessageBox.Show("Поле Отчества не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSeries.Text))
            {
                MessageBox.Show("Поле Серии не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNumber.Text))
            {
                MessageBox.Show("Поле Номер не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Поле Пароль не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //Проверка пароля на совпадение

            if (txtPassword.Text != txtRepeatPassword.Text)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Пользователь добавлен");

            employee.Login = txtLogin.Text;
            employee.LName = txtLName.Text;
            employee.FName = txtFName.Text;
            employee.IdSpecialisation = (cmbSpisial.SelectedItem as Specialisation).ID;
            employee.Password = txtPassword.Text;
            employee.Email = txtEmail.Text;
            employee.PassSeries = Int32.Parse( txtSeries.Text);
            employee.PassNum = Int32.Parse( txtNumber.Text);
            employee.MName = txtMName.Text;

            context.Employee.Add(employee);
            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Пользователь добавлен");

            this.Hide();
            EmpWindow empWindow = new EmpWindow();
            empWindow.ShowDialog();
            this.Close();


            //проверка на уникальност логина
            var userAuthLogin = ClassAppDate.context.Employee.ToList().
                  Where(i => i.Login == txtLogin.Text).
                  FirstOrDefault();
            if (userAuthLogin != null)
            {
                MessageBox.Show("Данный Логин занят", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var resClick = MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo);

            try
            {
                if (resClick == MessageBoxResult.Yes)
                {
                    if (isEdit)
                    {
                        editEmployee.FName = txtFName.Text;
                        editEmployee.LName = txtLName.Text;
                        editEmployee.MName = txtMName.Text;
                        editEmployee.IdSpecialisation = cmbSpisial.SelectedIndex + 1;
                        editEmployee.Email = txtEmail.Text;
                        editEmployee.Login = txtLogin.Text;
                        editEmployee.Password = txtPassword.Text;
                        employee.PassSeries = Int32.Parse(txtSeries.Text);
                        employee.PassNum = Int32.Parse(txtNumber.Text);


                        ClassAppDate.context.SaveChanges();

                        MessageBox.Show("Пользователь успешно изменен", "Успех", MessageBoxButton.OK);
                        this.Close();
                    }
                    else
                    {
                        EF.Employee addEmployee = new EF.Employee();
                        addEmployee.FName = txtFName.Text;
                        addEmployee.LName = txtLName.Text;
                        addEmployee.MName = txtMName.Text;
                        addEmployee.IdSpecialisation = cmbSpisial.SelectedIndex + 1;
                        addEmployee.Email = txtEmail.Text;
                        addEmployee.Login = txtLogin.Text;
                        addEmployee.Password = txtPassword.Text;
                        employee.PassSeries = Int32.Parse(txtSeries.Text);
                        employee.PassNum = Int32.Parse(txtNumber.Text);

                        ClassAppDate.context.Employee.Add(addEmployee);
                        ClassAppDate.context.SaveChanges();

                        MessageBox.Show("Пользователь успешно добавлен", "Успех", MessageBoxButton.OK);
                        this.Close();
                    }
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }

        private void btnEmpBack_Click(object sender, RoutedEventArgs e)
        {
          
            this.Close();
        }

        private void txtLName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                    textBox.Text.Where(ch => (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я') || ch == 'ё' || ch == 'Ё' || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z')).ToArray()
                    );

            }
        }

        private void txtFName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                    textBox.Text.Where(ch => ch == '-' || (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я') || ch == 'ё' || ch == 'Ё' || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z')).ToArray()
                    );

            }
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                     textBox.Text.Where(ch => ch == '-' || ch == '@' || (ch >= 'а' && ch <= 'я') || (ch >= 'А' && ch <= 'Я') || ch == 'ё' || ch == 'Ё' || (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || (ch >= '0' && ch <= '9')).ToArray()
                    );
            }
        }

        private void txtSeries_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                    textBox.Text.Where(ch => (ch >= '0' && ch <= '9')).ToArray()
                    );
            }
        }

        private void txtNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Text = new string
                    (
                    textBox.Text.Where(ch => (ch >= '0' && ch <= '9')).ToArray()
                    );
            }
        }
    }
}