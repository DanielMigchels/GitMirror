using GitMirror.API.Services.HistoryService.Models;
using GitMirror.API.Services.PaginationService;

namespace GitMirror.API.Services.HistoryService;

public interface IHistoryService
{
    Task<PaginatedList<HistoryResponseModel>> Get(int pageSize, int page);
    Task<HistoryResponseModel?> GetById(Guid id);
    Task<HistoryResponseModel> Create(HistoryRequestModel request);
    Task<HistoryResponseModel?> Update(Guid id, HistoryRequestModel request);
    Task<bool> Delete(Guid id);
}
