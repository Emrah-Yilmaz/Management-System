namespace ManagementSystem.Domain.Utilities
{
    public static class Shared
    {
        public struct JwtClaims
        {
            public const string UserId = "UserId";
            public const string FirstName = "Name";
            public const string LastName = "LastName";
            public const string UserName = "UserName";
            public const string Email = "Email";
            public const string Unknown = "Unknown";
            public const string Role = "Role";
        }
        public struct ErrorMessage
        {
            public const string UserNotFound = "User is not found.";
            public const string RoleNotFound = "Role is not found.";
            public const string UserAlreadyHasRole = "User already has role.";
            public const string AlreadyExist = "This {0} already exists.";
            public const string SavedError = "An error occurred while creating the record.";
            public const string NotFoundError = "{0} is not found.";
        }
        public struct Entities{
            public const string Project = "Project";
            public const string User = "User";
            public const string Department = "Department";
            public const string WorkTask = "WorkTask";
        }
    }
}
