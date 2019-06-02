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
    public partial class frmOrder : Form
    {
        clsOrder _Order;

        public frmOrder()
        {
            InitializeComponent();
        }

        internal static void Run(clsOrder prOrder)
        {
            frmOrder lcOrder = new frmOrder();
            if (prOrder == null)
            {
                lcOrder.SetDetails(new clsOrder());            }
            else
            {
                lcOrder.SetDetails(prOrder);
            }
        }

        private void SetDetails(clsOrder prOrder)
        {
            _Order = prOrder;
            txtID.Enabled = false;
            txtItem.Enabled = string.IsNullOrEmpty(_Order.Item.Name);
            txtBrand.Enabled = string.IsNullOrEmpty(_Order.Item.Brand);

            txtID.Text = _Order.Id.ToString();
            txtAddress.Text = _Order.Address;
            txtBrand.Text = _Order.Item.Brand;
            txtEmail.Text = _Order.Email;
            txtItem.Text = _Order.Item.Name;

        }
    }
}
