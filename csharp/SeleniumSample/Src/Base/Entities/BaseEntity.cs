using System.Globalization;
using System.Text.Json.Serialization;

namespace Base.Entities
{
    public class BaseEntity
    {
        public void SetValues(Dictionary<string, object> values)
        {
            foreach (KeyValuePair<string, object> value in values)
            {
                var targetProperty = this.GetType().GetProperty(value.Key);
                if (targetProperty is not null)
                {
                    targetProperty.SetValue(this, value.Value);
                }
            }
        }

    }
}