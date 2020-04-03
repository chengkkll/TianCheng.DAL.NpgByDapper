using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// 数据库连接信息
    /// </summary>
    public class ConnectionProvider
    {
        /// <summary>
        /// 配置文件中的连接配置节点名称
        /// </summary>
        static internal readonly string ConfigSectionName = "TianCheng.DB";

        static internal string connectionString = "";
        /// <summary>
        /// 数据库连接串
        /// </summary>
        static public string ConnectionString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    SetConnection(BuildConfiguration());
                }
                return connectionString;
            }
        }

        static private TianChengDBOptions options = null;
        /// <summary>
        /// 获取配置信息
        /// </summary>
        static internal TianChengDBOptions Options
        {
            get
            {
                if (options == null)
                {
                    SetConnection(BuildConfiguration());
                }
                return options;
            }
        }

        /// <summary>
        /// 设置数据库连接串
        /// </summary>
        /// <param name="connection"></param>
        static public void SetConnection(string connection)
        {
            connectionString = connection;
        }
        

        /// <summary>
        /// 设置数据库连接串
        /// </summary>
        /// <param name="config"></param>
        static public void SetConnection(IConfiguration config)
        {
            // todo : 连接信息做成数组形式。本打算能一个应用同时操作多个数据库。但是考虑微服务的发展，以后可能不会有太多类似的情况。所以暂时不做此功能。
            options = config.GetSection(ConfigSectionName).Get<TianChengDBOptions>();
            
            if (options == null)
            {
                throw new NpgConfigurationException("无法找到数据库配置信息");
            }

            var connList = options.Connection;
            if (connList == null || connList.Count() == 0)
            {
                connectionString = string.Empty;
                return;
            }
            connectionString = connList.FirstOrDefault().ConnectionString;
        }

        /// <summary>
        /// 创建一份配置信息
        /// </summary>
        /// <returns></returns>
        public static IConfiguration BuildConfiguration()
        {
            var config = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory);
            // 配置文件：
            // web : 开发使用 appsettings.Development.json 部署使用 appsettings.Production.json
            // 控制台: 使用db.config.json
            if (System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "db.config.json").Count() > 0)
            {
                config.AddJsonFile("db.config.json", optional: false, reloadOnChange: true);
            }
            if (System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), "appsettings.json").Count() > 0)
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            }
            string envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Machine);
            if (string.IsNullOrWhiteSpace(envName))
            {
                envName = "Development";
            }
            string envFile = $"appsettings.{envName}.json";
            if (System.IO.Directory.GetFiles(System.IO.Directory.GetCurrentDirectory(), envFile).Count() > 0)
            {
                config.AddJsonFile(envFile, optional: false, reloadOnChange: true);
            }

            return config.AddEnvironmentVariables().Build();
        }
    }
}
