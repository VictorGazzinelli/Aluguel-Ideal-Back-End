using AluguelIdeal.Application.Dtos.Residences;
using AluguelIdeal.Application.Dtos.Residences.Flats;
using AluguelIdeal.Application.Dtos.Residences.Houses;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AluguelIdeal.Api.Options.Mvc
{
    public class ResidenceDtoJsonConverter : JsonConverter<ResidenceDto>
    {
        public override bool CanConvert(Type type) 
            => type == typeof(ResidenceDto);

        public override ResidenceDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, ResidenceDto value, JsonSerializerOptions options)
        {
            if (value is FlatDto flat)
            {
                JsonSerializer.Serialize(writer, flat, options);
            }
            if (value is HouseDto house)
            {
                JsonSerializer.Serialize(writer, house, options);
            }
        }
    }
}
