namespace IcqApp.Helpers
{
    public class UserParams : PaginationParams
    {
        public string CurrentUsername { get; set; }
        public string KnownAs { get; set; }

        public string OrderBy { get; set; } = "lastActive";

    }
}
