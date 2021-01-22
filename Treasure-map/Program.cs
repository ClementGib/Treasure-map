using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using IO;

namespace Treasure_map
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //string currentPath = Directory.GetCurrentDirectory();
            //Console.WriteLine(currentPath);
            //IOManager theManager = IOManager.GetInstance;
            //string path = "C:\\Users\\Cleme\\source\\repos\\Treasure-map\\Treasure-map\\IO\\input.txt";

            //theManager.readFile(path);

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
