using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using TianCheng.DAL.NpgByDapper;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 注册数据库服务操作  webapi使用
    /// </summary>
    static public class DALConfigureServices
    {
        /// <summary>
        /// 增加PostgreSQL数据库操作
        /// </summary>
        /// <param name="services"></param>
        public static void AddTianChengPostgres(this IServiceCollection services)
        {
            // 设置数据库配置信息
            var config = services.BuildServiceProvider().GetService<IConfiguration>();
            ConnectionProvider.SetConnection(config);

            // 设置AutoMapper映射信息
            AutoMapperExtension.RegisterAutoMapper();

            // 注册数据库访问操作
            foreach (Type type in AssemblyProvider.GetTypeByInterface<IDBOperationRegister>())
            {
                if (type.GetTypeInfo().IsClass)
                    services.AddSingleton(type);
            }
        }
    }
}
