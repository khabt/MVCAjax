using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Asynchronous
{
    class Program
    {
        static void Main(string[] args)
        {
            Manipulate mani = new Manipulate();
            Task<int> result = mani.AccessTheWebAsync();
            Console.WriteLine(result.Result);
            //mani.DoIndependenceWork();

            Console.ReadKey();
        }
    }

    class Manipulate
    {
        public async Task<int> AccessTheWebAsync()
        {
            HttpClient httpClient = new HttpClient();
            Task<string> getStringTask = httpClient.GetStringAsync("https://coccoc.com");
            DoIndependenceWork();
            string content = await getStringTask;

            return content.Length;
        }

        private void DoIndependenceWork()
        {
            Console.WriteLine("Continue working...");
        }
    }
}
