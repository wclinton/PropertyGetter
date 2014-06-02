
using System.IO;
using Sage.Cloud.Domain.AppSbp.Interfaces.Models;

namespace PropertyGettter
{
    public class Sbp : Base
    {
        public static void Do()
        {

            var list = new[]
            {
                new ClassInfo("ClickToPayInvoice",NoDelete()), 
                new ClassInfo("ClickToPayInvoicePayment",NoDelete()), 
                new ClassInfo("ClickToPayPayment",NoDelete()),                 
                new ClassInfo("ClickToPayStatement",NoDelete()), 
                new ClassInfo("ClickToPayStatementPayment",NoDelete()), 


                new ClassInfo("CustomDocumentTemplate"), 
                new ClassInfo("DocumentTemplate"), 
                new ClassInfo("UnprocessedInvoice"), 
                new ClassInfo("UnprocessedStatement"), 
                new ClassInfo("CustomerPortalSetting"), 
                new ClassInfo("AppSetting"),
                new ClassInfo("AppTenantSetting"),
                new ClassInfo("AppTenantUserSetting"),
                new ClassInfo("ContactSetting"),
                new ClassInfo("CustomerSetting"),                                                  
            };

            using (var file = new StreamWriter(@"SbpApi.txt"))
            {
                DoApi(list, file);
            }

            var classList = new[]
                {                 
                    //ClickToPay
                    typeof (ClickToPayInvoice),
                    typeof (ClickToPayInvoicePayment), typeof (ClickToPayPayment), typeof (ClickToPayStat),
                    typeof (ClickToPayStatement), typeof (ClickToPayStatementPayment),

                    typeof (CustomDocumentTemplate),           
                    typeof (DocumentTemplate),
                    typeof (UnprocessedInvoice),
                    typeof (UnprocessedStatement),

                    typeof (CustomerPortalSetting),
                    typeof (Font),
                    typeof (Size),

                    //Settings
                    typeof (AppSetting), typeof (AppTenantSetting), typeof (AppTenantUserSetting), 
                    typeof (ContactSetting), typeof (CustomerSetting)
                };

            using (var file = new StreamWriter(@"sbp.txt"))
            {
                DoEntities(file, classList);
            }
        }
    }
}
