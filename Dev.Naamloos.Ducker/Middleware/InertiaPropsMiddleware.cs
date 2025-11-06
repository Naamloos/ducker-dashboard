using Dev.Naamloos.Ducker.Database.Entities;
using Dev.Naamloos.Ducker.Dto;
using InertiaCore;
using Microsoft.AspNetCore.Identity;

namespace Dev.Naamloos.Ducker.Middleware
{
    public class InertiaPropsMiddleware
    {
        private readonly RequestDelegate _next;
        public InertiaPropsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userManager = context.RequestServices.GetRequiredService<UserManager<User>>();
            var user = await userManager.GetUserAsync(context.User);
            if (user is not null)
            {
                Inertia.Share("user", SafeUserDto.FromUser(user));
            }
            await _next(context);
            // After the request
            // You can modify the response here if needed
        }
    }
}
