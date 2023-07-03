using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DvBCrud.Shared.Entities;

namespace DvBCrud.EFCore.Tests.Mocks;

[ExcludeFromCodeCoverage]
public record AnyEntity : IEntity<string>, ICreatedAt, IModifiedAt  // Using string type for Id to test nullable cases
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = "";

    public DateTimeOffset CreatedAt { get; set; }
        
    public DateTimeOffset ModifiedAt { get; set; }
        
    public string? AnyString { get; set; }
}