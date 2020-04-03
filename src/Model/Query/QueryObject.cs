using Newtonsoft.Json;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// 查询条件基类
    /// </summary>
    public class QueryObject
    {
        /// <summary>
        /// 排序规则
        /// </summary>
        public QuerySort Sort { get; set; } = new QuerySort();

        /// <summary>
        /// 分页信息
        /// </summary>
        public QueryPagination Page { get; set; } = new QueryPagination();
    }
}
