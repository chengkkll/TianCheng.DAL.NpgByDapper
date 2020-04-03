using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianCheng.DAL.NpgByDapper;
using WebDemo.Model;

namespace WebDemo.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class MockGuidDAL : GuidOperation<MockGuidDB>
    {
        protected override string TableName { get; set; } = "mock_guid";

        public MockGuidInfo First(MockGuidQuery query)
        {
            return First<MockGuidInfo, MockGuidQuery>(ConditionSQL(query), query);
        }

        /// <summary>
        /// 设置统一的查询条件
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public string ConditionSQL(MockGuidQuery query)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append((!string.IsNullOrWhiteSpace(query.FirstName)).Sql("and first_name=@FirstName"));
            //sql.Append((!string.IsNullOrWhiteSpace(query.LikeName)).Sql("and first_name like @LikeName", () => { query.LikeName = $"{query.LikeName}%"; }));  // 如果条件需要修改query值可以这样写
            sql.Append((!string.IsNullOrWhiteSpace(query.LikeName)).Sql("and first_name ~* @LikeName")); // like 可以用索引。 正则匹配有点慢
            sql.Append((query.IsDelete != null).Sql("and is_delete=@IsDelete"));

            return sql.ToString();
        }
    }

}
