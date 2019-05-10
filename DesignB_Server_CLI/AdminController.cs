﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignB_Server_CLI
{
    class AdminController
    {
        public List<clsOrder> GetOrderList()
        {
            DataTable lcResult = clsDbConnection.GetDataTable("SELECT * FROM tbl_orders", null);
            List<clsOrder> lcOrders = new List<clsOrder>();
            foreach (DataRow dr in lcResult.Rows)
                lcOrders.Add(dataRow2Order(dr));
            return lcOrders;
        }

        private clsOrder dataRow2Order(DataRow dr)
        {
            return new clsOrder()
            {
                Id = Convert.ToInt32(dr["ordr_id"]),
                Email = Convert.ToString(dr["ordr_email"]),
                Address = Convert.ToString(dr["ordr_address"]),
                DateOrdered = Convert.ToDateTime(dr["ordr_dateorderd"]),
                Item = getItem(Convert.ToInt32(dr["ordr_item"])), 
                Quantity = Convert.ToInt32(dr["ordr_quantity"]),
                TotalPrice = Convert.ToSingle(dr["ordr_totalprice"]),
                Status = Convert.ToString(dr["ordr_status"]),
                TimeStamp = Convert.ToDateTime(dr["ordr_timestamp"])
            };
        }

        private clsAllItems getItem(int prId)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("ID",prId); DataTable lcResult = clsDbConnection.GetDataTable("SELECT * FROM tbl_items WHERE item_id = @ID", par);
            clsAllItems lcItems = new clsAllItems();
            foreach (DataRow dr in lcResult.Rows)
                lcItems =(dataRow2AllItems(dr));
            return lcItems;
        }

        private clsAllItems dataRow2AllItems(DataRow dr)
        {
            return new clsAllItems()
            {
                Id = Convert.ToInt32(dr["item_id"]),
                Name = Convert.ToString(dr["item_name"]),
                Brand = Convert.ToString(dr["item_brand"]),
                Material = Convert.ToString(dr["item_material"]),
                Description = Convert.ToString(dr["item_description"]),
                Price = Convert.ToSingle(dr["item_price"]),
                TimeStamp = Convert.ToDateTime(dr["item_timestamp"]),
                Type = Convert.ToString(dr["item_type"]),

                Length = dr["item_length"] is DBNull ? (int?)null : Convert.ToInt32(dr["item_length"]),

                Diameter = dr["item_diameter"] is DBNull ? (float?)null : Convert.ToSingle(dr["item_diameter"]),

                RingSize = Convert.ToString(dr["item_size"])
            };
        }
    }
}