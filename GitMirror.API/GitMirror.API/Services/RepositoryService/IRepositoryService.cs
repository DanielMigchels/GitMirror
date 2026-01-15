using GitMirror.API.Services.PaginationService;
using GitMirror.API.Services.RepositoryService.Models;

namespace GitMirror.API.Services.RepositoryService;

public interface IRepositoryService
{
    Task<PaginatedList<RepositoryResponseModel>> Get(int pageSize, int page);
    Task<RepositoryResponseModel?> GetById(Guid id);
    Task<RepositoryResponseModel> Create(RepositoryRequestModel request);
    Task<RepositoryResponseModel?> Update(Guid id, RepositoryRequestModel request);
    Task<bool> Delete(Guid id);
}
