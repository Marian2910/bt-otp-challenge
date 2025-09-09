namespace BtOtp.Api.Models;

public class OtpEntry {
    public string Code { get; set; }
    public DateTimeOffset Expiry { get; set; }
}