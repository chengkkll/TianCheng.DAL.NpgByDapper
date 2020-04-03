using Newtonsoft.Json;

namespace TianCheng.DAL.NpgByDapper
{
    /// <summary>
    /// string主键
    /// </summary>
    public class StringPrimaryKey : IPrimaryKey<string>
    {
        /// <summary>
        /// Id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 获取ID的字符串格式
        /// </summary>
        [JsonIgnore]
        public string IdString
        {
            get
            {
                return id;
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
        public bool CheckId(string id)
        {
            return !string.IsNullOrWhiteSpace(id);
        }
        /// <summary>
        /// 设置对象ID。如果传入的ID无效，返回false
        /// </summary>
        /// <param name="strId"></param>
        /// <returns></returns>
        public bool SetId(string strId)
        {
            if (!CheckId(strId))
            {
                return false;
            }
            this.id = strId;
            return true;
        }

        /// <summary>
        /// 转化ID
        /// </summary>
        /// <param name="strId"></param>
        /// <returns></returns>
        public string ConvertID(string strId)
        {
            return strId;
        }
    }
}
