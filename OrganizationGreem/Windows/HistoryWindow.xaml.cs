using OrganizationGreem.DB;
using OrganizationGreem.Model;
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

namespace OrganizationGreem.Windows
{
    /// <summary>
    /// Interaction logic for HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        public HistoryWindow()
        {
            InitializeComponent();
            HistoryView.ItemsSource = DbConnection.db.Contracts.ToList();
        }

        private void BtnOpenNewContract_Click(object sender, RoutedEventArgs e)
        {
            AddNewContractWindow win = new AddNewContractWindow();
            win.ShowDialog();
            HistoryView.ItemsSource = DbConnection.db.Contracts.ToList();
        }

        private void HistoryView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is Contracts selectedItem)
            {
                int idContract = selectedItem.id_Contract;

                var newWindow = new ContractWindow(idContract);
                newWindow.ShowDialog();
                HistoryView.ItemsSource = DbConnection.db.Contracts.ToList();
            }
        }
    }
}
