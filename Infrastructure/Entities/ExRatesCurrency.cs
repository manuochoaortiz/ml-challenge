using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Infrastructure.Entities
{
    public class ExRatesCurrency
    {

        [JsonProperty("")]
        public Dictionary<string, object> subratings { get; set; }
}
}
