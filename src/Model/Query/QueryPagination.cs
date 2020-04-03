using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// 分页对象
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class QueryPagination
    {
        /// <summary>
        /// 要获取数据的页号
        /// </summary>
        public int Index { get; set; } = -1;

        /// <summary>
        /// 每页最多显示的数据条数
        /// </summary>
        public int Size { get; set; } = 100;

        /// <summary>
        /// 默认每页记录数
        /// </summary>
        public const int DefaultPageSize = 10;
        /// <summary>
        /// 默认的分页信息
        /// </summary>
        static public QueryPagination DefaultObject
        {
            get
            {
                return new QueryPagination
                {
                    Index = 1,
                    Size = DefaultPageSize
                };
            }
        }

        /// <summary>
        /// 一页内显示所有数据
        /// </summary>
        static public QueryPagination OnePage
        {
            get
            {
                return new QueryPagination
                {
                    Index = 1,
                    Size = 10000
                };
            }
        }
    }
}
