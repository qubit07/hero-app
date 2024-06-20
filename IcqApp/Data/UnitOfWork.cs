using IcqApp.Interfaces;

namespace IcqApp.Data
{
    public class UnitOfWork(DataContext context, IUserRepository userRepository,
        IMessageRepository messageRepository, IFriendshipRepository friendshipRepository) : IUnitOfWork
    {
        public IUserRepository UserRepository => userRepository;

        public IMessageRepository MessageRepository => messageRepository;

        public IFriendshipRepository FriendshipRepository => friendshipRepository;

        public async Task<bool> Complete()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return context.ChangeTracker.HasChanges();
        }
    }
}
