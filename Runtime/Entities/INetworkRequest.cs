using System.Collections.Generic;
using Networking.Enums;
using UnityEngine.Networking;

namespace Networking.Entities
{
    public interface INetworkRequest
    {
        string Url { get;}
        Dictionary<string, string> RequestHeaders { get; }
        Dictionary<string, object> RequestBody { get; }
        RequestMethods RequestMethod { get; }
        public int Timeout { get; }

        UnityWebRequest ToWebRequest();
    }
}