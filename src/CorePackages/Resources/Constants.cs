namespace Packages.Resources
{
    public struct Constants
    {
        internal struct AuthorizationErrorMessage
        {
            public const string UserNotAuthenticated = "User is not authenticated.";
            public const string UserNotAuthorized = "User does not have the required role: {0}";
        }
    }
}
