namespace BBU_SYSTEM.Model.Res;

public class AuthRes
{
    public int UserId { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? UserGroup { get; set; }
    public string? Token { get; set; }
    public DateTime? ExpireIn { get; set; }
    public string? Status { get; set; }
}