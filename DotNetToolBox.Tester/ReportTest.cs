using DotNetToolBox.Reporting;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetToolBox.Tester
{
    public static class ReportTest
    {
        public static void Test()
        {
            try
            {
                SampleReport report = new SampleReport(@"C:\Temp\Report.rdlc");
                report.SavePdfReport(@"C:\Temp\report.pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public class SampleReport : Rdlc14Manager
    {
        public SampleReport(string path)
            : base(path)
        {
            AddParameter("SampleParam", "test");

            DS.DataTable1DataTable dt = new DS.DataTable1DataTable();
            DS.DataTable1Row row = dt.NewDataTable1Row();
            row.Id = "1";
            row.Name = "John Doe";
            dt.Rows.Add(row);
            DS.DataTable1Row row2 = dt.NewDataTable1Row();
            row2.Id = "2";
            row2.Name = "Jane Doe";
            dt.Rows.Add(row2);
            DataSources.Add(new ReportDataSource("DataSet1", dt.DefaultView));
        }
    }
}
