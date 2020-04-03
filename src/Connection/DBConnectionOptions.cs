using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// 数据库链接配置
    /// </summary>
    public class DBConnectionOptions
    {
        /// <summary>
        /// 配置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
