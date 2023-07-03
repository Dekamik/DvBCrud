using System.Diagnostics.CodeAnalysis;
using DvBCrud.API.Permissions;
using DvBCrud.Shared;

namespace DvBCrud.API.Tests.Mocks.Controllers;

[ExcludeFromCodeCoverage]
[AllowedActions(CrudActions.Create | CrudActions.Update)]
public class AnyCreateUpdateController : CrudController<string, AnyModel, IRepository<string, AnyModel>>
{
    public AnyCreateUpdateController(IRepository<string, AnyModel> repository) : base(repository)
    {
    }
}