using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ConsoleApplication
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("quotes.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set;}

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<List<Quotation>>(Configuration.GetSection("Quotes"));
        }

        public void Configure(IApplicationBuilder app, 
            IOptions<List<Quotation>> quoteOptions)
        {
            app.UseStaticFiles();

            app.Use(async (context, next) => 
            {   
                context.Response.ContentType="text/html";
                await next();
            });

            var quotes = quoteOptions.Value;
            if(quotes != null) 
            {
                QuotationStore.Quotations = quotes;
            }

            app.Map("/quote", builder => builder.Run(async context =>
            {
                var id = int.Parse(context.Request.Path.ToString().Split('/')[1]);
                var quote = QuotationStore.Quotations[id];
                await context.Response.WriteAsync(quote.ToString());
            }));

            app.Map("/all", builder => builder.Run(async context =>
            {
                foreach(var quote in QuotationStore.Quotations)
                    {
                        await context.Response.WriteAsync("<div>");
                        await context.Response.WriteAsync(quote.ToString());
                        await context.Response.WriteAsync("</div>");
                    }
            }));

            app.Run(context =>
            {
                return context.Response.WriteAsync(QuotationStore.RandomQuotation().ToString());
            });
        }
    }
}