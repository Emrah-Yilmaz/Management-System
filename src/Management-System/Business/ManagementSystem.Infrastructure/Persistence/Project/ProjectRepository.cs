using ManagementSystem.Domain.Persistence.Comment;
using ManagementSystem.Infrastructure.Context;
namespace ManagementSystem.Infrastructure.Persistence.Project
{
    public class ProjectRepository : Repository<Domain.Entities.Project>, IProjectRepository
    {
        public ProjectRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
