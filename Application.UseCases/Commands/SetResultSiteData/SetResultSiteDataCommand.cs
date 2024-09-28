using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Application.UseCases.Commands.GetSiteData.DTOs;
using Application.UseCases.Results;
using Application.UseCases.UseCases;

namespace Application.UseCases.Commands.GetSiteData;

public class SetResultSiteDataCommand : ICommand<Result>
{
    public readonly SetResultSiteDataRequestDto RequestDto;
    public SetResultSiteDataCommand( SetResultSiteDataRequestDto requestDto)
    {
        RequestDto = requestDto;
    }
}
