using System.IO;
using Sage.Cloud.Domain.Invoices.Interfaces.Models;

namespace PropertyGettter
{
    class Invoice : Base
    {

        public void Do()
        {
            var list = new[]
            {
                new ClassInfo("Invoice",NonModifyable()), 
                new ClassInfo("Invoice('{Id}')/InvoiceDetail",NonModifyable()), 
                new ClassInfo("PurchaseHistoryByCustomer",NonModifyable()),                 
                new ClassInfo("PurchaseHistoryItem",NonModifyable()), 
                new ClassInfo("PurchaseHistoryQuantity",NonModifyable()),                                              
            };

            using (var file = new StreamWriter(@"InvoiceApi.txt"))
            {
                DoApi(list, file);
            }

            var classList = new[]
            {
                //Invoice
                typeof (Sage.Cloud.Domain.Invoices.Interfaces.Models.Invoice),
                typeof (InvoiceDetail),
                
                //PurchaseHistory
                typeof (PurchaseHistoryByCustomer), typeof (PurchaseHistoryItem),
                typeof (PurchaseHistoryQuantity),            
            };

            using (var file = new StreamWriter(@"Invoice.txt"))
            {
                DoEntities(file, classList);
            }
        }


       
    }
}
