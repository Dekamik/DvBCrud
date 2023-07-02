using DvBCrud.EFCore.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DvBCrud.EFCore.API.Filters;

public class NotFoundExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is NotFoundException)
        {
            context.Result = new NotFoundResult();
        }
    }
}