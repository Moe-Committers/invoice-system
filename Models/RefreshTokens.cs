namespace invoice_system.Models;

public class RefreshTokens : BaseEntity{
    public long UserId {get; set;}
    public string Token {get; set;}
    public DateTime ExpiredAt {get; set;}
    public string CreatedByIp {get; set;}
    public DateTime? RevokedAt {get; set;}
    public string? ReasonRevoked {get; set;}
    public string? ReplaceByToken {get; set;}
    public string? RevokedByIp {get; set;}
    public bool isExpired => DateTime.UtcNow >= ExpiredAt;
    public bool isRevoked => RevokedAt != null;
    public bool isActive => !isExpired && !isRevoked;
    public virtual Users user {get; set;}
}