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
        /// <summary>
        /// Gets a list of all the Brands names
        /// </summary>
        /// <returns>List of all the brand names</returns>
        public List<String> GetBrandList()
        {
            DataTable lcResult = clsDbConnection.GetDataTable("SELECT brnd_name FROM tbl_brands", null);
            List<string> lcNames = new List<string>();
            foreach (DataRow dr in lcResult.Rows)
                lcNames.Add((string)dr[0]);
            return lcNames;
        }

        public List<String> GetProductsList()
        {
            DataTable lcResult = clsDbConnection.GetDataTable("SELECT item_name,item_type,item_price FROM tbl_items", null);
            List<string> lcNames = new List<string>();
            foreach (DataRow dr in lcResult.Rows)
                lcNames.Add((string)dr[0] +"\t"+ dr[1] +"\t"+ dr[2]);
            return lcNames;
        }

        public PostOrder(clsOrder prOrder)
        {

        }

        private Dictionary<string,object> prepareOrderParameter(clsOrder prOrder) {
            Dictionary<string, object> lcPar = new Dictionary<string, object>;
            lcPar.Add("",prOrder.Id)
            return lcPar;

        }
    }
}
