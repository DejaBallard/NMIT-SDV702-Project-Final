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
        #region Local Variables
        private List<clsAllItems> _Items = new List<clsAllItems>();
        private List<clsOrder> _Orders = new List<clsOrder>();
        private float _TotalOrdersPrice;
        private int _TotalOrdersQTY;
        private float _TotalStockPrice;
        private int _TotalStockQTY;
        #endregion


        public frmMain()
        {
            InitializeComponent();
            updateDisplay();

        }


        #region Update and set details Methods
        /// <summary>
        /// Gets data from the database and fill onscreen information
        /// </summary>
        private async void updateDisplay()
        {
            //Set Lists to null
            lstStock.DataSource = null;
            lstOrders.DataSource = null;

            //Gets brands into a drop box
            //Once this is filled, it triggeres cboBrands_SelectedIndexChanged
            try
            {
                cboBrands.DataSource = await ServiceClient.GetBrandNamesAsync(null);
                //Get orders into a local list
                _Orders = await ServiceClient.GetOrderListAsync();

                //Set local orders to a onscreen list
                lstOrders.DataSource = _Orders;

                // Calculate the totals for price and QTY
                calculateTotals();

                //Update the totals labels
                UpdateTotals();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Update the onscreen labels for total prices and QTY's
        /// </summary>
        private void UpdateTotals()
        {
            lblOrdersPRC.Text = "Total Order Price: $" + _TotalOrdersPrice.ToString();
            lblOrdersQTY.Text = "Total Order Quantity: " + _TotalOrdersQTY.ToString();
            lblStockPRC.Text = "Total Stock Price: $" + _TotalStockPrice.ToString();
            lblStockQTY.Text = "Total Stock Quantity: " + _TotalStockQTY.ToString();
        }

        /// <summary>
        /// Calculate the total prices and QTY's from Orders and Items
        /// </summary>
        private void calculateTotals()
        {
            //Reset local variables
            _TotalOrdersPrice = 0;
            _TotalStockPrice = 0;
            _TotalOrdersQTY = 0;
            _TotalStockQTY = 0;

            //Add price and QTY of each order in the list to the local variables
            foreach (clsOrder lcOrder in _Orders)
            {
                _TotalOrdersPrice += lcOrder.TotalPrice;
                _TotalOrdersQTY += lcOrder.Quantity;
            }

            //Add price and QTY of each item in the list to the local variables
            foreach (clsAllItems lcItem in _Items)
            {
                _TotalStockPrice += lcItem.Price;
                _TotalStockQTY += lcItem.Quantity;
            }
        }

        /// <summary>
        /// Upon clicked, updates the screen with fresh and up to date data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            updateDisplay();
        }
        #endregion


        #region Stock Item Methods
        /// <summary>
        /// Upon changing the brands drop list, get the selected brands information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void cboBrands_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Go to the database
            clsBrand lcBrand = await ServiceClient.GetBrandAsync(cboBrands.SelectedItem as string);

            //Set brands items to local list
            _Items = lcBrand.ItemList;
            
            //Update onscreen list with local variable
            lstStock.DataSource = _Items;
            
            //Re Calculate the new list of items
            calculateTotals();
            //Update the onscreen labels for price and QTY
            UpdateTotals();
        }

        /// <summary>
        /// Upon double clicking a item in the stock list, Open a form with the selected details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstStock_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                frmStock.DispatchWorkForm(lstStock.SelectedValue as clsAllItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            updateDisplay();
        }

        /// <summary>
        /// Upon click, open a inputbox for selecting the type of item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            InputBox lcInput = new InputBox();
            if (lcInput.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Get answer from input box
                char lcResult = lcInput.GetAnswer();
                //if the result isn't F(Failed)
                if (lcResult != 'F')
                {
                    //Create a new item class with the type added
                    clsAllItems lcItem = clsAllItems.NewWork(lcResult);
                    //Open up the correct form based on the item type
                    frmStock.DispatchWorkForm(lcItem);
                }
                else
                {
                    MessageBox.Show("Error, the selected item type does not exist. Contact your administrator");
                }
            }
            updateDisplay();
        }

        /// <summary>
        /// Edit the highlighted item from onscreen stock list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Open the from with the item details filled in it
                frmStock.DispatchWorkForm(lstStock.SelectedValue as clsAllItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            updateDisplay();
        }

        /// <summary>
        /// Delete the highlighted item from the onscreen stock list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnDeleteItem_Click(object sender, EventArgs e)
        {
            //Get selected value into a local variable
            clsAllItems lcItem = lstStock.SelectedValue as clsAllItems;

            //Ask user if they are sure they want to delete this item from the brand's inventory
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete " +lcItem.Name +" from the "+ lcItem.Brand+" inventory?", "Delete", MessageBoxButtons.YesNo);

            //If user says yes
            if (dialogResult == DialogResult.Yes)
            {
                //Send a delete request to the server
               MessageBox.Show(await ServiceClient.DeleteItemAsync(lcItem));
            }
            
            //refresh and update the screen
            updateDisplay();
        }
        #endregion


        #region Order Methods
        /// <summary>
        /// Upon double clicking a order from the onscreen order list, edit the order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstOrders_DoubleClick(object sender, EventArgs e)
        {    
            //Open the form with the highlighted order's data from the onscreen order list
            frmOrder.Run(lstOrders.SelectedItem as clsOrder);
            updateDisplay();
        }

        /// <summary>
        /// Upon clicking, edit the highlighted order from the onscreen order list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditOrder_Click(object sender, EventArgs e)
        {
            //Open the form with the highlighted order's data from the onscreen order list
            frmOrder.Run(lstOrders.SelectedItem as clsOrder);
            updateDisplay();
        }

        /// <summary>
        /// Upon click, ask the user and then delete order from the on screen order list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            //Set highlisted order from onscreen order list to a local variable
            clsOrder lcOrder = lstOrders.SelectedValue as clsOrder;

            //Asks user if they want to delete the item from the server
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete " + lcOrder.Email + "'s order?", "Delete "+lcOrder.Id, MessageBoxButtons.YesNo);
            
            //If user says yes, send a request to the server
            if (dialogResult == DialogResult.Yes)
            {
                MessageBox.Show(await ServiceClient.DeleteOrderAsync(lcOrder));
            }

            //Update the display
            updateDisplay();
        }

        #endregion

    }
}
