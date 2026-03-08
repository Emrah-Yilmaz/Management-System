namespace CommonLibrary.Resources
{
    public struct Constants
    {
        public struct Caches
        {
            public struct Department
            {
                public const string CacheKey = "GetDeparmentQuery {0} - {1}";
                public const string CachceGroupKey = "GetDepartments";
                public const string BasicCacheKey = "GetDepartmentsBasicQuery";
            }

            public struct Users
            {
                public const string CacheKey = "GetUsersQuery {0} - {1}";
                public const string CachceGroupKey = "GetPagedUsers";
                public const string BasicCacheKey = "GetUsersBasicQuery";
            }

            public struct Projects
            {
                public const string CacheKey = "GetProjectsQuery {0} - {1}";
                public const string CachceGroupKey = "GetPagedProjects";
                public const string BasicCacheKey = "GetProjectsBasicQuery";
            }
        }

    }
}
