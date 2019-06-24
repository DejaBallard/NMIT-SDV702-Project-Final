using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using System.IO;
using System.Drawing;

namespace DesignB_Admin_WFA
{
    public partial class frmStock : Form
    {
        #region Local Variables
        protected clsAllItems _Item;
        private List<string> _Brands;
        private string _Image64Text;

        public delegate void LoadWorkFormDelegate(clsAllItems prItem);
        #endregion


        #region Form Creation Methods
        protected frmStock()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Run a delegate and send current item type to open correct subform
        /// </summary>
        /// <param name="prItems">Item class</param>
        internal static void DispatchWorkForm(clsAllItems prItems)
        {
            _ItemForm[prItems.Type].DynamicInvoke(prItems);
        }

        /// <summary>
        /// Dependant on the input of the user, run a subclass and subform
        /// </summary>
        public static Dictionary<char, Delegate> _ItemForm = new Dictionary<char, Delegate>
        {
            { 'R', new LoadWorkFormDelegate(frmRing.Run) },
            { 'N', new LoadWorkFormDelegate(frmNecklace.Run) },
            { 'B', new LoadWorkFormDelegate(frmBracelet.Run) }
        };
        #endregion


        #region Set and Update form Methods

        /// <summary>
        /// Set data to local variables, then update
        /// </summary>
        /// <param name="prItem"></param>
        public async void SetDetails(clsAllItems prItem)
        {
            //Get all brands apart from "All"
            _Brands = await ServiceClient.GetBrandNamesAsync("All");
            _Item = prItem;
            updateForm();
            ShowDialog();
        }

        /// <summary>
        /// Update the onscreen information with local varaibles
        /// </summary>
        protected virtual void updateForm()
        {
            txtID.Text = _Item.Id.ToString();
            txtDescription.Text = _Item.Description;
            txtMaterial.Text = _Item.Material;
            cboBrands.DataSource = _Brands;
            cboBrands.SelectedItem = _Item.Brand;
            
            txtName.Text = _Item.Name;
            txtQuantity.Text = _Item.Quantity.ToString();
            txtPrice.Text = _Item.Price.ToString();
            txtID.Enabled = String.IsNullOrEmpty(_Item.Id.ToString());
            cboBrands.Enabled = string.IsNullOrEmpty(_Item.Brand);
            if (!string.IsNullOrEmpty(_Item.Image64)) {
                lblImageName.Text = "Image Uploaded";
            }
        }

        /// <summary>
        /// Upon clicked, open a file explorer for user to select a image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog lcImageDialog = new OpenFileDialog();
                //Only allow JPG or PNG files
                lcImageDialog.Filter = "jpg files(.*jpg)|*.jpg| PNG files(.*png)|*.png";
                if (lcImageDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var lcFileSize = new FileInfo(lcImageDialog.FileName).Length;

                    //Check the size of the file
                    if (lcFileSize <= 16000)
                    {
                        //Show directory on form
                        lblImageName.Text = lcImageDialog.FileName;
                        //convert to a image
                        Bitmap lcImage = new Bitmap(lcImageDialog.FileName);
                        //convert to bytes
                        byte[] lcImageArray = System.IO.File.ReadAllBytes(lcImageDialog.FileName);
                        //convert to base64
                        _Image64Text = Convert.ToBase64String(lcImageArray);
                    }
                    else { MessageBox.Show("Image size is too big. Make sure it is smaller than 16kb"); }
                }
            }
            catch (Exception ex) { }
        }
        #endregion


        #region Save Methods
        /// <summary>
        /// upon clicked, Check to see if data is valid then save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnOkay_Click(object sender, EventArgs e)
        {
            //check to see if the data is valid
            if (isValid())
            {
                //If enabled, means the form is new and not a edit
                if (cboBrands.Enabled)
                {
                  //Save data to local variable
                  pushData();
                    // Send local variable to the server
                    MessageBox.Show(await ServiceClient.InsertItemAsync(_Item));

                }
                else
                {
                    //Save data to local variable
                    pushData();
                    // Send local variable to the server
                    MessageBox.Show(await ServiceClient.UpdateItemAsync(_Item));
                }
                Close();
            }
        }

        /// <summary>
        /// Checks to see if all data is valid
        /// </summary>
        /// <returns>true if data is valid, else show a message and return false</returns>
        private bool isValid()
        {
            //is the name box empty
            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please enter a character value in the name text box");
                return false;
            }
            //Does the price have something other than decimal characters
            else if (!decimal.TryParse(txtPrice.Text, out decimal PriceResult))
            {
                MessageBox.Show("Please enter a numberic value in the price text box");
                return false;
            }
            //Does the quantity have something other than a int character
            else if (!int.TryParse(txtQuantity.Text, out int QuantityResult))
            {
                MessageBox.Show("Please enter a numberic value in the quantity text box");
                return false;
            }
            //has a image been uploaded
            else if (string.IsNullOrEmpty(lblImageName.Text))
            {
                MessageBox.Show("Please upload a image");
                return false;
            }
            else
                return true;
        }

        /// <summary>
        /// Set data to local varaibles
        /// </summary>
        private void pushData()
        {
            _Item.Brand = cboBrands.SelectedItem as string;
            _Item.Name = txtName.Text;
            _Item.Image64 = _Image64Text;
            _Item.Material = txtMaterial.Text;
            _Item.Description = txtDescription.Text;
            _Item.Price = Convert.ToSingle(txtPrice.Text);
            _Item.Quantity = Convert.ToInt32(txtQuantity.Text);
        }
        #endregion

        /// <summary>
        /// upon clicked, ask if user wants to close the form without doing anything
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to cancel and close?","Cancel", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            Close();
        }
    }
}
