using NpgsqlTypes;
using Serilog;
using Serilog.Sinks.PostgreSQL;
using System.Collections.Generic;
using System.Net;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// PostgreSql 的日志操作
    /// </summary>
    public class NpgLog
    {
        /// <summary>
        /// 日志操作对象
        /// </summary>
        static private ILogger _Logger = null;
        /// <summary>
        /// 日志操作
        /// </summary>
        static public ILogger Logger
        {
            get
            {
                if (_Logger == null)
                {
                    // 初始化日志对象
                    _Logger = Configuration.CreateLogger();
                }
                return _Logger;
            }
        }
        public static readonly string FileFormat = "Logs/TianCheng.NpgDB-{Date}.txt";

        /// <summary>
        /// 记录日志的表名
        /// </summary>
        private static readonly string LogTableName = "system_logs";

        private static readonly IDictionary<string, ColumnWriterBase> ColumnWriters = new Dictionary<string, ColumnWriterBase>
        {
            {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
            {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
            {"level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
            {"raise_date", new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
            {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
            {"properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
            {"props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
            {"machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") }
        };

        /// <summary>
        /// 默认的发送邮箱账号
        /// </summary>
        private static readonly ICredentialsByHost NetworkCredential = new NetworkCredential("tianchengok2019@163.com", "1234qwer");
        /// <summary>
        /// 接受邮件账号
        /// </summary>
        static private string toEmail = "17814198@qq.com";

        /// <summary>
        /// 设置接受邮件的账号
        /// </summary>
        static public string ToEmail
        {
            set
            {
                toEmail = value;
                _Logger = Configuration.CreateLogger();
            }
        }

        /// <summary>
        /// 日志的配置信息
        /// </summary>
        private static LoggerConfiguration Configuration
        {
            get
            {
                //Sets size of all BIT and BIT VARYING columns to 20
                TableCreator.DefaultBitColumnsLength = 20;

                //Sets size of all CHAR columns to 30
                TableCreator.DefaultCharColumnsLength = 30;

                //Sets size of all VARCHAR columns to 50
                TableCreator.DefaultVarcharColumnsLength = 50;

                return new LoggerConfiguration()
                        .WriteTo.Console(Serilog.Events.LogEventLevel.Warning)
                        .WriteTo.Debug()
                        .WriteTo.RollingFile(FileFormat, Serilog.Events.LogEventLevel.Warning)
                        .WriteTo.PostgreSQL(ConnectionProvider.ConnectionString, LogTableName, ColumnWriters)
                        .WriteTo.Email(
                            fromEmail: "tianchengok2019@163.com",
                            toEmail: toEmail,
                            mailServer: "smtp.163.com",
                            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error,
                            networkCredential: NetworkCredential,
                            outputTemplate: "[{Level}] {NewLine}{Message} {NewLine}{Exception}",
                            mailSubject: "系统错误-提醒邮件");
            }
        }
    }
}
