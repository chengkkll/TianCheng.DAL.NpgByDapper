using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// String -> DateTime?
    /// </summary>
    public class StringToGuidConverter : ITypeConverter<string, Guid>
    {
        /// <summary>
        ///  ObjectId -> String
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Guid Convert(string source, Guid destination, ResolutionContext context)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return Guid.Empty;
            }

            return new Guid(source);
        }
    }
    /// <summary>
    /// DateTime? -> String
    /// </summary>
    public class GuidToStringConverter : ITypeConverter<Guid, string>
    {
        /// <summary>
        ///  DateTime? -> String
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string Convert(Guid source, string destination, ResolutionContext context)
        {
            if (source == null || source == Guid.Empty)
            {
                return "";
            }

            return source.ToString();
        }
    }
}
