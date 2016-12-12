using System.Collections.Generic;

public class Publisher
{
    public int PublisherId { get; set; }
    public string Name { get; set; }
    public List<Edition> Editions { get; set; }
}