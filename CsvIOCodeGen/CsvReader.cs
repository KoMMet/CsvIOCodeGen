using System;
using System.IO;
using System.Linq;

namespace CsvIO
{
    /// <summary>
    /// CSVファイルを読み込む
    /// </summary>
    public class CsvReader
    {
        public CsvData Read(string path)
        {
            if(path == null)
                throw new ArgumentNullException(nameof(path));

            string[] lines = File.ReadAllLines(path);
            if(lines.Length == 0)
                throw new ArgumentException("csvファイルが空");

            CsvData data = new CsvData();
            var head = lines[0];

            //ヘッダーがなない
            if(!head.Contains("#"))
                throw new ArgumentException("ヘッダー行がない");

            //チェック⇒例外
            var splitedheader = head.Replace("#", "").Split(',');
            data.Caption.Title = splitedheader.ToArray();

            lines.Skip(1).Select(l =>
            {
                if(l.IndexOf('#') == 0) return 0;
                var r = l.Split(',');
                data.Rows.Add(new Row { Value = r.ToArray() });
                return 0;
            }).Sum();

            return data;
        }
    }
}