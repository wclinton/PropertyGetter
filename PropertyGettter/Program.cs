
namespace PropertyGettter
{
    public class Program
    {


        private static void Main()
        {         
            var sbp = new Sbp();
            Sbp.Do();

            var invoice = new Invoice();
            invoice.Do();

            var statement = new Statement();
            statement.Do();
        }          
    }
}