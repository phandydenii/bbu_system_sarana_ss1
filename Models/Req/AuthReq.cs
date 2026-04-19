namespace BBU_SYSTEM.Models.Req;

public class AuthenticationReq
{
    public string? Username { get; set; } = "";
    public string? Password { get; set; } = "";
    public string? Campus { get; set; } = "";
    public bool Remember { get; set; } = false;
}