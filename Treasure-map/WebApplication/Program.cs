using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using DAL;
using BAL;


namespace Treasure_map
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string currentPath = Directory.GetCurrentDirectory();
            currentPath += "\\src\\IO\\";

            IOManager fileManager = IOManager.GetInstance;
            fileManager.readFile(currentPath + "input.txt");


            Map TheMap = Map.GetInstance(fileManager.InstructionFromInput);


            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
