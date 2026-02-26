using GitMirror.API.Services.PaginationService;
using GitMirror.API.Services.RepositoryService.Models;

namespace GitMirror.API.Services.RepositoryService
{
    public class RepositoryDemoService : IRepositoryService
    {
        private readonly List<RepositoryResponseModel> _demoData = new()
        {
            new RepositoryResponseModel
            {
                Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440301"),
                SourceCloneUrl = "https://github.com/DanielMigchels/GitMirror",
                SourceUsername = "Administrator",
                TargetCloneUrl = "https://gitlab.yourorganization.com/DanielMigchels/GitMirror",
                TargetUsername = "Administrator"
            }
        };

        public Task<RepositoryResponseModel> Create(RepositoryRequestModel request)
        {
            var newRepository = new RepositoryResponseModel
            {
                Id = Guid.NewGuid(),
                SourceCloneUrl = request.SourceCloneUrl,
                SourceUsername = request.SourceUsername,
                TargetCloneUrl = request.TargetCloneUrl,
                TargetUsername = request.TargetUsername
            };
            return Task.FromResult(newRepository);
        }

        public Task<bool> Delete(Guid id)
        {
            return Task.FromResult(true);
        }

        public Task<PaginatedList<RepositoryResponseModel>> Get(int pageSize, int page)
        {
            var paginatedData = _demoData
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToList();

            return Task.FromResult(new PaginatedList<RepositoryResponseModel>
            {
                Page = page,
                PageSize = pageSize,
                Data = paginatedData,
                HasNext = (page + 1) * pageSize < _demoData.Count,
                HasPrevious = page > 0
            });
        }

        public Task<RepositoryResponseModel?> GetById(Guid id)
        {
            var repository = _demoData.FirstOrDefault(r => r.Id == id);
            return Task.FromResult(repository);
        }

        public Task<RepositoryResponseModel?> Update(Guid id, RepositoryRequestModel request)
        {
            var repository = new RepositoryResponseModel
            {
                Id = id,
                SourceCloneUrl = request.SourceCloneUrl,
                SourceUsername = request.SourceUsername,
                TargetCloneUrl = request.TargetCloneUrl,
                TargetUsername = request.TargetUsername
            };
            return Task.FromResult<RepositoryResponseModel?>(repository);
        }
    }
}
