using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BiostimeProcess.Service.AppService.Jsons
{
    public abstract class AbstractJsonService
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        protected AbstractJsonService()
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        protected string Serialize(object value)
        {
            string json = JsonConvert.SerializeObject(value, _jsonSerializerSettings);

            return json;
        }

        protected T Deserialize<T>(string json)
        {
            var value = JsonConvert.DeserializeObject<T>(json, _jsonSerializerSettings);

            return value;
        }
    }
}
