using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TheBall.CORE.InstanceSupport;
using TheBall.CORE.Storage;

namespace TheBall.Payments
{
    public class StripeSupport
    {
        public static string GetStripeApiKey(bool isTestMode)
        {
            return isTestMode
                ? SecureConfig.Current.StripeTestSecretKey
                : SecureConfig.Current.StripeLiveSecretKey;
        }

        private const string ProductAPIUrl = "https://api.stripe.com/v1/products";
        public static async Task<ProductJson.RootObject> GetProducts(bool isTestMode)
        {
            var userName = GetStripeApiKey(isTestMode);
            string passWord = "";
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userName}:{passWord}")));
                var response = await httpClient.GetAsync(ProductAPIUrl);
                HttpContent responseContent = response.Content;
                var data = await responseContent.ReadAsByteArrayAsync();
                var productJson = JSONSupport.GetObjectFromData<ProductJson.RootObject>(data);
                return productJson;
            }
        }


        public class ProductJson
        {
            public class Attributes
            {
                public string Type { get; set; }
                public string Rank { get; set; }
                public string Variants { get; set; }
            }

            public class Inventory
            {
                public ExpandoObject quantity { get; set; }
                public string type { get; set; }
                public ExpandoObject value { get; set; }
            }

            public class Sku
            {
                public string id { get; set; }
                public bool active { get; set; }
                public Attributes attributes { get; set; }
                public int created { get; set; }
                public string currency { get; set; }
                public object image { get; set; }
                public Inventory inventory { get; set; }
                public bool livemode { get; set; }
                public ExpandoObject metadata { get; set; }
                public object package_dimensions { get; set; }
                public int price { get; set; }
                public string product { get; set; }
                public int updated { get; set; }
            }

            public class Skus
            {
                public List<Sku> data { get; set; }
                public bool has_more { get; set; }
                public int total_count { get; set; }
                public string url { get; set; }
            }

            public class Product
            {
                public string id { get; set; }
                public bool active { get; set; }
                public List<string> attributes { get; set; }
                public string caption { get; set; }
                public int created { get; set; }
                public List<ExpandoObject> deactivate_on { get; set; }
                public object description { get; set; }
                public List<ExpandoObject> images { get; set; }
                public bool livemode { get; set; }
                public ExpandoObject metadata { get; set; }
                public string name { get; set; }
                public ExpandoObject package_dimensions { get; set; }
                public bool shippable { get; set; }
                public Skus skus { get; set; }
                public int updated { get; set; }
                public string url { get; set; }
            }

            public class RootObject
            {
                public List<Product> data { get; set; }
                public bool has_more { get; set; }
                public string url { get; set; }
            }


        }

        public static string GetStripeCurrency(string currency)
        {
            if (currency.ToLower() == "usd")
                return "usd";
            return "eur";
        }
    }
}