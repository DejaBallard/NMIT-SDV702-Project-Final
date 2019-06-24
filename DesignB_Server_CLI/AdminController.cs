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
        #region Brand Methods
        /// <summary>
        /// Get all brand names from the database
        /// </summary>
        /// <param name="prExBrand">Get all brand names apart from this parameter</param>
        /// <returns>A list of brand names</returns>
        public List<String> GetBrandList(string prExBrand)
        {

                if (prExBrand != null)
                {
                    Dictionary<string, object> par = new Dictionary<string, object>(1);
                    par.Add("EXBRAND", prExBrand);
                    DataTable lcResult = clsDbConnection.GetDataTable("use dbdesignb; SELECT brnd_name FROM tbl_brands WHERE NOT brnd_name = @EXBRAND", par);
                    List<String> lcBrands = new List<String>();
                    foreach (DataRow dr in lcResult.Rows)
                        lcBrands.Add((string)dr[0]);
                    return lcBrands;
                }
                else
                {
                    DataTable lcResult = clsDbConnection.GetDataTable("use dbdesignb; SELECT brnd_name FROM tbl_brands", null);
                    List<String> lcBrands = new List<String>();
                    foreach (DataRow dr in lcResult.Rows)
                        lcBrands.Add((string)dr[0]);
                    return lcBrands;
                }
            
        }

        /// <summary>
        /// Get all data about a brand from the database
        /// </summary>
        /// <param name="prName">brand name being searched</param>
        /// <returns>brand with all data, including item list</returns>
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

        /// <summary>
        /// Get all items connected to a brand
        /// </summary>
        /// <param name="prBrandName">Brand name being searched</param>
        /// <returns>list of items</returns>
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
                par.Add("NAME", prBrandName);
                DataTable lcResult = clsDbConnection.GetDataTable("SELECT * FROM tbl_items WHERE item_brand = @NAME", par);
                List<clsAllItems> lcItems = new List<clsAllItems>();
                foreach (DataRow dr in lcResult.Rows)
                    lcItems.Add(dataRow2AllItems(dr));
                return lcItems;
            }
        }
        #endregion


        #region Order Methods
        /// <summary>
        /// Get all orders from the database
        /// </summary>
        /// <returns>A list of orders</returns>
        public List<clsOrder> GetOrderList()
        {
            DataTable lcResult = clsDbConnection.GetDataTable("SELECT * FROM tbl_orders", null);
            List<clsOrder> lcOrders = new List<clsOrder>();
            foreach (DataRow dr in lcResult.Rows)
                lcOrders.Add(dataRow2Order(dr));
            return lcOrders;
        }

        /// <summary>
        /// Convert SQL row into a C# class 
        /// </summary>
        /// <param name="dr">SQL data row</param>
        /// <returns>converted order data</returns>
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

        /// <summary>
        /// Delete an order, if timestamp is correct
        /// </summary>
        /// <param name="prOrder">Order being deleted</param>
        /// <returns>result of the SQL row count</returns>
        public string DeleteOrder(clsOrder prOrder)
        {

            try
            {
                Dictionary<string, object> pars = new Dictionary<string, object>();
                pars.Add("ID", prOrder.Id);
                pars.Add("STAMP", prOrder.TimeStamp);
                int lcRecCount = clsDbConnection.Execute(
                "DELETE FROM tbl_orders WHERE ordr_id = @ID AND ordr_timestamp = @STAMP", pars
                );
                if (lcRecCount == 1)
                    return "Order ID: " + prOrder.Id + " has been deleted";
                else if (lcRecCount == 0)
                {
                    return "Item ID: " + prOrder.Id + " has been updated since you submitted, refresh and try again";
                }
                else
                    return "Unexpected Order update count: " + lcRecCount;
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }
        #endregion

    
        #region Item Methods
        /// <summary>
        /// Get a item from the database
        /// </summary>
        /// <param name="prId">the item ID</param>
        /// <returns>a completed class of the searched item</returns>
        private clsAllItems getItem(int prId)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("ID", prId); DataTable lcResult = clsDbConnection.GetDataTable("SELECT * FROM tbl_items WHERE item_id = @ID", par);
            clsAllItems lcItems = new clsAllItems();
            foreach (DataRow dr in lcResult.Rows)
                lcItems = (dataRow2AllItems(dr));
            return lcItems;
        }
  
        /// <summary>
        /// Convert SQL row to C#
        /// </summary>
        /// <param name="dr">SQL data row</param>
        /// <returns>Coverted C# item</returns>
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

        /// <summary>
        /// Insert a new item into the database
        /// </summary>
        /// <param name="prItem">the item being inserted</param>
        /// <returns>row count</returns>
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

        public string PutItem(clsAllItems prItems)
        {
            try
            {
                int lcRecCount = clsDbConnection.Execute("UPDATE tbl_items "+
                    "SET item_name = @NAME, item_brand = @BRAND, item_type = @TYPE, item_material = @MATERIAL, item_description = @DESCRIPTION, item_price = @PRICE, item_quantity =  @QUANTITY, item_image = @IMAGE, item_length = @LENGTH, item_size = @SIZE, item_diameter = @DIAMETER " +
                    " where item_id = @ID;",prepareItemPars(prItems));
                return "Updated " + prItems.Name;
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().ToString();
            }
        }
        /// <summary>
        /// converting C# to SQL 
        /// </summary>
        /// <param name="prItem">item being converted</param>
        /// <returns>SQL code</returns>
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
            if (prItem.Id != 0) {
                par.Add("ID", prItem.Id);
                par.Add("TIMESTAMP", prItem.TimeStamp);
            }
            return par;
        }

        /// <summary>
        /// Delete a item from the database
        /// </summary>
        /// <param name="prItem">item being deleted</param>
        /// <returns>row count</returns>
        public string DeleteItem(clsAllItems prItem)
        {

            try
            {
                Dictionary<string, object> pars = new Dictionary<string, object>();
                pars.Add("ID", prItem.Id);
                pars.Add("STAMP", prItem.TimeStamp);
                int lcRecCount = clsDbConnection.Execute(
                "DELETE FROM tbl_items WHERE item_id = @ID AND item_timestamp = @STAMP",pars
                );
                if (lcRecCount == 1)
                    return "Item ID: " + prItem.Id + " has been deleted";
                else if(lcRecCount == 0)
                {
                    return "Item ID: "+prItem.Id+" has been updated since you submitted, refresh and try again";
                }else
                    return "Unexpected artist update count: " + lcRecCount;
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }
        #endregion

      
    }
}
