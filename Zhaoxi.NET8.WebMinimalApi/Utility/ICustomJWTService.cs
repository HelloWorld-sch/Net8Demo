namespace Zhaoxi.NET8.WebMinimalApi.Utility
{
    public interface ICustomJWTService
    {
        string GetToken(CurrentUser user);
    }
}
