using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Common.API
{
    public class StringJsonConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            return reader.TokenType switch
            {
                JsonTokenType.Number => reader.GetInt32(),
                JsonTokenType.Null => 0,
                JsonTokenType.String => int.TryParse(reader.GetString(), out var parsed) ? parsed : 0,
                _ => throw new ArgumentOutOfRangeException(nameof(reader), reader.TokenType, 
                "Cannot parse unexpected JSON token type.")
            };
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {

            writer.WriteNumberValue(value);

        }
    }
}
