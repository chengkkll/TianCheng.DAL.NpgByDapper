using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// 数据库操作对象需要制定操作的表名
    /// </summary>
    public class NpgTableNameNullException : Exception
    {
        public NpgTableNameNullException() : base("请指定表名")
        {
            Log.CommonLog.Logger.Warning("请指定表名");
        }

        public NpgTableNameNullException(string message) : base(message)
        {
            Log.CommonLog.Logger.Warning(message);
        }
    }
}
