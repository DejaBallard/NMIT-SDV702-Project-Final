using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DesignB_Admin_WFA
{
    public partial class frmNecklace : DesignB_Admin_WFA.frmStock
    {
        public static readonly frmNecklace Instance = new frmNecklace();
        public frmNecklace()
        {
            InitializeComponent();
        }

        internal static void Run(clsAllItems prItem)
        {
            Instance.SetDetails(prItem);
        }

        protected override void updateForm()
        {
            base.updateForm();

            txtLength.Text = _Item.Length.ToString();
        }
    }
}
