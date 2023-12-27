using OrganizationGreem.DB;
using OrganizationGreem.Model;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace OrganizationGreem.Windows
{
    /// <summary>
    /// Interaction logic for ContractWindow.xaml
    /// </summary>
    public partial class ContractWindow : Window
    {
        private Contracts contract = null;
        public ContractWindow(int contractId)
        {
            InitializeComponent();
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(contractId.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeBitmap = qrCode.GetGraphic(100);
            using (MemoryStream memory = new MemoryStream())
            {
                qrCodeBitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                ImageQrCode.Source = bitmapimage;
            }

            contract = DbConnection.db.Contracts.Where(x => x.id_Contract == contractId).FirstOrDefault();

            ListItems.ItemsSource = contract.Items.ToList();
            StatusCombo.ItemsSource = DbConnection.db.StatusContrants.ToList();
            StatusCombo.SelectedItem = contract.StatusContrants;
            DateEnd.Content = contract.DateEnd;

            if (DateTime.Now < contract.DateEnd || DbConnection.logins.id_Login != contract.Login_id || contract.StatusContract_id == 2)
            {
                Save.IsEnabled = false;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var status = (StatusContrants)StatusCombo.SelectedItem;
            if (status == null)
                return;

            contract.StatusContract_id = status.id_StatusContrant;
            DbConnection.db.SaveChanges();
            this.Close();
        }
    }
}
