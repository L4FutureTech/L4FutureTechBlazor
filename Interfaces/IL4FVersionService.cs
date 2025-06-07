namespace L4FutureTechBlazor.Interfaces;
public interface IL4FVersionService
{
    string GetVersion();
    string GetEnvironment();

    Task CheckVersion();
    Task<bool> CheckForAppUpdate();

    Task OpenStore();
}
