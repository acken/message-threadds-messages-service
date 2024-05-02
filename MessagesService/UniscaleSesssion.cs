using Microsoft.Extensions.Logging;
using Uniscale.Core;
using Uniscale.Core.Session.Platform;
using Uniscale.Designtime;

namespace MessagesService
{
    public class UniscaleSession
    {
        private readonly ILogger _logger; 
        private object _initLock = new object();
        private PlatformSession? _session;
        private ForwardingSession? _forwardingSession;

        private readonly Messages.InterceptorHandler _messagesInterceptorHandler;

        public UniscaleSession(
            ILoggerFactory loggerFactory, 
            Messages.InterceptorHandler messagesInterceptorHandler
        ) {
            _logger = loggerFactory.CreateLogger<UniscaleSession>();
            _messagesInterceptorHandler = messagesInterceptorHandler;
        }

        public async Task<PlatformSession> GetPlatformSession()
        {
            if (_session == null) {
                await initialize();
            }
            return _session!;
        }

        public async Task<ForwardingSession> GetForwardingSession()
        {
            if (_forwardingSession == null) {
                await initialize();
            }
            return _forwardingSession!;
        }

        private Task initialize() {
            lock (_initLock) {
                if (_session != null) {
                    return Task.CompletedTask;
                }
                var sessionBuilder = Platform.Builder();

                // Prepare forwading session. This session is used for outgoing requests from this
                // service to other services.
                var forwardingTask = sessionBuilder.ForwardingSessionBuilder(Guid.NewGuid())
                    .InspectRequests((input, ctx) => {
                        _logger.LogDebug($"Outgoing request {ctx.FeatureId} with transaction id {ctx.TransactionId}");
                    })
                    .WithInterceptors(i => {
                        // Wire up forwaded service to service calls towards other services here
                    })
                    .Build();
                forwardingTask.Wait();
                _forwardingSession = forwardingTask.Result;

                // Prepare handler session. This session is used for incoming Endpoint requests handled by
                // this service.
                var sessionTask = Platform.Builder()
                    .InspectRequests((input, ctx) => {
                        _logger.LogDebug($"Incoming request {ctx.FeatureId} with transaction id {ctx.TransactionId}");
                    })
                    .InspectResponses((output, input, ctx) => {
                        if (!output.Success) {
                            _logger.LogError($"Incoming request to {ctx.FeatureId} with transaction id {ctx.TransactionId} failed: {output.Error.ToLongString()}");
                        }
                    })
                    .WithInterceptors(i => {
                        _messagesInterceptorHandler.Setup(i, _forwardingSession);
                    })
                    .Build();
                sessionTask.Wait();
                _session = sessionTask.Result;
                return Task.CompletedTask;
            } 
        }
    }
}