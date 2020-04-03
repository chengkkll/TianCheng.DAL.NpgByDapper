using System;
using System.Data;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// Id为Guid类型的数据库操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GuidOperation<T> : BaseOperation<T, Guid>
        where T : GuidPrimaryKey, new()
    {

    }
}
