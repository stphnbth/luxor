using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using Reference;

class Program
{
    static void Main()
    {        
        
        
        try
        {
            StreamReader sr = new StreamReader(new FileStream("eof_test.html", FileMode.Open));
            Luxor.Tokenizer tokenizer = new Luxor.Tokenizer(sr);

            tokenizer.run();
            
            sr.Close();
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
        }
    }
}