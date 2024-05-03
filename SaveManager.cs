using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpartaConsoleGame
{
    public static class SaveManager
    {
        const string DIR_NAME = "data";
        public static void SaveData<T>(string fileName, T data)
        {
            string localFilePath = Path.Combine(DIR_NAME, $"{fileName}.json");

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);
            if (!Directory.Exists(DIR_NAME))
            {
                Directory.CreateDirectory(DIR_NAME);
            }
            File.WriteAllText(localFilePath, json);
        }

        public static T LoadData<T>(string fileName, JsonSerializerSettings jsonSetting = null)
        {
            string localFilePath = Path.Combine(DIR_NAME, $"{fileName}.json");
            if (File.Exists(localFilePath))
            {
                string json = File.ReadAllText(localFilePath);
                // var settings = new JsonSerializerSettings
                // {
                //     Converters = { new IJobConverter(), new IEnemyConverter() },
                //     TypeNameHandling = TypeNameHandling.Auto
                // };
                if (json != null)
                {
                    if (jsonSetting == null)
                    {
                        return JsonConvert.DeserializeObject<T>(json);
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<T>(json, jsonSetting);
                    }
                }

            }
            return default(T);
        }
    }
}