namespace LinkedOutApi.DTOs.CV
{
    public class GetCV
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
        public Guid UserId { get; set; }
    }
}