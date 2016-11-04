using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace ConsoleApplication
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) => 
            {   
                context.Response.ContentType="text/html";
                await next();
            });

            // next steps solution
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