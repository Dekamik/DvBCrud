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
        private readonly AnyDbContext _dbContext;
        private readonly IAnyRepository _repository;

        public RepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AnyDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _dbContext = new AnyDbContext(options);
            var logger = A.Fake<ILogger<AnyRepository>>();
            _repository = new AnyRepository(_dbContext, logger);
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
            _dbContext.AnyEntities.AddRange(expected);
            _dbContext.SaveChanges();

            // Act
            var actual = _repository.GetAll();

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
            _dbContext.AnyEntities.AddRange(expected);
            _dbContext.SaveChanges();

            // Act
            var actual = _repository.Get(1);

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
            _dbContext.AnyEntities.AddRange(expected);
            _dbContext.SaveChanges();

            // Act
            var actual = _repository.Get(3);

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public void Get_Null_ThrowsArgumentNullException()
        {
            // Arrange
            var logger = A.Fake<ILogger<AnyNullableIdRepository>>();
            var nullableRepository = new AnyNullableIdRepository(_dbContext, logger);

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
            _dbContext.AnyEntities.AddRange(expected);
            _dbContext.SaveChanges();

            // Act
            var actual = await _repository.GetAsync(1);

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
            _dbContext.AnyEntities.AddRange(expected);
            _dbContext.SaveChanges();

            // Act
            var actual = await _repository.GetAsync(3);

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public async Task GetAsync_Null_ThrowsArgumentNullException()
        {
            // Arrange
            var logger = A.Fake<ILogger<AnyNullableIdRepository>>();
            var nullableRepository = new AnyNullableIdRepository(_dbContext, logger);

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
            _repository.Create(expected);
            _dbContext.SaveChanges();

            // Assert
            _dbContext.AnyEntities.First().AnyString.Should().Be(expected.AnyString);
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
            _dbContext.AnyEntities.Add(entity);
            _dbContext.SaveChanges();

            // Act
            _repository.Create(entity);

            // Assert
            _dbContext.Invoking(db => db.SaveChanges()).Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Create_Null_ThrowsArgumentNullException()
        {
            _repository.Invoking(r => r.Create(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Update_ExistingEntity_EntityUpdatedAsync()
        {
            // Arrange
            _dbContext.AnyEntities.Add(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            _dbContext.SaveChanges();
            var expected = new AnyEntity
            {
                AnyString = "AnyNewString"
            };

            // Act
            _repository.Update(1, expected);
            _dbContext.SaveChanges();

            // Assert
            _dbContext.AnyEntities.First(e => e.Id == 1).AnyString.Should().BeEquivalentTo(expected.AnyString);
        }

        [Fact]
        public void Update_NonExistingEntity_ThrowsKeyNotFoundException()
        {
            // Arrange
            _dbContext.AnyEntities.Add(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            _dbContext.SaveChanges();
            var updatedEntity = new AnyEntity
            {
                AnyString = "AnyNewString"
            };

            // Act & Assert
            _repository.Invoking(r => r.Update(2, updatedEntity)).Should().Throw<KeyNotFoundException>();
        }

        [Fact]
        public void Update_Null_ThrowsArgumentNullException()
        {
            _repository.Invoking(r => r.Update(1, null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Update_NullId_ThrowsArgumentNullException()
        {
            // Arrange
            var logger = A.Fake<ILogger<AnyNullableIdRepository>>();
            var nullableRepository = new AnyNullableIdRepository(_dbContext, logger);
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
            _dbContext.AnyEntities.Add(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            _dbContext.SaveChanges();
            var expected = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyNewString"
            };

            // Act
            await _repository.UpdateAsync(1, expected);
            _dbContext.SaveChanges();

            // Assert
            _dbContext.AnyEntities.First(e => e.Id == 1).Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task UpdateAsync_NonExistingEntity_ThrowsKeyNotFoundException()
        {
            // Arrange
            _dbContext.AnyEntities.Add(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            _dbContext.SaveChanges();
            var updatedEntity = new AnyEntity
            {
                AnyString = "AnyNewString"
            };

            // Act & Assert
            await _repository.Awaiting(r => r.UpdateAsync(2, updatedEntity)).Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task UpdateAsync_Null_ThrowsArgumentNullException()
        {
            await _repository.Awaiting(r => r.UpdateAsync(1, null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateAsync_NullId_ThrowsArgumentNullException()
        {
            // Arrange
            var logger = A.Fake<ILogger<AnyNullableIdRepository>>();
            var repository = new AnyNullableIdRepository(_dbContext, logger);
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
            _dbContext.AnyEntities.AddRange(entities);
            _dbContext.SaveChanges();
            _dbContext.AnyEntities.Should().Contain(entities);

            // Act
            _repository.Delete(1);
            _dbContext.SaveChanges();

            // Assert
            _dbContext.AnyEntities.First().Should().BeEquivalentTo(entities.Last());
        }

        [Fact]
        public void Delete_Null_ThrowsArgumentNullException()
        {
            // Arrange
            var logger = A.Fake<ILogger<AnyNullableIdRepository>>();
            var repository = new AnyNullableIdRepository(_dbContext, logger);

            // Act & Assert
            repository.Invoking(r => r.Delete(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Delete_NonExistingId_ThrowsKeyNotFoundException()
        {
            _repository.Invoking(r => r.Delete(1)).Should().Throw<KeyNotFoundException>();
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
            _dbContext.AnyEntities.AddRange(entities);
            _dbContext.SaveChanges();
            _dbContext.AnyEntities.Should().Contain(entities);

            // Act
            await _repository.DeleteAsync(1);
            _dbContext.SaveChanges();

            // Assert
            _dbContext.AnyEntities.First().Should().BeEquivalentTo(entities.Last());
        }

        [Fact]
        public async Task DeleteAsync_Null_ThrowsArgumentNullException()
        {
            // Arrange
            var logger = A.Fake<ILogger<AnyNullableIdRepository>>();
            var nullableRepository = new AnyNullableIdRepository(_dbContext, logger);

            // Act & Assert
            await nullableRepository.Awaiting(r => r.DeleteAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task DeleteAsync_NonExistingId_ThrowsKeyNotFoundException()
        {
            await _repository.Awaiting(r => r.DeleteAsync(1)).Should().ThrowAsync<KeyNotFoundException>();
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
            _dbContext.AnyEntities.Add(expected);
            _repository.SaveChanges();

            // Assert
            _dbContext.AnyEntities.First().Should().BeEquivalentTo(expected);
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
            _dbContext.AnyEntities.Add(entity);

            // Assert
            _dbContext.AnyEntities.Should().BeEmpty();
        }

        [Fact]
        public void SaveChanges_CallAfterModify_ChangesSaved()
        {
            // Arrange
            _dbContext.Add(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            _dbContext.SaveChanges();
            var modifiedEntity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyNewString"
            };

            // Act
            _dbContext.AnyEntities.Find(modifiedEntity.Id).AnyString = modifiedEntity.AnyString;
            _repository.SaveChanges();

            // Assert
            _dbContext.AnyEntities.First().Should().BeEquivalentTo(modifiedEntity);
        }

        [Fact]
        public void SaveChanges_NoCallAfterModify_ChangesSaved()
        {
            // Arrange
            _dbContext.Add(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            _dbContext.SaveChanges();
            var modifiedEntity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyNewString"
            };

            // Act
            _dbContext.AnyEntities.Find(modifiedEntity.Id).AnyString = modifiedEntity.AnyString;

            // Assert
            _dbContext.AnyEntities.First().Should().BeEquivalentTo(modifiedEntity);
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
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            _dbContext.AnyEntities.Should().Contain(entity);

            // Act
            _dbContext.AnyEntities.Remove(entity);
            _repository.SaveChanges();

            // Assert
            _dbContext.AnyEntities.Should().BeEmpty();
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
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            _dbContext.AnyEntities.Should().Contain(entity);

            // Act
            _dbContext.AnyEntities.Remove(entity);

            // Assert
            _dbContext.AnyEntities.Should().Contain(entity);
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
            _dbContext.AnyEntities.Add(expected);
            await _repository.SaveChangesAsync();

            // Assert
            _dbContext.AnyEntities.First().Should().BeEquivalentTo(expected);
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
            _dbContext.AnyEntities.Add(entity);

            // Assert
            _dbContext.AnyEntities.Should().BeEmpty();
        }

        [Fact]
        public async Task SaveChangesAsync_CallAfterModify_ChangesSaved()
        {
            // Arrange
            _dbContext.Add(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            _dbContext.SaveChanges();
            var modifiedEntity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyNewString"
            };

            // Act
            _dbContext.AnyEntities.Find(modifiedEntity.Id).AnyString = modifiedEntity.AnyString;
            await _repository.SaveChangesAsync();

            // Assert
            _dbContext.AnyEntities.First().Should().BeEquivalentTo(modifiedEntity);
        }

        [Fact]
        public void SaveChangesAsync_NoCallAfterModify_ChangesSaved()
        {
            // Arrange
            _dbContext.Add(new AnyEntity
            {
                Id = 1,
                AnyString = "AnyString"
            });
            _dbContext.SaveChanges();
            var modifiedEntity = new AnyEntity
            {
                Id = 1,
                AnyString = "AnyNewString"
            };

            // Act
            _dbContext.AnyEntities.Find(modifiedEntity.Id).AnyString = modifiedEntity.AnyString;

            // Assert
            _dbContext.AnyEntities.First().Should().BeEquivalentTo(modifiedEntity);
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
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            _dbContext.AnyEntities.Should().Contain(entity);

            // Act
            _dbContext.AnyEntities.Remove(entity);
            await _repository.SaveChangesAsync();

            // Assert
            _dbContext.AnyEntities.Should().BeEmpty();
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
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            _dbContext.AnyEntities.Should().Contain(entity);

            // Act
            _dbContext.AnyEntities.Remove(entity);

            // Assert
            _dbContext.AnyEntities.Should().Contain(entity);
        }
    }
}
