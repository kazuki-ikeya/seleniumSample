using System.Globalization;
using System.Text.Json.Serialization;
using Base.Entities;

namespace JsonOperate.Entities
{

    public class JsonOperateDataEntity : BaseEntity
    {
        public JsonOperateDataEntity()
        {
            this.Action = "";
            this.Url = "";
            this.CssSelector = "";
            this.ElementType = "";
            this.Text = "";
            this.IsChecked = false;
            this.Name = "";
            this.IsAdd = false;
            this.WaitSeconds = 0;
            this.Index = -1;
        }

        [JsonPropertyName("action")] public string Action { get; set; }
        [JsonPropertyName("url")] public string Url { get; set; }
        [JsonPropertyName("cssSelector")] public string CssSelector { get; set; }
        [JsonPropertyName("elementType")] public string ElementType { get; set; }
        [JsonPropertyName("text")] public string Text { get; set; }
        [JsonPropertyName("isChecked")] public bool IsChecked { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("isAdd")] public bool IsAdd { get; set; }
        [JsonPropertyName("waitSeconds")] public double WaitSeconds { get; set; }
        [JsonPropertyName("index")] public int Index { get; set; }
    }

}