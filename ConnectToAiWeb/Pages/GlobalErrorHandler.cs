namespace ConnectToAiWeb.Pages
{
    using Microsoft.AspNetCore.Mvc.Filters;
    public class GlobalErrorHandler : IExceptionFilter
    {
        private readonly ErrorState _errorState;
        public GlobalErrorHandler(ErrorState errorState)
        {
            _errorState = errorState;
        }
        public void OnException(ExceptionContext context)
        {
            _errorState.ErrorMessage = context.Exception.Message;
        }
    }

}
