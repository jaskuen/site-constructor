using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.ValueObjects.GitHub.DTOs;

public class CloneGithubRepositoryRequestDto
{
    public string RepositoryName { get; set; }
    public string DirectoryPath { get; set; }
}
