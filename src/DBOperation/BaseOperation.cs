using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// PostgreSql数据库操作处理
    /// </summary>
    public partial class BaseOperation<T, IdType>
    {
        #region 构造
        /// <summary>
        /// 数据库连接串
        /// </summary>
        static internal string ConnectionString = ConnectionProvider.ConnectionString;

        /// <summary>
        /// 表名  可通过派生类重写
        /// todo : 通过派生类特性来设置
        /// </summary>
        protected virtual string TableName { get; set; }

        /// <summary>
        /// 记录当前对象的所有属性列表
        /// </summary>
        protected IList<PropertyInfo> PropertyList = new List<PropertyInfo>();

        /// <summary>
        /// 默认插入SQL
        /// </summary>
        protected string DefaultInsertSQL { get; set; }
        /// <summary>
        /// 默认插入SQL  不包含Id的插入
        /// </summary>
        protected string DefaultInsertNotIdSQL { get; set; }
        /// <summary>
        /// 默认更新SQL
        /// </summary>
        protected string DefaultUpdateSQL { get; set; }
        /// <summary>
        /// 按ID查询对象
        /// </summary>
        protected string DefaultSelectByIdSQL { get; set; }
        /// <summary>
        /// 默认删除记录SQL
        /// </summary>
        protected string DefaultRemoveByIdSQL { get; set; }
        /// <summary>
        /// 默认逻辑删除SQL
        /// </summary>
        protected string DefaultDeleteByIdSQL { get; set; }
        /// <summary>
        /// 恢复逻辑删除SQL
        /// </summary>
        protected string DefaultUndeleteByIdSQL { get; set; }
        /// <summary>
        /// 默认查询SQL
        /// </summary>
        protected string DefaultSearchMainSQL { get; set; }
        /// <summary>
        /// 默认查询SQL
        /// </summary>
        protected string DefaultSearchSQL { get; set; }
        /// <summary>
        /// 默认Count的SQL
        /// </summary>
        protected string DefaultCountSQL { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        public BaseOperation()
        {
            // 检查数据
            if (string.IsNullOrWhiteSpace(TableName))
            {
                throw new Exception("请指定表名");
            }

            // 生成一些常用的数据
            foreach (PropertyInfo p in (typeof(T)).GetProperties())
            {
                if (p.GetCustomAttributes(typeof(Newtonsoft.Json.JsonIgnoreAttribute), true).Length == 0)
                {
                    PropertyList.Add(p);
                }
            }

            InitDefaultInsertSQL();
            InitDefaultUpdateSQL();
            DefaultSelectByIdSQL = $"select * from {TableName} where id=@id";
            DefaultRemoveByIdSQL = $"delete from {TableName} where id=@id";
            DefaultDeleteByIdSQL = $"update {TableName} set is_delete=true where id=@id";
            DefaultUndeleteByIdSQL = $"delete from {TableName} where id=@id";
            DefaultSearchMainSQL = $"select * from {TableName}";
            DefaultSearchSQL = $"select * from {TableName} where 1=1";
            DefaultCountSQL = $"select count(*) from {TableName} where 1=1";
        }
        #endregion

        #region 连接 & 事务
        /// <summary>
        /// 获取一个事务
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        public IDbTransaction ConnTran(out IDbConnection connection)
        {
            connection = GetConnection();
            ConnectionOpen(connection, null);
            return connection.BeginTransaction();
        }
        /// <summary>
        /// 关闭事务
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="tran"></param>
        public void CloseTran(IDbConnection connection, IDbTransaction tran)
        {
            if (tran != null)
            {
                tran.Dispose();
            }
            if (connection != null)
            {
                connection.Close();
            }
        }
        /// <summary>
        /// 获取一个数据库连接
        /// </summary>
        /// <returns></returns>
        protected IDbConnection GetConnection()
        {
            return new NpgsqlConnection(ConnectionString);
        }
        /// <summary>
        /// 打开链接
        /// </summary>
        /// <param name="connection"></param>
        public void ConnectionOpen(IDbConnection connection, IDbTransaction tran)
        {
            if (tran != null)
            {
                return;
            }
            if (connection.State == ConnectionState.Closed)
            {
                try
                {
                    connection.Open();
                }
                catch (Exception te)
                {
                    NpgLog.Logger.Warning(te, $"数据库链接超时。链接字符串：{ConnectionString}");
                    throw;
                }
            }
        }
        /// <summary>
        /// 关闭链接
        /// </summary>
        /// <param name="connection"></param>
        public void ConnectionClose(IDbConnection connection, IDbTransaction tran)
        {
            if (tran == null && connection != null)
            {
                connection.Close();
            }
        }
        #endregion

        #region 工具
        /// <summary>
        /// 仅用于id操作的一个实例
        /// </summary>
        private static readonly IPrimaryKey<IdType> IdInstance = new T();
        #endregion
    }

}
