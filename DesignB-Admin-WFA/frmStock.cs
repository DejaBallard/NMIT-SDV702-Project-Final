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
    public partial class frmStock : Form
    {
        protected clsAllItems _Item;

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

        public void SetDetails(clsAllItems prItem)
        {
            _Item = prItem;
            updateForm();
            ShowDialog();
        }

        private void updateForm()
        {
            txtID.Text = _Item.Id.ToString();
        }
    }
}
