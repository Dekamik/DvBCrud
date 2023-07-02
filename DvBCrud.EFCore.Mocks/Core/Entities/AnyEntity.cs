using System.Diagnostics.CodeAnalysis;
using DvBCrud.EFCore.Entities;

namespace DvBCrud.EFCore.Mocks.Core.Entities
{
    [ExcludeFromCodeCoverage]
    public class AnyEntity : IEntity<string>, ICreatedAt, IModifiedAt  // Using string type for Id to test nullable cases
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        
        public string AnyString { get; set; }
    }
}
