using Luxor.Parser;

class Program
{
    static void Main()
    {        
        try
        {        
            StreamReader sr = new StreamReader(new FileStream("../Luxor.Tests/Files/index.html", FileMode.Open));
            Parser parser = new Parser(sr);
            
            sr.Close();
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
        }
    }
}