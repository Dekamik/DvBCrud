using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DvBCrud.EFCore.Mocks.Repositories
{
    public interface IAnyRepository : IRepository<AnyEntity, int>
    {
    }
}
