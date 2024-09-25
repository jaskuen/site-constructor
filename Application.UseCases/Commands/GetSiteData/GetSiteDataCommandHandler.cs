using Application.UseCases.Results;
using Application.UseCases.UseCases;
using SiteConstructor.Domain.Repositories;

namespace Application.UseCases.Commands.GetSiteData;

public class GetSiteDataCommandHandler : ICommandHandler<GetSiteDataCommand, Result>
{
    private readonly ISiteDataRepository _siteDataRepository;

    public GetSiteDataCommandHandler(ISiteDataRepository siteDataRepository)
    {
        _siteDataRepository = siteDataRepository;
    }

    public async Task<Result> Handle(GetSiteDataCommand command)
    {
        Error? validationError = Validate(command);
        if (validationError != null)
        {
            return Result.Fail(validationError);
        }

        _siteDataRepository.SetOrUpdateData(command.RequestDto.SiteData);
        _siteDataRepository.CreateHugoDirectory();
        _siteDataRepository.ApplyDataToHugo();

        return Result.Ok();


    }

    private Error? Validate(GetSiteDataCommand command)
    {
        if (command.RequestDto == null)
        {
            return new Error("No data provided");
        }
        if (String.IsNullOrWhiteSpace(command.RequestDto.SiteData.ContentPageData.Header))
        {
            return new Error("No site header");
        }
        return null;
    }
}
