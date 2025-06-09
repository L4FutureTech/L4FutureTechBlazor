namespace L4FutureTechBlazor.Interfaces;

/// <summary>
/// Defines storage operations for browser local storage,
/// supporting raw string storage and generic JSON serialization.
/// </summary>
public interface IL4FStorageService
{
    /// <summary>
    /// Retrieves a raw JSON string from local storage by key.
    /// </summary>
    /// <param name="key">The key under which the value is stored.</param>
    /// <returns>
    /// A <see cref="ValueTask{String}"/> containing the stored JSON string, or null if not found.
    /// </returns>
    ValueTask<string?> GetItemAsStringAsync(string key);

    /// <summary>
    /// Stores a raw string value in local storage under the specified key.
    /// </summary>
    /// <param name="key">The key under which to store the value.</param>
    /// <param name="value">The string value to store.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask SetItemAsStringAsync(string key, string value);

    /// <summary>
    /// Removes an entry from local storage.
    /// </summary>
    /// <param name="key">The key of the entry to remove.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask RemoveItemAsync(string key);

    /// <summary>
    /// Serializes an object to JSON and stores it under the specified key.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize and store.</typeparam>
    /// <param name="key">The key under which the object will be stored.</param>
    /// <param name="value">The object to serialize to JSON.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask SetItemAsync<T>(string key, T value);

    /// <summary>
    /// Retrieves a JSON value by key and deserializes it into the specified type.
    /// </summary>
    /// <typeparam name="T">The type to which the JSON is deserialized.</typeparam>
    /// <param name="key">The key under which the JSON is stored.</param>
    /// <returns>
    /// A <see cref="ValueTask{T}"/> containing the deserialized object,
    /// or the default value of <typeparamref name="T"/> if not found.
    /// </returns>
    ValueTask<T?> GetItemAsync<T>(string key);
}
