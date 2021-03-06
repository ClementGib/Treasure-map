using BAL;
using DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;


namespace Treasure_map
{
    public static class Program
    {

        public static Map TheMap;

        public static Instruction GetInstructionFromFile(string P_nameFile)
        {
            string path = Directory.GetCurrentDirectory() + "\\wwwroot\\content\\txt\\";
            IOManager fileManager = new IOManager();
            fileManager.readInstructionsFile(path + P_nameFile);

            return fileManager.InstructionFromInput;
        }

        public static Instruction GetInstructionFromText(string P_textContent)
        {
            IOManager fileManager = new IOManager();
            fileManager.readInstructionsText(P_textContent);

            return fileManager.InstructionFromInput;
        }

        public static void CreateMap(Instruction P_instruction)
        {
            TheMap = new Map(P_instruction);
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
