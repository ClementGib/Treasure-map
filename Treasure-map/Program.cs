using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using DAL;
using BAL;


namespace Treasure_map
{
    public static class Program
    {

        public static Map TheMap;

        public static Instruction GetInstructionFromFile(string P_nameFile)
        {
            string path = Directory.GetCurrentDirectory() + "\\wwwroot\\content\\txt\\";
            IOManager fileManager = IOManager.GetInstance;
            fileManager.readInstructionsFile(path + P_nameFile);

            return fileManager.InstructionFromInput;
        }

        public static Instruction GetInstructionFromText(string P_textContent)
        {
            IOManager fileManager = IOManager.GetInstance;
            fileManager.readInstructionsText(P_textContent);

            return fileManager.InstructionFromInput;
        }

        public static void CreateMap(Instruction P_instruction)
        {
            TheMap = Map.GetInstance(P_instruction);
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }





        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
