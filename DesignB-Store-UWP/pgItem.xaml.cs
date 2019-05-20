using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
        private delegate void LoadItemControlDelegate(clsAllItems prItem);
        private Dictionary<char, Delegate> _ItemContent;
        private bool _Navigation = true;

        public pgItem()
        {
            this.InitializeComponent();
            _ItemContent = new Dictionary<char, Delegate>
            {
                {'R',new LoadItemControlDelegate(RunRing)},
                {'B',new LoadItemControlDelegate(RunBracelet)},
                {'N',new LoadItemControlDelegate(RunNecklace)}
            };
        }

        private void RunRing(clsAllItems prItem)
        {
            ctcItemSpecs.Content = new ucRing();
        }
        private void RunBracelet(clsAllItems prItem)
        {
            ctcItemSpecs.Content = new ucBracelet();
        }
        private void RunNecklace(clsAllItems prItem)
        {
            ctcItemSpecs.Content = new ucNecklace();
        }

        private void updatePage(clsAllItems prItem)
        {
            _Item = prItem;
            txbName.Text += prItem.Name;
            txbDescription.Text += prItem.Description;
            txbMaterial.Text += prItem.Material;
            txbPrice.Text += Convert.ToString(prItem.Price);
            txbStock.Text += Convert.ToString(prItem.Quantity);
            (ctcItemSpecs.Content as IItemControl).UpdateControl(prItem);
            for(int i = 1; i <= _Item.Quantity; i++)
            {
                cmbQuanity.Items.Add(i);
            }
            cmbQuanity.SelectedIndex = 0;
            txbTotalPrice.Text = "Total Price: " + prItem.Price;
            LoadImage();
        }

        private void dispatchItemContent(clsAllItems prWork)
        {
            _ItemContent[prWork.Type].DynamicInvoke(prWork);
            updatePage(prWork);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            dispatchItemContent(e.Parameter as clsAllItems);
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
            _Navigation = false;
            float lcresult = 0;
            lcresult = _Item.Price * (Convert.ToInt32(cmbQuanity.SelectedItem));
            txbTotalPrice.Text ="Total Price: $"+ lcresult.ToString();
        }

        private async void BtnPurchase_Tapped(object sender, TappedRoutedEventArgs e)
        {
            _Navigation = false;
            int lcQuantity = (Convert.ToInt32(cmbQuanity.SelectedItem));
            _Item.Quantity -=lcQuantity;

                OrderDialog orderDialog = new OrderDialog();
                var Dialogresult = await orderDialog.ShowAsync();
                if (Dialogresult == ContentDialogResult.Primary)
                {
                    int lcStockResult = await ServiceClient.UpdateItemAsync(_Item);

                if (lcStockResult == 1)
                {
                    clsOrder lcOrder = new clsOrder()
                    {
                        Email = orderDialog.EmailText(),
                        Address = orderDialog.AddressText(),
                        Item = _Item,
                        DateOrdered = DateTime.Now,
                        Quantity = lcQuantity,
                        TotalPrice = lcQuantity * _Item.Price,
                        Status = "Pending"
                    };
                        int order = await ServiceClient.InsertOrderAsync(lcOrder);
                        if(order == 1)
                        {
                            MessageDialog message = new MessageDialog("Your order has been sent!");
                            message.ShowAsync();
                        }
                    }
                    else
                    {
                        MessageDialog message = new MessageDialog("Session Expired, Reloading page");
                        message.ShowAsync();
                }

            }
           
        }


    }
}

