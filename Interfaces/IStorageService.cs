namespace L4FutureTechBlazor.Interfaces;
public interface IStorageService
{
    ValueTask SetItem(string key, string value);
    ValueTask<string?> GetItem(string key);
    ValueTask RemoveItem(string key);
    ValueTask SetItem<T>(string key, T value);
    ValueTask<T?> GetItem<T>(string key);
}
