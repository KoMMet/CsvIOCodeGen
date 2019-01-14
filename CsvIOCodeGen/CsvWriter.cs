using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvIO
{
    /// <summary>
    /// csvをファイルに書き出す
    /// </summary>
    public class CsvWriter
    {
        public void Write(string path, CsvData data)
        {
            var list = new List<string> { string.Join(",", data.Caption.Title) };
            data.Rows.Select(l =>
            {
                list.Add(string.Join(",", l.Value));
                return 0;
            }).Sum();

            File.WriteAllLines(path, list.ToArray());
        }
    }
}