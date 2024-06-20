namespace IcqApp.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IMessageRepository MessageRepository { get; }
        IFriendshipRepository FriendshipRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
