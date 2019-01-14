using System.Collections.Generic;
using System.Linq;

namespace CsvIO
{
    /// <summary>
    /// CSVデータ管理クラス
    /// </summary>
    public class CsvData
    {
        internal CaptionLine Caption { get; set; } = new CaptionLine();
        internal List<Row> Rows { get; set; } = new List<Row>();

        public int Length => Rows.Count + 1;
        public int ColumnCount => Caption.Title.Length;
        public string GetCaptionString() => Caption.ToString();

        /// <summary>
        /// 行をカンマ区切りで取得する
        /// </summary>
        /// <param name="index">行番号</param>
        /// <returns>行の文字列</returns>
        public string GetRowString(int index)
        {
            if(index == 0) return Caption.ToString();
            return Rows[index - 1].ToString();
        }
        /// <summary>
        /// 行を集合で取得する
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IEnumerable<string> GetRows(int index)
        {
            if(index == 0) return Caption.Title.ToList();
            return Rows[index - 1].Value.ToList();
        }
        /// <summary>
        /// 列を集合で取得する
        /// </summary>
        /// <typeparam name="T">取得したい型</typeparam>
        /// <param name="index">列番号</param>
        /// <returns>列の集合</returns>
        public IEnumerable<T> GetColumnData<T>(int index)
        {
            if(index >= ColumnCount) return new List<T>();
            return DataConverter<T>.Convert(Rows.Select(v => v.Value[index]));
        }
        /// <summary>
        /// 列を集合で取得する
        /// </summary>
        /// <typeparam name="T">取得したい型</typeparam>
        /// <param name="columnName">カラム名</param>
        /// <returns>列の集合</returns>
        public IEnumerable<T> GetColumnData<T>(string columnName)
        {
            if(Caption.Title.All(t => t != columnName)) return new List<T>();

            //検索
            var foundItems = Caption.Title.Select((item, i) => new { Index = i, Value = item })
                    .Where(item => item.Value == columnName)
                    .Select(item => item.Index);

            return GetColumnData<T>(foundItems.First());
        }

        /// <summary>
        /// 行を最後尾に追加する
        /// </summary>
        /// <param name="row">カンマ区切りの文字列</param>
        public void AddRow(string row) => Rows.Add(new Row() { Value = row.Split(',') });

        /// <summary>
        /// 行を最後尾に追加する
        /// </summary>
        /// <param name="src">行の集合</param>
        public void AddRow(IEnumerable<string> src)
        {
            AddRow(string.Join(",", src));
        }
        /// <summary>
        /// 行を指定された行番号に追加する
        /// </summary>
        /// <param name="index">行番号</param>
        /// <param name="row">カンマ区切りの文字列</param>
        public void InsertRow(int index, string row)
        {
            Rows.Insert(index, new Row() { Value = row.Split(',') });
        }
        /// <summary>
        /// セルの内容を変更する
        /// </summary>
        /// <param name="rowIndex">行番号</param>
        /// <param name="columnIndex">列番号</param>
        /// <param name="data">変更後の値</param>
        public void UpdateCell(int rowIndex, int columnIndex, string data) =>
                Rows[rowIndex - 1].Value[columnIndex] = data;

        /// <summary>
        /// セルの内容を変更する
        /// </summary>
        /// <param name="rowIndex">行番号</param>
        /// <param name="columnName">カラム名</param>
        /// <param name="data">変更後の値</param>
        public void UpdateCell(int rowIndex, string columnName, string data)
        {
            //検索
            var foundItems = Caption.Title.Select((item, i) => new { Index = i, Value = item })
                    .Where(item => item.Value == columnName)
                    .Select(item => item.Index);

            Rows[rowIndex - 1].Value[foundItems.First()] = data;
        }
        /// <summary>
        /// 列の値を一括で変更する
        /// </summary>
        /// <param name="columnIndex">カラム名</param>
        /// <param name="value">変更後の値</param>
        public void UpdateAllColumn(int columnIndex, string value)
        {
            foreach(var row in Rows)
            {
                row.Value[columnIndex] = value;
            }
        }
    }
}