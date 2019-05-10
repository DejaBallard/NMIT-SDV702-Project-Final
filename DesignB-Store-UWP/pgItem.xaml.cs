using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace DesignB_Store_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pgItem : Page
    {
        private clsAllItems _Item;

        public pgItem()
        {
            this.InitializeComponent();
        }

        private void updatePage(clsAllItems prItem)
        {
            _Item = prItem;
            txbName.Text += prItem.Name;
            txbDescription.Text += prItem.Description;
            txbMaterial.Text += prItem.Material;
            txbPrice.Text += Convert.ToString(prItem.Price);
            txbStock.Text += Convert.ToString(prItem.Quantity);
            for(int i = 1; i <= _Item.Quantity; i++)
            {
                cmbQuanity.Items.Add(i);
            }
            LoadImage();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            updatePage(e.Parameter as clsAllItems);
        }
        public async void LoadImage()
        {
            var bitmap = new BitmapImage();
            using (var stream = new MemoryStream(Convert.FromBase64String(_Item.Image64)))
            {
                //We're using WinRT (Windows Phone or Windows 8 app) in this example. Bitmaps in WinRT use an IRandomAccessStream as their source
                await bitmap.SetSourceAsync(stream.AsRandomAccessStream());
            }
            imgItem.Source = bitmap;
        }

        private void CmbQuanity_DropDownClosed(object sender, object e)
        {
            float lcresult = 0;
            lcresult = _Item.Price * (Convert.ToInt32(cmbQuanity.SelectedItem));
            txbTotalPrice.Text ="Total Price: $"+ lcresult.ToString();
        }

        private async void BtnPurchase_Tapped(object sender, TappedRoutedEventArgs e)
        {
            _Item.Quantity -= (Convert.ToInt32(cmbQuanity.SelectedItem));
            //{
            //    OrderDialog orderDialog = new OrderDialog();
            //    var result = await orderDialog.ShowAsync();
            //}
                int lcResult = await ServiceClient.UpdateItemAsync(_Item);
            if(lcResult == 1)
            {
                OrderDialog orderDialog = new OrderDialog();
                var result = await orderDialog.ShowAsync();
            }
            else
            {
                MessageDialog message = new MessageDialog("Err");
                message.ShowAsync();
            }
        }
    }
}

