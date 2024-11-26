using Microsoft.EntityFrameworkCore;
using Packages.Repositories.EfCore;
using Packages.Repositories.EfCore.Entity;

namespace ManagementSystem.Domain.Persistence
{
    public interface IRepository<TEntity> : IGenericRepository<TEntity, DbContext> where TEntity : BaseEntity
    {
    }
}
