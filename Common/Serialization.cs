using Newtonsoft.Json;

namespace Common
{
    public static class Serialization
    {
        public static T Deserialize<T>(this string toDeserialize)
        {
            return JsonConvert.DeserializeObject<T>(toDeserialize);
        }

        public static string Serialize<T>(this T toSerialize)
        {
            return JsonConvert.SerializeObject(toSerialize);
        }
    }
}
