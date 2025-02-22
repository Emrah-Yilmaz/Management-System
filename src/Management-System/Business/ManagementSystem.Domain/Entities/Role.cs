﻿using Packages.Repositories.EfCore.Entity;

namespace ManagementSystem.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}
