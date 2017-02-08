using System.Collections.Generic;

namespace ConsoleApplication
{
    public interface IQuotationStore
    {
        IEnumerable<Quotation> List();
        Quotation RandomQuotation();
    }
}