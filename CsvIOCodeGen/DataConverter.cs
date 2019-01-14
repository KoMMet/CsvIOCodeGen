using System;
using System.Collections.Generic;

namespace CsvIO
{
    internal static class DataConverter<T>
    {
        /// <summary>
        /// 文字列の型を変換する
        /// </summary>
        /// <param name="src">変換元</param>
        /// <returns>変換先</returns>
        internal static IEnumerable<T> Convert(IEnumerable<string> src)
        {
            switch(typeof(T).Name)
            {
                case nameof(Byte): return (IEnumerable<T>)DataConverterImp.ByteConvert(src);
                case nameof(SByte): return (IEnumerable<T>)DataConverterImp.SByteConvert(src);
                case nameof(Int16): return (IEnumerable<T>)DataConverterImp.ShortConvert(src);
                case nameof(UInt16): return (IEnumerable<T>)DataConverterImp.UshortConvert(src);
                case nameof(Int32): return (IEnumerable<T>)DataConverterImp.IntConvert(src);
                case nameof(UInt32): return (IEnumerable<T>)DataConverterImp.UintConvert(src);
                case nameof(Int64): return (IEnumerable<T>)DataConverterImp.LongConvert(src);
                case nameof(UInt64): return (IEnumerable<T>)DataConverterImp.UlongConvert(src);
                case nameof(Double): return (IEnumerable<T>)DataConverterImp.DoubleConvert(src);
                case nameof(Boolean): return (IEnumerable<T>)DataConverterImp.BoolConvert(src);
                case nameof(String): return (IEnumerable<T>)src;
                default:
                    return new List<T>();
            }
        }
    }
}