using TianCheng.DAL.NpgByDapper;
using WebDemo.Model;

namespace WebDemo.DAL.Mapper
{
    /// <summary>
    /// 持久化对象与数据领域对象的转换
    /// </summary>
    public class DALMapperProfile : AutoMapper.Profile, IDBAutoProfile
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public DALMapperProfile()
        {
            Register();
        }
        /// <summary>
        /// 注册需要自动AutoMapper的对象信息
        /// </summary>
        public void Register()
        {
            //时间与字符串的处理
            CreateMap<MockGuidPO, MockGuidInfo>();
            CreateMap<MockGuidInfo, MockGuidPO>();
        }
    }
}
