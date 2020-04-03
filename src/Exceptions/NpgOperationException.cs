using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// 数据库操作错误
    /// </summary>
    public class NpgOperationException : Exception
    {
        public NpgOperationException(Exception ex, string message)
        {
            NpgLog.Logger.Warning(ex, message);
        }
    }
}
