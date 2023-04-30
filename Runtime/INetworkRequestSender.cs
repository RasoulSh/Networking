using System;
using System.Collections.Generic;
using Networking.Entities;
using UnityEngine.Networking;
using UnityEngine;

namespace Networking
{
    public interface INetworkRequestSender<TE> where TE : NetworkEntity
    {
        void Request(Action<NetworkResponse<TE>> callback);
    }
}