using DvBCrud.EFCore.API.JSON;
using System;
using System.Collections.Generic;
using System.Text;

namespace DvBCrud.EFCore.API.Mocks.JSONControllers
{
    public interface IAnyReadOnlyController : IReadOnlyController<int>
    {
    }
}
