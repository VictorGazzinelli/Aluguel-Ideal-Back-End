using AluguelIdeal.Application.Dtos.Residences;
using AluguelIdeal.Application.Dtos.Residences.Flats;
using AluguelIdeal.Application.Dtos.Residences.Houses;
using AluguelIdeal.Application.Interactors.Common;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AluguelIdeal.Api.Options.Mvc
{
    public class QueryResultResidenceDtoJsonConverter : JsonConverter<QueryResult<ResidenceDto>>
    {
        private readonly JsonConverter<ResidenceDto> residenceDtoJsonConverter;
        public QueryResultResidenceDtoJsonConverter(JsonConverter<ResidenceDto> residenceDtoJsonConverter)
        {
            this.residenceDtoJsonConverter = residenceDtoJsonConverter;
        }

        public override bool CanConvert(Type type)
            => type == typeof(QueryResult<ResidenceDto>);

        public override QueryResult<ResidenceDto> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, QueryResult<ResidenceDto> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName(nameof(QueryResult<ResidenceDto>.Items));
            writer.WriteStartArray();
            foreach(var residence in value.Items)
                residenceDtoJsonConverter.Write(writer, residence, options);
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}
