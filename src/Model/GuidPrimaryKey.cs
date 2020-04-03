using Newtonsoft.Json;
using System;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// Guid主键
    /// </summary>
    public class GuidPrimaryKey : IPrimaryKey<Guid>
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid id { get; set; }

        /// <summary>
        /// 获取ID的字符串格式
        /// </summary>
        [JsonIgnore]
        public string IdString
        {
            get
            {
                return id.ToString();
            }
        }
        /// <summary>
        /// 判断Id是否为空
        /// </summary>
        /// <returns></returns>
        [JsonIgnore]
        public bool IdEmpty
        {
            get
            {
                return !CheckId(this.id);
            }
        }
        /// <summary>
        /// 检查id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckId(Guid id)
        {
            return id != Guid.Empty;
        }
        /// <summary>
        /// 设置对象ID。如果传入的ID无效，返回false
        /// </summary>
        /// <param name="strId"></param>
        /// <returns></returns>
        public bool SetId(string strId)
        {
            if (!string.IsNullOrWhiteSpace(strId))
            {
                return false;
            }
            this.id = new Guid(strId);
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strId"></param>
        /// <returns></returns>
        public Guid ConvertID(string strId)
        {
            return new Guid(strId);
        }

    }
}
