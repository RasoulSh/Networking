using System;
using Networking.Entities;

namespace Networking.RequestHandler
{
    public interface IRequestHandler
    {
        IRequestHandler SetNext(IRequestHandler handler);
        void Handle(Action<INetworkResponse> callback);
    }
}