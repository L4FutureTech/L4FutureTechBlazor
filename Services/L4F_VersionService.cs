using System.Diagnostics;
using System.Reflection;

using L4FutureTechBlazor.Interfaces;

namespace L4FutureTechBlazor.Services;
public class L4F_VersionService : IL4FVersionService
{
    public string GetVersion()
    {
        Version? appVersion = Assembly.GetEntryAssembly()?.GetName().Version;
        return appVersion is null ? "" : "v" + appVersion.Major + "." + appVersion.Minor + "." + appVersion.Build + ".";
    }

    public string GetEnvironment()
    {
        return Environment.GetEnvironmentVariable("Environment") ?? string.Empty;
    }

    public async Task CheckVersion()
    {
        string version = GetVersion();
        await Task.Run(() => Debug.WriteLine($"Version: {version}"));
    }

    public async Task CheckForAppUpdate()
    {
        string version = GetVersion();
        await Task.Run(() => Debug.WriteLine($"Version: {version}"));
    }

    public Task OpenStore()
    {
        return Task.CompletedTask;
    }

    Task<bool> IL4FVersionService.CheckForAppUpdate()
    {
        return Task.FromResult(false);
    }
}