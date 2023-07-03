using DvBCrud.API.Permissions;
using FluentAssertions;
using Xunit;

namespace DvBCrud.API.Tests.Permissions
{
    public class CrudActionExtensionsTests
    {
        [Fact]
        public void IsActionAllowed_AllActionsAllowed_AllActionsAllowed()
        {
            const CrudActions crudActions = CrudActions.All;
            crudActions.IsActionAllowed(CrudActions.Create).Should().BeTrue();
            crudActions.IsActionAllowed(CrudActions.Read).Should().BeTrue();
            crudActions.IsActionAllowed(CrudActions.Update).Should().BeTrue();
            crudActions.IsActionAllowed(CrudActions.Delete).Should().BeTrue();
        }

        [Fact]
        public void IsActionAllowed_ReadOnlyActions_OnlyReadAllowed()
        {
            const CrudActions crudActions = CrudActions.Read;
            crudActions.IsActionAllowed(CrudActions.Create).Should().BeFalse();
            crudActions.IsActionAllowed(CrudActions.Read).Should().BeTrue();
            crudActions.IsActionAllowed(CrudActions.Update).Should().BeFalse();
            crudActions.IsActionAllowed(CrudActions.Delete).Should().BeFalse();
        }

        [Fact]
        public void IsActionAllowed_NoDelete_OnlyDeleteForbidden()
        {
            const CrudActions crudActions = CrudActions.Create | CrudActions.Read | CrudActions.Update;
            crudActions.IsActionAllowed(CrudActions.Create).Should().BeTrue();
            crudActions.IsActionAllowed(CrudActions.Read).Should().BeTrue();
            crudActions.IsActionAllowed(CrudActions.Update).Should().BeTrue();
            crudActions.IsActionAllowed(CrudActions.Delete).Should().BeFalse();
        }

        [Fact]
        public void IsActionAllowed_NoActionAllowed_AllActionsForbidden()
        {
            const CrudActions crudActions = CrudActions.None;
            crudActions.IsActionAllowed(CrudActions.Create).Should().BeFalse();
            crudActions.IsActionAllowed(CrudActions.Read).Should().BeFalse();
            crudActions.IsActionAllowed(CrudActions.Update).Should().BeFalse();
            crudActions.IsActionAllowed(CrudActions.Delete).Should().BeFalse();
        }
    }
}
