using System.Text.Json;
using System.Text.Json.Serialization;

namespace GymTracer.Extensions
{
    public static class JsonSerializerExtension
    {
        public static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };
        public static T Deserialize<T>(this string json) where T : new()
        {
            return JsonSerializer.Deserialize<T>(json, DefaultJsonSerializerOptions) ?? new T();
        }

    }
}
