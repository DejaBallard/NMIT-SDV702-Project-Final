using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DesignB_Store_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pgHome : Page
    {
        public pgHome()
        {
            this.InitializeComponent();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (cboBrands.SelectedItem != null)

                Frame.Navigate(typeof(pgProducts), cboBrands.SelectedItem as string);
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string br in await ServiceClient.GetBrandNamesAsync())
            {
                cboBrands.Items.Add(br);
            }
        }
    }
}
