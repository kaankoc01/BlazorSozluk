namespace BlazorSozluk.WebApp.Infrastructure.Services.Interfaces
{
    public interface IVoteServices
    {
        Task CreateEntryCommentDownVote(Guid entryCommentId);
        Task CreateEntryCommentUpVote(Guid entryCommentId);
        Task CreateEntryDownVote(Guid entryCommentId);
        Task CreateEntryUpVote(Guid entryId);
        Task DeleteEntryCommentVote(Guid entryCommentId);
        Task DeleteEntryVote(Guid entryId);

    }
}
