using Microsoft.AspNetCore.Http;
using UOW.UoW;

namespace UOW.Middleware;

public class AutoSave(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;
    

    public async Task InvokeAsync(HttpContext context, IUnitOfWork unitOfWork)
    {
        await _next(context);
        await unitOfWork.CompleteAsync();
    }
}