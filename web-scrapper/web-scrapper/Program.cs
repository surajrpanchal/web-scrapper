using ConsoleTables;
using HtmlAgilityPack;
using System.Collections.Generic;
using static System.Console;

namespace web_scrapper
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Enter Url:");
            string url = ReadLine();

            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(url);
            var node = htmlDoc.DocumentNode.SelectSingleNode("/html/body/div/div/div[3]/div[1]/div[2]");
            
            List<Mobile> mobiles = new List<Mobile>();
            int i = 0;
            foreach (var item in node.ChildNodes)
            {
                ++i;
                var modelName = item.SelectSingleNode($"/html/body/div/div/div[3]/div[1]/div[2]/div[{i}]/div/div/div/a/div[2]/div[1]/div[1]");
                var modelPrice = item.SelectSingleNode($"/html/body/div/div/div[3]/div[1]/div[2]/div[{i}]/div/div/div/a/div[2]/div[2]/div[1]/div/div[1]");
                if (modelName != null && modelPrice != null)
                {
                    mobiles.Add(new Mobile(modelName?.InnerText, modelPrice?.InnerText.Remove(0, 1)));
                }
            }
            ConsoleTable
                .From(mobiles)
                .Configure(o => o.NumberAlignment = Alignment.Right)
                .Write(Format.Alternative);
            ReadKey();
        }

        
    }
    public class Mobile
    {
        public Mobile(string name, string price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }

        public string Price { get; set; }
    }
}
