using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame.JsonConverts
{
    public class IJobConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IJob).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            string jobType = token.ToObject<string>();

            switch (jobType)
            {
                case "전사":
                    return new Warrior();
                case "마법사":
                    return new Mage();
                default:
                    throw new JsonSerializationException($"존재하지 않는 직업입니다: {jobType}");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
