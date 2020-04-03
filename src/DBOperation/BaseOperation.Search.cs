using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// PostgreSql数据库操作处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class BaseOperation<T, IdType> : IDBOperationRegister
        where T : IPrimaryKey<IdType>, new()
    {
        #region Single
        /// <summary>
        /// 根据id查询对象
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public T SingleByStringId(string id, IDbTransaction tran = null, int? commandTimeout = null)
        {
            return SingleById(IdInstance.ConvertID(id), tran, commandTimeout);
        }
        /// <summary>
        /// 根据id查询对象
        /// </summary>
        /// <typeparam name="Info"></typeparam>
        /// <param name="id"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public Info SingleByStringId<Info>(string id, IDbTransaction tran = null, int? commandTimeout = null)
        {
            T t = SingleByStringId(id, tran, commandTimeout);
            if (t == null)
            {
                return default;
            }
            return t.AutoMapper<Info>();
        }
        /// <summary>
        /// 根据id查询对象
        /// </summary>
        /// <returns></returns>
        public T SingleById(IdType id, IDbTransaction tran = null, int? commandTimeout = null)
        {
            // 检查参数
            if (!IdInstance.CheckId(id))
            {
                NpgLog.Logger.Error($"按id查询时，id参数错误。id:{id}");
                throw new ArgumentException("查询参数无效");
            }
            DynamicParameters param = new DynamicParameters();
            param.Add("id", id);

            // 获取数据库连接
            IDbConnection connection = tran != null ? tran.Connection : GetConnection();

            // 打开数据库连接
            ConnectionOpen(connection, tran);
            T result;
            try
            {
                result = connection.Query<T>(DefaultSelectByIdSQL, param, tran, commandTimeout: commandTimeout).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw new NpgOperationException(ex, $"按id查询异常，sql：{DefaultSelectByIdSQL}   查询id：{id}");
            }
            // 关闭数据库连接
            ConnectionClose(connection, tran);

            return result;
        }
        /// <summary>
        /// 根据id查询对象
        /// </summary>
        /// <typeparam name="Info"></typeparam>
        /// <param name="id"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public Info SingleById<Info>(IdType id, IDbTransaction tran = null, int? commandTimeout = null)
        {
            T t = SingleById(id, tran, commandTimeout);
            if (t == null)
            {
                return default;
            }
            return t.AutoMapper<Info>();
        }
        /// <summary>
        /// 获取第一个满足条件的数据
        /// </summary>
        /// <param name="conditionSql"></param>
        /// <param name="parameters"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public T First<Q>(string conditionSql, Q query, IDbTransaction tran = null, int? commandTimeout = null) where Q : QueryObject
        {
            query.Page.Index = 0;
            query.Page.Size = 1;
            return Search(conditionSql, query, tran, commandTimeout).FirstOrDefault();
        }
        /// <summary>
        /// 获取第一个满足条件的数据
        /// </summary>
        /// <typeparam name="Info"></typeparam>
        /// <param name="condition"></param>
        /// <param name="query"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public Info First<Info, Q>(string conditionSql, Q query, IDbTransaction tran = null, int? commandTimeout = null) where Q : QueryObject
        {
            T t = First(conditionSql, query, tran, commandTimeout);
            if (t == null)
            {
                return default;
            }
            return t.AutoMapper<Info>();
        }
        #endregion

        #region SQL
        /// <summary>
        /// 查询满足条件的数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<T> SearchSql(string sql = "", DynamicParameters parameters = null, int pageIndex = -1, int pageSize = -1, string order = "", IDbTransaction tran = null, int? commandTimeout = null)
        {
            // 检查参数
            if (string.IsNullOrWhiteSpace(sql))
            {
                sql = DefaultSearchSQL;
            }
            if (!string.IsNullOrWhiteSpace(order))
            {
                sql += $" order by {order}";
            }
            if (pageSize > -1)
            {
                sql += $" limit {pageSize}";
            }
            if (pageIndex > -1)
            {
                sql += $" offset {pageIndex * pageSize}";
            }

            // 获取数据库连接
            IDbConnection connection = tran != null ? tran.Connection : GetConnection();

            // 打开数据库连接
            ConnectionOpen(connection, tran);
            IEnumerable<T> result;
            try
            {
                result = connection.Query<T>(sql, parameters, transaction: tran, commandTimeout: commandTimeout);
            }
            catch (Exception ex)
            {
                throw new NpgOperationException(ex, $"按sql查询异常  \r\nsql：{sql}  \r\n页号：{pageIndex}  \r\n每页数据量：{pageSize}  \r\n查询参数：{parameters?.ToJson()}");
            }
            // 关闭数据库连接
            ConnectionClose(connection, tran);

            return result;
        }
        /// <summary>
        /// 查询满足条件的数据
        /// </summary>
        /// <typeparam name="Info"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="order"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<Info> SearchSql<Info>(string sql = "", DynamicParameters parameters = null, int pageIndex = -1, int pageSize = -1, string order = "", IDbTransaction tran = null, int? commandTimeout = null)
        {
            var data = SearchSql(sql, parameters, pageIndex, pageSize, order, tran, commandTimeout);
            if (data == null)
            {
                return default;
            }
            return data.AutoMapper<IEnumerable<Info>>();
        }
        /// <summary>
        /// 查询所有的数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="order"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<T> SearchAll(int pageIndex = -1, int pageSize = -1, string order = "", IDbTransaction tran = null, int? commandTimeout = null)
        {
            return SearchSql(DefaultSearchMainSQL, null, pageIndex, pageSize, order, tran, commandTimeout);
        }
        /// <summary>
        /// 查询所有的数据
        /// </summary>
        /// <typeparam name="Info"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="order"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<Info> SearchAll<Info>(int pageIndex = -1, int pageSize = -1, string order = "", IDbTransaction tran = null, int? commandTimeout = null)
        {
            var data = SearchAll(pageIndex, pageSize, order, tran, commandTimeout);
            if (data == null)
            {
                return default;
            }
            return data.AutoMapper<IEnumerable<Info>>();
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="conditionSql"></param>
        /// <param name="query"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<T> Search<Q>(string conditionSql, Q query, IDbTransaction tran = null, int? commandTimeout = null) where Q : QueryObject
        {
            string sql = $"{DefaultSearchSQL} {conditionSql}";

            string order = string.IsNullOrWhiteSpace(query.Sort.Property) ? "" : string.Format(" {0} {1}", query.Sort.Property, query.Sort.IsAsc ? "ASC" : "DESC");
            DynamicParameters param = new DynamicParameters();

            foreach (PropertyInfo p in (typeof(Q)).GetProperties())
            {
                if (p.Name == "Sort" || p.Name == "Page") continue;
                if (p.GetCustomAttributes(typeof(Newtonsoft.Json.JsonIgnoreAttribute), true).Length == 0)
                {
                    param.Add(p.Name, p.GetValue(query));
                }
            }

            return SearchSql(sql, param, query.Page.Index, query.Page.Size, order, tran, commandTimeout);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Info"></typeparam>
        /// <typeparam name="Q"></typeparam>
        /// <param name="conditionSql"></param>
        /// <param name="query"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<Info> Search<Info, Q>(string conditionSql, Q query, IDbTransaction tran = null, int? commandTimeout = null) where Q : QueryObject
        {
            var data = Search(conditionSql, query, tran, commandTimeout);
            if (data == null)
            {
                return default;
            }
            return data.AutoMapper<IEnumerable<Info>>();
        }
        #endregion

        #region Count
        /// <summary>
        /// 查询满足条件的记录数量
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public int CountSql(string sql = "", DynamicParameters parameters = null, IDbTransaction tran = null, int? commandTimeout = null)
        {
            // 检查参数
            if (string.IsNullOrWhiteSpace(sql))
            {
                sql = DefaultCountSQL;
            }

            // 获取数据库连接
            IDbConnection connection = tran != null ? tran.Connection : GetConnection();

            // 打开数据库连接
            ConnectionOpen(connection, tran);
            int count;
            try
            {
                count = connection.ExecuteScalar<int>(sql, parameters, transaction: tran, commandTimeout: commandTimeout);
            }
            catch (Exception ex)
            {
                throw new NpgOperationException(ex, $"按sql查询异常  \r\nsql：{sql}  \r\n查询参数：{parameters?.ToJson()}");
            }
            // 关闭数据库连接
            ConnectionClose(connection, tran);

            return count;
        }
        /// <summary>
        /// 查询满足条件的记录数量
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="conditionSql"></param>
        /// <param name="query"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public int Count<Q>(string conditionSql, Q query, IDbTransaction tran = null, int? commandTimeout = null) where Q : QueryObject
        {
            string sql = $"{DefaultCountSQL} {conditionSql}";

            DynamicParameters param = new DynamicParameters();
            foreach (PropertyInfo p in (typeof(Q)).GetProperties())
            {
                if (p.Name == "Sort" || p.Name == "Page") continue;
                if (p.GetCustomAttributes(typeof(Newtonsoft.Json.JsonIgnoreAttribute), true).Length == 0)
                {
                    param.Add(p.Name, p.GetValue(query));
                }
            }

            return CountSql(sql, param, tran, commandTimeout);
        }
        #endregion
    }
}
