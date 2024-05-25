using MediatR;
using Serilog.Context;
using SmScan.API.Common;

namespace SmScan.API.Abstractions.Behaviors;
internal sealed class RequestLoggingPipeLineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    private readonly ILogger<RequestLoggingPipeLineBehavior<TRequest, TResponse>> _logger;

    public RequestLoggingPipeLineBehavior(ILogger<RequestLoggingPipeLineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request,
                                        RequestHandlerDelegate<TResponse> next,
                                        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        _logger.LogInformation("Procesing request: {RequestName}", requestName);

        TResponse result = await next();

        if (result.IsSuccess)
        {
            _logger.LogInformation("Completed request {RequestName}", requestName);
        }
        else
        {
            using (LogContext.PushProperty("Error", result.Error, true))
            {
                _logger.LogWarning("Completed request {RequestName} with error", requestName);
            }
        }
        return result;
    }
}

