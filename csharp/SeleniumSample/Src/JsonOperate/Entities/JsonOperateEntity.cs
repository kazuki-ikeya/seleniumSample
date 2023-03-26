using System.Globalization;
using System.Text.Json.Serialization;
using Base.Entities;

namespace JsonOperate.Entities
{

    public class JsonOperateEntity : BaseEntity
    {
        public JsonOperateEntity()
        {
            this.DataList = new List<JsonOperateDataEntity>();
        }

        [JsonPropertyName("dataList")] public List<JsonOperateDataEntity> DataList { get; set; }
    }

}