using System;
using System.Collections;
using Networking.Entities;
using UnityEngine;
using UnityEngine.Networking;

namespace Networking
{
    public class RequestSenderHelper : MonoBehaviour
    {
        public void Send<TE>(INetworkRequest request, Action<NetworkResponse<TE>> callback) where TE : NetworkEntity
        {
            StartCoroutine(SendRoutine(request, callback));
        }

        private IEnumerator SendRoutine<TE>(INetworkRequest request, Action<NetworkResponse<TE>> callback) where TE : NetworkEntity
        {
            using UnityWebRequest webRequest = request.ToWebRequest();
            webRequest.SendWebRequest();
            while (!webRequest.isDone)
                yield return null;
            callback?.Invoke(NetworkResponse<TE>.FromWebRequest(webRequest));
        }
    }
}