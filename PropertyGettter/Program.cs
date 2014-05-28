using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Sage.Cloud.Domain.AppSbp.Interfaces.Models;

namespace PropertyGettter
{
    class Program
    {

        [FlagsAttribute]
        enum ClassAttribute : short
        {
            Multi = 0,
            Get = 1,
            Post = 2,
            Patch = 4,
            Delete = 8,
        }

        private static void Main()
        {
           DoSbp();
        }

        private static void SbpEntityList()
        {

            using (var file = new StreamWriter(@"sbp.txt"))
            {
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

                //Show EntityList
                file.WriteLine("<h2>​​​Domain Entities</h2>");
                file.WriteLine("<ul style=\"list-style-type: disc;\">");


                foreach (var t in classList)
                {

                    file.WriteLine("<li>" + t.Name + "</li>");


                    file.WriteLine("<ul style=\"list-style-type: circle;\">");

                    file.WriteLine("<li></li>");
                    file.WriteLine("</ul>");
                }

                file.WriteLine("</ul>");
                file.WriteLine("<h2>​​​Entity Definition</h2>");
                file.WriteLine("<p>​<br/></p>");

                foreach (var t in classList)
                {
                    var propertyInfos = t.GetProperties();

                    var propertyList = propertyInfos.ToList();

                    propertyList = FixList(propertyList);

                    Console.WriteLine("<h3>" + t.Name + "</h3>");
                    file.WriteLine("<h3>" + t.Name + "</h3>");

                    StartTable(file);
                    bool even = false;
                    foreach (var propertyInfo in propertyList)
                    {
                        if (propertyInfo.MemberType == MemberTypes.Property && IsValid(propertyInfo))
                        {
                            Row(file, even, propertyInfo.Name, TypeName(propertyInfo));
                            even = !even;
                        }
                    }
                    EndTable(file);
                }
            }
        }


        private static void DoSbp()
        {
            SbpApiCalls();
            SbpEntityList(); 
        }


        private static List<PropertyInfo> FixList(List<PropertyInfo> infoList)
        {
            var item = infoList.Find(x => x.Name == "Id");

            if (item != null)
            {
                infoList.Remove(item);
                infoList.Insert(0, item);
            }

            return infoList;
        }

        private static bool IsValid(PropertyInfo info)
        {
            var invalidList = new[] { "TenantId", "EntityStatus", "CreatedOn", "UpdatedOn", "CreatedBy", "UpdatedBy", "ObjectState", "ETag" };
            return !(invalidList.Contains(info.Name));
        }


        private static string TypeName(PropertyInfo info)
        {
            var type = info.PropertyType.Name;

            if (String.Compare(type, "Int32", StringComparison.Ordinal) == 0)
                type = "Integer";

            if (String.Compare(type, "Nullable`1", StringComparison.Ordinal) == 0)
                type = "DateTime?";

            return type;
        }

        private static void StartTable(StreamWriter stream)
        {
            Console.WriteLine("<table cellspacing=\"0\" class=\"ms-rteTable-7\" style=\"width: 100%;\">");
            Console.WriteLine("<tbody>");
            Console.WriteLine("<tr class=\"ms-rteTableHeaderRow-7\">");

            Console.WriteLine("<th colspan=\"1\" rowspan=\"1\" class=\"ms-rteTableHeaderEvenCol-7\" style=\"width: 33.3333%;\"> Property </th>");
            Console.WriteLine("<th colspan=\"1\" rowspan=\"1\" class=\"ms-rteTableHeaderOddCol-7 \" style=\"width: 33.3333%;\"> Type </th>");
            Console.WriteLine("<th colspan=\"1\" rowspan=\"1\" class=\"ms-rteTableHeaderEvenCol-7\" style=\"width: 33.3333%;\"> Notes </th>");
            Console.WriteLine("</tr>");

            stream.WriteLine("<table cellspacing=\"0\" class=\"ms-rteTable-7\" style=\"width: 100%;\">");
            stream.WriteLine("<tbody>");
            stream.WriteLine("<tr class=\"ms-rteTableHeaderRow-7\">");

            stream.WriteLine("<th colspan=\"1\" rowspan=\"1\" class=\"ms-rteTableHeaderEvenCol-7\" style=\"width: 33.3333%;\"> Property </th>");
            stream.WriteLine("<th colspan=\"1\" rowspan=\"1\" class=\"ms-rteTableHeaderOddCol-7 \" style=\"width: 33.3333%;\"> Type </th>");
            stream.WriteLine("<th colspan=\"1\" rowspan=\"1\" class=\"ms-rteTableHeaderEvenCol-7\" style=\"width: 33.3333%;\"> Notes </th>");
            stream.WriteLine("</tr>");
        }

