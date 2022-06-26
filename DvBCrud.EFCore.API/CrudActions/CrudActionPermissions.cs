namespace DvBCrud.EFCore.API.CrudActions
{
    public class CrudActionPermissions
    {
        private readonly CrudAction[]? _allowedActions;

        public CrudActionPermissions()
        {
            _allowedActions = null;
        }

        public CrudActionPermissions(params CrudAction[]? allowedActions)
        {
            _allowedActions = allowedActions;
        }
    }
}
