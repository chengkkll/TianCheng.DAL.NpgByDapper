using System;
using System.Collections.Generic;
using System.Text;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// Id类型为string类型的数据库操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StringOperation<T> : BaseOperation<T, string>
        where T : StringPrimaryKey, new()
    {

    }
}
