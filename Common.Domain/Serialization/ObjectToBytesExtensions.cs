using System.Text;

namespace Common.Domain.Serialization
{
    public static class ObjectToBytesExtensions
    {

        public static byte[] ToBytes(this object value)
        {

            var resultJson = System.Text.Json.JsonSerializer.Serialize(value);
            var resultBytes = Encoding.UTF8.GetBytes(resultJson);

            return resultBytes;

        }

        public static object ToObject(this byte[] value)
        {

            string resultJson = Encoding.UTF8.GetString(value);
            var resultObject = System.Text.Json.JsonSerializer.Deserialize<object>(resultJson);
            return resultObject;

        }

        public static T ToType<T>(this byte[] value)
        {
            if (value.IsNull())
                return default(T);

            string resultJson = Encoding.UTF8.GetString(value);
            var resultObject = System.Text.Json.JsonSerializer.Deserialize<T>(resultJson);
            return resultObject;

        }

    }
}
