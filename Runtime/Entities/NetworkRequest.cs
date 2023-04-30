using System.Collections.Generic;
using System.Text;
using Networking.Enums;
using Networking.HeaderHelpers;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace Networking.Entities
{
    public class NetworkRequest : INetworkRequest
    {
        public string Url { get; set; }
        public Dictionary<string, string> RequestHeaders { get; set; } = new();
        [JsonProperty] public Dictionary<string, object> RequestBody { get; set; }
        public string GraphQlBody { get; set; }
        public RequestMethods RequestMethod { get; set; } = RequestMethods.Get;
        public int Timeout { get; set; }
        
        public UnityWebRequest ToWebRequest()
        {
            UnityWebRequest generatedRequest = null;
            generatedRequest = new UnityWebRequest(Url, RequestMethod.ToString());
            generatedRequest.downloadHandler = new DownloadHandlerBuffer();

            if (RequestMethod == RequestMethods.Post)
            {
                var postData = !string.IsNullOrEmpty(GraphQlBody) ? GraphQlBody : JsonConvert.SerializeObject(RequestBody);
                var uploadHandler = new UploadHandlerRaw(Encoding.ASCII.GetBytes(postData));
                uploadHandler.contentType = NetworkContentTypes.ApplicationJson;
                generatedRequest.uploadHandler = uploadHandler;
            }
            foreach (var header in RequestHeaders)
            {
                generatedRequest.SetRequestHeader(header.Key, header.Value);
            }
            
            generatedRequest.timeout = Timeout;
            return generatedRequest;
        }
    }
}