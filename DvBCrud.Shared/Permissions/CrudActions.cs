namespace DvBCrud.Shared.Permissions;

[Flags]
public enum CrudActions
{
    None = 0x00,
    Create = 0x01,
    ReadById = 0x02,
    Update = 0x04,
    Delete = 0x08,
    List = 0x10,
    ReadOnly = ReadById | List,
    All = Create | ReadById | Update | Delete | List,
}
