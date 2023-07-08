using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TestWebServices
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //http://api.bigon.az/api/brands
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://api.bigon.az");

            await Console.Out.WriteLineAsync("Enter name:");
            string read = Console.ReadLine();

            var appData = new Brand();
            appData.Name = read;

            var appDataJson = JsonConvert.SerializeObject(appData);

            StringContent sc = new StringContent(appDataJson, Encoding.UTF8, "application/json");

            await client.PostAsync("api/brands", sc);

            var response = await client.GetAsync("/api/brands");

            string responseText = await response.Content.ReadAsStringAsync();

            var brands = JsonConvert.DeserializeObject<List<Brand>>(responseText);

            foreach (var brand in brands)
            {
                Console.WriteLine($"{brand.Id}-{brand.Name}-{brand.CreatedDate}");
            }
        }
    }

    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
