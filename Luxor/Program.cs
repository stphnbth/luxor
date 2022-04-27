using Luxor.Parser;

class Program
{
    static void Main()
    {        
        try
        {        
            StreamReader sr = new StreamReader(new FileStream("test_files/index.html", FileMode.Open));
            Parser parser = new Parser(sr);

            parser.run();
            
            sr.Close();
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
        }
    }
}