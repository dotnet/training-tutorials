public class Address  
{ 
    public int Id { get; set; } 
    public string City { get; set; } 
    public string State { get; set; } 
 
    public int ReaderId { get; set; } 
    public Reader Reader { get; set; } 
}