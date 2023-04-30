using System;
using System.Collections.Generic;
using Networking.Entities;
using UnityEngine.Networking;
using UnityEngine;

namespace Networking
{
    public interface INetworkRequestSender
    {
        void Request<TE>(NetworkRequest request, Action<NetworkResponse<TE>> callback) where TE : NetworkEntity;
    }
}