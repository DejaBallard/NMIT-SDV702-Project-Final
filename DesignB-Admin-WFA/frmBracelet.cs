using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DesignB_Admin_WFA
{
    public partial class frmBracelet : DesignB_Admin_WFA.frmStock
    {
        //Singleton instance
        public static readonly frmBracelet Instance = new frmBracelet();

        public frmBracelet()
        {
            InitializeComponent();
        }

        /// <summary>
        ///Set the details of the item to the display
        /// </summary>
        /// <param name="prItem">either existing item, selected from frmMain or a new item</param>
        internal static void Run(clsAllItems prItem)
        {
            //Set the details for the form
            Instance.SetDetails(prItem);
            
            //Go to frmStock to see this method
        }

        /// <summary>
        /// Run base method of updating the form with items details, then update more details
        /// </summary>
        protected override void updateForm()
        {
            base.updateForm();
            txtDiameter.Text = _Item.Diameter.ToString();
        }
    }
}
