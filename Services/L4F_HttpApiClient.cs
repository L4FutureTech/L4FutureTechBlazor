using System.Text;
using System.Text.Json;

namespace L4FutureTechBlazor.Services;
public class L4F_HttpApiClient(HttpClient _httpClient)
{

    public JsonSerializerOptions jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<string> GetStringAsync(string endpoint, CancellationToken cancellationToken = default)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Request to {endpoint} failed with status code {response.StatusCode} and message: {errorContent}");
            }

            //TODO: Check if this is the correct way to read the response content
            return await response.Content.ReadAsStringAsync(cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"An error occurred while sending the request to {endpoint}: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred: {ex.Message}", ex);
        }
    }

    public async Task<T> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(endpoint, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new KeyNotFoundException();
                }

                string errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Request to {endpoint} failed with status code {response.StatusCode} and message: {errorContent}");
            }

            string content = await response.Content.ReadAsStringAsync(cancellationToken);
            T? data = JsonSerializer.Deserialize<T>(content, jsonSerializerOptions) ?? throw new InvalidOperationException($"Deserialization of the response content failed. Endpoint: {endpoint}");
            return data;
        }
        catch (KeyNotFoundException ex)
        {
            throw new Exception($"KeyNotFoundException while sending the request to {endpoint}: {ex.Message}", ex);
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"An error occurred while sending the request to {endpoint}: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred: {ex.Message}", ex);
        }
    }

    public async Task<T> PostAsync<T>(string endpoint, object data, CancellationToken cancellationToken = default)
    {
        try
        {
            StringContent content = new(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"POST request to {endpoint} failed with status code {response.StatusCode} and message: {errorContent}");
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            T? result = JsonSerializer.Deserialize<T>(responseContent, jsonSerializerOptions) ?? throw new InvalidOperationException($"Deserialization of the response content failed. Endpoint: {endpoint}");
            return result;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"An error occurred while sending the POST request to {endpoint}: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred during the POST request: {ex.Message}", ex);
        }
    }

    public async Task<T> PutAsync<T>(string endpoint, object data, CancellationToken cancellationToken = default)
    {
        try
        {
            StringContent content = new(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(endpoint, content, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"PUT request to {endpoint} failed with status code {response.StatusCode} and message: {errorContent}");
            }

            string responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            T? result = JsonSerializer.Deserialize<T>(responseContent, jsonSerializerOptions) ?? throw new InvalidOperationException($"Deserialization of the response content failed. Endpoint: {endpoint}");
            return result;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"An error occurred while sending the PUT request to {endpoint}: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred during the PUT request: {ex.Message}", ex);
        }
    }


    public async Task<bool> DeleteAsync(string endpoint, CancellationToken cancellationToken = default)
    {
        try
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(endpoint, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"DELETE request to {endpoint} failed with status code {response.StatusCode} and message: {errorContent}");
            }

            return true;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"An error occurred while sending the DELETE request to {endpoint}: {ex.Message}");
        }
        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred during the DELETE request to {endpoint}: {ex.Message}");
        }
    }

    public async Task<T> PatchAsync<T>(string endpoint, object data, CancellationToken cancellationToken = default)
    {
        try
        {
            StringContent content = new(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            HttpRequestMessage request = new(new HttpMethod("PATCH"), endpoint)
            {
                Content = content
            };
            HttpResponseMessage response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"PATCH request to {endpoint} failed with status code {response.StatusCode} and message: {errorContent}");
            }

            string responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
            T? result = JsonSerializer.Deserialize<T>(responseContent, jsonSerializerOptions) ?? throw new InvalidOperationException($"Deserialization of the response content failed. Endpoint: {endpoint}");
            return result;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"An error occurred while sending the PATCH request to {endpoint}: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"An unexpected error occurred during the PATCH request: {ex.Message}", ex);
        }
    }

    public void AddDefaultHeaders()
    {
        if (!_httpClient.DefaultRequestHeaders.Contains("Accept"))
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }
    }

    public async Task<T> ReadObjectResponseAsync<T>(HttpResponseMessage response)
    {
        string responseContent = await response.Content.ReadAsStringAsync();
        T? deserializedObject = JsonSerializer.Deserialize<T>(responseContent, jsonSerializerOptions);

        return deserializedObject == null
            ? throw new InvalidOperationException($"Deserialization of the response content failed.")
            : deserializedObject;
    }
    public void ProcessResponse(HttpResponseMessage response)
    {
        _ = response.EnsureSuccessStatusCode();
    }

    public void SetBearerToken(string token)
    {
        if (_httpClient.DefaultRequestHeaders.Authorization != null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }

    public void RemoveBearerToken()
    {
        if (_httpClient.DefaultRequestHeaders.Authorization != null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }

    public void SetBaseAddress(string baseAddress)
    {
        _httpClient.BaseAddress = new Uri(baseAddress);
    }

    public Uri? GetBaseAddress()
    {
        return _httpClient.BaseAddress;
    }
}