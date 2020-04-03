using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// Id类型为int类型的数据库操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IntOperation<T> : BaseOperation<T, int>
        where T : IntPrimaryKey,new()
    {
        
    }
}
