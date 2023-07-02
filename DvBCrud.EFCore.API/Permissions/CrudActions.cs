using System;

namespace DvBCrud.EFCore.API.Permissions;

[Flags]
public enum CrudActions
{
    None = 0b_0000,
    Create = 0b_0001,
    Read = 0b_0010,
    Update = 0b_0100,
    Delete = 0b_1000,
    All = Create | Read | Update | Delete
}