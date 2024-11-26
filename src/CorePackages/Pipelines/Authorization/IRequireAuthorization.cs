namespace Packages.Pipelines.Authorization
{
    public interface IRequireAuthorization
    {
        List<string> RequiredRole { get; }
    }

}
