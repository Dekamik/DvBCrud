using DvBCrud.EFCore.Mocks.DbContexts;
using DvBCrud.EFCore.Mocks.Entities;
using DvBCrud.EFCore.Mocks.Repositories;
using FakeItEasy;
using FluentAssertions;
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

        public RepositoryTests()
        {
            logger = A.Fake<ILogger>();
        }

        [Fact]
        public void Create_AnyEntity_EntityCreated()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Create_AnyEntity_EntityCreated));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var expected = new AnyEntity
            {
                AnyString = "AnyString"
            };

            repository.Create(expected);
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Single().AnyString.Should().Be(expected.AnyString);
        }

        [Fact]
        public void Create_ExistingEntity_ThrowsArgumentException()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Create_ExistingEntity_ThrowsArgumentException));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            dbContextProvider.Mock(entity);

            repository.Create(entity);

            dbContextProvider.DbContext.Invoking(db => db.SaveChanges()).Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Create_Null_ThrowsArgumentNullException()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Create_Null_ThrowsArgumentNullException));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);

            repository.Invoking(r => r.Create(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Update_ExistingEntity_EntityUpdatedAsync()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Update_ExistingEntity_EntityUpdatedAsync));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            dbContextProvider.Mock(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            var expected = new AnyEntity
            {
                AnyString = "AnyNewString"
            };

            repository.Update(1, expected);
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities.Single(e => e.Id == 1);
            actual.AnyString.Should().BeEquivalentTo(expected.AnyString);
        }

        [Fact]
        public void Update_NonExistingEntity_ThrowsKeyNotFoundException()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Update_NonExistingEntity_ThrowsKeyNotFoundException));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            dbContextProvider.Mock(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            var updatedEntity = new AnyEntity
            {
                AnyString = "AnyNewString"
            };

            repository.Invoking(r => r.Update(2, updatedEntity)).Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void Update_Null_ThrowsArgumentNullException()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Update_Null_ThrowsArgumentNullException));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);

            repository.Invoking(r => r.Update(1, null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Update_NullId_ThrowsArgumentNullException()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Update_NullId_ThrowsArgumentNullException));
            var repository = new AnyNullableIdRepository(dbContextProvider.DbContext, logger);
            var updatedEntity = new AnyNullableIdEntity
            {
                AnyString = "AnyNewString"
            };

            repository.Invoking(r => r.Update(null, updatedEntity)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateAsync_ExistingEntity_EntityUpdatedAsync()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(UpdateAsync_ExistingEntity_EntityUpdatedAsync));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            dbContextProvider.Mock(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            var expected = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyNewString"
            };

            await repository.UpdateAsync(1, expected);
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities.Single(e => e.Id == 1);
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateAsync_NonExistingEntity_ThrowsKeyNotFoundException()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Update_NonExistingEntity_ThrowsKeyNotFoundException));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            dbContextProvider.Mock(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            var updatedEntity = new AnyEntity
            {
                AnyString = "AnyNewString"
            };

            await repository.Awaiting(r => r.UpdateAsync(2, updatedEntity)).Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task UpdateAsync_Null_ThrowsArgumentNullException()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(UpdateAsync_Null_ThrowsArgumentNullException));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);

            await repository.Awaiting(r => r.UpdateAsync(1, null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateAsync_NullId_ThrowsArgumentNullException()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(UpdateAsync_NullId_ThrowsArgumentNullException));
            var repository = new AnyNullableIdRepository(dbContextProvider.DbContext, logger);
            var updatedEntity = new AnyNullableIdEntity
            {
                AnyString = "AnyNewString"
            };

            await repository.Awaiting(r => r.UpdateAsync(null, updatedEntity)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public void Delete_ExistingEntity_EntityDeleted()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Delete_ExistingEntity_EntityDeleted));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
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
            dbContextProvider.Mock(entities);

            repository.Delete(1);
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Single().Should().BeEquivalentTo(entities.Last());
        }

        [Fact]
        public void Delete_Null_ThrowsArgumentNullException()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(Delete_Null_ThrowsArgumentNullException));
            var repository = new AnyNullableIdRepository(dbContextProvider.DbContext, logger);

            repository.Invoking(r => r.Delete(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Delete_NonExistingId_ThrowsKeyNotFoundException()
        {
            // Arrange
            using var dbContextProvider = new AnyDbContextProvider(nameof(Delete_NonExistingId_ThrowsKeyNotFoundException));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);

            // Act & Assert
            repository.Invoking(r => r.Delete(1)).Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public async Task DeleteAsync_ExistingEntity_EntityDeleted()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(DeleteAsync_ExistingEntity_EntityDeleted));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
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
            dbContextProvider.Mock(entities);

            await repository.DeleteAsync(1);
            dbContextProvider.DbContext.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Single().Should().BeEquivalentTo(entities.Last());
        }

        [Fact]
        public async Task DeleteAsync_Null_ThrowsArgumentNullException()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(DeleteAsync_Null_ThrowsArgumentNullException));
            var repository = new AnyNullableIdRepository(dbContextProvider.DbContext, logger);

            await repository.Awaiting(r => r.DeleteAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task DeleteAsync_NonExistingId_ThrowsKeyNotFoundException()
        {
            // Arrange
            using var dbContextProvider = new AnyDbContextProvider(nameof(DeleteAsync_NonExistingId_ThrowsKeyNotFoundException));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);

            // Act and Assert
            await repository.Awaiting(r => r.DeleteAsync(1)).Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public void SaveChanges_CallAfterAdd_ChangesSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChanges_CallAfterAdd_ChangesSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var expected = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };

            dbContextProvider.DbContext.AnyEntities.Add(expected);
            repository.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Single().Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void SaveChanges_NoCallAfterAdd_ChangesNotSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChanges_NoCallAfterAdd_ChangesNotSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };

            dbContextProvider.DbContext.AnyEntities.Add(entity);

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Should().BeEmpty();
        }

        [Fact]
        public void SaveChanges_CallAfterAddRange_ChangesSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChanges_CallAfterAddRange_ChangesSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var expected = new[]
            {
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

            dbContextProvider.DbContext.AnyEntities.AddRange(expected);
            repository.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void SaveChanges_NoCallAfterAddRange_ChangesNotSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChanges_NoCallAfterAddRange_ChangesNotSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var entities = new[]
            {
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

            dbContextProvider.DbContext.AnyEntities.AddRange(entities);

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Should().BeEmpty();
        }

        [Fact]
        public void SaveChanges_CallAfterModify_ChangesSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChanges_CallAfterModify_ChangesSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            dbContextProvider.Mock(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            var modifiedEntity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyNewString"
            };

            dbContextProvider.DbContext.AnyEntities.Find(modifiedEntity.Id).AnyString = modifiedEntity.AnyString;
            repository.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Single().Should().BeEquivalentTo(modifiedEntity);
        }

        [Fact]
        public void SaveChanges_NoCallAfterModify_ChangesSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChanges_NoCallAfterModify_ChangesSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            dbContextProvider.Mock(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            var modifiedEntity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyNewString"
            };

            dbContextProvider.DbContext.AnyEntities.Find(modifiedEntity.Id).AnyString = modifiedEntity.AnyString;

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Single().Should().BeEquivalentTo(modifiedEntity);
        }

        [Fact]
        public void SaveChanges_CallAfterRemove_ChangesSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChanges_CallAfterRemove_ChangesSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            dbContextProvider.Mock(entity);

            dbContextProvider.DbContext.AnyEntities.Remove(entity);
            repository.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Should().BeEmpty();
        }

        [Fact]
        public void SaveChanges_NoCallAfterRemove_ChangesNotSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChanges_NoCallAfterRemove_ChangesNotSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            dbContextProvider.Mock(entity);

            dbContextProvider.DbContext.AnyEntities.Remove(entity);

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Should().Contain(entity);
        }

        [Fact]
        public void SaveChanges_CallAfterRemoveRange_ChangesSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChanges_CallAfterRemoveRange_ChangesSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
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
            dbContextProvider.Mock(entities);

            dbContextProvider.DbContext.AnyEntities.RemoveRange(entities);
            repository.SaveChanges();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Should().BeEmpty();
        }

        [Fact]
        public void SaveChanges_NoCallAfterRemoveRange_ChangesNotSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChanges_NoCallAfterRemoveRange_ChangesNotSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
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
            dbContextProvider.Mock(entities);

            dbContextProvider.DbContext.AnyEntities.RemoveRange(entities);

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Should().BeEquivalentTo(entities);
        }

        [Fact]
        public async Task SaveChangesAsync_CallAfterAdd_ChangesSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChangesAsync_CallAfterAdd_ChangesSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var expected = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };

            dbContextProvider.DbContext.AnyEntities.Add(expected);
            await repository.SaveChangesAsync();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Single().Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void SaveChangesAsync_NoCallAfterAdd_ChangesNotSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChangesAsync_NoCallAfterAdd_ChangesNotSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };

            dbContextProvider.DbContext.AnyEntities.Add(entity);

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Should().BeEmpty();
        }

        [Fact]
        public async Task SaveChangesAsync_CallAfterAddRange_ChangesSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChangesAsync_CallAfterAddRange_ChangesSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var expected = new[]
            {
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

            dbContextProvider.DbContext.AnyEntities.AddRange(expected);
            await repository.SaveChangesAsync();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void SaveChangesAsync_NoCallAfterAddRange_ChangesNotSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChangesAsync_NoCallAfterAddRange_ChangesNotSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var entities = new[]
            {
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

            dbContextProvider.DbContext.AnyEntities.AddRange(entities);

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Should().BeEmpty();
        }

        [Fact]
        public async Task SaveChangesAsync_CallAfterModify_ChangesSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChangesAsync_CallAfterModify_ChangesSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            dbContextProvider.Mock(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            var modifiedEntity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyNewString"
            };

            dbContextProvider.DbContext.AnyEntities.Find(modifiedEntity.Id).AnyString = modifiedEntity.AnyString;
            await repository.SaveChangesAsync();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Single().Should().BeEquivalentTo(modifiedEntity);
        }

        [Fact]
        public void SaveChangesAsync_NoCallAfterModify_ChangesSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChangesAsync_NoCallAfterModify_ChangesSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            dbContextProvider.Mock(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            var modifiedEntity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyNewString"
            };

            dbContextProvider.DbContext.AnyEntities.Find(modifiedEntity.Id).AnyString = modifiedEntity.AnyString;

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Single().Should().BeEquivalentTo(modifiedEntity);
        }

        [Fact]
        public async Task SaveChangesAsync_CallAfterRemove_ChangesSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChangesAsync_CallAfterRemove_ChangesSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            dbContextProvider.Mock(entity);

            dbContextProvider.DbContext.AnyEntities.Remove(entity);
            await repository.SaveChangesAsync();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Should().BeEmpty();
        }

        [Fact]
        public void SaveChangesAsync_NoCallAfterRemove_ChangesNotSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChangesAsync_NoCallAfterRemove_ChangesNotSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
            var entity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            };
            dbContextProvider.Mock(entity);

            dbContextProvider.DbContext.AnyEntities.Remove(entity);

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Should().Contain(entity);
        }

        [Fact]
        public async Task SaveChangesAsync_CallAfterRemoveRange_ChangesSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChangesAsync_CallAfterRemoveRange_ChangesSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
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
            dbContextProvider.Mock(entities);

            dbContextProvider.DbContext.AnyEntities.RemoveRange(entities);
            await repository.SaveChangesAsync();

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Should().BeEmpty();
        }

        [Fact]
        public void SaveChangesAsync_NoCallAfterRemoveRange_ChangesNotSaved()
        {
            using var dbContextProvider = new AnyDbContextProvider(nameof(SaveChangesAsync_NoCallAfterRemoveRange_ChangesNotSaved));
            var repository = new AnyRepository(dbContextProvider.DbContext, logger);
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
            dbContextProvider.Mock(entities);

            dbContextProvider.DbContext.AnyEntities.RemoveRange(entities);

            var actual = dbContextProvider.DbContext.AnyEntities;
            actual.Should().BeEquivalentTo(entities);
        }
    }
}
