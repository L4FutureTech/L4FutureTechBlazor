namespace L4FutureTechBlazor.Interfaces;
public interface IVersionService
{
    string GetVersion();
    string GetEnvironment();

    Task CheckVersion();
    Task<bool> CheckForAppUpdate();

    Task OpenStore();
}
