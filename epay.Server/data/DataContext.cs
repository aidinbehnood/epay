
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
            var lastnameSort = BubblesortBy(x => x.lastName, oldCustmerlst);
            var finalSortlst=BubblesortBy(x => x.firstName, lastnameSort);
            string json2 = JsonConvert.SerializeObject(finalSortlst);
            File.WriteAllText(@"data/Customers.json", json2);

        }

        public static async Task<bool> CheckDuplicateID(int id)
        {
            var customerIds = ReadData().Result.Select(x => x.id).ToList();
            return customerIds.Any(x => x == id);

        }


        public static List<TSource> BubblesortBy<TSource, TKey>(Func<TSource, TKey> keySelector,
                                        List<TSource> customers)
        {
            int loopCount = 0;
            bool doBreak = true;

            for (int i = 0; i < customers.Count; i++)
            {
                doBreak = true;
                for (int j = 0; j < customers.Count - 1; j++)
                {
                    if (Compare(keySelector(customers[j]), keySelector(customers[j + 1])))
                    {
                        TSource temp = customers[j + 1];
                        customers[j + 1] = customers[j];
                        customers[j] = temp;
                        doBreak = false;
                    }
                    loopCount++;
                }
                if (doBreak) { break;  }
            }

            return customers;
        }
        private static bool Compare<T>(T l, T r)
        {
            return Comparer<T>.Default.Compare(l, r) > 0;
        }
    }
}
