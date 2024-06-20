using IcqApp.Extensions;
using IcqApp.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IcqApp.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();
            if (!resultContext.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            var userId = resultContext.HttpContext.User.GetUserId();

            var repository = resultContext.HttpContext.RequestServices.GetRequiredService<IUnitOfWork>();
            var user = await repository.UserRepository.GetUserByIdAsync(userId);
            user.LastActive = DateTime.UtcNow;
            await repository.Complete();

        }
    }
}
