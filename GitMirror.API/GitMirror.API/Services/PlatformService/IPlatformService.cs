using GitMirror.API.Services.PaginationService;
using GitMirror.API.Services.PlatformService.Models;

namespace GitMirror.API.Services.PlatformService;

public interface IPlatformService
{
    Task<PaginatedList<PlatformResponseModel>> Get(int pageSize, int page);
    Task<PlatformResponseModel?> GetById(Guid id);
    Task<PlatformResponseModel> Create(PlatformRequestModel request);
    Task<PlatformResponseModel?> Update(Guid id, PlatformRequestModel request);
    Task<bool> Delete(Guid id);
}