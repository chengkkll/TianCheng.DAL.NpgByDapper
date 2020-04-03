using AutoMapper;
using System;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// AutoMapper的扩展处理
    /// </summary>
    public static class AutoMapperExtension
    {
        /// <summary>
        /// 用于转换的Mapper
        /// </summary>
        static public IMapper Mapper;

        static private MapperConfiguration Configuration;

        /// <summary>
        /// 初始化对象映射关系
        /// </summary>
        static public void RegisterAutoMapper()
        {
            var cfg = new AutoMapper.Configuration.MapperConfigurationExpression();

            foreach (Type proType in AssemblyProvider.GetTypeByInterface<IDBAutoProfile>())
            {
                cfg.AddProfile(proType);
            }

            cfg.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention(); // 命名是小写并包含下划线
            cfg.DestinationMemberNamingConvention = new PascalCaseNamingConvention(); // 帕斯卡命名规则

            Configuration = new MapperConfiguration(cfg);
            Mapper = Configuration.CreateMapper();
        }

        /// <summary>
        /// 检验 Mapper 是否正确
        /// </summary>
        public static void AssertMapper()
        {
            try
            {
                Configuration.AssertConfigurationIsValid();
            }
            catch (Exception ex)
            {
                TianCheng.Log.CommonLog.Logger.Information(ex.Message);
            }
        }

        /// <summary>
        /// 将对像转换成指定的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="info"></param>
        /// <returns></returns>
        public static T AutoMapper<T>(this object info)
        {
            return Mapper.Map<T>(info);
        }
    }
}
