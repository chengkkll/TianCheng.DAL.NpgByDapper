using AutoMapper;
using System;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// 基础信息的AutoMapper转换
    /// </summary>
    public class ModelProfile : Profile, IDBAutoProfile
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public ModelProfile()
        {
            Register();
        }
        /// <summary>
        /// 注册需要自动AutoMapper的对象信息
        /// </summary>
        public void Register()
        {
            //时间与字符串的处理
            CreateMap<string, DateTime>().ConvertUsing(new StringToDateTimeConverter());
            CreateMap<DateTime, string>().ConvertUsing(new DateTimeToStringConverter());
            CreateMap<string, DateTime?>().ConvertUsing(new StringToDateTimeNullConverter());
            CreateMap<DateTime?, string>().ConvertUsing(new DateTimeNullToStringConverter());
            CreateMap<string, Guid>().ConvertUsing(new StringToGuidConverter());
            CreateMap<Guid, string>().ConvertUsing(new GuidToStringConverter());
        }
    }
}
