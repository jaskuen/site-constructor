using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.UseCases.Queries.GetSavedUserSiteData.DTOs;
using Application.UseCases.Results;

namespace Application.UseCases.Queries.UploadSavedUserSiteData;

public class GetSavedUserSiteDataQueryResult : Result<GetSavedUserSiteDataQueryResult, SavedUserSiteDataDto>
{
}
