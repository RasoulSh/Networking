using System.Linq;
using System.Net;
using Networking.Enums;
using Networking.HeaderHelpers;
using UnityEngine;
using UnityEngine.Networking;

namespace Networking.Entities
{
    public class NetworkResponse<TE> : INetworkResponse where TE : NetworkEntity
    {
        public bool IsSuccessful { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public TE Data { get; set; }

        public static NetworkResponse<TE> FromWebRequest(UnityWebRequest webRequest)
        {
            var responseContentType = webRequest.GetResponseHeader(NetworkHeaderKeys.ContentType);
            var isJsonResponse = responseContentType == NetworkContentTypes.ApplicationJson;
            var data = isJsonResponse ? JsonUtility.FromJson<TE>(webRequest.downloadHandler.text) : null;
            return new NetworkResponse<TE>()
            {
                IsSuccessful = webRequest.result == UnityWebRequest.Result.Success &&
                    data.Status == ResponseStatus.Success,
                StatusCode = (HttpStatusCode)webRequest.responseCode,
                Message = data != null ? (data.Errors != null && data.Errors.Any() ? data.Errors[0] : webRequest.error) : webRequest.error,
                Data = data
            };
        }
    }
}