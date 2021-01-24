using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DvBCrud.EFCore.API.JSON
{
    public interface IReadOnlyController<TId>
    {
        [HttpGet, Route("{id}")]
        IActionResult Read(TId id);

        [HttpGet]
        IActionResult ReadAll();
    }
}
