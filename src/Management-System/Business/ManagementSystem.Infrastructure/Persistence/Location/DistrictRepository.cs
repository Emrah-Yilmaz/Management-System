﻿using ManagementSystem.Domain.Persistence.City;
using ManagementSystem.Infrastructure.Context;

namespace ManagementSystem.Infrastructure.Persistence.Location
{
    public class DistrictRepository : Repository<Domain.Entities.District>, IDistrictRepository
    {
        public DistrictRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

}
