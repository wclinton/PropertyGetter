
using System.IO;
using Sage.Cloud.Domain.Statements.Interfaces.Models;

namespace PropertyGettter
{
    public class Statement:Base
    {
        public void Do()
        {
            var list = new[]
            {
                new ClassInfo("Statement",NonModifyable()), 
                new ClassInfo("Statement('{Id}')/StatementDetail",NonModifyable()), 
                new ClassInfo("UnprocessedStatement"),                                                                             
            };

            using (var file = new StreamWriter(@"StatementApi.txt"))
            {
                DoApi(list, file);
            }

           var classList = new[]
                {                 
                  typeof(Sage.Cloud.Domain.Statements.Interfaces.Models.Statement),
                  typeof(StatementDetail),                  
                  typeof(UnprocessedStatement),
                };

            using (var file = new StreamWriter(@"statement.txt"))
            {
                DoEntities(file, classList);
            }
        }
    }
}
