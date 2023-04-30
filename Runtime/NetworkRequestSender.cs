using System;
using System.Collections;
using Networking.Entities;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace Networking
{
    public class NetworkRequestSender : MonoBehaviour, INetworkRequestSender
    {
        public void Request<TE>(NetworkRequest request, Action<NetworkResponse<TE>> callback) where TE : NetworkEntity
        {
            StartCoroutine(SendRoutine(request, callback));
        }

        private IEnumerator SendRoutine<TE>(NetworkRequest request, Action<NetworkResponse<TE>> callback) where TE : NetworkEntity
        {
            using UnityWebRequest webRequest = request.ToWebRequest();
            webRequest.SendWebRequest();
            while (!webRequest.isDone)
                yield return null;
            callback?.Invoke(NetworkResponse<TE>.FromWebRequest(webRequest));
        }
    }
}