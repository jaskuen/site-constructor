namespace Domain.Models.ValueObjects.GitHub.DTOs;

public class CloneGithubRepositoryRequestDto
{
    public string RepositoryName { get; set; }
    public string DirectoryPath { get; set; }
}
