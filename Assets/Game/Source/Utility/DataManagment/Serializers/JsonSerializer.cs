using Newtonsoft.Json;

namespace Game.Utility.DataManagment.Serializers
{
    public class JsonSerializer : IDataSerializer
    {
        private readonly JsonSerializerSettings settings;

        public JsonSerializer(Formatting formatting)
        {
            settings = new JsonSerializerSettings
            {
                Formatting = formatting,
                TypeNameHandling = TypeNameHandling.Auto,
            };
        }

        public TData Deserialize<TData>(string serializedData)
        {
            return JsonConvert.DeserializeObject<TData>(serializedData, settings);
        }

        public string Serialize<TData>(TData data)
        {
            return JsonConvert.SerializeObject(data, settings);
        }
    }
}