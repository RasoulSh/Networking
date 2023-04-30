using System;
using System.Linq;
using Networking.Enums;
using Newtonsoft.Json;

namespace Networking.Entities
{
    public record NetworkResponseEntity : NetworkEntity
    {
        public string status;
        public string[] errors;
        [JsonIgnore]
        public override ResponseStatus Status
        {
            get
            {
                if (string.IsNullOrEmpty(status))
                {
                    return Errors != null && errors.Any() ? ResponseStatus.Error : ResponseStatus.Success;
                }
                Enum.TryParse(typeof(ResponseStatus), status, true, out object result);
                return (ResponseStatus)result;
            }
        }

        [JsonIgnore] public override string[] Errors => errors;
    }
}