using GitMirror.API.Services.MirrorService.Models;
using GitMirror.API.Services.PaginationService;

namespace GitMirror.API.Services.MirrorService;

public interface IMirrorService
{
    Task<PaginatedList<MirrorResponseModel>> Get(int pageSize, int page);
    Task<MirrorResponseModel?> GetById(Guid id);
    Task<MirrorResponseModel> Create(MirrorRequestModel request);
    Task<MirrorResponseModel?> Update(Guid id, MirrorRequestModel request);
    Task<bool> Delete(Guid id);
}
