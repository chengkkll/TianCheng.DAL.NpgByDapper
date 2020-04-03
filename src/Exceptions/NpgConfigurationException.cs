using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// 数据库配置异常
    /// </summary>
    public class NpgConfigurationException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="connectionString"></param>
        public NpgConfigurationException(string message = "无法找到数据库配置信息") : base(message)
        {
            Log.CommonLog.Logger.Fatal(message);
        }
    }
}