        private static void EndTable(StreamWriter stream)
        {
            Console.WriteLine(" </tbody>");
            Console.WriteLine("</table>");
            Console.WriteLine("<p>&#160;</p>");

            stream.WriteLine(" </tbody>");
            stream.WriteLine("</table>");
            stream.WriteLine("<p>&#160;</p>");
        }

        private static void Row(StreamWriter stream, bool even, string name, string type)
        {
            if (even)
            {
                Console.WriteLine("<tr class=\"ms-rteTableEvenRow-7\">");
                stream.WriteLine("<tr class=\"ms-rteTableEvenRow-7\">");
            }
            else
            {
                Console.WriteLine("<tr class=\"ms-rteTableOddRow-7\">");
                stream.WriteLine("<tr class=\"ms-rteTableOddRow-7\">");
            }
            var nameRow =
                "<th colspan=\"1\" rowspan=\"1\" class=\"ms-rteTableHeaderEvenCol-7\" style=\"width: 33.3333%;\">" +
                name + "​​</th>";

            Console.WriteLine(nameRow);
            Console.WriteLine("<th colspan=\"1\" rowspan=\"1\" class=\"ms-rteTableHeaderOddCol-7\" style=\"width: 33.3333%;\">" + type + "</th>");
            Console.WriteLine("<th colspan=\"1\" rowspan=\"1\" class=\"ms-rteTableHeaderEvenCol-7\" style=\"width: 33.3333%;\"></th>");


            stream.WriteLine(nameRow);
            stream.WriteLine("<th colspan=\"1\" rowspan=\"1\" class=\"ms-rteTableHeaderOddCol-7\" style=\"width: 33.3333%;\">" + type + "</th>");
            stream.WriteLine("<th colspan=\"1\" rowspan=\"1\" class=\"ms-rteTableHeaderEvenCol-7\" style=\"width: 33.3333%;\"></th>");
        }


        private static void SbpApiCalls()
        {

            var list = new ClassInfo[]
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

                var even = false;
                foreach (var cl in list)
                {
                    if (IsMulti(cl.Attributes))
                    {
                        ShowMulti(file, even, cl.Name);
                        even = !even;
                    }

                    if (IsGet(cl.Attributes))
                    {
                        ShowGet(file, even, cl.Name);
                        even = !even;
                    }

                    if (IsPost(cl.Attributes))
                    {
                        ShowPost(file, even, cl.Name);
                        even = !even;
                    }

                    if (IsPatch(cl.Attributes))
                    {
                        ShowPatch(file, even, cl.Name);
                        even = !even;
                    }

                    if (IsDelete(cl.Attributes))
                    {
                        ShowDelete(file, even, cl.Name);
                        even = !even;
                    }
                }
            }
        }


        private static bool IsMulti(ClassAttribute attrib)
        {
            return ((attrib & ClassAttribute.Multi) == ClassAttribute.Multi);
        }

        private static bool IsGet(ClassAttribute attrib)
        {
            return ((attrib & ClassAttribute.Get) == ClassAttribute.Get);
        }

        private static bool IsPost(ClassAttribute attrib)
        {
            return ((attrib & ClassAttribute.Post) == ClassAttribute.Post);
        }

        private static bool IsPatch(ClassAttribute attrib)
        {
            return ((attrib & ClassAttribute.Patch) == ClassAttribute.Patch);
        }

        private static bool IsDelete(ClassAttribute attrib)
        {
            return ((attrib & ClassAttribute.Delete) == ClassAttribute.Delete);
        }

