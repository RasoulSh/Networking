using System;
using Networking.Entities;

namespace Networking.RequestHandler
{
    public class RequestHandler<TE> : IRequestHandler where TE : NetworkEntity
    {
        private IRequestHandler nextHandler;
        private readonly INetworkRequest request;

        public RequestHandler(INetworkRequest request)
        {
            this.request = request;
        }

        public IRequestHandler SetNext(IRequestHandler handler)
        {
            nextHandler = handler;
            return handler;
        }

        public virtual void Handle(Action<INetworkResponse> callback)
        {
            new NetworkRequestSender<TE>(request).Request(
                requestResponse =>
                {
                    if (!requestResponse.IsSuccessful)
                    {
                        callback.Invoke(requestResponse);
                        return;
                    }
                    if (nextHandler == null)
                    {
                        callback.Invoke(requestResponse);
                        return;
                    }
                    nextHandler.Handle(nextResponse =>
                    {
                        if (nextResponse.IsSuccessful)
                        {
                            callback.Invoke(requestResponse);
                            return;
                        }
                        callback.Invoke(new NetworkResponse<TE>()
                        {
                            IsSuccessful = false,
                            Message = nextResponse.Message,
                            StatusCode = nextResponse.StatusCode
                        });
                    });
                });
        }
    }
}