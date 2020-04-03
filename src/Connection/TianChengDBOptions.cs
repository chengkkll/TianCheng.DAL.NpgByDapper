using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// 数据库操作配置
    /// </summary>
    public class TianChengDBOptions
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        public IEnumerable<DBConnectionOptions> Connection { get; set; }

        /// <summary>
        /// 操作的数据集
        /// </summary>
        public IEnumerable<string> Assembly { get; set; }
    }
}
