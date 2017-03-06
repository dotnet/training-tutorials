using System.Collections.Generic;

public class Reader
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public List<CheckoutRecord> CheckoutRecords { get; set; }
}