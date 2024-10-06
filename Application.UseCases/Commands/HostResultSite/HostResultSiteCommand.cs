using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UseCases.Commands.SaveUserSiteData.DTOs;
using Application.UseCases.Results;
using Application.UseCases.UseCases;

namespace Application.UseCases.Commands.SaveUserSiteData;

public class HostResultSiteCommand : ICommand<Result>
{
    public readonly HostResultSiteRequestDto RequestDto;
    public HostResultSiteCommand(HostResultSiteRequestDto requestDto)
    {
        RequestDto = requestDto;
    }
}
