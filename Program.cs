using System;
using System.Threading.Tasks;

namespace WebPageDependencyParser
{
    class Program
    {
        static void Main(string[] args)
        {
            StartWork().Wait();
        }

        public static async Task StartWork(){
            Parser parser = new Parser("");
            string page = await parser.LoadPageAsync("https://google.com.ua");
            System.Console.WriteLine(page);
        }
    }
}
