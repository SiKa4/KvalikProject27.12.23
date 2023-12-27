using OrganizationGreem.DB;
using OrganizationGreem.Windows;
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

namespace OrganizationGreem
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

        private void BtnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if(Password.Password == "" && Login.Text == "")
            {
                MessageBox.Show("Введите хоть какие нибудь данные!", "Ошибка");
                return;
            }

            var answer = DbConnection.db.Logins.Where(x => x.Password == Password.Password && x.Login == Login.Text).FirstOrDefault();

            if(answer == null)
            {
                MessageBox.Show("Пользователь не найден!", "Ошибка");
                return;
            }

            DbConnection.logins = answer;
            HistoryWindow win = new HistoryWindow();
            win.Show();
            this.Close();
        }
    }
}
