using DvBCrud.Shared.Permissions;
using FluentAssertions;

namespace DvBCrud.Tests.UnitTests.Shared.Permissions;

public class CrudActionExtensionsTests
{
    [Fact]
    public void IsActionAllowed_AllActionsAllowed_AllActionsAllowed()
    {
        const CrudActions crudActions = CrudActions.All;
        crudActions.IsActionAllowed(CrudActions.Create).Should().BeTrue();
        crudActions.IsActionAllowed(CrudActions.ReadById).Should().BeTrue();
        crudActions.IsActionAllowed(CrudActions.Update).Should().BeTrue();
        crudActions.IsActionAllowed(CrudActions.Delete).Should().BeTrue();
        crudActions.IsActionAllowed(CrudActions.List).Should().BeTrue();
    }

    [Fact]
    public void IsActionAllowed_ReadOne_OnlyReadAllowed()
    {
        const CrudActions crudActions = CrudActions.ReadById;
        crudActions.IsActionAllowed(CrudActions.Create).Should().BeFalse();
        crudActions.IsActionAllowed(CrudActions.ReadById).Should().BeTrue();
        crudActions.IsActionAllowed(CrudActions.Update).Should().BeFalse();
        crudActions.IsActionAllowed(CrudActions.Delete).Should().BeFalse();
        crudActions.IsActionAllowed(CrudActions.List).Should().BeFalse();
    }

    [Fact]
    public void IsActionAllowed_NoDelete_OnlyDeleteForbidden()
    {
        const CrudActions crudActions = CrudActions.Create | CrudActions.ReadById | CrudActions.Update;
        crudActions.IsActionAllowed(CrudActions.Create).Should().BeTrue();
        crudActions.IsActionAllowed(CrudActions.ReadById).Should().BeTrue();
        crudActions.IsActionAllowed(CrudActions.Update).Should().BeTrue();
        crudActions.IsActionAllowed(CrudActions.Delete).Should().BeFalse();
        crudActions.IsActionAllowed(CrudActions.List).Should().BeFalse();
    }

    [Fact]
    public void IsActionAllowed_NoActionAllowed_AllActionsForbidden()
    {
        const CrudActions crudActions = CrudActions.None;
        crudActions.IsActionAllowed(CrudActions.Create).Should().BeFalse();
        crudActions.IsActionAllowed(CrudActions.ReadById).Should().BeFalse();
        crudActions.IsActionAllowed(CrudActions.Update).Should().BeFalse();
        crudActions.IsActionAllowed(CrudActions.Delete).Should().BeFalse();
        crudActions.IsActionAllowed(CrudActions.List).Should().BeFalse();
    }

    [Fact]
    public void IsActionAllowed_ReadOnlyAllowed_ReadOneAndListAllowed()
    {
        const CrudActions crudActions = CrudActions.ReadOnly;
        crudActions.IsActionAllowed(CrudActions.Create).Should().BeFalse();
        crudActions.IsActionAllowed(CrudActions.ReadById).Should().BeTrue();
        crudActions.IsActionAllowed(CrudActions.Update).Should().BeFalse();
        crudActions.IsActionAllowed(CrudActions.Delete).Should().BeFalse();
        crudActions.IsActionAllowed(CrudActions.List).Should().BeTrue();
    }
}