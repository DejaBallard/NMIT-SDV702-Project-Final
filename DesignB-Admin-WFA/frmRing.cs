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
        public static readonly frmRing Instance = new frmRing();

        public frmRing()
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
            // clsSculpture lcWork = (clsSculpture)this._Work;
            txtSize.Text = _Item.RingSize;
        }

    }
}
