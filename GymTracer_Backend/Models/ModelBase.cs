using GymTracer.Extensions;

namespace GymTracer.models
{
    public abstract class ModelBase<T> where T : ModelBase<T>, new()
    {
        public static T Deserialize(string json)
        {
            return json.Deserialize<T>();
        }

        public static T Deserialize(dynamic json)
        {
            return Deserialize(json.ToString());
        }
    }
}
