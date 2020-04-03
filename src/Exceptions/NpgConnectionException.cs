using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// 数据库连接异常
    /// </summary>
    public class NpgConnectionException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="connectionString"></param>
        public NpgConnectionException(Exception ex, string connectionString)
        {
            if (ex != null || ex.InnerException is TimeoutException)
            {
                Log.CommonLog.Logger.Fatal(ex, $"数据库链接超时。链接字符串：{connectionString}");
            }
            else
            {
                Log.CommonLog.Logger.Fatal(ex, $"数据库链接错误。链接字符串：{connectionString}");
            }
        }
    }
}
