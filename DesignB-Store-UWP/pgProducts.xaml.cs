using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
    public sealed partial class pgProducts : Page
    {
        private clsBrand _Brand;

        public pgProducts()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                string lcArtistName = e.Parameter.ToString();
                _Brand = await ServiceClient.GetBrandAsync(lcArtistName);
                UpdateDisplay();
            }
        }


        private void UpdateDisplay()
        {
            txbDescription.Text = _Brand.Description;
            txbBrand.Text = _Brand.Name;
            lstItems.ItemsSource = _Brand.ItemList;
            LoadImage();
            
        }
        public async void LoadImage()
        {
            var bitmap = new BitmapImage();
            using (var stream = new MemoryStream(Convert.FromBase64String(_Brand.Image64)))
            {
                //We're using WinRT (Windows Phone or Windows 8 app) in this example. Bitmaps in WinRT use an IRandomAccessStream as their source
                await bitmap.SetSourceAsync(stream.AsRandomAccessStream());
            }
            imgBrand.Source = bitmap;
        }

        private void LstItems_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(pgItem), lstItems.SelectedItem as clsAllItems);
        }
    }
}
