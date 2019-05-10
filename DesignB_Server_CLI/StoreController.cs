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
        public List<String> GetBrandList()
        {
            DataTable lcResult = clsDbConnection.GetDataTable("SELECT brnd_name FROM tbl_brands", null);
            List<String> lcBrands = new List<String>();
            foreach (DataRow dr in lcResult.Rows)
               lcBrands.Add((string)dr[0]);
            return lcBrands;
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


        #region Get items from database, into a list
        private List<clsAllItems> getBrandItems(string prBrandName)
        {
            Dictionary<string, object> par = new Dictionary<string, object>(1);
            par.Add("NAME", prBrandName); DataTable lcResult = clsDbConnection.GetDataTable("SELECT * FROM tbl_items WHERE item_brand = @NAME", par);
            List<clsAllItems> lcItems = new List<clsAllItems>();
            foreach (DataRow dr in lcResult.Rows)
                lcItems.Add(dataRow2AllItems(dr, prBrandName));
            return lcItems;
        }

        private clsAllItems dataRow2AllItems(DataRow dr, string prBrandName)
        {
            return new clsAllItems()
            {
                Id = Convert.ToInt32(dr["item_id"]),
                Name = Convert.ToString(dr["item_name"]),
                Brand = prBrandName,
                Material = Convert.ToString(dr["item_material"]),
                Description = Convert.ToString(dr["item_description"]),
                Price = Convert.ToSingle(dr["item_price"]),
                Quantity = Convert.ToInt32(dr["item_quantity"]),
                TimeStamp = Convert.ToDateTime(dr["item_timestamp"]),
                Image64 = Convert.ToString(dr["item_image"]),
                Type = Convert.ToString(dr["item_type"]),

                Length = dr["item_length"] is DBNull ? (int?)null : Convert.ToInt32(dr["item_length"]),

                Diameter = dr["item_diameter"] is DBNull ? (float?)null : Convert.ToSingle(dr["item_diameter"]),

                RingSize = Convert.ToString(dr["item_size"])
            };
        }
        #endregion

        #region Post Order into database
        public string PostOrder(clsOrder prOrder)
        {
            try
            {
                int lcRecCount = clsDbConnection.Execute("INSERT INTO tbl_order" +
                    "(ordr_email, ordr_address, ordr_item, ordr_dateorderd, ordr_quantity, ordr_totalprice, ordr_status" +
                    "VALUES (@EMAIL, @ADDRESS, @ITEMID, @DATEORDERED, @QUANTITY, @TOTALPRICE, @STATUS",
                    prepareOrderParameter(prOrder));
                if (lcRecCount == 1)
                    return "Order has been sent";
                else
                    return "Unexpected order count: " + lcRecCount;
            } catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }
        }
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
        #endregion

        #region Update Item QTY
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