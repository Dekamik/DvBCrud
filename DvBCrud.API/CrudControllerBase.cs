using Microsoft.AspNetCore.Mvc;

namespace DvBCrud.API;

/// <summary>
/// Internal class for shared methods.
/// </summary>
/// <typeparam name="TApiModel">Api model type</typeparam>
public abstract class CrudControllerBase<TApiModel> : ControllerBase
{
    protected ObjectResult NotAllowed(string method) => StatusCode(405, $"{method} not allowed on {typeof(TApiModel).Name}");
}