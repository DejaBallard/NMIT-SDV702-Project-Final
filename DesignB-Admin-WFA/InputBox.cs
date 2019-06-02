﻿using System;
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
    public partial class InputBox : Form
    {
        private int _Answer;

        public InputBox()
        {
            InitializeComponent();
            cboType.Items.Add("Ring");
            cboType.Items.Add("Necklace");
            cboType.Items.Add("Bracelet");
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            _Answer = cboType.SelectedIndex;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        public char GetAnswer()
        {
            switch (_Answer)
            {
                case 0:
                    return 'R';
                case 1:
                    return 'N';
                case 2:
                    return 'B';
                default:
                    return 'F';
            }
        }
    }
}