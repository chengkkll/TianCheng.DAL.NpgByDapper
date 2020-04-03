using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// 
    /// </summary>
    static public class SqlParameterExt
    {
        /// <summary>
        /// 累加sql
        /// </summary>
        /// <param name="isAppend">是否使用</param>
        /// <param name="sql">sql</param>
        /// <param name="param">参数处理</param>
        /// <returns></returns>
        public static string Sql(this bool isAppend, string sql, Action param = null)
        {
            if (isAppend)
            {
                param?.Invoke();
                if (!sql.StartsWith(" "))
                {
                    return $" {sql}";
                }
                return sql;
            }
            return string.Empty;
        }
    }
}
