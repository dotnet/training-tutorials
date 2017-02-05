using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace ConsoleApplication
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("quotes.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set;}

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<List<Quotation>>(Configuration.GetSection("Quotes"));
            services.AddSingleton<IQuotationStore,QuotationStore>();
        }

        public void Configure(IApplicationBuilder app, 
            IQuotationStore quotationStore,
            ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            //app.UseStatusCodePages("text/plain","HTTP Status Code: {0}");
            app.UseStatusCodePagesWithRedirects("~/{0}.html");
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();

            app.Map("/quote", builder => builder.Run(async context =>
            {
                var id = int.Parse(context.Request.Path.ToString().Split('/')[1]);
                var quote = quotationStore.List().ToList()[id];
                await context.Response.WriteAsync(quote.ToString());
            }));

            app.Map("/all", builder => builder.Run(async context =>
            {
                foreach(var quote in quotationStore.List())
                    {
                        await context.Response.WriteAsync("<div>");
                        await context.Response.WriteAsync(quote.ToString());
                        await context.Response.WriteAsync("</div>");
                    }
            }));

            app.Map("/random", builder => builder.Run(async context =>
            {
                await context.Response.WriteAsync(quotationStore.RandomQuotation().ToString());
            }));
        }
    }
}