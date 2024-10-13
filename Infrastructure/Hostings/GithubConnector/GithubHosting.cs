using System.Diagnostics;
using System.Text;
using Domain.Models.ValueObjects.GitHub.DTOs;
using Infrastructure.Hostings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Domain.Models.ValueObjects.GitHub;

public class GithubHostingParameters
{
    public CreateGithubRepositoryRequestDto createGithubRepositoryRequestDto { get; set; }
}

public class GithubHosting : IHosting<GithubHostingParameters, string>
{
    private readonly string _token = Environment.GetEnvironmentVariable("WEBSITE_GITHUB_KEY");
    private readonly string _baseUrl = "https://api.github.com/";
    private readonly string _repoOwner = "cool-website";
    private readonly string _githubConfigPath;
    private readonly ILogger _logger;
    private readonly HttpClient _httpClient;

    public GithubHosting(IConfiguration configuration, ILogger<GithubHosting> logger)
    {
        _logger = logger;
        _githubConfigPath = configuration.GetSection("Publication")["GithubConfigFolder"];
        _httpClient = new HttpClient();
    }

    public async Task<string> HostAsync(GithubHostingParameters paramaters)
    {
        string newSiteUrl = "";
        bool isCreated = await CreateAndCloneRepositoryAsync(paramaters.createGithubRepositoryRequestDto);
        if (isCreated)
        {
            newSiteUrl = await DeployToGitHubPages(paramaters.createGithubRepositoryRequestDto.Name);
        }
        return newSiteUrl;
    }

    private HttpRequestMessage CreateDefaultRequest<T>(HttpMethod method, string path, T? body)
    {
        var request = new HttpRequestMessage(method, $"{_baseUrl.TrimEnd('/')}/{path.Trim('/')}");
        request.Headers.Add("Authorization", $"Bearer {_token}");
        request.Headers.Add("User-Agent", "request");
        request.Headers.Add("Accept", "application/vnd.github.v3+json");

        request.Content = new StringContent(
            JsonConvert.SerializeObject(body),
            Encoding.UTF8,
            "application/vnd.github+json"
            );
        return request;
    }

    private async Task<bool> CreateAndCloneRepositoryAsync(CreateGithubRepositoryRequestDto request)
    {
        _logger.LogInformation("Попытка создать репозиторий...");
        var response = await _httpClient.SendAsync(CreateDefaultRequest(HttpMethod.Post, "user/repos", new
        {
            name = request.Name,
            description = request.Description,
        }));

        if (response.IsSuccessStatusCode)
        {
            _logger.LogDebug("Репозиторий создан успешно!");
            string repoUrl = $"https://github.com/{_repoOwner}/{request.Name}.git";
            CloneRepositoryIntoFolderAndPush(request.DirectoryPath, repoUrl);
            return true;
        }
        _logger.LogError("Ошибка создания репозитория: " + response.StatusCode);
        return false;
    }

    private async Task<string> DeployToGitHubPages(string repoName)
    {
        _logger.LogInformation("Попытка выложить сайт на Github Pages...");
        var response = await _httpClient.SendAsync(
            CreateDefaultRequest(HttpMethod.Post,
            $"repos/{_repoOwner}/{repoName}/pages",
            new
            {
                owner = _repoOwner,
                repo = repoName,
                source = new
                {
                    branch = "main",
                    path = "/"
                },
            }));

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Github Pages создан успешно!");
            return $"https://{_repoOwner}.github.io/{repoName}/";
        }
        _logger.LogError("Ошибка создания Github Pages: " + response.StatusCode);
        return "";
    }

    public async Task<bool> IsSiteDeployed(string repoName)
    {
        _logger.LogInformation("Проверка на то, является ли сайт собранным...");
        var response = await _httpClient.SendAsync(
            CreateDefaultRequest(
                HttpMethod.Get,
                $"repos/{_repoOwner}/{repoName}/pages",
                new
                {
                    owner = _repoOwner,
                    repo = repoName,
                    source = new
                    {
                        branch = "main",
                        path = "/"
                    },
                })
            );

        if (response.IsSuccessStatusCode)
        {
            _logger.LogDebug("Github Pages собран!");
            return true;
        }
        _logger.LogError("Github Pages не собран!");
        return false;
    }

    private async Task<bool> GetPublicRepositories()
    {
        _logger.LogInformation("Получение публичных репозиториев...");
        var response = await _httpClient.SendAsync(
            CreateDefaultRequest(
                HttpMethod.Get,
                $"repositories",
                new { })
            );

        if (response.IsSuccessStatusCode)
        {
            _logger.LogDebug("Количество репозиториев: " + response.Content);
            return true;
        }
        _logger.LogError("Ошибка создания репозитория: " + response.StatusCode);
        return false;
    }

    private void CloneRepositoryIntoFolderAndPush(string folderPath, string repoUrl)
    {
        _logger.LogInformation("Попытка отправить данные сайта на Github");
        try
        {
            ChangeGithubToSiteConstructor(true);
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "git",
                WorkingDirectory = folderPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            psi.EnvironmentVariables["GIT_ASKPASS"] = $"echo {_token}";

            psi.Arguments = "init";
            Process process = new Process();
            process.StartInfo = psi;

            process.Start();
            process.WaitForExit();

            psi.Arguments = "add .";
            process.Start();
            process.WaitForExit();

            psi.Arguments = "commit -m \"Initial commit\"";
            process.Start();
            process.WaitForExit();

            psi.Arguments = "branch -M main";
            process.Start();
            process.WaitForExit();

            psi.Arguments = $"remote add origin {repoUrl}";
            process.Start();
            process.WaitForExit();

            // Push the changes to the remote repository
            psi.Arguments = "push -u origin main";
            process.Start();
            process.WaitForExit();

            ChangeGithubToSiteConstructor(false);

            _logger.LogDebug("Данные успешно отправлены на Github!");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    private void ChangeGithubToSiteConstructor(bool isStart)
    {
        string pathToConfig = $"{_githubConfigPath}/config";
        string config;
        if (isStart)
        {
            config = File.ReadAllText($"{_githubConfigPath}/config_site_constructor");
        }
        else
        {
            config = File.ReadAllText($"{_githubConfigPath}/config_default");
        }
        File.WriteAllText(pathToConfig, config);
    }
}
