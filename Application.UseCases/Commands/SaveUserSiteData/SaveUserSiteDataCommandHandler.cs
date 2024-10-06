using Application.UseCases.Results;
using Application.UseCases.UseCases;
using Domain.Repositories;

namespace Application.UseCases.Commands.SaveUserSiteData;

public class SaveUserSiteDataCommandHandler : ICommandHandler<SaveUserSiteDataCommand, Result>
{
    private readonly IUserSiteDataRepository _userSiteDataRepository;
    private readonly IUnitOfWork _unitOfWork;
    public SaveUserSiteDataCommandHandler(
        IUserSiteDataRepository userSiteDataRepository,
        IUnitOfWork unitOfWork)
    {
        _userSiteDataRepository = userSiteDataRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(SaveUserSiteDataCommand command)
    {
        Error? validateError = Validate(command);
        if (validateError != null)
        {
            return Result.Fail(validateError);
        }
        var userSiteData = _userSiteDataRepository.Table.FirstOrDefault(d => d.UserId == command.RequestDto.UserSiteData.UserId);

        if (userSiteData != null)
        {
            _userSiteDataRepository.Remove(userSiteData);
        }
        _userSiteDataRepository.Add(command.RequestDto.UserSiteData);

        _unitOfWork.Commit();

        return Result.Ok();
    }

    private Error? Validate(SaveUserSiteDataCommand command)
    {
        if (command.RequestDto == null)
        {
            return new Error("No data provided");
        }
        if (String.IsNullOrWhiteSpace(command.RequestDto.UserSiteData.UserId.ToString()))
        {
            return new Error("No user id");
        }
        return null;
    }
}
