using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using Domain.Models.ValueObjects.GitHub.DTOs;
using Newtonsoft.Json;

namespace Domain.Models.ValueObjects.GitHub;

public class GithubApi
{
    private readonly string _token = Environment.GetEnvironmentVariable("WEBSITE_GITHUB_KEY");
    private readonly string _baseUrl = "https://api.github.com";
    private readonly string _repoOwner = "cool-website";

    public async Task<bool> CreateAndCloneRepositoryAsync(CreateGithubRepositoryRequestDto request)
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
            string repoUrl = $"https://github.com/{_repoOwner}/{request.Name}.git";
            CloneRepositoryIntoFolderAndPush(request.DirectoryPath, repoUrl);
            return true;
        }
        else
        {
            Console.WriteLine("Ошибка создания репозитория: " + response.StatusCode);
        }
        return false;
    }

    public async Task<string> DeployToGitHubPages(string repoName)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        client.DefaultRequestHeaders.Add("User-Agent", "request");

        var body = new
        {
            owner = _repoOwner,
            repo = repoName,
            source = new
            {
                branch = "main",
                path = "/"
            },
        };

        var json = JsonConvert.SerializeObject(body);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/vnd.github+json");

        var response = await client.PostAsync($"{_baseUrl}/repos/{_repoOwner}/{repoName}/pages", content);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Github Pages создан успешно!");
            return $"https://{_repoOwner}.github.io/{repoName}/";
        }
        else
        {
            Console.WriteLine("Ошибка создания Github Pages: " + response.StatusCode);
        }
        return "";
    }

    public async Task<bool> IsSiteDeployed(string repoName)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        client.DefaultRequestHeaders.Add("User-Agent", "request");

        var body = new
        {
            owner = _repoOwner,
            repo = repoName,
            source = new
            {
                branch = "main",
                path = "/"
            },
        };

        var json = JsonConvert.SerializeObject(body);
        HttpContent content = new StringContent(json, Encoding.UTF8, "application/vnd.github+json");

        var response = await client.PostAsync($"{_baseUrl}/repos/{_repoOwner}/{repoName}/pages", content);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Github Pages собран!");
            return true;
        }
        else
        {
            Console.WriteLine("Github Pages не собран!");
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

    private void CloneRepositoryIntoFolderAndPush(string folderPath, string repoUrl)
    {
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
            process.OutputDataReceived += (sender, data) => Console.WriteLine(data.Data);
            process.ErrorDataReceived += (sender, data) => Console.WriteLine(data.Data);
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            ChangeGithubToSiteConstructor(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void ChangeGithubToSiteConstructor(bool isStart)
    {
        string pathToConfig = "C:/Users/Jaskuen/.ssh/config";
        string config;
        if (isStart)
        {
            config = File.ReadAllText("C:/Users/Jaskuen/.ssh/config_site_constructor");
        }
        else
        {
            config = File.ReadAllText("C:/Users/Jaskuen/.ssh/config_default");
        }
        File.WriteAllText(pathToConfig, config);
    }
}
