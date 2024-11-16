using ManagementSystem.Domain.Entities;
using ManagementSystem.Domain.Persistence.User;
using ManagementSystem.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Infrastructure.Persistence.User
{
    public class UserRepository : Repository<Domain.Entities.User>, IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
