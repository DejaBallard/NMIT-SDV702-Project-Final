using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignB_Server_CLI
{
    public class StoreController : System.Web.Http.ApiController
    {
        #region Brand and Items Methods
        /// <summary>
        /// Get all brand names from the database
        /// </summary>
        /// <returns>A list of brand names</returns>
        public List<String> GetBrandList()
        {
            DataTable lcResult = clsDbConnection.GetDataTable("SELECT brnd_name FROM tbl_brands", null);
            List<String> lcBrands = new List<String>();
            foreach (DataRow dr in lcResult.Rows)
                lcBrands.Add((string)dr[0]);
            return lcBrands;
        }

        /// <summary>
        /// Get all data about the brand from the database
        /// </summary>
        /// <param name="prName">Name of brand being searched</param>
        /// <returns>Brand details and items</returns>
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
        /// Get all items connected with the brand
        /// </summary>
        /// <param name="prBrandName">Name of brand being searched</param>
        /// <returns>A list of items</returns>
        private List<clsAllItems> getBrandItems(string prBrandName)
        {
            if (prBrandName == "All")
            {
                DataTable lcResult = clsDbConnection.GetDataTable("SELECT * FROM tbl_items WHERE item_quantity >0", null);
                List<clsAllItems> lcItems = new List<clsAllItems>();
                foreach (DataRow dr in lcResult.Rows)
                    lcItems.Add(dataRow2AllItems(dr));
                return lcItems;
            }
            else
            {
                Dictionary<string, object> par = new Dictionary<string, object>(1);
                par.Add("NAME", prBrandName); DataTable lcResult = clsDbConnection.GetDataTable("SELECT * FROM tbl_items WHERE item_brand = @NAME AND item_quantity >0", par);
                List<clsAllItems> lcItems = new List<clsAllItems>();
                foreach (DataRow dr in lcResult.Rows)
                    lcItems.Add(dataRow2AllItems(dr));
                return lcItems;
            }
        }

        /// <summary>
        /// Converting SQL Data rows into C# class data
        /// </summary>
        /// <param name="dr">the datarow</param>
        /// <returns>Converted C# item</returns>
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
                Quantity = Convert.ToInt32(dr["item_quantity"]),
                TimeStamp = Convert.ToDateTime(dr["item_timestamp"]),
                Image64 = Convert.ToString(dr["item_image"]),
                Type = Convert.ToChar(dr["item_type"]),

                Length = dr["item_length"] is DBNull ? (int?)null : Convert.ToInt32(dr["item_length"]),

                Diameter = dr["item_diameter"] is DBNull ? (float?)null : Convert.ToSingle(dr["item_diameter"]),

                RingSize = Convert.ToString(dr["item_size"])
            };
        }
        #endregion


        #region Order Methods
        /// <summary>
        /// Insert the Order into the database
        /// </summary>
        /// <param name="prOrder">Order being inserted</param>
        /// <returns>Return the row count</returns>
        public int PostOrder(clsOrder prOrder)
        {

                int lcRecCount = clsDbConnection.Execute("INSERT INTO tbl_orders" +
                    "(ordr_email, ordr_address, ordr_item, ordr_dateordered, ordr_quantity, ordr_totalprice, ordr_status)" +
                    "VALUES (@EMAIL, @ADDRESS, @ITEMID, @DATEORDERED, @QUANTITY, @TOTALPRICE, @STATUS);",
                    prepareOrderParameter(prOrder));
                return lcRecCount;

        }

        /// <summary>
        /// Convert the C# class into SQL
        /// </summary>
        /// <param name="prOrder"></param>
        /// <returns></returns>
        private Dictionary<string, object> prepareOrderParameter(clsOrder prOrder) {
            Dictionary<string, object> lcPar = new Dictionary<string, object>();
            lcPar.Add("EMAIL", prOrder.Email);
            lcPar.Add("ADDRESS", prOrder.Address);
            lcPar.Add("ITEMID", prOrder.Item.Id);
            lcPar.Add("QUANTITY", prOrder.Quantity);
            lcPar.Add("TOTALPRICE", prOrder.TotalPrice);
            lcPar.Add("STATUS", prOrder.Status);
            lcPar.Add("DATEORDERED", prOrder.DateOrdered);
            return lcPar;

        }

        /// <summary>
        ///Checks to see if there is the correct amount of QTY left by checking if the timestamp has been updated.
        /// </summary>
        /// <param name="prItem">item they want to buy with updated quantity</param>
        /// <returns>true if the quantity was updated, false if not.</returns>
        public int PutItemQTY(clsAllItems prItem)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(3);
            par.Add("QUANTITY", prItem.Quantity);
            par.Add("ID", prItem.Id);
            par.Add("TIMESTAMP", prItem.TimeStamp);
            int lcRecCount = clsDbConnection.Execute(
                "UPDATE tbl_items SET item_quantity = @QUANTITY where item_id = @ID and item_timestamp = @TIMESTAMP",
                par);
            return lcRecCount;
        }
        #endregion

    }

}