using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DesignB_Admin_WFA
{
    public partial class frmRing : DesignB_Admin_WFA.frmStock
    {
        //Singleton instance
        public static readonly frmRing Instance = new frmRing();


        public frmRing()
        {
            InitializeComponent();
        }


        /// <summary>
        ///Set the details of the item to the display
        /// </summary>
        /// <param name="prItem">either existing item, selected from frmMain or a new item</param>
        internal static void Run(clsAllItems prItem)
        {
            Instance.SetDetails(prItem);
        }


        /// <summary>
        /// Run base method of updating the form with items details, then update more details
        /// </summary>
        protected override void updateForm()
        {
            base.updateForm();
             txtSize.Text = _Item.RingSize;
        }

    }
}
