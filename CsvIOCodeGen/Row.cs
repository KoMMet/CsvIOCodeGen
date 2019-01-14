namespace CsvIO
{
    /// <summary>
    /// CSVの行データ
    /// </summary>
    internal class Row
    {
        /// <summary>
        /// 値の配列
        /// </summary>
        internal string[] Value { get; set; }
        /// <summary>
        /// 行データをカンマ区切りにする
        /// </summary>
        /// <returns>カンマ区切りの行</returns>
        public override string ToString() => string.Join(",", Value);
    }
}