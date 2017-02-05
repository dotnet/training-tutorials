using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public static class QuotationStore : IQuotationStore
    {
        private static List<Quotation> _quotations {get; set;}

        public QuotationStore(IOptions<List<Quotation>> quoteOptions)
        {
            _quotations = quoteOptions.Value;
            if(_quotations == null)
            {
                // if nothing configured, use default in code
                _quotations = DefaultQuotations();
            }
        }

        private List<Quotation> DefaultQuotations()
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

        public IEnumerable<Quotation> List()
        {
            return _quotations.AsEnumerable();
        }

        public Quotation RandomQuotation()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            return _quotations[rnd.Next(0,Quotations.Count)];
        }
    }
}