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
            this.lblOrdersQTY = new System.Windows.Forms.Label();
            this.lblStockQTY = new System.Windows.Forms.Label();
            this.lblOrdersPRC = new System.Windows.Forms.Label();
            this.lblStockPRC = new System.Windows.Forms.Label();
            this.btnDeleteOrder = new System.Windows.Forms.Button();
            this.lstOrders = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.lstStock = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnEditItem = new System.Windows.Forms.Button();
            this.btnEditOrder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblOrdersQTY
            // 
            this.lblOrdersQTY.AutoSize = true;
            this.lblOrdersQTY.Location = new System.Drawing.Point(305, 417);
            this.lblOrdersQTY.Name = "lblOrdersQTY";
            this.lblOrdersQTY.Size = new System.Drawing.Size(105, 13);
            this.lblOrdersQTY.TabIndex = 23;
            this.lblOrdersQTY.Text = "Total Order Quantity:";
            // 
            // lblStockQTY
            // 
            this.lblStockQTY.AutoSize = true;
            this.lblStockQTY.Location = new System.Drawing.Point(12, 417);
            this.lblStockQTY.Name = "lblStockQTY";
            this.lblStockQTY.Size = new System.Drawing.Size(107, 13);
            this.lblStockQTY.TabIndex = 22;
            this.lblStockQTY.Text = "Total Stock Quantity:";
            // 
            // lblOrdersPRC
            // 
            this.lblOrdersPRC.AutoSize = true;
            this.lblOrdersPRC.Location = new System.Drawing.Point(305, 395);
            this.lblOrdersPRC.Name = "lblOrdersPRC";
            this.lblOrdersPRC.Size = new System.Drawing.Size(90, 13);
            this.lblOrdersPRC.TabIndex = 21;
            this.lblOrdersPRC.Text = "Total Order Price:";
            // 
            // lblStockPRC
            // 
            this.lblStockPRC.AutoSize = true;
            this.lblStockPRC.Location = new System.Drawing.Point(12, 395);
            this.lblStockPRC.Name = "lblStockPRC";
            this.lblStockPRC.Size = new System.Drawing.Size(92, 13);
            this.lblStockPRC.TabIndex = 20;
            this.lblStockPRC.Text = "Total Stock Price:";
            // 
            // btnDeleteOrder
            // 
            this.btnDeleteOrder.Location = new System.Drawing.Point(496, 66);
            this.btnDeleteOrder.Name = "btnDeleteOrder";
            this.btnDeleteOrder.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteOrder.TabIndex = 19;
            this.btnDeleteOrder.Text = "Delete Order";
            this.btnDeleteOrder.UseVisualStyleBackColor = true;
            this.btnDeleteOrder.Click += new System.EventHandler(this.btnDeleteOrder_Click);
            // 
            // lstOrders
            // 
            this.lstOrders.FormattingEnabled = true;
            this.lstOrders.Location = new System.Drawing.Point(308, 37);
            this.lstOrders.Name = "lstOrders";
            this.lstOrders.Size = new System.Drawing.Size(183, 355);
            this.lstOrders.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(304, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 24);
            this.label2.TabIndex = 16;
            this.label2.Text = "Orders";
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Location = new System.Drawing.Point(203, 95);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteItem.TabIndex = 15;
            this.btnDeleteItem.Text = "Delete Item";
            this.btnDeleteItem.UseVisualStyleBackColor = true;
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(203, 37);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(75, 23);
            this.btnAddItem.TabIndex = 14;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            // 
            // lstStock
            // 
            this.lstStock.FormattingEnabled = true;
            this.lstStock.Location = new System.Drawing.Point(14, 37);
            this.lstStock.Name = "lstStock";
            this.lstStock.Size = new System.Drawing.Size(183, 355);
            this.lstStock.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 24);
            this.label1.TabIndex = 12;
            this.label1.Text = "Stock";
            // 
            // btnEditItem
            // 
            this.btnEditItem.Location = new System.Drawing.Point(203, 66);
            this.btnEditItem.Name = "btnEditItem";
            this.btnEditItem.Size = new System.Drawing.Size(75, 23);
            this.btnEditItem.TabIndex = 24;
            this.btnEditItem.Text = "Edit Item";
            this.btnEditItem.UseVisualStyleBackColor = true;
            // 
            // btnEditOrder
            // 
            this.btnEditOrder.Location = new System.Drawing.Point(496, 37);
            this.btnEditOrder.Name = "btnEditOrder";
            this.btnEditOrder.Size = new System.Drawing.Size(75, 23);
            this.btnEditOrder.TabIndex = 18;
            this.btnEditOrder.Text = "Edit Order";
            this.btnEditOrder.UseVisualStyleBackColor = true;
            this.btnEditOrder.Click += new System.EventHandler(this.btnEditOrder_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 450);
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

        private System.Windows.Forms.Label lblOrdersQTY;
        private System.Windows.Forms.Label lblStockQTY;
        private System.Windows.Forms.Label lblOrdersPRC;
        private System.Windows.Forms.Label lblStockPRC;
        private System.Windows.Forms.Button btnDeleteOrder;
        private System.Windows.Forms.ListBox lstOrders;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDeleteItem;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.ListBox lstStock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEditItem;
        private System.Windows.Forms.Button btnEditOrder;
    }
}

