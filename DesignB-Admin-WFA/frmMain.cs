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
        private string Item = "Ring   Silvermoon      $100    QTY:2";
        private string Item2 = "Ring   Silvermoon      $100    QTY:1";

        public frmMain()
        {
            InitializeComponent();
            lstStock.Items.Add(Item);
            lstOrders.Items.Add(Item2);
            lblStockPRC.Text += " $200";
            lblStockQTY.Text += " 2";
            lblOrdersPRC.Text += " $100";
            lblOrdersQTY.Text += " 1";
        }

        private void btnEditOrder_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {

        }
    }
}
