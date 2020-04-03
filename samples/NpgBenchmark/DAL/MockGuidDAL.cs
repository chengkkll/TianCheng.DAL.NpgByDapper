using Dapper;
using NpgBenchmark.Model;
using System;
using System.Collections.Generic;
using System.Text;
using TianCheng.DAL.NpgByDapper;

namespace NpgBenchmark.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class MockGuidDAL : GuidOperation<MockGuidDB>
    {
        protected override string TableName { get; set; } = "mock_guid";

        public MockGuidInfo First(GuidQuery query)
        {
            return First<MockGuidInfo, GuidQuery>(ConditionSQL(query), query);
        }

        public IEnumerable<MockGuidDB> Search(GuidQuery query)
        {
            return Search(ConditionSQL(query), query);
        }

        public IEnumerable<Info> Search<Info>(GuidQuery query)
        {
            return Search<Info, GuidQuery>(ConditionSQL(query), query);
        }

        public int Count(GuidQuery query)
        {
            return Count(ConditionSQL(query), query);
        }

        /// <summary>
        /// 设置统一的查询条件
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public string ConditionSQL(GuidQuery query)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append((!string.IsNullOrWhiteSpace(query.FirstName)).Sql("and first_name=@FirstName"));
            //sql.Append((!string.IsNullOrWhiteSpace(query.LikeName)).Sql("and first_name like @LikeName", () => { query.LikeName = $"{query.LikeName}%"; }));  // 如果条件需要修改query值可以这样写
            sql.Append((!string.IsNullOrWhiteSpace(query.LikeName)).Sql("and first_name ~* @LikeName")); // like 可以用索引。 正则匹配有点慢
            sql.Append((query.IsDelete != null).Sql("and is_delete=@IsDelete"));

            return sql.ToString();
        }

    }

    public class GuidQuery : QueryInfo
    {
        public string FirstName { get; set; }

        public string LikeName { get; set; }

        public bool? IsDelete { get; set; } = null;

    }
}
