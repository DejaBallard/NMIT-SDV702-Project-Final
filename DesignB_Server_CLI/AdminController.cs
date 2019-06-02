using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignB_Server_CLI
{
    public class AdminController : System.Web.Http.ApiController
    {
        public List<String> GetBrandList()
        {
            DataTable lcResult = clsDbConnection.GetDataTable("SELECT brnd_name FROM tbl_brands", null);
            List<String> lcBrands = new List<String>();
            foreach (DataRow dr in lcResult.Rows)
                lcBrands.Add((string)dr[0]);
            return lcBrands;
        }
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
                DateOrdered = Convert.ToDateTime(dr["ordr_dateordered"]),
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
            par.Add("ID", prId); DataTable lcResult = clsDbConnection.GetDataTable("SELECT * FROM tbl_items WHERE item_id = @ID", par);
            clsAllItems lcItems = new clsAllItems();
            foreach (DataRow dr in lcResult.Rows)
                lcItems = (dataRow2AllItems(dr));
            return lcItems;
        }
  
        private clsAllItems dataRow2AllItems(DataRow dr)
        {
            return new clsAllItems()
            {
                Id = Convert.ToInt32(dr["item_id"]),
                Name = Convert.ToString(dr["item_name"]),
                Brand = Convert.ToString(dr["item_brand"]),
                Quantity = Convert.ToInt32(dr["item_quantity"]),
                Material = Convert.ToString(dr["item_material"]),
                Description = Convert.ToString(dr["item_description"]),
                Price = Convert.ToSingle(dr["item_price"]),
                TimeStamp = Convert.ToDateTime(dr["item_timestamp"]),
                Type = Convert.ToChar(dr["item_type"]),
                Image64 = Convert.ToString(dr["item_image"]),

                Length = dr["item_length"] is DBNull ? (int?)null : Convert.ToInt32(dr["item_length"]),

                Diameter = dr["item_diameter"] is DBNull ? (float?)null : Convert.ToSingle(dr["item_diameter"]),

                RingSize = Convert.ToString(dr["item_size"])
            };
        }


        public clsBrand GetBrand(string prName)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("NAME", prName);
            DataTable lcResult =
            clsDbConnection.GetDataTable("SELECT * FROM tbl_brands WHERE brnd_name = @NAME", par);
            if (lcResult.Rows.Count > 0)
                return new clsBrand()
                {
                    Name = Convert.ToString(lcResult.Rows[0]["brnd_name"]),
                    Description = Convert.ToString(lcResult.Rows[0]["brnd_description"]),
                    Image64 = Convert.ToString(lcResult.Rows[0]["brnd_image"]),
                    ItemList = getBrandItems(prName)
                };
            else
                return null;
        }


        private List<clsAllItems> getBrandItems(string prBrandName)
        {
            if (prBrandName == "All")
            {
                DataTable lcResult = clsDbConnection.GetDataTable("SELECT * FROM tbl_items", null);
                List<clsAllItems> lcItems = new List<clsAllItems>();
                foreach (DataRow dr in lcResult.Rows)
                    lcItems.Add(dataRow2AllItems(dr));
                return lcItems;
            }
            else
            {
                Dictionary<string, object> par = new Dictionary<string, object>(1);
                par.Add("NAME", prBrandName); DataTable lcResult = clsDbConnection.GetDataTable("SELECT * FROM tbl_items WHERE item_brand = @NAME", par);
                List<clsAllItems> lcItems = new List<clsAllItems>();
                foreach (DataRow dr in lcResult.Rows)
                    lcItems.Add(dataRow2AllItems(dr));
                return lcItems;
            }
        }


        public string PostItem(clsAllItems prItem)
        {
            try
            {
                int lcRecCount = clsDbConnection.Execute("INSERT INTO tbl_items" +
                    "(item_name, item_brand, item_type, item_material, item_description, item_price, item_quantity, item_image, item_length, item_size, item_diameter)" +
                    "VALUES( @NAME, @BRAND, @TYPE, @MATERIAL, @DESCRIPTION, @PRICE, @QUANTITY, @IMAGE, @LENGTH, @SIZE, @DIAMETER)", prepareItemPars(prItem));
                if (lcRecCount == 1) return "Item: " + prItem.Name + " has been aded to the store";
                else return "Unexpected item count: " + lcRecCount;
            }
            catch (Exception ex) { return ex.GetBaseException().Message; }
        }

        private Dictionary<string, object> prepareItemPars(clsAllItems prItem)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(11);
            par.Add("NAME", prItem.Name);
            par.Add("BRAND", prItem.Brand);
            par.Add("TYPE", prItem.Type);
            par.Add("MATERIAL", prItem.Material);
            par.Add("DESCRIPTION", prItem.Description);
            par.Add("PRICE", prItem.Price);
            par.Add("QUANTITY", prItem.Quantity);
            par.Add("IMAGE", prItem.Image64);
            par.Add("LENGTH", prItem.Length);
            par.Add("SIZE", prItem.RingSize);
            par.Add("DIAMETER", prItem.Diameter);
            return par;
        }

        public string DeleteItem(int prItemID)
        {

            try
            {
                Dictionary<string, object> pars = new Dictionary<string, object>();
                pars.Add("ID", prItemID);
                int lcRecCount = clsDbConnection.Execute(
                "DELETE FROM tbl_items WHERE item_id = @ID",pars
                );
                if (lcRecCount == 1)
                    return "Item ID: " + prItemID + " has been deleted";
                else
                    return "Unexpected artist update count: " + lcRecCount;
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }
    }
}
