using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Core.Model.Serialization
{
    /// <summary>
    /// Custom Json.Net JsonConvertor for Starship model.
    /// </summary>
    public class StarshipConvertor : JsonConverter<Starship>
    {
        public override Starship ReadJson(JsonReader reader, Type objectType, Starship existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Load(reader);
        }

        public override void WriteJson(JsonWriter writer, Starship value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        private Starship Load(JsonReader reader)
        {
            var jsonObject = JObject.Load(reader);
            int? interval = null;
            int maxSpeed = 0;
            IntervalUnit unit = CalculateDuration(jsonObject["consumables"].Value<string>(), out interval);

            return new Starship()
            {
                Name = jsonObject["name"].Value<string>(),
                MaximumSpeed = int.TryParse(jsonObject["MGLT"].Value<string>(), out maxSpeed) ? maxSpeed : (int?)null,
                ConsumablesInterval = interval,
                ConsumablesIntervalUnit = unit
            };
        }
        private IntervalUnit CalculateDuration(string duration, out int? durationValue)
        {
            var tokens = duration.Split(' ');

            int parsedValue = 0;
            if (int.TryParse(tokens[0], out parsedValue))
            {
                durationValue = parsedValue;
            }
            else
            {
                durationValue = null;
                return IntervalUnit.Unknown;
            }
           
            var intervalType = tokens[1].ToLower();

            switch (intervalType)
            {
                case "days":
                    return IntervalUnit.Day;
                case "weeks":
                case "week":
                    return IntervalUnit.Week;
                case "month":
                case "months":
                    return IntervalUnit.Month;
                case "year":
                case "years":
                    return IntervalUnit.Year;
                default:
                    return IntervalUnit.Unknown;
            }


        }

    }
}

