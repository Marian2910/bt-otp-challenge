using BtOtp.Api.Models;

public interface IOtpService
{
    OtpResponseDto IssueOtp(string userId);
    bool ValidateOtp(string userId, string code);
}