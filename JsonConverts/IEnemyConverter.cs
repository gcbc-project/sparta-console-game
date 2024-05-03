using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaConsoleGame.JsonConverts
{
    public class IEnemyConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IEnemy).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);

            string name = obj["Name"].ToObject<string>();
            switch (name)
            {
                case "미니언":
                    return new Minion();
                case "공허충":
                    return new Voidling();
                case "대포 미니언":
                    return new CannonMinion();
                default:
                    throw new JsonSerializationException($"존재하지 않는 적 입니다 : {name}");
            }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
