using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Runtime.Serialization;


namespace QTP.Infra
{
    [DataContract]
    public class SecList
    { 
        [DataMember]
        public List<SecItem> data {get;set;}

        public SecList() 

        {

            data = new List<SecItem>(); 

        } 
    }

    [DataContract]
    public class SecItem
    {
        [DataMember(Order = 0)]
        public string exchange { get; set; }

        [DataMember(Order = 1)]
        public string sec_id { get; set; }

        [DataMember(Order = 2)]
        public string sec_name { get; set; }
    }

    /// <summary>
    /// 解析JSON，仿Javascript风格
    /// </summary>
    public static class JSON
    {

        public static T parse<T>(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }

        public static string stringify(object jsonObject)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}