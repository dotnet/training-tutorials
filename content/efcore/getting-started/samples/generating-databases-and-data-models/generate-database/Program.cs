using System;
using System.Linq;
using REPLHelper;

public class Program
{
    public static void Main()
    {
        using (var context = new LibraryContext())
        {
            DBHelper.PrintDatabaseSchema(context);
        }
    }
}