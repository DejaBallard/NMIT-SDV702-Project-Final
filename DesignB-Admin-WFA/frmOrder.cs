using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesignB_Admin_WFA
{
    public partial class frmOrder : Form
    {
        #region local Varaibles
        clsOrder _Order;
        float _Price;
        #endregion


        #region Form Creation Methods
        private frmOrder()
        {
            InitializeComponent();
            cboStatus.Items.Add("Pending");
            cboStatus.Items.Add("Shipped");
            cboStatus.Items.Add("Completed");
            cboStatus.Items.Add("Canceled");
        }

        /// <summary>
        /// Open a new form or a fill the screen with data based on the parameter
        /// </summary>
        /// <param name="prOrder">a existing order that was passed from frmMain</param>
        internal static void Run(clsOrder prOrder)
        {
            //Create a new form
            frmOrder lcOrder = new frmOrder();

            //if parameter is null, create a fresh form, else set details with existing
            if (prOrder == null)
            {
                lcOrder.SetDetails(new clsOrder());
            }
            else
            {
                lcOrder.SetDetails(prOrder);
            }
   
            lcOrder.Show();
            lcOrder.Activate();
        }
        #endregion


        #region Set and update the on screen information methods

        /// <summary>
        /// Set parameter to local variables then update
        /// </summary>
        /// <param name="prOrder"></param>
        private void SetDetails(clsOrder prOrder)
        {
            //set parameter to local varaibles
            _Order = prOrder;
            _Price = _Order.TotalPrice;
            //update the form
            updateForm();

        }

        /// <summary>
        /// update the form with details
        /// </summary>
        private void updateForm()
        {
            txtID.Enabled = false;
            txtItem.Enabled = string.IsNullOrEmpty(_Order.Item.Name);
            txtBrand.Enabled = string.IsNullOrEmpty(_Order.Item.Brand);

            txtID.Text = _Order.Id.ToString();
            txtAddress.Text = _Order.Address;
            
            txtBrand.Text = _Order.Item.Brand;
            txtEmail.Text = _Order.Email;
            txtItem.Text = _Order.Item.Name;
            lblTotalPrice.Text = "Total Price: " +_Order.TotalPrice.ToString();
            nudQuantity.Value = _Order.Quantity;
            cboStatus.SelectedItem = _Order.Status;
        }

        /// <summary>
        /// Upon the quantity is changed, update the total price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nudQuantity_ValueChanged(object sender, EventArgs e)
        {
            //multiply the item price by the quantity
            _Price = _Order.Item.Price * ((float)nudQuantity.Value);
            //update the label
            lblTotalPrice.Text = "Total Price: " + _Price;
        }

        /// <summary>
        /// Reset the form to the original information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            updateForm();
        }

        #endregion


        #region Saving onscreen information methods

        /// <summary>
        /// Upon clicked, check to see if the onscreen data is valid, then save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnOkay_Click(object sender, EventArgs e)
        {

            //check to see if the data is valid
            if (isValid())
            {
                //save data to local varaibles
                pushData();

                //send local varible to the server to be added
                MessageBox.Show(await ServiceClient.UpdateOrderAsync(_Order));
                Close();
            }
        }

        /// <summary>
        /// Check to see if the data is valid and has no errors
        /// </summary>
        /// <returns>true if no errors, else show a message and return false</returns>
        private bool isValid()
        {
            //is the email box empty
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Please make sure the email text box is filled");
                return false;
            }
            //is the email in the correct format
            else if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Make sure the email is valid");
                return false;
            }
            //is the address box empty
            else if (string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Please make sure the address text box is filled");
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Checks to see if the email is valid
        /// URL: https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        /// </summary>
        /// <param name="email">email address being checked</param>
        /// <returns>true is valid, else false</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Save data to the local variables
        /// </summary>
        private void pushData()
        {
            _Order.Email = txtEmail.Text;
            _Order.Address = txtAddress.Text;
            _Order.Quantity = Convert.ToInt32(Math.Round(nudQuantity.Value, 0));
            _Order.Status = cboStatus.SelectedItem.ToString();
            _Order.TotalPrice = _Price;
        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel and close?", "Cancel", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                Close();
        }
    }
}
