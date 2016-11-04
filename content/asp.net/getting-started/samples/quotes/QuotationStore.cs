using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public static class QuotationStore
    {
        public static List<Quotation> Quotations {get; private set;}

        static QuotationStore()
        {
            Quotations = new List<Quotation>()
            {
                new Quotation() { 
                    Quote="Measuring programming progress by lines of code is like measuring aircraft building progress by weight.", 
                    Author="Bill Gates"},
                new Quotation() { 
                    Quote="Be kind whenever possible. It is always possible.", 
                    Author="Dalai Lama"},
                new Quotation() { 
                    Quote="Before software can be reusable it first has to be usable.", 
                    Author="Ralph Johnson"}
            };
        }

        public static Quotation RandomQuotation()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            return Quotations[rnd.Next(0,Quotations.Count)];
        }
    }
}