using System.Collections.Generic;
using System.Linq;

namespace CsvIO
{
    /// <summary>
    /// 文字列の集合を型変換した集合に変換する
    /// </summary>
    internal static class DataConverterImp
    {
        internal static IEnumerable<byte> ByteConvert(IEnumerable<string> s) => s.Select(v =>
        {
            byte.TryParse((string) v, out byte o);
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