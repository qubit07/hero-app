namespace IcqApp.Middleware
{
    public class ExceptionMiddleware
    {
        public RequestDelegate _next { get; }
        public ILogger<ExceptionMiddleware> _logger { get; }
        public IHostEnvironment _env { get; }

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync()
        {

        }


    }
}
