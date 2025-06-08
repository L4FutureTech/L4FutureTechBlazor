using L4FutureTechBlazor.Enums;

namespace L4FutureTechBlazor.Interfaces;
public interface IL4FDeviceService
{
    AppPlatformEnum GetPlatform();
    bool IsApp();
}