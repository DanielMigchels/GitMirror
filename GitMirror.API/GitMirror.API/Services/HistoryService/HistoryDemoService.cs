using GitMirror.API.Data.Enums;
using GitMirror.API.Services.HistoryService.Models;
using GitMirror.API.Services.PaginationService;

namespace GitMirror.API.Services.HistoryService
{
    public class HistoryDemoService : IHistoryService
    {
        private readonly List<HistoryResponseModel> _demoData = new()
        {
            new HistoryResponseModel
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440100"),
                State = HistoryState.InProgress,
                CreatedOnUtc = DateTimeOffset.UtcNow.AddDays(-1),
                MirrorId = Guid.Parse("550e8400-e29b-41d4-a716-446655440201"),
                SourceUrl = "https://yourorganization.visualstudio.com/organization/Website",
                TargetUrl = "https://gitlab.yourorganization.com/organization/website"
            },
            new HistoryResponseModel
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440100"),
                State = HistoryState.Successful,
                CreatedOnUtc = DateTimeOffset.UtcNow.AddDays(-1),
                MirrorId = Guid.Parse("550e8400-e29b-41d4-a716-446655440201"),
                SourceUrl = "https://yourorganization.visualstudio.com/organization/Website",
                TargetUrl = "https://gitlab.yourorganization.com/organization/website"
            },
            new HistoryResponseModel
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440100"),
                State = HistoryState.Failed,
                CreatedOnUtc = DateTimeOffset.UtcNow.AddDays(-2),
                MirrorId = Guid.Parse("550e8400-e29b-41d4-a716-446655440201"),
                SourceUrl = "https://yourorganization.visualstudio.com/organization/Website",
                TargetUrl = "https://gitlab.yourorganization.com/organization/website"
            },
        };

        public Task<HistoryResponseModel> Create(HistoryRequestModel request)
        {
            var newHistory = new HistoryResponseModel
            {
                Id = Guid.NewGuid(),
                State = request.State,
                CreatedOnUtc = DateTimeOffset.UtcNow,
                MirrorId = request.MirrorId,
                RepositoryId = request.RepositoryId
            };
            return Task.FromResult(newHistory);
        }

        public Task<bool> Delete(Guid id)
        {
            return Task.FromResult(true);
        }

        public Task<PaginatedList<HistoryResponseModel>> Get(int pageSize, int page)
        {
            var paginatedData = _demoData
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult(new PaginatedList<HistoryResponseModel>
            {
                Page = page,
                PageSize = pageSize,
                Data = paginatedData,
                HasNext = (page + 1) * pageSize < _demoData.Count,
                HasPrevious = page > 0
            });
        }

        public Task<HistoryResponseModel?> GetById(Guid id)
        {
            var history = _demoData.FirstOrDefault(h => h.Id == id);
            return Task.FromResult(history);
        }

        public Task<HistoryResponseModel?> Update(Guid id, HistoryRequestModel request)
        {
            var history = new HistoryResponseModel
            {
                Id = id,
                State = request.State,
                CreatedOnUtc = DateTimeOffset.UtcNow,
                MirrorId = request.MirrorId,
                RepositoryId = request.RepositoryId
            };
            return Task.FromResult<HistoryResponseModel?>(history);
        }
    }
}
