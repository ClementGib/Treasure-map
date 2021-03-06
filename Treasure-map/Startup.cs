//using Newtonsoft.Json;
using BAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;


namespace Treasure_map
{



    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization();
            services.AddControllers();

            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("index.html");

            app.UseHttpsRedirection();

            app.UseDefaultFiles(options);
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                }
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapPost("/text", async context =>
                {
                    //clear the map
                    Map.clear();

                    var bodyStr = "";
                    var req = context.Request;

                    using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
                    {
                        bodyStr = reader.ReadToEnd();
                    }

                    try
                    {
                        Program.CreateMap(Program.GetInstructionFromText(bodyStr));
                        string json = JsonConvert.SerializeObject(Program.TheMap, Formatting.Indented);
                        await context.Response.WriteAsync(json);
                    }
                    catch (Exception exception)
                    {
                        string json = JsonConvert.SerializeObject(exception, Formatting.Indented);
                        await context.Response.WriteAsync(json);
                    }

                });

                endpoints.MapPost("/file", async context =>
                {
                    //clear the map
                    Map.clear();

                    try
                    {

                        string root = "~/wwwroot/content/txt";
                        var provider = new MultipartFormDataStreamProvider(root);



                    }
                    catch (System.Exception e)
                    {
                    }

                    var bodyStr = "";
                    var req = context.Request;

                    using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
                    {
                        bodyStr = reader.ReadToEnd();
                    }

                    try
                    {
                        Program.CreateMap(Program.GetInstructionFromFile(bodyStr));
                        string json = JsonConvert.SerializeObject(Program.TheMap, Formatting.Indented);
                        await context.Response.WriteAsync(json);
                    }
                    catch (Exception exception)
                    {
                        string json = JsonConvert.SerializeObject(exception, Formatting.Indented);
                        await context.Response.WriteAsync(json);
                    }

                });



                endpoints.MapPost("/move", async context =>
                {

                    var bodyStr = "";
                    var req = context.Request;

                    using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
                    {
                        bodyStr = reader.ReadToEnd();
                    }

                    try
                    {
                        Program.TheMap.AdventurerMoveStepByStep();

                        string json = JsonConvert.SerializeObject(Program.TheMap.Adventurer, Formatting.Indented);
                        await context.Response.WriteAsync(json);
                    }
                    catch (Exception exception)
                    {
                        string json = JsonConvert.SerializeObject(exception, Formatting.Indented);
                        await context.Response.WriteAsync(json);
                    }

                });

                endpoints.MapPost("/treasure", async context =>
                {

                    var bodyStr = "";
                    var req = context.Request;

                    using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
                    {
                        bodyStr = reader.ReadToEnd();
                    }

                    try
                    {
                        //clean x
                        string x = bodyStr.Split(" - ")[0];
                        string temp = "";
                        for (int index_x = 0; index_x < x.Length; index_x++)
                        {
                            if (Char.IsDigit(x[index_x]))
                                temp += x[index_x];
                        }
                        byte P_x = byte.Parse(temp);

                        temp = "";
                        //clean x
                        string y = bodyStr.Split(" - ")[1];
                        for (int index_y = 0; index_y < y.Length; index_y++)
                        {
                            if (Char.IsDigit(y[index_y]))
                                temp += y[index_y];
                        }
                        byte P_y = byte.Parse(temp);


                        //get a treasure from the box
                        //Program.TheMap.getTreasure(P_x, P_y);
                        //transform null treasure to Plain
                        Program.TheMap.updateMapGrid();

                        string json = JsonConvert.SerializeObject(Program.TheMap, Formatting.Indented);
                        await context.Response.WriteAsync(json);
                    }
                    catch (Exception exception)
                    {
                        string json = JsonConvert.SerializeObject(exception, Formatting.Indented);
                        await context.Response.WriteAsync(json);
                    }

                });


            });




        }
    }
}
