using Networking.Enums;
using Newtonsoft.Json;

namespace Networking.Entities
{
    public abstract record NetworkEntity
    {
        [JsonIgnore] public abstract ResponseStatus Status { get; }
        [JsonIgnore] public abstract string[] Errors { get; }
    }
}