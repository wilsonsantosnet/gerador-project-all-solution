using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.API
{
    public class DatimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            return reader.TokenType switch
            {
                JsonTokenType.Number => reader.GetDateTime(),
                JsonTokenType.Null => default(DateTime),
                JsonTokenType.String => DateTime.TryParse(reader.GetString(), out var parsed) ? parsed : default(DateTime),
                _ => throw new ArgumentOutOfRangeException(nameof(reader), reader.TokenType,
                "Cannot parse unexpected JSON token type.")
            };
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {

            writer.WriteStringValue(value);

        }
    }

}
