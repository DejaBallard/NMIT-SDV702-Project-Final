using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesignB_Admin_WFA
{
    public partial class frmMain : Form
    {
        private List<clsAllItems> _Items = new List<clsAllItems>();
        private List<clsOrder> _Orders = new List<clsOrder>();
        private float _TotalOrders;

        public frmMain()
        {
            InitializeComponent();
            updateDisplay();

        }
        private async void updateDisplay()
        {
            lstStock.DataSource = null;
            lstOrders.DataSource = null;
            _TotalOrders = 0;

            cboBrands.DataSource = await ServiceClient.GetBrandNamesAsync();
            _Orders = await ServiceClient.GetOrderListAsync();
            lstOrders.DataSource = _Orders;
            foreach( clsOrder lcOrder in _Orders)
            {
                _TotalOrders += lcOrder.TotalPrice;
            }
            lblOrdersPRC.Text = "Total Order Price: " + _TotalOrders.ToString();
        }

        private async void cboBrands_SelectedIndexChanged(object sender, EventArgs e)
        {
            clsBrand lcBrand = await ServiceClient.GetBrandAsync(cboBrands.SelectedItem as string);
            lstStock.DataSource = lcBrand.ItemList;
        }

        private void lstStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                frmStock.DispatchWorkForm(lstStock.SelectedValue as clsAllItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
