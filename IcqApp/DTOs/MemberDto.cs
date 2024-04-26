namespace IcqApp.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public int Age { get; set; }

        public string KnownAs { get; set; }

        public string PhotoUrl { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }

        public List<PhotoDto> Photos { get; set; } = new List<PhotoDto>();
    }
}
