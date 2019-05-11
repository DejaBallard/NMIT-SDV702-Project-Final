using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DesignB_Store_UWP
{
    public static class ServiceClient
    {


        internal async static Task<List<string>> GetBrandNamesAsync()
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<List<string>>
                    (await lcHttpClient.GetStringAsync("http://localhost:60064/api/store/GetBrandList/"));
        }

        internal async static Task<clsBrand> GetBrandAsync(string prBrandName)
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<clsBrand>
                    (await lcHttpClient.GetStringAsync("http://localhost:60064/api/store/GetBrand?prName=" + prBrandName));
        }




        private async static Task<int> InsertOrUpdateAsync<TItem>(TItem prItem, string prUrl, string prRequest)
        {
            using (HttpRequestMessage lcReqMessage = new HttpRequestMessage(new HttpMethod(prRequest), prUrl))
            using (lcReqMessage.Content = new StringContent(JsonConvert.SerializeObject(prItem), Encoding.UTF8, "application/json"))
            using (HttpClient lcHttpClient = new HttpClient())
            {
                HttpResponseMessage lcRespMessage = await lcHttpClient.SendAsync(lcReqMessage);
                return Convert.ToInt32(await lcRespMessage.Content.ReadAsStringAsync());
            }
        }
        public async static Task<int> InsertOrderAsync(clsOrder prOrder)
        {
            return await InsertOrUpdateAsync(prOrder, "http://localhost:60064/api/store/PostOrder", "POST");
        }
        internal async static Task<int> UpdateItemAsync(clsAllItems item)
        {
           return await InsertOrUpdateAsync(item, "http://localhost:60064/api/store/PutItemQTY", "PUT");
        }
    }
}

