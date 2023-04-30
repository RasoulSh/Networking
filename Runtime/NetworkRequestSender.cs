using System;
using System.Collections.Generic;
using Networking.Entities;
using UnityEngine.Networking;
using UnityEngine;

namespace Networking
{
    public class NetworkRequestSender<TE> where TE : NetworkEntity
    {
        private readonly INetworkRequest request;
        private static RequestSenderHelper _senderHelper;
        private static RequestSenderHelper senderHelper => _senderHelper ??=
            new GameObject("NetworkRequestSender").AddComponent<RequestSenderHelper>();

        public NetworkRequestSender(INetworkRequest request)
        {
            this.request = request;
        }

        public void Request(Action<NetworkResponse<TE>> callback)
        { ;
            senderHelper.Send(request, callback);
        }
    }
}