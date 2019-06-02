namespace DesignB_Admin_WFA
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEditItem = new System.Windows.Forms.Button();
            this.lblOrdersQTY = new System.Windows.Forms.Label();
            this.lblStockQTY = new System.Windows.Forms.Label();
            this.lblOrdersPRC = new System.Windows.Forms.Label();
            this.lblStockPRC = new System.Windows.Forms.Label();
            this.btnDeleteOrder = new System.Windows.Forms.Button();
            this.btnEditOrder = new System.Windows.Forms.Button();
            this.lstOrders = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.lstStock = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboBrands = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEditItem
            // 
            this.btnEditItem.Location = new System.Drawing.Point(205, 100);
            this.btnEditItem.Name = "btnEditItem";
            this.btnEditItem.Size = new System.Drawing.Size(75, 23);
            this.btnEditItem.TabIndex = 50;
            this.btnEditItem.Text = "Edit Item";
            this.btnEditItem.UseVisualStyleBackColor = true;
            this.btnEditItem.Click += new System.EventHandler(this.btnEditItem_Click);
            // 
            // lblOrdersQTY
            // 
            this.lblOrdersQTY.AutoSize = true;
            this.lblOrdersQTY.Location = new System.Drawing.Point(307, 451);
            this.lblOrdersQTY.Name = "lblOrdersQTY";
            this.lblOrdersQTY.Size = new System.Drawing.Size(105, 13);
            this.lblOrdersQTY.TabIndex = 49;
            this.lblOrdersQTY.Text = "Total Order Quantity:";
            // 
            // lblStockQTY
            // 
            this.lblStockQTY.AutoSize = true;
            this.lblStockQTY.Location = new System.Drawing.Point(14, 451);
            this.lblStockQTY.Name = "lblStockQTY";
            this.lblStockQTY.Size = new System.Drawing.Size(107, 13);
            this.lblStockQTY.TabIndex = 48;
            this.lblStockQTY.Text = "Total Stock Quantity:";
            // 
            // lblOrdersPRC
            // 
            this.lblOrdersPRC.AutoSize = true;
            this.lblOrdersPRC.Location = new System.Drawing.Point(307, 429);
            this.lblOrdersPRC.Name = "lblOrdersPRC";
            this.lblOrdersPRC.Size = new System.Drawing.Size(90, 13);
            this.lblOrdersPRC.TabIndex = 47;
            this.lblOrdersPRC.Text = "Total Order Price:";
            // 
            // lblStockPRC
            // 
            this.lblStockPRC.AutoSize = true;
            this.lblStockPRC.Location = new System.Drawing.Point(14, 429);
            this.lblStockPRC.Name = "lblStockPRC";
            this.lblStockPRC.Size = new System.Drawing.Size(92, 13);
            this.lblStockPRC.TabIndex = 46;
            this.lblStockPRC.Text = "Total Stock Price:";
            // 
            // btnDeleteOrder
            // 
            this.btnDeleteOrder.Location = new System.Drawing.Point(498, 100);
            this.btnDeleteOrder.Name = "btnDeleteOrder";
            this.btnDeleteOrder.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteOrder.TabIndex = 45;
            this.btnDeleteOrder.Text = "Delete Order";
            this.btnDeleteOrder.UseVisualStyleBackColor = true;
            // 
            // btnEditOrder
            // 
            this.btnEditOrder.Location = new System.Drawing.Point(498, 71);
            this.btnEditOrder.Name = "btnEditOrder";
            this.btnEditOrder.Size = new System.Drawing.Size(75, 23);
            this.btnEditOrder.TabIndex = 44;
            this.btnEditOrder.Text = "Edit Order";
            this.btnEditOrder.UseVisualStyleBackColor = true;
            // 
            // lstOrders
            // 
            this.lstOrders.FormattingEnabled = true;
            this.lstOrders.Location = new System.Drawing.Point(310, 71);
            this.lstOrders.Name = "lstOrders";
            this.lstOrders.Size = new System.Drawing.Size(183, 355);
            this.lstOrders.TabIndex = 43;
            this.lstOrders.DoubleClick += new System.EventHandler(this.lstOrders_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(306, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 24);
            this.label2.TabIndex = 42;
            this.label2.Text = "Orders";
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Location = new System.Drawing.Point(205, 129);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteItem.TabIndex = 41;
            this.btnDeleteItem.Text = "Delete Item";
            this.btnDeleteItem.UseVisualStyleBackColor = true;
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(205, 71);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(75, 23);
            this.btnAddItem.TabIndex = 40;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // lstStock
            // 
            this.lstStock.FormattingEnabled = true;
            this.lstStock.Location = new System.Drawing.Point(16, 71);
            this.lstStock.Name = "lstStock";
            this.lstStock.Size = new System.Drawing.Size(183, 355);
            this.lstStock.TabIndex = 39;
            this.lstStock.DoubleClick += new System.EventHandler(this.lstStock_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 24);
            this.label1.TabIndex = 38;
            this.label1.Text = "Stock";
            // 
            // cboBrands
            // 
            this.cboBrands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBrands.FormattingEnabled = true;
            this.cboBrands.Location = new System.Drawing.Point(78, 44);
            this.cboBrands.Name = "cboBrands";
            this.cboBrands.Size = new System.Drawing.Size(121, 21);
            this.cboBrands.TabIndex = 51;
            this.cboBrands.SelectedIndexChanged += new System.EventHandler(this.cboBrands_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 52;
            this.label3.Text = "Brand:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 471);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboBrands);
            this.Controls.Add(this.btnEditItem);
            this.Controls.Add(this.lblOrdersQTY);
            this.Controls.Add(this.lblStockQTY);
            this.Controls.Add(this.lblOrdersPRC);
            this.Controls.Add(this.lblStockPRC);
            this.Controls.Add(this.btnDeleteOrder);
            this.Controls.Add(this.btnEditOrder);
            this.Controls.Add(this.lstOrders);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDeleteItem);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.lstStock);
            this.Controls.Add(this.label1);
            this.Name = "frmMain";
            this.Text = "Admin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEditItem;
        private System.Windows.Forms.Label lblOrdersQTY;
        private System.Windows.Forms.Label lblStockQTY;
        private System.Windows.Forms.Label lblOrdersPRC;
        private System.Windows.Forms.Label lblStockPRC;
        private System.Windows.Forms.Button btnDeleteOrder;
        private System.Windows.Forms.Button btnEditOrder;
        private System.Windows.Forms.ListBox lstOrders;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDeleteItem;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.ListBox lstStock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboBrands;
        private System.Windows.Forms.Label label3;
    }
}

