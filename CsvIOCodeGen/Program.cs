using System.Collections.Generic;


namespace CsvIO
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new CsvReader();
            var w = new CsvWriter();
            var csv = c.Read(args[0]);
            var cap = csv.GetCaptionString();
            var r = csv.GetRowString(0);
            var r1 = csv.GetRows(1);
            var r2 = csv.GetColumnData<int>(0);
            var c1 = csv.GetColumnData<int>(0);
            var c3 = csv.GetColumnData<double>(2);
            var cr = csv.GetColumnData<int>(cap[1]);
            var cb = csv.GetColumnData<int>("col3");

            var cc = new List<string>(r1) {[2] = "12"};
            csv.AddRow(cc);

            csv.UpdateCell(1,0,"99");
            csv.UpdateAllColumn(1,"88");

            w.Write("newdata1.csv", csv);
        }
    }
}