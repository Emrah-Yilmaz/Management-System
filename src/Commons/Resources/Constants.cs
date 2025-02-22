﻿namespace CommonLibrary.Resources
{
    public struct  Constants
    {
        public struct Caches
        {
            public struct Department
            {
                public const string CacheKey = "GetDeparmentQuery {0} - {1}";
                public const string CachceGroupKey = "GetDepartments";
            }

            public struct Users
            {
                public const string CacheKey = "GetUsersQuery {0} - {1}";
                public const string CachceGroupKey = "GetPagedUsers";
            }
        }
        
    }
}
