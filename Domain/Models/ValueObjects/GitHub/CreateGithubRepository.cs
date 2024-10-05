using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Models.ValueObjects.GitHub.DTOs;
using Newtonsoft.Json;

namespace Domain.Models.ValueObjects.GitHub;

public class CreateGithubRepository
{
    private readonly string _token = "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";
    private readonly string _baseUrl = "https://api.github.com";

    public async Task<bool> CreateRepositoryAsync(CreateGithubRepositoryRequestDto request)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        client.DefaultRequestHeaders.Add("User-Agent", "request");

        var body = new
        {
            name = request.Name,
            description = request.Description,
            request.Private,
        };

        var json = JsonConvert.SerializeObject(body);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/vnd.github+json");

        var response = await client.PostAsync($"{_baseUrl}/user/repos", content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Репозиторий создан успешно!");
            return true;
        }
        else
        {
            Console.WriteLine("Ошибка создания репозитория: " + response.StatusCode);
        }
        return false;
    }

    public async Task<bool> GetPublicRepositories()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        client.DefaultRequestHeaders.Add("User-Agent", "request");

        var response = await client.GetAsync($"{_baseUrl}/repositories");

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Количество репозиториев: ", response.Content);
            return true;
        }
        else
        {
            Console.WriteLine("Ошибка создания репозитория: " + response.StatusCode);
        }
        return false;
    }
}
