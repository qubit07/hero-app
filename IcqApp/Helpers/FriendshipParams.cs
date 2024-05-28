namespace IcqApp.Helpers
{
    public class FriendshipParams : PaginationParams
    {
        public int UserId { get; set; }
        public string Predicate { get; set; }
    }
}