        private static ClassAttribute NoDelete()
        {
            return ClassAttribute.Multi | ClassAttribute.Get | ClassAttribute.Patch | ClassAttribute.Post;
        }

        private static void ShowMulti(StreamWriter file, bool even, string name)
        {
            if (even)
                file.WriteLine("<tr class=\"ms-rteTableEvenRow-5\">");
            else
                file.WriteLine("<tr class=\"ms-rteTableOddRow-5\">"); ;

            file.WriteLine("<td class=\"ms-rteTableEvenCol-5\">/" + name + "</td>");
            file.WriteLine("<td class=\"ms-rteTableOddCol-5\">GET</td>");
            file.WriteLine("<td class=\"ms-rteTableEvenCol-5\">Gets all " + name + " associated with the tenant​</td>");
            file.WriteLine("</tr>");
        }

        private static void ShowGet(StreamWriter file, bool even, string name)
        {
            if (even)
                file.WriteLine("<tr class=\"ms-rteTableEvenRow-5\">");
            else
                file.WriteLine("<tr class=\"ms-rteTableOddRow-5\">"); ;

            file.WriteLine("<td class=\"ms-rteTableEvenCol-5\">/" + name + "(&#39;{Id}&#39;)</td>");
            file.WriteLine("<td class=\"ms-rteTableOddCol-5\">GET</td>");
            file.WriteLine("<td class=\"ms-rteTableEvenCol-5\">Gets a specific " + name + " associated with the tenant​</td>");
            file.WriteLine("</tr>");
        }

        private static void ShowPost(StreamWriter file, bool even, string name)
        {
            if (even)
                file.WriteLine("<tr class=\"ms-rteTableEvenRow-5\">");
            else
                file.WriteLine("<tr class=\"ms-rteTableOddRow-5\">"); ;

            file.WriteLine("<td class=\"ms-rteTableEvenCol-5\">/" + name + "(&#39;{Id}&#39;)</td>");
            file.WriteLine("<td class=\"ms-rteTableOddCol-5\">POST</td>");
            file.WriteLine("<td class=\"ms-rteTableEvenCol-5\">Adds a new " + name + "</td>");
            file.WriteLine("</tr>");
        }

        private static void ShowPatch(StreamWriter file, bool even, string name)
        {

            if (even)
                file.WriteLine("<tr class=\"ms-rteTableEvenRow-5\">");
            else
                file.WriteLine("<tr class=\"ms-rteTableOddRow-5\">"); ;

            file.WriteLine("<td class=\"ms-rteTableEvenCol-5\">/" + name + "(&#39;{Id}&#39;)</td>");
            file.WriteLine("<td class=\"ms-rteTableOddCol-5\">PATCH</td>");
            file.WriteLine("<td class=\"ms-rteTableEvenCol-5\">Updates " + name + "</td>");
            file.WriteLine("</tr>");

        }

        private static void ShowDelete(StreamWriter file, bool even, string name)
        {

            if (even)
                file.WriteLine("<tr class=\"ms-rteTableEvenRow-5\">");
            else
                file.WriteLine("<tr class=\"ms-rteTableOddRow-5\">"); ;

            file.WriteLine("<td class=\"ms-rteTableEvenCol-5\">/" + name + "(&#39;{Id}&#39;)</td>");
            file.WriteLine("<td class=\"ms-rteTableOddCol-5\">DELETE</td>");
            file.WriteLine("<td class=\"ms-rteTableEvenCol-5\">Deletes a specific " + name + " associated with the tenant​</td>");
            file.WriteLine("</tr>");
        }

        class ClassInfo
        {
            public string Name { get; set; }
            public ClassAttribute Attributes { get; set; }

            public ClassInfo(string name, ClassAttribute attribute)
            {
                this.Name = name;
                this.Attributes = attribute;
            }

            public ClassInfo(string name)
            {
                this.Name = name;
                this.Attributes = (ClassAttribute.Delete | ClassAttribute.Get | ClassAttribute.Multi |
                                           ClassAttribute.Patch | ClassAttribute.Post);
            }
        }
    }
}