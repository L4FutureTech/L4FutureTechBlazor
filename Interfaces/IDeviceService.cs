using L4FutureTechBlazor.Enums;

namespace L4FutureTechBlazor.Interfaces;
public interface IDeviceService
{
    AppPlatform GetPlatform();
    bool IsApp();
}