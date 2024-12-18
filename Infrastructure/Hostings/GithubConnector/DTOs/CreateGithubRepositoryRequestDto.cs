﻿namespace Domain.Models.ValueObjects.GitHub.DTOs;

public class CreateGithubRepositoryRequestDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Private { get; set; }
    public string DirectoryPath { get; set; }
}
