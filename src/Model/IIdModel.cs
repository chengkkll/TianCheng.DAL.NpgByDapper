using Newtonsoft.Json;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// 数据库id列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDbId<T>
    {
        /// <summary>
        /// 数据库id
        /// </summary>
        T id { get; set; }

        /// <summary>
        /// 获取ID的字符串格式
        /// </summary>
        string IdString { get; }

        /// <summary>
        /// 判断ID是否为空
        /// </summary>
        /// <returns></returns>
        bool IdEmpty { get; }

        /// <summary>
        /// 检查指定ID是否正确
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckId(T id);

        /// <summary>
        /// 设置对象ID，如果传入的ID无效，返回false
        /// </summary>
        /// <param name="strId"></param>
        /// <returns></returns>
        bool SetId(string strId);
        /// <summary>
        /// 转化id
        /// </summary>
        /// <param name="strId"></param>
        /// <returns></returns>
        T ConvertID(string strId);
    }
}
