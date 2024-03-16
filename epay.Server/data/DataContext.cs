
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
            BubblesortBy(x => x.lastName, oldCustmerlst);
            BubblesortBy(x => x.firstName, oldCustmerlst);
            //var newCustomerlst = oldCustmerlst.OrderBy(x => x.lastName).ThenBy(x => x.firstName).ToList();
            string json2 = JsonConvert.SerializeObject(oldCustmerlst);
            File.WriteAllText(@"data/Customers.json", json2);

        }

        public static async Task<bool> CheckDuplicateID(int id)
        {
            var customerIds = ReadData().Result.Select(x => x.id).ToList();
            return customerIds.Any(x => x == id);

        }


        public static void BubblesortBy<TSource, TKey>(Func<TSource, TKey> keySelector,
                                        List<TSource> stocks)
        {
            int loopCount = 0;
            bool doBreak = true;

            for (int i = 0; i < stocks.Count; i++)
            {
                doBreak = true;
                for (int j = 0; j < stocks.Count - 1; j++)
                {
                    if (Compare(keySelector(stocks[j]), keySelector(stocks[j + 1])))
                    {
                        TSource temp = stocks[j + 1];
                        stocks[j + 1] = stocks[j];
                        stocks[j] = temp;
                        doBreak = false;
                    }
                    loopCount++;
                }
                if (doBreak) { break;  }
            }
        }
        private static bool Compare<T>(T l, T r)
        {
            return Comparer<T>.Default.Compare(l, r) > 0;
        }
    }
}
