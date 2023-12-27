using OrganizationGreem.DB;
using OrganizationGreem.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace OrganizationGreem.Windows
{
    /// <summary>
    /// Interaction logic for MainBodyWindow.xaml
    /// </summary>
    
    class ItemsIsChecked
    {
        public int id_Item { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
        public int? Contract_id { get;set; }
    };

    public partial class AddNewContractWindow : Window
    {
        public AddNewContractWindow()
        {
            InitializeComponent();
            Organization.ItemsSource = DbConnection.db.Organizations.ToList();
            ListItems.ItemsSource = DbConnection.db.Items.Where(x => x.Contract_id == null).Select(item => new ItemsIsChecked
            {
                id_Item = item.id_Item,
                Name = item.Name,
                Contract_id = item.Contract_id,
                IsSelected = false
            }).ToList();
        }

        private static readonly Regex _regex = new Regex("[^0-9]+");
        private void Cost_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }

        private void BtnClickAddContract_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = ListItems.Items.OfType<ItemsIsChecked>().Where(item => item.IsSelected).ToList();

            if (Name.Text == "" || DatePost.SelectedDate == null|| Cost.Text == "" || Organization.SelectedItem == null || selectedItems.Count == 0)
            {
                MessageBox.Show("Заполните все поля!", "Ошибка!");
                return;
            }

            var newContract = DbConnection.db.Contracts.Add(new Contracts()
            {
                Name = Name.Text,
                Cost = Convert.ToInt32(Cost.Text),
                DateEnd = DatePost.SelectedDate,
                Organozation_id = ((Organizations)Organization.SelectedItem).id_Organization,
                Login_id = DbConnection.logins.id_Login,
                StatusContract_id = 1
            });


            var organizationSelected = Organization.SelectedItem as Organizations;

            foreach (var selectedItem in selectedItems)
            {
                var item = DbConnection.db.Items.Where(x => x.id_Item == selectedItem.id_Item).FirstOrDefault();

                if (item == null)
                    return;

                item.Contract_id = newContract.id_Contract;
            }
            DbConnection.db.SaveChanges();
            this.Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var item = (ItemsIsChecked)checkBox.DataContext;
            item.IsSelected = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var item = (ItemsIsChecked)checkBox.DataContext;
            item.IsSelected = false;
        }
    }
}
