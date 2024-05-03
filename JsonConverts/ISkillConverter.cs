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
    public class ISkillConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(ISkill).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);

            string name = obj["Name"].ToObject<string>();
            switch (name)
            {
                case "파이어 볼":
                    return new FireBall();
                case "파워 스트라이크":
                    return new PowerStrike();
                default:
                    throw new JsonSerializationException($"스킬 없음: {name}");
            }
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
