namespace FormAPP.Models
{
    public class UserSession
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string DeviceName { get; set; }
        public string Token { get; set; }
    }
}
