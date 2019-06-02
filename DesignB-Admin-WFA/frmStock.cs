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
        protected clsAllItems _Item;
        private List<string> _Brands;
        private string _Image64Text;

        public delegate void LoadWorkFormDelegate(clsAllItems prItem);

        /// <summary>
        /// Dependant on the input of the user, run a subclass and subform
        /// </summary>
        public static Dictionary<char, Delegate> _ItemForm = new Dictionary<char, Delegate>
        {
            { 'R', new LoadWorkFormDelegate(frmRing.Run) },
            { 'N', new LoadWorkFormDelegate(frmNecklace.Run) },
            { 'B', new LoadWorkFormDelegate(frmBracelet.Run) }
        };
        public frmStock()
        {
            InitializeComponent();
        }

        internal static void DispatchWorkForm(clsAllItems prItems)
        {
            _ItemForm[prItems.Type].DynamicInvoke(prItems);
        }

        public async void SetDetails(clsAllItems prItem)
        {
            _Brands = await ServiceClient.GetBrandNamesAsync();
            _Item = prItem;
            updateForm();
            ShowDialog();
        }

        protected virtual void updateForm()
        {
            txtID.Text = _Item.Id.ToString();
            txtDescription.Text = _Item.Description;
            txtMaterial.Text = _Item.Material;
            cboBrands.DataSource = _Brands;
            cboBrands.SelectedItem =_Item.Brand;
            txtName.Text = _Item.Name;
            txtQuantity.Text = _Item.Quantity.ToString();
            txtPrice.Text = _Item.Price.ToString();
            txtID.Enabled = String.IsNullOrEmpty(_Item.Id.ToString());
            cboBrands.Enabled = string.IsNullOrEmpty(_Item.Brand);
            if (!string.IsNullOrEmpty(_Item.Image64)) {
                lblImageName.Text = "Image Uploaded";
            }
        }

        private async void btnOkay_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                if (cboBrands.Enabled)
                {
                    pushData();
                  await ServiceClient.InsertItemAsync(_Item);

                }
                else
                {
                    pushData();
                    MessageBox.Show(ServiceClient.UpdateItemAsync(_Item).ToString());
                }
                Close();
            }
            else
            MessageBox.Show("Text not valid");
        }

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

        private bool isValid()
        {

            if (string.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show("Please enter a character value in the name text box");
                return false;
            }
            else if (!decimal.TryParse(txtPrice.Text, out decimal PriceResult))
            {
                MessageBox.Show("Please enter a numberic value in the price text box");
                return false;
            }
            else if (int.TryParse(txtQuantity.Text, out int QuantityResult))
            {
                MessageBox.Show("Please enter a numberic value in the quantity text box");
                return false;
            }
            else if (string.IsNullOrEmpty(lblImageName.Text))
            {
                MessageBox.Show("Please upload a image");
                return false;
            }
            else
                return true;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog lcImageDialog = new OpenFileDialog();
                lcImageDialog.Filter = "jpg files(.*jpg)|*.jpg| PNG files(.*png)|*.png| All Files(*.*)|*.*";
                if(lcImageDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var size = new FileInfo(lcImageDialog.FileName).Length;
                    if (size <= 16000) {
                        lblImageName.Text = lcImageDialog.FileName;
                        Bitmap lcImage = new Bitmap(lcImageDialog.FileName);
                        byte[] lcImageArray = System.IO.File.ReadAllBytes(lcImageDialog.FileName);
                        _Image64Text = Convert.ToBase64String(lcImageArray);
                    }
                    else { MessageBox.Show("Image size is too big. Make sure it is smaller than 16kb"); }
                }
            }
            catch(Exception ex) { }
        }
    }
}
