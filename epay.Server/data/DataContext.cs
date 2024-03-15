
using epay.Server.Models;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace epay.Server.data
{
    public static class DataContext
    {
        
        public static async Task<List<Customer>> ReadData()
        {
            List<Customer> customers = new List<Customer>();
            using (StreamReader r = new StreamReader(@"data/Customers.json"))
            {
                string json = await r.ReadToEndAsync();
                customers = JsonConvert.DeserializeObject<List<Customer>>(json);
            }

            return customers;
        }


        public static async void WriteData(List<Customer> customers)
        {
            List<Customer> oldCustmerlst = new List<Customer>();
            using (StreamReader r = new StreamReader(@"data/Customers.json"))
            {
                string json = await r.ReadToEndAsync();
                oldCustmerlst = JsonConvert.DeserializeObject<List<Customer>>(json);
            }

            oldCustmerlst.AddRange(customers);
            var newCustomerlst =oldCustmerlst.OrderBy(x => x.lastName).ThenBy(x => x.firstName).ToList();
            string json2 = JsonConvert.SerializeObject(newCustomerlst);
            File.WriteAllText(@"data/Customers.json", json2);
           
        }

        public static async Task<bool> CheckDuplicateID(int id)
        {
            var customerIds= ReadData().Result.Select(x=>x.id).ToList();
            return customerIds.Any(x => x == id);

        }
    }
}
