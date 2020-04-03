using TianCheng.DAL.NpgByDapper;

namespace NpgBenchmark.Model
{
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
            CreateMap<MockGuidDB, MockGuidInfo>();
            CreateMap<MockGuidInfo, MockGuidDB>();

            CreateMap<MockGuidBase, MockGuidDB>();
            CreateMap<MockGuidBase, MockIntDB>();
        }
    } 
}
