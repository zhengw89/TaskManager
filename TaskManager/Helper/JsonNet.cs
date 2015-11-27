using Newtonsoft.Json;

namespace TaskManager.Helper
{
    /// <summary>
    /// 解析Json格式封装公用方法
    /// </summary>
    internal static class JsonNet
    {
        /// <summary>
        /// 将实体对象转换成Json字符串 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public static string SerializeToString(object item, int? depth = null)
        {
            //return JsonConvert.SerializeObject(item);
            return JsonConvert.SerializeObject(item, Formatting.None,
              new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, MaxDepth = depth });
        }

        /// <summary>
        /// 将Json字符串转换成实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T DeserializeToEntity<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}