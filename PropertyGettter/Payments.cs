using System.IO;
using Sage.Cloud.Domain.Payments.Interfaces.Models;

namespace PropertyGettter
{
    public class Payments: Base
    {
        public static void Do()
        {
            var list = new[]
            {
               new ClassInfo("Payments"), 
                new ClassInfo("PaymentInvoice")                                         
            };

            using (var file = new StreamWriter(@"PaymentsApi.txt"))
            {
                DoApi(list, file);
            }

            var classList = new[]
            {
                //Invoice
                typeof (Payment),
                typeof (InvoicePayment)                           
            };

            using (var file = new StreamWriter(@"Payment.txt"))
            {
                DoEntities(file, classList);
            }
        }
    }
}
