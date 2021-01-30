using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DvBCrud.EFCore.Tests.Repositories
{
    public class RepositoryTests
    {
        private readonly ILogger logger;
        private readonly AnyDbContext dbContext;
        private readonly IAnyRepository repository;

        public RepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AnyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            dbContext = new AnyDbContext(options);
            logger = A.Fake<ILogger>();
            repository = new AnyRepository(dbContext, logger);
        }

        [Fact]
        public void GetAll_Default_ReturnsAll()
        {
            // Arrange
            var expected = new[]
            {
                new AnyEntity
                {
                    Id = 1,
                    AnyString = "Any"
                },
                new AnyEntity
                {
                    Id = 2,
                    AnyString = "Any"
                }
            };
            dbContext.AnyEntities.AddRange(expected);
            dbContext.SaveChanges();

            // Act
            var actual = repository.GetAll();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void Get_ExistingId_ReturnsEntity()
        {
            // Arrange
            var expected = new[]
            {
                new AnyEntity
                {
                    Id = 1,
                    AnyString = "Any"
                },
                new AnyEntity {
                    Id = 2,
                    AnyString = "Any"
                }
            };
            dbContext.AnyEntities.AddRange(expected);
            dbContext.SaveChanges();

            // Act
            var actual = repository.Get(1);

            // Assert
            actual.Should().BeEquivalentTo(expected.First());
        }

        [Fact]
        public void Get_NonExistingId_ReturnsNull()
        {
            // Arrange
            var expected = new[]
            {
                new AnyEntity
                {
                    Id = 1,
                    AnyString = "Any"
                },
                new AnyEntity {
                    Id = 2,
                    AnyString = "Any"
                }
            };
            dbContext.AnyEntities.AddRange(expected);
            dbContext.SaveChanges();

            // Act
            var actual = repository.Get(3);

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public void Get_Null_ThrowsArgumentNullException()
        {
            // Arrange
            var nullableRepository = new AnyNullableIdRepository(dbContext, logger);

            // Act & Assert
            nullableRepository.Invoking(r => r.Get(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task GetAsync_ExistingId_ReturnsEntity()
        {
            // Arrange
            var expected = new[]
            {
                new AnyEntity
                {
                    Id = 1,
                    AnyString = "Any"
                },
                new AnyEntity {
                    Id = 2,
                    AnyString = "Any"
                }
            };
            dbContext.AnyEntities.AddRange(expected);
            dbContext.SaveChanges();

            // Act
            var actual = await repository.GetAsync(1);

            // Assert
            actual.Should().BeEquivalentTo(expected.First());
        }

        [Fact]
        public async Task GetAsync_NonExistingId_ReturnsNull()
        {
            // Arrange
            var expected = new[]
            {
                new AnyEntity
                {
                    Id = 1,
                    AnyString = "Any"
                },
                new AnyEntity {
                    Id = 2,
                    AnyString = "Any"
                }
            };
            dbContext.AnyEntities.AddRange(expected);
            dbContext.SaveChanges();

            // Act
            var actual = await repository.GetAsync(3);

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public async Task GetAsync_Null_ThrowsArgumentNullException()
        {
            // Arrange
            var nullableRepository = new AnyNullableIdRepository(dbContext, logger);

            // Act & Assert
            await nullableRepository.Awaiting(r => r.GetAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public void Create_AnyEntity_EntityCreated()
        {
            // Arrange
            var expected = new AnyEntity
            {
                AnyString = "AnyString"
            };

            // Act
            repository.Create(expected);
            dbContext.SaveChanges();

            // Assert
            dbContext.AnyEntities.First().AnyString.Should().Be(expected.AnyString);
        }

        [Fact]
        public void Create_ExistingEntity_ThrowsArgumentException()
        {
            // Arrange
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            dbContext.AnyEntities.Add(entity);
            dbContext.SaveChanges();

            // Act
            repository.Create(entity);

            // Assert
            dbContext.Invoking(db => db.SaveChanges()).Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Create_Null_ThrowsArgumentNullException()
        {
            repository.Invoking(r => r.Create(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Update_ExistingEntity_EntityUpdatedAsync()
        {
            // Arrange
            dbContext.AnyEntities.Add(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            dbContext.SaveChanges();
            var expected = new AnyEntity
            {
                AnyString = "AnyNewString"
            };

            // Act
            repository.Update(1, expected);
            dbContext.SaveChanges();

            // Assert
            dbContext.AnyEntities.First(e => e.Id == 1).AnyString.Should().BeEquivalentTo(expected.AnyString);
        }

        [Fact]
        public void Update_NonExistingEntity_ThrowsKeyNotFoundException()
        {
            // Arrange
            dbContext.AnyEntities.Add(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            dbContext.SaveChanges();
            var updatedEntity = new AnyEntity
            {
                AnyString = "AnyNewString"
            };

            // Act & Assert
            repository.Invoking(r => r.Update(2, updatedEntity)).Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void Update_Null_ThrowsArgumentNullException()
        {
            repository.Invoking(r => r.Update(1, null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Update_NullId_ThrowsArgumentNullException()
        {
            // Arrange
            var nullableRepository = new AnyNullableIdRepository(dbContext, logger);
            var updatedEntity = new AnyNullableIdEntity
            {
                AnyString = "AnyNewString"
            };

            // Act & Assert
            nullableRepository.Invoking(r => r.Update(null, updatedEntity)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateAsync_ExistingEntity_EntityUpdatedAsync()
        {
            // Arrange
            dbContext.AnyEntities.Add(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            dbContext.SaveChanges();
            var expected = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyNewString"
            };

            // Act
            await repository.UpdateAsync(1, expected);
            dbContext.SaveChanges();

            // Assert
            dbContext.AnyEntities.First(e => e.Id == 1).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateAsync_NonExistingEntity_ThrowsKeyNotFoundException()
        {
            // Arrange
            dbContext.AnyEntities.Add(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            dbContext.SaveChanges();
            var updatedEntity = new AnyEntity
            {
                AnyString = "AnyNewString"
            };

            // Act & Assert
            await repository.Awaiting(r => r.UpdateAsync(2, updatedEntity)).Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task UpdateAsync_Null_ThrowsArgumentNullException()
        {
            await repository.Awaiting(r => r.UpdateAsync(1, null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateAsync_NullId_ThrowsArgumentNullException()
        {
            // Arrange
            var repository = new AnyNullableIdRepository(dbContext, logger);
            var updatedEntity = new AnyNullableIdEntity
            {
                AnyString = "AnyNewString"
            };

            // Act & Assert
            await repository.Awaiting(r => r.UpdateAsync(null, updatedEntity)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public void Delete_ExistingEntity_EntityDeleted()
        {
            // Arrange
            var entities = new[] {
                new AnyEntity
                {
                    Id = 1,
                    AnyString = "AnyString"
                },
                new AnyEntity
                {
                    Id = 2,
                    AnyString = "AnyString"
                }
            };
            dbContext.AnyEntities.AddRange(entities);
            dbContext.SaveChanges();
            dbContext.AnyEntities.Should().Contain(entities);

            // Act
            repository.Delete(1);
            dbContext.SaveChanges();

            // Assert
            dbContext.AnyEntities.First().Should().BeEquivalentTo(entities.Last());
        }

        [Fact]
        public void Delete_Null_ThrowsArgumentNullException()
        {
            // Arrange
            var repository = new AnyNullableIdRepository(dbContext, logger);

            // Act & Assert
            repository.Invoking(r => r.Delete(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Delete_NonExistingId_ThrowsKeyNotFoundException()
        {
            repository.Invoking(r => r.Delete(1)).Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public async Task DeleteAsync_ExistingEntity_EntityDeleted()
        {
            // Arrange
            var entities = new[] {
                new AnyEntity
                {
                    Id = 1,
                    AnyString = "AnyString"
                },
                new AnyEntity
                {
                    Id = 2,
                    AnyString = "AnyString"
                }
            };
            dbContext.AnyEntities.AddRange(entities);
            dbContext.SaveChanges();
            dbContext.AnyEntities.Should().Contain(entities);

            // Act
            await repository.DeleteAsync(1);
            dbContext.SaveChanges();

            // Assert
            dbContext.AnyEntities.First().Should().BeEquivalentTo(entities.Last());
        }

        [Fact]
        public async Task DeleteAsync_Null_ThrowsArgumentNullException()
        {
            // Arrange
            var nullableRepository = new AnyNullableIdRepository(dbContext, logger);

            // Act & Assert
            await nullableRepository.Awaiting(r => r.DeleteAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task DeleteAsync_NonExistingId_ThrowsKeyNotFoundException()
        {
            await repository.Awaiting(r => r.DeleteAsync(1)).Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public void SaveChanges_CallAfterAdd_ChangesSaved()
        {
            // Arrange
            var expected = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };

            // Act
            dbContext.AnyEntities.Add(expected);
            repository.SaveChanges();

            // Assert
            dbContext.AnyEntities.First().Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void SaveChanges_NoCallAfterAdd_ChangesNotSaved()
        {
            // Arrange
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };

            // Act
            dbContext.AnyEntities.Add(entity);

            // Assert
            dbContext.AnyEntities.Should().BeEmpty();
        }

        [Fact]
        public void SaveChanges_CallAfterModify_ChangesSaved()
        {
            // Arrange
            dbContext.Add(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            dbContext.SaveChanges();
            var modifiedEntity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyNewString"
            };

            // Act
            dbContext.AnyEntities.Find(modifiedEntity.Id).AnyString = modifiedEntity.AnyString;
            repository.SaveChanges();

            // Assert
            dbContext.AnyEntities.First().Should().BeEquivalentTo(modifiedEntity);
        }

        [Fact]
        public void SaveChanges_NoCallAfterModify_ChangesSaved()
        {
            // Arrange
            dbContext.Add(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            dbContext.SaveChanges();
            var modifiedEntity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyNewString"
            };

            // Act
            dbContext.AnyEntities.Find(modifiedEntity.Id).AnyString = modifiedEntity.AnyString;

            // Assert
            dbContext.AnyEntities.First().Should().BeEquivalentTo(modifiedEntity);
        }

        [Fact]
        public void SaveChanges_CallAfterRemove_ChangesSaved()
        {
            // Arrange
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            dbContext.Add(entity);
            dbContext.SaveChanges();
            dbContext.AnyEntities.Should().Contain(entity);

            // Act
            dbContext.AnyEntities.Remove(entity);
            repository.SaveChanges();

            // Assert
            dbContext.AnyEntities.Should().BeEmpty();
        }

        [Fact]
        public void SaveChanges_NoCallAfterRemove_ChangesNotSaved()
        {
            // Arrange
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            dbContext.Add(entity);
            dbContext.SaveChanges();
            dbContext.AnyEntities.Should().Contain(entity);

            // Act
            dbContext.AnyEntities.Remove(entity);

            // Assert
            dbContext.AnyEntities.Should().Contain(entity);
        }

        [Fact]
        public async Task SaveChangesAsync_CallAfterAdd_ChangesSaved()
        {
            // Arrange
            var expected = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };

            // Act
            dbContext.AnyEntities.Add(expected);
            await repository.SaveChangesAsync();

            // Assert
            dbContext.AnyEntities.First().Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void SaveChangesAsync_NoCallAfterAdd_ChangesNotSaved()
        {
            // Arrange
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };

            // Act
            dbContext.AnyEntities.Add(entity);

            // Assert
            dbContext.AnyEntities.Should().BeEmpty();
        }

        [Fact]
        public async Task SaveChangesAsync_CallAfterModify_ChangesSaved()
        {
            // Arrange
            dbContext.Add(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            dbContext.SaveChanges();
            var modifiedEntity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyNewString"
            };

            // Act
            dbContext.AnyEntities.Find(modifiedEntity.Id).AnyString = modifiedEntity.AnyString;
            await repository.SaveChangesAsync();

            // Assert
            dbContext.AnyEntities.First().Should().BeEquivalentTo(modifiedEntity);
        }

        [Fact]
        public void SaveChangesAsync_NoCallAfterModify_ChangesSaved()
        {
            // Arrange
            dbContext.Add(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            dbContext.SaveChanges();
            var modifiedEntity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyNewString"
            };

            // Act
            dbContext.AnyEntities.Find(modifiedEntity.Id).AnyString = modifiedEntity.AnyString;

            // Assert
            dbContext.AnyEntities.First().Should().BeEquivalentTo(modifiedEntity);
        }

        [Fact]
        public async Task SaveChangesAsync_CallAfterRemove_ChangesSaved()
        {
            // Arrange
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            dbContext.Add(entity);
            dbContext.SaveChanges();
            dbContext.AnyEntities.Should().Contain(entity);

            // Act
            dbContext.AnyEntities.Remove(entity);
            await repository.SaveChangesAsync();

            // Assert
            dbContext.AnyEntities.Should().BeEmpty();
        }

        [Fact]
        public void SaveChangesAsync_NoCallAfterRemove_ChangesNotSaved()
        {
            // Arrange
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            dbContext.Add(entity);
            dbContext.SaveChanges();
            dbContext.AnyEntities.Should().Contain(entity);

            // Act
            dbContext.AnyEntities.Remove(entity);

            // Assert
            dbContext.AnyEntities.Should().Contain(entity);
        }
    }
}
