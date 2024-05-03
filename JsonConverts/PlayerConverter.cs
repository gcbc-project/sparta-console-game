using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpartaConsoleGame.Skill;

namespace SpartaConsoleGame.JsonConverts
{
    public class PlayerConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Player).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);

            string jobLabel = obj["JobLabel"].ToObject<string>();
            switch (jobLabel)
            {
                case "전사":
                    return JsonConvert.DeserializeObject<Warrior>(obj.ToString(), new ISkillConverter());
                case "마법사":
                    return JsonConvert.DeserializeObject<Mage>(obj.ToString(), new ISkillConverter());
                default:
                    throw new JsonSerializationException($"직업 없음: {jobLabel}");
            }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
