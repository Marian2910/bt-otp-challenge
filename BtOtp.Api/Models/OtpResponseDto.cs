namespace BtOtp.Api.Models;

public class OtpResponseDto
{
    public string Code { get; set; }
    public int ExpiresIn { get; set; }
    
}