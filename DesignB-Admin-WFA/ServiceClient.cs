using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DesignB_Admin_WFA
{
    class ServiceClient
    {

        #region Brand Methods
        /// <summary>
        /// Get brand names from server
        /// </summary>
        /// <param name="prExBrand">Apart from this brand name</param>
        /// <returns>List of Brand names</returns>
        internal async static Task<List<string>> GetBrandNamesAsync(string prExBrand)
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<List<string>>
                    (await lcHttpClient.GetStringAsync("http://localhost:60064/api/admin/GetBrandList?prExBrand=" + prExBrand));
        }

        /// <summary>
        /// Get brand details / data from server
        /// </summary>
        /// <param name="prBrandName">Name of brand</param>
        /// <returns>class brand with all details / data, including a list of items</returns>
        internal async static Task<clsBrand> GetBrandAsync(string prBrandName)
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<clsBrand>
                    (await lcHttpClient.GetStringAsync("http://localhost:60064/api/admin/GetBrand?prName=" + prBrandName));
        }
        #endregion


        #region Item Methods
        /// <summary>
        /// Send a new item to be saved into the server
        /// </summary>
        /// <param name="prItem">the item being saved</param>
        /// <returns>a string of if the server completed the task or a exeption message</returns>
        internal async static Task<string> InsertItemAsync(clsAllItems prItem)
        {
            return await InsertOrUpdateAsync(prItem, "Http://localhost:60064/api/admin/PostItem", "POST");
        }

        /// <summary>
        /// Update a existing item in the server
        /// </summary>
        /// <param name="prItem">Updated version of the item</param>
        /// <returns>a string of if the server completed the task or a exeption message</returns>
        internal async static Task<string> UpdateItemAsync(clsAllItems prItem)
        {
            return await InsertOrUpdateAsync(prItem, "Http://localhost:60064/api/admin/PutItem", "PUT");

        }

        /// <summary>
        /// Sends a request to delete the item from the server
        /// </summary>
        /// <param name="prItem">The item being deleted</param>
        /// <returns>a string of if the server completed the task or a exeption message</returns>
        internal async static Task<string> DeleteItemAsync(clsAllItems prItem)
        {
            using (HttpClient lcHttpClient = new HttpClient())
            {
                HttpResponseMessage lcRespMessage = await lcHttpClient.DeleteAsync
                ($"http://localhost:60064/api/admin/DeleteItem?prItemID={prItem.Id}&prItemStamp={prItem.TimeStamp}");
                return await lcRespMessage.Content.ReadAsStringAsync();
            }
        }
        #endregion


        #region Order Methods
        /// <summary>
        /// Get a list of all orders from the server
        /// </summary>
        /// <returns>returns a list of all orders</returns>
        internal async static Task<List<clsOrder>> GetOrderListAsync()
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<List<clsOrder>>
                    (await lcHttpClient.GetStringAsync("http://localhost:60064/api/admin/GetOrderList"));
        }

        /// <summary>
        /// update a existing order from the server
        /// </summary>
        /// <param name="prOrder">updated version of the order</param>
        /// <returns>a string of if the server completed the task or a exeption message</returns>
        internal async static Task<string> UpdateOrderAsync(clsOrder prOrder)
        {
            return await InsertOrUpdateAsync(prOrder, "Http://localhost:60064/api/admin/PutOrder", "PUT");
        }

        /// <summary>
        /// Delete the order from the server
        /// </summary>
        /// <param name="prOrder">order being deleted</param>
        /// <returns>a string of if the server completed the task or a exeption message</returns>
        internal async static Task<string> DeleteOrderAsync(clsOrder prOrder)
        {
            return await InsertOrUpdateAsync(prOrder, "Http://localhost:60064/api/admin/DeleteOrder", "DELETE");

        }
        #endregion

        /// <summary>
        /// convert the object into Json, so multiple data can be sent to the server
        /// </summary>
        /// <typeparam name="TItem">object being sent</typeparam>
        /// <param name="prObject"> Object being sent</param>
        /// <param name="prUrl">where the object is being sent to</param>
        /// <param name="prRequest">the type of  RESTful request the object is.</param>
        /// <returns></returns>
        private async static Task<string> InsertOrUpdateAsync<TItem>(TItem prObject, string prUrl, string prRequest)
        {
            using (HttpRequestMessage lcReqMessage = new HttpRequestMessage(new HttpMethod(prRequest), prUrl))
            using (lcReqMessage.Content =
                new StringContent(JsonConvert.SerializeObject(prObject), Encoding.Default, "application/json"))
            using (HttpClient lcHttpClient = new HttpClient())
            {
                HttpResponseMessage lcRespMessage = await lcHttpClient.SendAsync(lcReqMessage);
                return await lcRespMessage.Content.ReadAsStringAsync();
            }
        }
    }
}
