using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


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
            csv.UpdateAllColunm(1,"88");

            w.Write("newdata1.csv", csv);
        }
    }

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

    public class CsvData
    {
        internal CaptionLine Caption { get; set; } = new CaptionLine();
        internal List<Row> Rows { get; set; } = new List<Row>();

        public int Length => Rows.Count + 1;
        public int ColumnCount => Caption.Title.Length;
        public string GetCaptionString() => Caption.ToString();

        public string GetRowString(int index)
        {
            if(index == 0) return Caption.ToString();
            return Rows[index - 1].ToString();
        }

        public IEnumerable<string> GetRows(int index)
        {
            if(index == 0) return Caption.Title.ToList();
            return Rows[index - 1].Value.ToList();
        }

        public IEnumerable<T> GetColumnData<T>(int index)
        {
            if(index >= ColumnCount) return new List<T>();
            return DataConverter<T>.f(Rows.Select(v => v.Value[index]));
        }

        public IEnumerable<T> GetColumnData<T>(string columnName)
        {
            if(Caption.Title.All(t => t != columnName)) return new List<T>();

            //検索
            var foundItems = Caption.Title.Select((item, i) => new { Index = i, Value = item })
                    .Where(item => item.Value == columnName)
                    .Select(item => item.Index);

            return GetColumnData<T>(foundItems.First());
        }

        public void AddRow(string row) => Rows.Add(new Row() { Value = row.Split(',') });

        public void AddRow(IEnumerable<string> src)
        {
            AddRow(string.Join(",", src));
        }

        public bool InsertRow(int index, string row)
        {
            if(Length < index) return false;
            Rows.Insert(index, new Row() { Value = row.Split(',') });
            return true;
        }

        public void UpdateCell(int rowIndex, int columnIndex, string data) =>
                Rows[rowIndex - 1].Value[columnIndex] = data;

        public void UpdateCell(int rowIndex, string columnName, string data)
        {
            //検索
            var foundItems = Caption.Title.Select((item, i) => new { Index = i, Value = item })
                    .Where(item => item.Value == columnName)
                    .Select(item => item.Index);

            Rows[rowIndex - 1].Value[foundItems.First()] = data;
        }

        public void UpdateAllColunm(int columnIndex, string value)
        {
            foreach(var row in Rows)
            {
                row.Value[columnIndex] = value;
            }
        }
    }

    internal class Row
    {
        internal string[] Value { get; set; }
        public override string ToString() => string.Join(",", Value);
    }

    internal class CaptionLine
    {
        internal string[] Title { get; set; }
        public override string ToString() => string.Join(",", Title);
    }

    internal static class DataConverter<T>
    {
        internal static IEnumerable<T> f(IEnumerable<string> s)
        {
            switch(typeof(T).Name)
            {
                case nameof(Byte): return (IEnumerable<T>)DataConverterImp.ByteConvert(s);
                case nameof(SByte): return (IEnumerable<T>)DataConverterImp.SByteConvert(s);
                case nameof(Int16): return (IEnumerable<T>)DataConverterImp.ShortConvert(s);
                case nameof(UInt16): return (IEnumerable<T>)DataConverterImp.UshortConvert(s);
                case nameof(Int32): return (IEnumerable<T>)DataConverterImp.IntConvert(s);
                case nameof(UInt32): return (IEnumerable<T>)DataConverterImp.UintConvert(s);
                case nameof(Int64): return (IEnumerable<T>)DataConverterImp.LongConvert(s);
                case nameof(UInt64): return (IEnumerable<T>)DataConverterImp.UlongConvert(s);
                case nameof(Double): return (IEnumerable<T>)DataConverterImp.DoubleConvert(s);
                case nameof(Boolean): return (IEnumerable<T>)DataConverterImp.BoolConvert(s);
                case nameof(String): return (IEnumerable<T>)s;
                default:
                    return new List<T>();
            }
        }
    }

    internal static class DataConverterImp
    {
        internal static IEnumerable<byte> ByteConvert(IEnumerable<string> s) => s.Select(v =>
        {
            byte.TryParse(v, out byte o);
            return o;
        });

        internal static IEnumerable<sbyte> SByteConvert(IEnumerable<string> s) => s.Select(v =>
        {
            sbyte.TryParse(v, out sbyte o);
            return o;
        });

        internal static IEnumerable<short> ShortConvert(IEnumerable<string> s) => s.Select(v =>
        {
            short.TryParse(v, out short o);
            return o;
        });

        internal static IEnumerable<ushort> UshortConvert(IEnumerable<string> s) => s.Select(v =>
        {
            ushort.TryParse(v, out ushort o);
            return o;
        });

        internal static IEnumerable<int> IntConvert(IEnumerable<string> s) => s.Select(v =>
        {
            int.TryParse(v, out int o);
            return o;
        });

        internal static IEnumerable<uint> UintConvert(IEnumerable<string> s) => s.Select(v =>
        {
            uint.TryParse(v, out uint o);
            return o;
        });

        internal static IEnumerable<long> LongConvert(IEnumerable<string> s) => s.Select(v =>
        {
            long.TryParse(v, out long o);
            return o;
        });

        internal static IEnumerable<ulong> UlongConvert(IEnumerable<string> s) => s.Select(v =>
        {
            ulong.TryParse(v, out ulong o);
            return o;
        });

        internal static IEnumerable<double> DoubleConvert(IEnumerable<string> s) => s.Select(v =>
        {
            double.TryParse(v, out double o);
            return o;
        });

        internal static IEnumerable<bool> BoolConvert(IEnumerable<string> s) => s.Select(v =>
        {
            bool.TryParse(v, out bool o);
            return o;
        });
    }
}